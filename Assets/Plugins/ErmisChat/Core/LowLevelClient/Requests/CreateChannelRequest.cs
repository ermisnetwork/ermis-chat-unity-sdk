using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Requests;

namespace Ermis.Core.LowLevelClient.Requests
{
    public class CreateChannelRequest : RequestObjectBase, ISavableTo<CreateChannelRequestInternalDto>
    {
        public CreateChannelInfo ChannelInfo { get; set; }

        public string ProjectId { get; set; }
        public CreateChannelMessages Messages { get; set; }


        CreateChannelRequestInternalDto ISavableTo<CreateChannelRequestInternalDto>.SaveToDto()
        {
            return new CreateChannelRequestInternalDto
            {
                ChannelInfo = ChannelInfo.TrySaveToDto(),
                ProjectId = ProjectId,
                Messages = Messages.TrySaveToDto(),
            };
        }
    }

    public class CreateChannelInfo : RequestObjectBase, ISavableTo<CreateChannelInfoInternalDto>
    {
        
        public string Name { get; set; }
        public System.Collections.Generic.List<string> Members { get; set; }



        CreateChannelInfoInternalDto ISavableTo<CreateChannelInfoInternalDto>.SaveToDto()
        {
            return new CreateChannelInfoInternalDto
            {
                Name = Name,
                Members = Members,
            };
        }
    }

    public class CreateChannelMessages : RequestObjectBase, ISavableTo<CreateChannelMessagesInternalDto>
    {

        public int Limit { get; set; }

        CreateChannelMessagesInternalDto ISavableTo<CreateChannelMessagesInternalDto>.SaveToDto()
        {
            return new CreateChannelMessagesInternalDto
            {
                Limit = Limit,
            };
        }
    }
}
