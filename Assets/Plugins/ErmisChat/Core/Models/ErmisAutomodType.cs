using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient;

namespace Ermis.Core.Models
{
    public readonly struct ErmisAutomodType : System.IEquatable<ErmisAutomodType>,
        ILoadableFrom<AutomodTypeInternalDTO, ErmisAutomodType>, ISavableTo<AutomodTypeInternalDTO>
    {
        public static readonly ErmisAutomodType Disabled = new ErmisAutomodType("disabled");
        public static readonly ErmisAutomodType Simple = new ErmisAutomodType("simple");
        public static readonly ErmisAutomodType AI = new ErmisAutomodType("AI");
        
        public ErmisAutomodType(string value)
        {
            _value = value;
        }

        public override string ToString() => _value;

        public bool Equals(ErmisAutomodType other) => _value == other._value;

        public override bool Equals(object obj) => obj is ErmisAutomodType other && Equals(other);

        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(ErmisAutomodType left, ErmisAutomodType right) => left.Equals(right);

        public static bool operator !=(ErmisAutomodType left, ErmisAutomodType right) => !left.Equals(right);

        public static implicit operator ErmisAutomodType(string value) => new ErmisAutomodType(value);

        public static implicit operator string(ErmisAutomodType type) => type._value;

        ErmisAutomodType ILoadableFrom<AutomodTypeInternalDTO, ErmisAutomodType>.
            LoadFromDto(AutomodTypeInternalDTO dto) => new ErmisAutomodType(dto.Value);

        AutomodTypeInternalDTO ISavableTo<AutomodTypeInternalDTO>.SaveToDto() => new AutomodTypeInternalDTO(_value);

        private readonly string _value;
    }
}