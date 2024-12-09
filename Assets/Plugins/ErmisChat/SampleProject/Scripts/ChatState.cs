using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ermis.Core;
using Ermis.Core.Exceptions;
using Ermis.Core.LowLevelClient.Events;
using Ermis.Core.LowLevelClient.Models;
using Ermis.Core.StatefulModels;
using Ermis.Core.Helpers;
using Ermis.Core.QueryBuilders.Filters;
using Ermis.Core.QueryBuilders.Filters.Channels;
using Ermis.Core.QueryBuilders.Sort;
using Ermis.Libs.Logs;
using ErmisChat.SampleProject.Popups;
using ErmisChat.SampleProject.Views;
using UnityEngine;
using Object = UnityEngine.Object;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient.Requests;
using UnityEditor.PackageManager.Requests;

namespace ErmisChat.SampleProject
{
    /// <summary>
    /// Implementation of <see cref="IChatState"/>
    /// </summary>
    public class ChatState : IChatState
    {
        public const string MessageDeletedInfo = "This message was deleted...";

        public event Action<IErmisChannel> ActiveChanelChanged;
        public event Action ChannelsUpdated;

        public event Action<IErmisMessage> MessageEditRequested;
        public event Action<List<IErmisUser>> OnQueryUser;
        public event Action<EventTypingStart> OnTypingStart;
        public event Action<EventTypingStop> OnTypingStop;

        public IErmisChannel ActiveChannel
        {
            get => _activeChannel;
            private set
            {
                var prevValue = _activeChannel;
                _activeChannel = value;

                if (prevValue != value)
                {
                    ActiveChanelChanged?.Invoke(_activeChannel);
                }
            }
        }

        public IReadOnlyList<IErmisChannel> Channels => _channels;
        public IReadOnlyList<IErmisUser> ErmisUsers => _ermisUsers;

        public IErmisChatClient Client { get; }

        public ChatState(IErmisChatClient client, IViewFactory viewFactory)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
            _viewFactory = viewFactory ?? throw new ArgumentNullException(nameof(viewFactory));

            Client.Connected += OnClientConnected;
            Client.ConnectionStateChanged += OnClientConnectionStateChanged;

            Client.ChannelInviteReceived += OnClientChannelInviteReceived;
            Client.ChannelInviteAccepted += ClientOnChannelInviteAccepted;
            Client.ChannelInviteRejected += ClientOnChannelInviteRejected;

            //ErmisTodo: handle this
            // Client.MessageRead += OnMessageRead;
            //
            // Client.NotificationMarkRead += OnNotificationMarkRead;
          
        }

        

        public void Dispose()
        {
            Client.Connected -= OnClientConnected;
            Client.ConnectionStateChanged -= OnClientConnectionStateChanged;

            Client.ChannelInviteReceived -= OnClientChannelInviteReceived;
            Client.ChannelInviteAccepted -= ClientOnChannelInviteAccepted;
            Client.ChannelInviteRejected -= ClientOnChannelInviteRejected;

            // Client.MessageRead -= OnMessageRead;
            //
            // Client.NotificationMarkRead -= OnNotificationMarkRead;

            Client.Dispose();
        }

        public TPopup ShowPopup<TPopup>()
            where TPopup : BaseFullscreenPopup
        {
            return _viewFactory.CreateFullscreenPopup<TPopup>();
        }

        public void HidePopup<TPopup>(TPopup instance)
            where TPopup : BaseFullscreenPopup
        {
            Object.Destroy(instance.gameObject);
        }

        public Task<IErmisChannel> CreateNewChannelAsync(string channelName, IEnumerable<IErmisUser> users)
            => Client.CreateChannelWithIdAsync(ChannelType.Team, channelId: Guid.NewGuid().ToString(),
                channelName, users);

        public Task<IErmisChannel> CreateNewChannelAsync(IEnumerable<IErmisUser> user)
           => Client.CreateChannelWithMembersAsync(ChannelType.Messaging, user);

        public void OpenChannel(IErmisChannel channel) => ActiveChannel = channel;

        public void EditMessage(IErmisMessage message) => MessageEditRequested?.Invoke(message);

