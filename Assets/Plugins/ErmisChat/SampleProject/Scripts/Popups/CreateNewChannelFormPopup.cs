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

namespace ErmisChat.SampleProject.Popups
{
    /// <summary>
    /// Create new channel form
    /// </summary>
    public class CreateNewChannelFormPopup : BaseFullscreenPopup
    {
        public UserViewSelect _userViewPrefabs;
        private readonly List<UserViewSelect> _users = new List<UserViewSelect>();
        private readonly UnityImageWebLoader _imageLoader = new UnityImageWebLoader();
        [SerializeField]
        private Transform _membersContainer;
        [SerializeField]
        private GameObject _popupError;
        [SerializeField]
        private TextMeshProUGUI _textError;
        private List<IErmisUser> _listUsers;
        protected override void OnInited()
        {
            base.OnInited();

            _createButton.onClick.AddListener(OnCreateButtonClicked);
        }

        protected override void OnShow()
        {
            base.OnShow();

            _channelIdInput.Select();
            _channelIdInput.ActivateInputField();
            _listUsers = new List<IErmisUser>();
            AddUser();
        }

        private void AddUser()
        {
            _users.Clear();
            if (State.ErmisUsers == null)
            {
                return;
            }

            foreach (var m in State.ErmisUsers)
            {
                var memberEntryView = Instantiate(_userViewPrefabs, _membersContainer);
                memberEntryView.UpdateData(m, _imageLoader, OnSelectUser);
                _users.Add(memberEntryView);
            }
        }

        private void OnSelectUser(UserViewSelect user, bool val)
        {
            if (val)
            {
                if (!_listUsers.Contains(user._user))
                {
                    _listUsers.Add(user._user);
                }
            }
            else
            {
                if (_listUsers.Contains(user._user))
                {
                    _listUsers.Remove(user._user);
                }
            }
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            if (InputSystem.WasEnteredPressedThisFrame && !_isProcessing)
            {
                OnCreateButtonClicked();
            }
        }

        protected override void OnDisposing()
        {
            _createButton.onClick.RemoveListener(OnCreateButtonClicked);

            base.OnDisposing();
        }

        [SerializeField]
        private TMP_InputField _channelIdInput;

        [SerializeField]
        private Button _createButton;

        private bool _isProcessing;

        private void OnCreateButtonClicked()
        {
            if (_channelIdInput.text.IsNullOrEmpty())
            {
                
                ErrorPopup error= Factory.CreateFullscreenPopup<ErrorPopup>();
                error.SetData("Create Channel", "Please Set Name Channel");
                error.gameObject.SetActive(true);
                return;
            }

            if (_listUsers.Count<=0)
            {
                ErrorPopup error = Factory.CreateFullscreenPopup<ErrorPopup>();
                error.SetData("Create Channel", "Please add user");
                error.gameObject.SetActive(true);
                return;
            }

            if (_isProcessing)
            {
                return;
            }

            _isProcessing = true;

            State.CreateNewChannelAsync(_channelIdInput.text,_listUsers).ContinueWith(task =>
            {
                _isProcessing = false;

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

                    Hide();
                });
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}