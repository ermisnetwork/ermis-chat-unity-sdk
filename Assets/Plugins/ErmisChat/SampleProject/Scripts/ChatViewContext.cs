﻿using System;
using Ermis.Core;
using ErmisChat.SampleProject.Configs;
using ErmisChat.SampleProject.Inputs;
using ErmisChat.SampleProject.Utils;
using ErmisChat.SampleProject.Views;

namespace ErmisChat.SampleProject
{
    /// <inheritdoc />
    public class ChatViewContext : IChatViewContext
    {
        public IErmisChatClient Client { get; }
        public IImageLoader ImageLoader { get; }
        public IViewFactory Factory { get; }
        public IInputSystem InputSystem { get; }

        public IChatState State { get; }
        public IAppConfig AppConfig { get; }

        public ChatViewContext(IErmisChatClient client, IImageLoader imageLoader, ViewFactory viewFactory,
            IInputSystem inputSystem, IAppConfig appConfig)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
            ImageLoader = imageLoader ?? throw new ArgumentNullException(nameof(imageLoader));
            Factory = viewFactory ?? throw new ArgumentNullException(nameof(viewFactory));
            InputSystem = inputSystem ?? throw new ArgumentNullException(nameof(inputSystem));
            AppConfig = appConfig ?? throw new ArgumentNullException(nameof(appConfig));

            State = new ChatState(client, Factory);
        }
    }
}