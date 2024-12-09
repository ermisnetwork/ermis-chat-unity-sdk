using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ermis.Core.Helpers;
using Ermis.Core.StatefulModels;
using Ermis.Libs.Utils;
using ErmisChat.SampleProject.Utils;
using ErmisChat.SampleProject.Views;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ErmisChat.SampleProject.Popups;


namespace ErmisChat.SampleProject.Views
{
    public class UserListView : BaseView
    {
        protected override void OnInited()
        {
            base.OnInited();

            State.OnQueryUser += OnQueryUser;
            _inviteButton.onClick.AddListener(OnInviteButtonClicked);

            RebuildMembers(new List<IErmisUser>());
        }

        protected override void OnDisposing()
        {
            State.OnQueryUser -= OnQueryUser;

            ClearAll();

            base.OnDisposing();
        }

        private readonly List<UserView> _users = new List<UserView>();
        private readonly UnityImageWebLoader _imageLoader = new UnityImageWebLoader();

        [SerializeField]
        private UserView _userViewPrefab;

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
            foreach (var m in _users)
            {
                Destroy(m.gameObject);
            }

            _users.Clear();
        }

        private void OnInviteButtonClicked() => State.ShowPopup<InviteChannelMembersPopup>();

        private void OnQueryUser(List<IErmisUser> ermisUsers) => RebuildMembers(ermisUsers);

        private void RebuildMembers(List<IErmisUser> ermisUsers)
        {
            ClearAll();

            //if (State.ActiveChannel == null)
            //{
            //    return;
            //}

            foreach (var m in ermisUsers)
            {
                var userEntryView = Instantiate(_userViewPrefab, _membersContainer);
                userEntryView.UpdateData(m, _imageLoader, OnClickMemeber);
                _users.Add(userEntryView);
            }
        }

        private void OnClickMemeber(UserView user)
        {
            _DirectButton.GetComponent<RectTransform>().position = user.GetComponentInParent<RectTransform>().position;
            _panelInteractButton.SetActive(true);
            _DirectButton.onClick.RemoveAllListeners();
            _DirectButton.onClick.AddListener(() => OnClickDirectMessage(user._user));
            //_RemoveButton.onClick.AddListener(() => OnClickRemoveMember(user._user));
        }

        private void OnClickDirectMessage(IErmisUser user)
        {

            _panelInteractButton.SetActive(false);
            List<IErmisUser> users = new List<IErmisUser>();
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
            State.ActiveChannel.RemoveMembersAsync(users);
        }
    }
}
