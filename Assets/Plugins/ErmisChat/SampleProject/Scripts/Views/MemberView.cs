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
    public class MemberView : MonoBehaviour
    {
        public void UpdateData(IErmisChannelMember member, IImageLoader imageLoader,Action<MemberView> onClickMember)
        {
            if (_member != null)
            {
                _member.User.PresenceChanged -= OnlineStatusChanged;
            }

            _member = member;
            _member.User.PresenceChanged += OnlineStatusChanged;

            _label.text = GetName(_member.User);
            UpdateOnlineStatus(_member.User.Online);
            
            ShowAvatarAsync(_member.User.Image, imageLoader).LogIfFailed();
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(()=> onClickMember(this));
        }

        protected void OnDestroy()
        {
            _isDestroyed = true;
            if (_member == null)
            {
                return;
            }

            _member.User.PresenceChanged -= OnlineStatusChanged;
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

        public IErmisChannelMember _member;
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