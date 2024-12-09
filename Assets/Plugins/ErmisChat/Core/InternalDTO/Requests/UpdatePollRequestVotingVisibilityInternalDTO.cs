﻿//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------


using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.InternalDTO.Events;
using Ermis.Core.InternalDTO.Models;

namespace Ermis.Core.InternalDTO.Requests
{
    using System = global::System;

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]

    internal readonly struct UpdatePollRequestVotingVisibilityInternalDTO : System.IEquatable<UpdatePollRequestVotingVisibilityInternalDTO>, Ermis.Core.LowLevelClient.IEnumeratedStruct<UpdatePollRequestVotingVisibilityInternalDTO>
    {
        public string Value { get; }

        public static readonly UpdatePollRequestVotingVisibilityInternalDTO Anonymous = new UpdatePollRequestVotingVisibilityInternalDTO("anonymous");
        public static readonly UpdatePollRequestVotingVisibilityInternalDTO Public = new UpdatePollRequestVotingVisibilityInternalDTO("public");

        public UpdatePollRequestVotingVisibilityInternalDTO(string value)
        {
            Value = value;
        }

        public UpdatePollRequestVotingVisibilityInternalDTO Parse(string value) => new UpdatePollRequestVotingVisibilityInternalDTO(value);

        public override string ToString() => Value;

        public bool Equals(UpdatePollRequestVotingVisibilityInternalDTO other) => Value == other.Value;

        public override bool Equals(object obj) => obj is UpdatePollRequestVotingVisibilityInternalDTO other && Equals(other);

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(UpdatePollRequestVotingVisibilityInternalDTO left, UpdatePollRequestVotingVisibilityInternalDTO right) => left.Equals(right);

        public static bool operator !=(UpdatePollRequestVotingVisibilityInternalDTO left, UpdatePollRequestVotingVisibilityInternalDTO right) => !left.Equals(right);

        public static implicit operator UpdatePollRequestVotingVisibilityInternalDTO(string value) => new UpdatePollRequestVotingVisibilityInternalDTO(value);

        public static implicit operator string(UpdatePollRequestVotingVisibilityInternalDTO type) => type.Value;
    }
}