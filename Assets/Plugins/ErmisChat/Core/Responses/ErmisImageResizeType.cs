using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient;

namespace Ermis.Core.Responses
{
    public readonly struct ErmisImageResizeType : System.IEquatable<ErmisImageResizeType>,
        ILoadableFrom<ImageResizeTypeInternalDTO, ErmisImageResizeType>, ISavableTo<ImageResizeTypeInternalDTO>
    {
        public static readonly ErmisImageResizeType Clip = new ErmisImageResizeType("clip");
        public static readonly ErmisImageResizeType Crop = new ErmisImageResizeType("crop");
        public static readonly ErmisImageResizeType Scale = new ErmisImageResizeType("scale");
        public static readonly ErmisImageResizeType Fill = new ErmisImageResizeType("fill");

        public ErmisImageResizeType(string value)
        {
            _value = value;
        }

        public override string ToString() => _value;

        public bool Equals(ErmisImageResizeType other) => _value == other._value;

        public override bool Equals(object obj) => obj is ErmisImageResizeType other && Equals(other);

        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(ErmisImageResizeType left, ErmisImageResizeType right) => left.Equals(right);

        public static bool operator !=(ErmisImageResizeType left, ErmisImageResizeType right) => !left.Equals(right);

        public static implicit operator ErmisImageResizeType(string value) => new ErmisImageResizeType(value);

        public static implicit operator string(ErmisImageResizeType type) => type._value;

        ErmisImageResizeType ILoadableFrom<ImageResizeTypeInternalDTO, ErmisImageResizeType>.
            LoadFromDto(ImageResizeTypeInternalDTO dto)
            => new ErmisImageResizeType(dto.ToString());

        ImageResizeTypeInternalDTO ISavableTo<ImageResizeTypeInternalDTO>.SaveToDto()
            => new ImageResizeTypeInternalDTO(_value);

        private readonly string _value;
    }
}