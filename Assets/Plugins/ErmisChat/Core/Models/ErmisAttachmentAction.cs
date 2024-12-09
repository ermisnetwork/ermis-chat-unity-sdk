using Ermis.Core.InternalDTO.Models;
using Ermis.Core.State;
using Ermis.Core.State.Caches;

namespace Ermis.Core.Models
{
    public class ErmisAttachmentAction : IStateLoadableFrom<ActionInternalDTO, ErmisAttachmentAction>
    {
        public string Name { get; private set; }

        public string Style { get; private set; }

        public string Text { get; private set; }

        public string Type { get; private set; }

        public string Value { get; private set; }

        ErmisAttachmentAction IStateLoadableFrom<ActionInternalDTO, ErmisAttachmentAction>.LoadFromDto(ActionInternalDTO dto, ICache cache)
        {
            Name = dto.Name;
            Style = dto.Style;
            Text = dto.Text;
            Type = dto.Type;
            Value = dto.Value;

            return this;
        }
    }
}