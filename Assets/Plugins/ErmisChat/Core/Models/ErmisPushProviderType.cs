using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient;

namespace Ermis.Core.Models
{
    public readonly struct ErmisPushProviderType : System.IEquatable<ErmisPushProviderType>,
        ILoadableFrom<PushProviderTypeInternalDTO, ErmisPushProviderType>,
        ISavableTo<PushProviderTypeInternalDTO>
    {
        public static readonly ErmisPushProviderType Firebase = new ErmisPushProviderType("firebase");
        public static readonly ErmisPushProviderType Apn = new ErmisPushProviderType("apn");
        public static readonly ErmisPushProviderType Huawei = new ErmisPushProviderType("huawei");
        public static readonly ErmisPushProviderType Xiaomi = new ErmisPushProviderType("xiaomi");
        
        public ErmisPushProviderType(string value)
        {
            _value = value;
        }

        public override string ToString() => _value;

        public bool Equals(ErmisPushProviderType other) => _value == other._value;

        public override bool Equals(object obj) => obj is ErmisPushProviderType other && Equals(other);

        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(ErmisPushProviderType left, ErmisPushProviderType right) => left.Equals(right);

        public static bool operator !=(ErmisPushProviderType left, ErmisPushProviderType right)
            => !left.Equals(right);

        public static implicit operator ErmisPushProviderType(string value) => new ErmisPushProviderType(value);

        public static implicit operator string(ErmisPushProviderType type) => type._value;

        ErmisPushProviderType ILoadableFrom<PushProviderTypeInternalDTO, ErmisPushProviderType>.
            LoadFromDto(PushProviderTypeInternalDTO dto)
            => new ErmisPushProviderType(dto.Value);

        PushProviderTypeInternalDTO ISavableTo<PushProviderTypeInternalDTO>.SaveToDto()
            => new PushProviderTypeInternalDTO(_value);

        private readonly string _value;
    }
}