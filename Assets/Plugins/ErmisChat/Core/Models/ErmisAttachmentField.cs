using Ermis.Core.InternalDTO.Models;
using Ermis.Core.State;
using Ermis.Core.State.Caches;

namespace Ermis.Core.Models
{
    public class ErmisAttachmentField : IStateLoadableFrom<FieldInternalDTO, ErmisAttachmentField>
    {
        public bool? Short { get; private set; }

        public string Title { get; private set; }

        public string Value { get; private set; }

        ErmisAttachmentField IStateLoadableFrom<FieldInternalDTO, ErmisAttachmentField>.LoadFromDto(FieldInternalDTO dto, ICache cache)
        {
            Short = dto.Short;
            Title = dto.Title;
            Value = dto.Value;

            return this;
        }
    }
}