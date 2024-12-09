using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ermis.Core;
using Ermis.Core.LowLevelClient.Events;
using Ermis.Core.StatefulModels;
using ErmisChat.SampleProject.Popups;
using ErmisChat.SampleProject.Views;

namespace ErmisChat.SampleProject
{
    /// <summary>
    /// Keep chat state
    /// </summary>
    public interface IChatState : IDisposable
    {
        event Action<IErmisChannel> ActiveChanelChanged;
        event Action ChannelsUpdated;
        event Action<IErmisMessage> MessageEditRequested;
        event Action<List<IErmisUser>> OnQueryUser;

        IErmisChannel ActiveChannel { get; }
        IReadOnlyList<IErmisChannel> Channels { get; }
        public IReadOnlyList<IErmisUser> ErmisUsers { get; }
        IErmisChatClient Client { get; }

        void OpenChannel(IErmisChannel channel);

        void EditMessage(IErmisMessage message);

        Task<IErmisChannel> CreateNewChannelAsync(string channelName, IEnumerable<IErmisUser> users);

        //Task<IErmisChannel> RemoveMemberChannelAsync(IEnumerable<string> usersId);

        Task<IErmisChannel> CreateNewChannelAsync(IEnumerable<IErmisUser> users);

        TPopup ShowPopup<TPopup>()
            where TPopup : BaseFullscreenPopup;

        void HidePopup<TPopup>(TPopup instance)
            where TPopup : BaseFullscreenPopup;

        Task UpdateChannelsAsync();

        Task LoadPreviousMessagesAsync();
    }
}