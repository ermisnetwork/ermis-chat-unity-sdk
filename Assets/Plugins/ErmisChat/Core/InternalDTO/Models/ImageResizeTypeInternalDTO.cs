﻿//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------


using Ermis.Core.InternalDTO.Responses;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.InternalDTO.Events;

namespace Ermis.Core.InternalDTO.Models
{
    using System = global::System;

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]

    internal readonly struct ImageResizeTypeInternalDTO : System.IEquatable<ImageResizeTypeInternalDTO>, Ermis.Core.LowLevelClient.IEnumeratedStruct<ImageResizeTypeInternalDTO>
    {
        public string Value { get; }

        public static readonly ImageResizeTypeInternalDTO Clip = new ImageResizeTypeInternalDTO("clip");
        public static readonly ImageResizeTypeInternalDTO Crop = new ImageResizeTypeInternalDTO("crop");
        public static readonly ImageResizeTypeInternalDTO Scale = new ImageResizeTypeInternalDTO("scale");
        public static readonly ImageResizeTypeInternalDTO Fill = new ImageResizeTypeInternalDTO("fill");

        public ImageResizeTypeInternalDTO(string value)
        {
            Value = value;
        }

        public ImageResizeTypeInternalDTO Parse(string value) => new ImageResizeTypeInternalDTO(value);

        public override string ToString() => Value;

        public bool Equals(ImageResizeTypeInternalDTO other) => Value == other.Value;

        public override bool Equals(object obj) => obj is ImageResizeTypeInternalDTO other && Equals(other);

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(ImageResizeTypeInternalDTO left, ImageResizeTypeInternalDTO right) => left.Equals(right);

        public static bool operator !=(ImageResizeTypeInternalDTO left, ImageResizeTypeInternalDTO right) => !left.Equals(right);

        public static implicit operator ImageResizeTypeInternalDTO(string value) => new ImageResizeTypeInternalDTO(value);

        public static implicit operator string(ImageResizeTypeInternalDTO type) => type.Value;
    }
}
