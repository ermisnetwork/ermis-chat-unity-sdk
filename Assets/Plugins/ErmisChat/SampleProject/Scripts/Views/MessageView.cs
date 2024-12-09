using System;
using System.Linq;
using System.Threading.Tasks;
using Ermis.Core.Models;
using Ermis.Core.StatefulModels;
using Ermis.Libs.Utils;
using ErmisChat.SampleProject.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

namespace ErmisChat.SampleProject.Views
{
    /// <summary>
    /// Message view
    /// </summary>
    public class MessageView : BaseView, IPointerDownHandler
    {
        public event Action<MessageView, PointerEventData> PointedDown;
        
        public IErmisMessage Message { get; private set; }

        public void UpdateData(IErmisMessage message, IImageLoader imageLoader)
        {
            imageLoader = imageLoader ?? throw new ArgumentNullException(nameof(imageLoader));
            Message = message ?? throw new ArgumentNullException(nameof(message));

            var isVideo = HasVideoAttachment(message, out var videoAttachment);
            var isImage = HasImageAttachment(message, out var imageAttachment);
            _videoPlayer.gameObject.SetActive(isVideo);
            _text.gameObject.SetActive(!isVideo);

            if (isVideo)
            {
                if (_renderTexture != null)
                {
                    _renderTexture.Release();
                }

                _renderTexture = new RenderTexture(_sourceRenderTexture);

                _videoPlayer.playOnAwake = false;
                _videoPlayer.isLooping = false;
                _videoPlayer.url = videoAttachment.AssetUrl;

                _videoPlayer.targetTexture = _renderTexture;
                _videoPlayer.GetComponentInChildren<RawImage>().texture = _renderTexture;
            }

            else if(isImage)
            {
                _imageAttact.gameObject.SetActive(isImage);
                ShowImageAsync(imageAttachment.ImageUrl, imageLoader).LogIfFailed();
                _text.gameObject.SetActive(!isImage);
            }    

            _text.text = GetMessageText(message);
            _author.text = Message.User.Name;
            _date.text = Message.CreatedAt.DateTime.TimeAgo();

            ShowAvatarAsync(Message.User.Image, imageLoader).LogIfFailed();

            ShowReactions(Message);
        }

        public void TryPlay()
        {
            if (!_videoPlayer.url.IsNullOrEmpty())
            {
                Play();
            }
        }

        public void OnPointerDown(PointerEventData eventData) => PointedDown?.Invoke(this, eventData);

        protected void Awake()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _videoPlayer.loopPointReached += OnVideoPlayerLoopPointReached;
        }

        protected override void OnDisposing()
        {
            _isDestroyed = true;

            if (_renderTexture != null)
            {
                _renderTexture.Release();
            }

            base.OnDisposing();
        }

        private bool _isDestroyed;
        private RenderTexture _renderTexture;

        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private TMP_Text _author;

        [SerializeField]
        private TMP_Text _date;

        [SerializeField]
        private VideoPlayer _videoPlayer;
        [SerializeField]
        private Image _imageAttact;

        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private Image _avatar;

        [SerializeField]
        private Transform _emojisContainer;

        [SerializeField]
        private Image _emojiPrefab;

        [SerializeField]
        private RenderTexture _sourceRenderTexture;

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


        private async Task ShowImageAsync(string url, IImageLoader imageLoader)
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

            _imageAttact.sprite = sprite;
        }
        private void ShowReactions(IErmisMessage message)
        {
            var anyShown = false;
            foreach (var reactionCount in message.ReactionCounts)
            {
                anyShown = true;
                Factory.CreateEmoji(_emojiPrefab, _emojisContainer, reactionCount.Key);
            }

            _emojisContainer.gameObject.SetActive(anyShown);
        }

        private static string GetMessageText(IErmisMessage message)
            => message.IsDeleted ? ChatState.MessageDeletedInfo : message.Text;

        private bool HasVideoAttachment(IErmisMessage message, out ErmisMessageAttachment videoAttachment)
        {
            for (int i = 0; i < message.Attachments.Count; i++)
            {
                var attachment = message.Attachments[0];

                if (attachment.Type=="video")
                {
                    videoAttachment = attachment;
                    return true;
                }
            }

            videoAttachment = default;
            return false;
        }

        private bool HasImageAttachment(IErmisMessage message, out ErmisMessageAttachment imageAttachment)
        {
            for (int i = 0; i < message.Attachments.Count; i++)
            {
                var attachment = message.Attachments[0];

                if (attachment.Type=="image")
                {
                    imageAttachment = attachment;
                    return true;
                }
            }

            imageAttachment = default;
            return false;
        }

        private void OnPlayButtonClicked()
        {
            Play();
        }

        private void Play()
        {
            _videoPlayer.Play();
            _playButton.gameObject.SetActive(false);
        }

        private void OnVideoPlayerLoopPointReached(VideoPlayer source)
        {
            _playButton.gameObject.SetActive(true);
        }
    }
}