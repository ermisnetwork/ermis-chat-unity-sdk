using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ermis.Core.Helpers;
using Ermis.Core.Requests;
using Ermis.Core.StatefulModels;
using Ermis.Libs.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using SFB;
#if UNITY_EDITOR
#endif

namespace ErmisChat.SampleProject.Views
{
    /// <summary>
    /// Message input view
    /// </summary>
    public class MessageInputView : BaseView
    {
        protected void Awake()
        {
            _sendButton.onClick.AddListener(OnSendButtonClicked);
            _attachmentButton.onClick.AddListener(OnAttachmentButtonClicked);
            _messageInput.onValueChanged.AddListener(OnMessageInputValueChanged);
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            _typingMonitor?.Update();

            if (InputSystem.WasEnteredPressedThisFrame)
            {
                OnSendButtonClicked();
            }
        }

        protected override void OnInited()
        {
            base.OnInited();

            _typingMonitor = new TypingMonitor(_messageInput, Client, State, isActive: () => _mode == Mode.Create);

            ViewContext.State.MessageEditRequested += OnMessageEditRequested;

            var sb = new StringBuilder();
            foreach (var sprite in ViewContext.AppConfig.Emojis.AllSprites)
            {
                if (sprite == null)
                {
                    continue;
                }

                sb.Append(":");
                sb.Append(sprite.name);
                sb.Append(":");

                var shortcode = sb.ToString();
                _emojisShortcodes.Add(shortcode);
                _emojiShortcodeToSpriteName[shortcode] = sprite.name;

                sb.Length = 0;
            }
        }

        protected override void OnDisposing()
        {
            _sendButton.onClick.RemoveListener(OnSendButtonClicked);
            _attachmentButton.onClick.RemoveListener(OnAttachmentButtonClicked);
            _messageInput.onValueChanged.RemoveListener(OnMessageInputValueChanged);

            ViewContext.State.MessageEditRequested -= OnMessageEditRequested;

            _typingMonitor?.Dispose();
            _typingMonitor = null;

            base.OnDisposing();
        }

        private void OnMessageEditRequested(IErmisMessage message)
        {
            _currentEditMessage = message;
            _mode = Mode.Edit;
            _messageInput.text = message.Text;

            _messageInput.Select();
            _messageInput.ActivateInputField();
        }

        private enum Mode
        {
            Create,
            Edit,
        }

        private readonly List<string> _emojisShortcodes = new List<string>();
        private readonly Dictionary<string, string> _emojiShortcodeToSpriteName = new Dictionary<string, string>();

        [SerializeField]
        private TMP_InputField _messageInput;

        [SerializeField]
        private Button _sendButton;

        [SerializeField]
        private Button _attachmentButton;

        private Mode _mode;
        private IErmisMessage _currentEditMessage;
        private string _lastAttachmentUrl;
        private TypingMonitor _typingMonitor;

