using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ermis.Core.InternalDTO.Requests
{
    internal  class CreateChannelRequestInternalDto
    {
        [JsonProperty("data")]
        public CreateChannelInfoInternalDto ChannelInfo;
        [JsonProperty("project_id")]
        public string ProjectId;
        [JsonProperty("messages")]
        public CreateChannelMessagesInternalDto Messages;
    }

    internal class CreateChannelInfoInternalDto
    {
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("members")]
        public List<string> Members;
    }

    internal class CreateChannelMessagesInternalDto
    {
        [JsonProperty("limit")]
        public int Limit;
    }
}
