using Ermis.Core.InternalDTO.Models;
using Ermis.Core.State;
using Ermis.Core.State.Caches;

namespace Ermis.Core.Models
{
    public class ErmisImageData : IStateLoadableFrom<ImageDataInternalDTO, ErmisImageData>
    {
        public string Frames { get; private set; }

        public string Height { get; private set; }

        public string Size { get; private set; }

        public string Url { get; private set; }

        public string Width { get; private set; }

        ErmisImageData IStateLoadableFrom<ImageDataInternalDTO, ErmisImageData>.LoadFromDto(ImageDataInternalDTO dto, ICache cache)
        {
            Frames = dto.Frames;
            Height = dto.Height;
            Size = dto.Size;
            Url = dto.Url;
            Width = dto.Width;

            return this;
        }
    }
}