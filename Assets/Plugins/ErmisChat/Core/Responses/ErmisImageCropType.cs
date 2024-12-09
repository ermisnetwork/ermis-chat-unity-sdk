using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient;

namespace Ermis.Core.Responses
{
    public readonly struct ErmisImageCropType : System.IEquatable<ErmisImageCropType>,
        ILoadableFrom<ImageCropTypeInternalDTO, ErmisImageCropType>, ISavableTo<ImageCropTypeInternalDTO>
    {
        public static readonly ErmisImageCropType Top = new ErmisImageCropType("top");
        public static readonly ErmisImageCropType Bottom = new ErmisImageCropType("bottom");
        public static readonly ErmisImageCropType Left = new ErmisImageCropType("left");
        public static readonly ErmisImageCropType Right = new ErmisImageCropType("right");
        public static readonly ErmisImageCropType Center = new ErmisImageCropType("center");

        public ErmisImageCropType(string value)
        {
            _value = value;
        }

        public override string ToString() => _value;

        public bool Equals(ErmisImageCropType other) => _value == other._value;

        public override bool Equals(object obj) => obj is ErmisImageCropType other && Equals(other);

        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(ErmisImageCropType left, ErmisImageCropType right) => left.Equals(right);

        public static bool operator !=(ErmisImageCropType left, ErmisImageCropType right) => !left.Equals(right);

        public static implicit operator ErmisImageCropType(string value) => new ErmisImageCropType(value);

        public static implicit operator string(ErmisImageCropType type) => type._value;

        ErmisImageCropType ILoadableFrom<ImageCropTypeInternalDTO, ErmisImageCropType>.
            LoadFromDto(ImageCropTypeInternalDTO dto)
            => new ErmisImageCropType(dto.Value);

        ImageCropTypeInternalDTO ISavableTo<ImageCropTypeInternalDTO>.SaveToDto()
            => new ImageCropTypeInternalDTO(_value);

        private readonly string _value;
    }
}