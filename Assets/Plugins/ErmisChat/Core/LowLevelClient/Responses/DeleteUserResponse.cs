using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.LowLevelClient.Models;

namespace Ermis.Core.LowLevelClient.Responses
{
    public partial class DeleteUserResponse : ResponseObjectBase, ILoadableFrom<DeleteUserResponseInternalDTO, DeleteUserResponse>
    {
        /// <summary>
        /// Duration of the request in human-readable format
        /// </summary>
        public string Duration { get; set; }

        public string TaskId { get; set; }

        public User User { get; set; }

        DeleteUserResponse ILoadableFrom<DeleteUserResponseInternalDTO, DeleteUserResponse>.LoadFromDto(DeleteUserResponseInternalDTO dto)
        {
            Duration = dto.Duration;
            TaskId = dto.TaskId;
            User = User.TryLoadFromDto<UserIdObjectInternalDTO, User>(dto.User);
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}