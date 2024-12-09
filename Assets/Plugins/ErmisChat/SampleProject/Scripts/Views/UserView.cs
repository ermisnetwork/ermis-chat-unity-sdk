using System;
using System.Threading.Tasks;
using Ermis.Core.StatefulModels;
using Ermis.Libs.Utils;
using ErmisChat.SampleProject.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ErmisChat.SampleProject.Views
{
    /// <summary>
    /// Represents a single member on the channel member list
    /// </summary>
    public class UserView : MonoBehaviour
    {
        public void UpdateData(IErmisUser user, IImageLoader imageLoader,Action<UserView> onClickMember)
        {
            if (_user != null)
            {
                _user.PresenceChanged -= OnlineStatusChanged;
            }

            _user = user;
            _user.PresenceChanged += OnlineStatusChanged;

            _label.text = GetName(_user);
            UpdateOnlineStatus(_user.Online);
            
            ShowAvatarAsync(_user.Image, imageLoader).LogIfFailed();
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(()=> onClickMember(this));
        }

        protected void OnDestroy()
        {
            _isDestroyed = true;
            if (_user == null)
            {
                return;
            }

            _user.PresenceChanged -= OnlineStatusChanged;
        }

        [SerializeField]
        private TMP_Text _label;

        [SerializeField]
        private GameObject _statusIsOnline;

        [SerializeField]
        private GameObject _statusIsOffline;
        
        [SerializeField]
        private Image _avatar;

        [SerializeField]
        private Button _button;

        public IErmisUser _user;
        private bool _isDestroyed;

        private async Task ShowAvatarAsync(string url, IImageLoader imageLoader)
        {
            if (string.IsNullOrEmpty(url))
            {
                return;
            }

            var sprite = await imageLoader.LoadImageAsync(url);
            if (_isDestroyed || sprite == null)
            {
                return;
            }

            _avatar.sprite = sprite;
        }
        
        private void UpdateOnlineStatus(bool online)
        {
            _statusIsOnline.SetActive(online);
            _statusIsOffline.SetActive(!online);
        }

        private void OnlineStatusChanged(IErmisUser user, bool isOnline, DateTimeOffset? lastActive)
            => UpdateOnlineStatus(isOnline);

        private string GetName(IErmisUser user) => user.Name.IsNullOrEmpty() ? user.Id : user.Name; // Name is optional
    }
}