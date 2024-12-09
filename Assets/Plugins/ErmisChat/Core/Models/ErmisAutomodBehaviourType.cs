using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient;

namespace Ermis.Core.Models
{
    public readonly struct ErmisAutomodBehaviourType : System.IEquatable<ErmisAutomodBehaviourType>,
        ILoadableFrom<AutomodBehaviourTypeInternalDTO, ErmisAutomodBehaviourType>,
        ISavableTo<AutomodBehaviourTypeInternalDTO>
    {
        public static readonly ErmisAutomodBehaviourType Flag = new ErmisAutomodBehaviourType("flag");
        public static readonly ErmisAutomodBehaviourType Block = new ErmisAutomodBehaviourType("block");
        public static readonly ErmisAutomodBehaviourType ShadowBlock = new ErmisAutomodBehaviourType("shadow_block");

        public ErmisAutomodBehaviourType(string value)
        {
            _value = value;
        }

        public override string ToString() => _value;

        public bool Equals(ErmisAutomodBehaviourType other) => _value == other._value;

        public override bool Equals(object obj) => obj is ErmisAutomodBehaviourType other && Equals(other);

        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(ErmisAutomodBehaviourType left, ErmisAutomodBehaviourType right)
            => left.Equals(right);

        public static bool operator !=(ErmisAutomodBehaviourType left, ErmisAutomodBehaviourType right)
            => !left.Equals(right);

        public static implicit operator ErmisAutomodBehaviourType(string value)
            => new ErmisAutomodBehaviourType(value);

        public static implicit operator string(ErmisAutomodBehaviourType type) => type._value;

        ErmisAutomodBehaviourType ILoadableFrom<AutomodBehaviourTypeInternalDTO, ErmisAutomodBehaviourType>.
            LoadFromDto(AutomodBehaviourTypeInternalDTO dto)
            => new ErmisAutomodBehaviourType(dto.Value);

        AutomodBehaviourTypeInternalDTO ISavableTo<AutomodBehaviourTypeInternalDTO>.SaveToDto()
            => new AutomodBehaviourTypeInternalDTO(_value);

        private readonly string _value;
    }
}