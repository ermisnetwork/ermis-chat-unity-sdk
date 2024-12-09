using System;
using ErmisChat.SampleProject.Popups;
using ErmisChat.SampleProject.Views;
using UnityEngine;
using UnityEngine.Serialization;

namespace ErmisChat.SampleProject.Configs
{
    [Serializable]
    public struct ViewFactoryConfig : IViewFactoryConfig
    {
        public MessageOptionsPopup MessageOptionsPopupPrefab => _messageOptionsPopupPrefab;
        public CreateNewChannelFormPopup CreateNewChannelFormPopupPrefab => _createNewChannelPopupPrefab;
        public InviteChannelMembersPopup InviteChannelMembersPopupPrefab => _inviteChannelMembersPopup;
        public InviteReceivedPopup InviteReceivedPopup => inviteReceivedPopup;
        public ErrorPopup ErrorPopupPrefab => _errorPopupPrefab;

        [SerializeField]
        private MessageOptionsPopup _messageOptionsPopupPrefab;

        [SerializeField]
        private CreateNewChannelFormPopup _createNewChannelPopupPrefab;
        
        [SerializeField]
        private InviteChannelMembersPopup _inviteChannelMembersPopup;
        
        [FormerlySerializedAs("_invitationReceivedPopup")]
        [SerializeField]
        private InviteReceivedPopup inviteReceivedPopup;

        [SerializeField]
        private ErrorPopup _errorPopupPrefab;
    }
}