        private async void OnSendButtonClicked()
        {
            if (_messageInput.text.Length == 0)
            {
                return;
            }

            var channelState = ViewContext.State.ActiveChannel;

            if (channelState == null)
            {
                Debug.LogError("Failed to send message because the active channel is null");
                return;
            }

            var uploadedFileUrl = string.Empty;
            var uploadedFileType = "";
            var uploadedFileTitle = "";
            long uploadedFileSize = 0;
            var uploadFileMime = "image/png";

            if (!_lastAttachmentUrl.IsNullOrEmpty())
            {
                try
                {
                    uploadedFileType = GetFileType(Path.GetExtension(_lastAttachmentUrl));
                    uploadedFileTitle = Path.GetFileName(_lastAttachmentUrl);
                    uploadedFileSize = (new FileInfo(_lastAttachmentUrl)).Length;
                    // Trả về kích thước tệp tính bằng byte
                    //      uploadedFileType = uploadedFileType.Replace(".", "");
                    Debug.Log("Start uploading attachment: " + _lastAttachmentUrl + ". This may take a while.");
                    _messageInput.text =
                        "Uploading attachment. This may take a while. Operation is asynchronous so you can continue using chat without being blocked.";

                    var fileContent = File.ReadAllBytes(_lastAttachmentUrl);


                    var uploadFileResponse = await State.ActiveChannel.UploadFileAsync(fileContent, "attachment-1");
                    uploadedFileUrl = uploadFileResponse.FileUrl;
                    _lastAttachmentUrl = string.Empty;

                    Debug.Log("Upload successful, CDN url: " + uploadedFileUrl);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }

            switch (_mode)
            {
                case Mode.Create:

                    var sendMessageRequest = new ErmisSendMessageRequest
                    {
                        Text = _messageInput.text,
                        Id = Guid.NewGuid().ToString()

                    };

                    if (!uploadedFileUrl.IsNullOrEmpty())
                    {

                        sendMessageRequest.Attachments = new List<ErmisAttachmentRequest>();

                        ErmisAttachmentRequest request = new ErmisAttachmentRequest();
                        request.Type = uploadedFileType;
                        request.Title = uploadedFileTitle;
                        request.FileSize = (int)uploadedFileSize;
                        request.MimeType = uploadFileMime;
                        if (uploadedFileType == "image")
                        {
                            request.ImageUrl = uploadedFileUrl;
                        }
                        else if (uploadedFileType == "file" || uploadedFileType == "voiceRecording")
                        {
                            request.AssetUrl = uploadedFileUrl;
                        }
                        else if (uploadedFileType == "video")
                        {
                            request.AssetUrl = uploadedFileUrl;
                            request.ThumbUrl = "";
                        }

                        sendMessageRequest.Attachments.Add(request);
                    }

                    var sentMessage = await State.ActiveChannel.SendNewMessageAsync(sendMessageRequest);

                    if (!uploadedFileUrl.IsNullOrEmpty())
                    {
                        Debug.Log(sentMessage.Attachments.First().AssetUrl);
                    }

                    break;

                case Mode.Edit:

                    _currentEditMessage.UpdateAsync(new ErmisUpdateMessageRequest
                    {
                        Text = _messageInput.text
                    }).LogExceptionsOnFailed();

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            _lastAttachmentUrl = string.Empty;
            _messageInput.text = "";

            _messageInput.Select();
            _messageInput.ActivateInputField();

            _currentEditMessage = null;
            _mode = Mode.Create;

            _typingMonitor.NotifyChannelStoppedTyping(State.ActiveChannel);
        }

        private string GetFileType(string extension)
        {

            // Xác định loại tệp dựa trên phần mở rộng
            if (extension == ".mp4" || extension == ".mkv" || extension == ".avi" || extension == ".mpeg" || extension == ".mov")
            {
                return "video";
            }
            else if (extension == ".jpg" || extension == ".png" || extension == ".gif" || extension == ".bmp" || extension == ".jpeg")
            {
                return "image";
            }
            else if (extension == ".mp3" || extension == ".wav" || extension == ".ogg")
            {
                return "voiceRecording";
            }
            else
            {
                return "file";  // Nếu không thuộc loại nào đã định nghĩa
            }
        }


        private void OnAttachmentButtonClicked()
        {
#if UNITY_EDITOR||UNITY_STANDALONE_WIN
            var filters = new string[8];
            filters[0] = "Video files";
            filters[1] = string.Join(",", AllowedVideoFormats);
            filters[2] = "Image files";
            filters[3] = string.Join(",", AllowedImageFormats);
            filters[4] = "Audio files";
            filters[5] = string.Join(",", AllowedVoiceFormats);
            filters[6] = "All files";
            filters[7] = "*";

            var extensions = new[] {
                new ExtensionFilter("Video files", AllowedVideoFormats ),
                new ExtensionFilter("Image files", AllowedImageFormats),
                new ExtensionFilter("Audio files",  AllowedVoiceFormats),
                 new ExtensionFilter("All files", "*" ),
            };
            string[] filePaths = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, false);
            if (filePaths != null)
                _lastAttachmentUrl = filePaths[0];
            _messageInput.text = "Attachment ready: " + _lastAttachmentUrl;
#else
            Debug.LogError("Please implement file picker for this platform. File picker in demo only works in editor.");
#endif
        }

        private void OnMessageInputValueChanged(string value) => ReplaceEmojisWithSpriteMarkdown();

        private void ReplaceEmojisWithSpriteMarkdown()
        {
            var source = _messageInput.text;
            foreach (var shortcode in _emojisShortcodes)
            {
                var spriteName = _emojiShortcodeToSpriteName[shortcode];
                source = source.Replace(shortcode, $"<sprite name=\"{spriteName}\">");
            }

            if (source != _messageInput.text)
            {
                _messageInput.SetTextWithoutNotify(source);
                _messageInput.caretPosition = _messageInput.text.Length;
            }
        }
    }
}