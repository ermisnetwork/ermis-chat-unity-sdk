using Ermis.Core.InternalDTO.Responses;

namespace Ermis.Core.LowLevelClient.Responses
{
    public partial class ModerationResponse : ResponseObjectBase, ILoadableFrom<ModerationResponseInternalDTO, ModerationResponse>
    {
        public float? Explicit { get; set; }

        public float? Spam { get; set; }

        public float? Toxic { get; set; }

        ModerationResponse ILoadableFrom<ModerationResponseInternalDTO, ModerationResponse>.LoadFromDto(ModerationResponseInternalDTO dto)
        {
            Explicit = dto.Explicit;
            Spam = dto.Spam;
            Toxic = dto.Toxic;
            AdditionalProperties = dto.AdditionalProperties;

            return this;
        }
    }
}