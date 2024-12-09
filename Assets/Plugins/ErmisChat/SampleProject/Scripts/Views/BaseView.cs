using System;
using Ermis.Core;
using ErmisChat.SampleProject.Inputs;
using ErmisChat.SampleProject.Utils;
using UnityEngine;

namespace ErmisChat.SampleProject.Views
{
    /// <summary>
    /// Base class for view
    /// </summary>
    public abstract class BaseView : MonoBehaviour
    {
        public void Init(IChatViewContext viewContext)
        {
            ViewContext = viewContext ?? throw new ArgumentNullException(nameof(viewContext));

            _isInited = true;
            OnInited();
        }

        protected IErmisChatClient Client => ViewContext.Client;
        protected IImageLoader ImageLoader => ViewContext.ImageLoader;
        protected IViewFactory Factory => ViewContext.Factory;
        protected IChatViewContext ViewContext { get; private set; }
        protected IChatState State => ViewContext.State;
        protected IInputSystem InputSystem => ViewContext.InputSystem;

        protected readonly string[] AllowedVideoFormats = new[] { "mp4", "mov", "mpg", "mpeg", "avi", "mkv" };
        protected readonly string[] AllowedImageFormats = new[] { "jpg", "png", "jpeg", "gif", "bmp" };
        protected readonly string[] AllowedVoiceFormats = new[] { "mp3", "wav", "flac" };

        protected void OnDestroy()
        {
            if (_isInited)
            {
                OnDisposing();
            }

            _destroyed = true;
        }

        protected void Start() => OnStart();

        protected void Update()
        {
            if (!_isInited || _destroyed)
            {
                return;
            }

            OnUpdate();
        }
        
        protected virtual void OnStart()
        {
        }

        protected virtual void OnInited()
        {

        }

        protected virtual void OnUpdate()
        {

        }

        protected virtual void OnDisposing()
        {

        }

        private bool _isInited;
        private bool _destroyed;
    }
}