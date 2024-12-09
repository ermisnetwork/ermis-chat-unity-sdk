using System;
using System.Collections.Generic;
using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient;
using Ermis.Core.State;
using Ermis.Core.StatefulModels;

namespace Ermis.Core.Requests
{
    public class ErmisUpdateMessageRequest : ISavableTo<UpdateMessageRequestInternalDTO>
    {
      

        /// <summary>
        /// Text of the message. Should be empty if `mml` is provided
        /// </summary>
        public string Text { get; set; }

        
        /// <summary>
        /// Add or update custom data associated with this message. This will be accessible through <see cref="IErmisMessage.CustomData"/>
        /// </summary>
        public ErmisCustomDataRequest CustomData { get; set; }

        UpdateMessageRequestInternalDTO ISavableTo<UpdateMessageRequestInternalDTO>.SaveToDto()
        {
            var messageRequestDto = new MessageUpdateRequestInternalDTO
            {
               
                Text = Text,
            };

            return new UpdateMessageRequestInternalDTO
            {
                Message = messageRequestDto,
            };
        }
    }
}