        public async Task UpdateChannelsAsync()
        {
            GetChannelsRequest request = new GetChannelsRequest();
            request.FilterConditions = new FilterConditions();
            request.FilterConditions.Type = new List<string>();
            request.FilterConditions.Type.Add("general");
            request.FilterConditions.Type.Add("team");
            request.FilterConditions.Type.Add("messaging");
            request.FilterConditions.Limit = null;
            request.FilterConditions.Offset = 0;
            request.FilterConditions.Roles = new List<string>();
            request.FilterConditions.Roles.Add("owner");
            request.FilterConditions.Roles.Add("moder");
            request.FilterConditions.Roles.Add("member");
            request.FilterConditions.OtherRoles = new List<string>();
            //requestBodyDto.FilterConditions.OtherRoles.Add("pending");
            request.FilterConditions.Banned = false;
            request.FilterConditions.Blocked = false;
            request.FilterConditions.ProjectId = Client.LowLevelClient.GetProjectId();
            request.Sort = new List<Sort>();
            Sort _sort = new Sort();
            _sort.Field = "last_message_at";
            _sort.Direction = -1;
            request.Sort.Add(_sort);
            request.MessageLimit = 25;
            try
            {
                var channels = await Client.QueryChannelsAsync(request);

                _channels.Clear();
                _channels.AddRange(channels);

                if (ActiveChannel == null)
                {
                    ActiveChannel = _channels.FirstOrDefault();
                }
            }
            catch (ErmisApiException e)
            {
                e.LogErmisExceptionDetails();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            ChannelsUpdated?.Invoke();
        }

        public async Task UpdateUsersAsync()
        {

            try
            {
                var users = await Client.QueryUsersListAsync();
                foreach (var user in users)
                {
                    _ermisUsers.Add(user);
                }

                OnQueryUser?.Invoke(_ermisUsers);
            }
            catch (ErmisApiException e)
            {
                e.LogErmisExceptionDetails();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            ChannelsUpdated?.Invoke();
        }

        public Task LoadPreviousMessagesAsync()
        {
            if (ActiveChannel == null)
            {
                return Task.CompletedTask;
            }

            return ActiveChannel.LoadOlderMessagesAsync();
        }

        private readonly List<IErmisChannel> _channels = new List<IErmisChannel>();
        private readonly List<IErmisUser> _ermisUsers = new List<IErmisUser>();

        private readonly IViewFactory _viewFactory;
        private readonly ILogs _unityLogger = new UnityLogs();

        //ErmisTodo: get it initially from health check event
        private OwnUser _localUser;
        private IErmisChannel _activeChannel;

        private Task _activeLoadPreviousMessagesTask;
        private bool _restoreStateAfterReconnect;

        private async void OnClientConnected(IErmisLocalUserData localUserData)
        {
            await UpdateChannelsAsync();
            await UpdateUsersAsync();
            if (ActiveChannel == null && _channels.Count > 0)
            {
                ActiveChannel = _channels.First();
            }
        }

        private void OnClientChannelInviteReceived(IErmisChannel channel, IErmisUser invitee)
        {
            var popup = ShowPopup<InviteReceivedPopup>();
            popup.SetData(channel);
        }

        private void ClientOnChannelInviteAccepted(IErmisChannel channel, IErmisUser invitee)
        {
            Debug.LogError("ClientOnChannelInviteAccepted");
        }

        private void ClientOnChannelInviteRejected(IErmisChannel channel, IErmisUser invitee)
        {
            Debug.LogError("ClientOnChannelInviteRejected");
        }

        private void OnNotificationMarkRead(EventNotificationMarkRead eventNotificationMarkRead)
            => Debug.Log($"Notified mark read for channel: {eventNotificationMarkRead.Cid}, " +
                         $"TotalUnreadCount: {eventNotificationMarkRead.TotalUnreadCount}, UnreadChannels: {eventNotificationMarkRead.UnreadChannels}");

        private void OnMessageRead(EventMessageRead eventMessageRead)
            => Debug.Log("Message read received for channel: " + eventMessageRead.Cid);

        private void OnClientConnectionStateChanged(ConnectionState prev, ConnectionState current)
        {
            if (current == ConnectionState.Disconnected)
            {
                _restoreStateAfterReconnect = true;
            }

            if (current == ConnectionState.Connected && _restoreStateAfterReconnect)
            {
                _restoreStateAfterReconnect = false;
                //ErmisTodo: this should be handled by state client
            }
        }
    }
}