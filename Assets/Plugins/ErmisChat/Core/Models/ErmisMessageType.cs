using System;
using Ermis.Core.InternalDTO.Models;
using Ermis.Core.LowLevelClient;

namespace Ermis.Core.Models
{
    public readonly struct ErmisMessageType : IEquatable<ErmisMessageType>,
        ILoadableFrom<MessageTypeInternalDTO, ErmisMessageType>, ISavableTo<MessageTypeInternalDTO>
    {
        public static readonly ErmisMessageType Regular = new ErmisMessageType("regular");
        public static readonly ErmisMessageType Ephemeral = new ErmisMessageType("ephemeral");
        public static readonly ErmisMessageType Error = new ErmisMessageType("error");
        public static readonly ErmisMessageType Reply = new ErmisMessageType("reply");
        public static readonly ErmisMessageType System = new ErmisMessageType("system");
        public static readonly ErmisMessageType Deleted = new ErmisMessageType("deleted");
        
        public ErmisMessageType(string value)
        {
            _value = value;
        }

        public override string ToString() => _value;

        public bool Equals(ErmisMessageType other) => _value == other._value;

        public override bool Equals(object obj) => obj is ErmisMessageType other && Equals(other);

        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(ErmisMessageType left, ErmisMessageType right) => left.Equals(right);

        public static bool operator !=(ErmisMessageType left, ErmisMessageType right) => !left.Equals(right);

        public static implicit operator ErmisMessageType(string value) => new ErmisMessageType(value);

        public static implicit operator string(ErmisMessageType type) => type._value;

        ErmisMessageType ILoadableFrom<MessageTypeInternalDTO, ErmisMessageType>.LoadFromDto(MessageTypeInternalDTO dto) => new ErmisMessageType(dto);

        MessageTypeInternalDTO ISavableTo<MessageTypeInternalDTO>.SaveToDto() => new MessageTypeInternalDTO(_value);

        private readonly string _value;
    }
}