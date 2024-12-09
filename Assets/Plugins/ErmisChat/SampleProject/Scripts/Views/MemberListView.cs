using System.Collections;
using System.Collections.Generic;
using Ermis.Core.Helpers;
using System.Threading.Tasks;
using Ermis.Core.StatefulModels;
using ErmisChat.SampleProject.Popups;
using ErmisChat.SampleProject.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace ErmisChat.SampleProject.Views
{
    /// <summary>
    /// Shows list of channel members
    /// </summary>
    public class MemberListView : BaseView
    {
        protected override void OnInited()
        {
            base.OnInited();

            State.ActiveChanelChanged += OnActiveChannelChanged;
            _inviteButton.onClick.AddListener(OnInviteButtonClicked);

            RebuildMembers();
        }

        protected override void OnDisposing()
        {
            State.ActiveChanelChanged -= OnActiveChannelChanged;

            ClearAll();

            base.OnDisposing();
        }

        private readonly List<MemberView> _members = new List<MemberView>();
        private readonly UnityImageWebLoader _imageLoader = new UnityImageWebLoader();

        [SerializeField]
        private MemberView _memberViewPrefab;

        [SerializeField]
        private GameObject _panelInteractButton;
        [SerializeField]
        private Button _DirectButton;
        [SerializeField]
        private Button _RemoveButton;

        [SerializeField]
        private Transform _membersContainer;
        
        [SerializeField]
        private Button _inviteButton;

        private void ClearAll()
        {
            foreach (var m in _members)
            {
                Destroy(m.gameObject);
            }

            _members.Clear();
        }
        
        private void OnInviteButtonClicked() => State.ShowPopup<InviteChannelMembersPopup>();

        private void OnActiveChannelChanged(IErmisChannel ermisChannel) => RebuildMembers();

        private void RebuildMembers()
        {
            ClearAll();

            if (State.ActiveChannel == null)
            {
                return;
            }
            
            foreach (var m in State.ActiveChannel.Members)
            {
                var memberEntryView = Instantiate(_memberViewPrefab, _membersContainer);
                memberEntryView.UpdateData(m, _imageLoader, OnClickMemeber);
                _members.Add(memberEntryView);
            }
        }

        private void OnClickMemeber(MemberView member)
        {
            _panelInteractButton.GetComponent<RectTransform>().position=member.GetComponentInParent<RectTransform>().position;
            _panelInteractButton.SetActive(true);
            _DirectButton.onClick.RemoveAllListeners();
            _DirectButton.onClick.AddListener(() => OnClickDirectMessage(member._member.User));
            _RemoveButton.onClick.AddListener(() => OnClickRemoveMember(member._member.User));
        }

        private void OnClickDirectMessage(IErmisUser user) {

            _panelInteractButton.SetActive(false);
            List<IErmisUser> users= new List<IErmisUser>();
            users.Add(user);
            State.CreateNewChannelAsync(users).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("Adding new channel failed with exception");
                    Debug.LogException(task.Exception.InnerException);
                    return;
                }

                var channel = task.Result;

                Debug.Log("Added new channel with id: " + channel.Id);

                channel.AddMembersAsync(new[] { Client.LocalUserData.User }).ContinueWith(_ =>
                {
                    State.UpdateChannelsAsync().LogExceptionsOnFailed();
                });
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        private void OnClickRemoveMember(IErmisUser user)
        {

            _panelInteractButton.SetActive(false);
            List<string> users = new List<string>();
            users.Add(user.Id);


            State.ActiveChannel.RemoveMembersAsync(users).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("Adding new channel failed with exception");
                    Debug.LogException(task.Exception.InnerException);
                    return;
                }

                State.UpdateChannelsAsync().LogExceptionsOnFailed();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}