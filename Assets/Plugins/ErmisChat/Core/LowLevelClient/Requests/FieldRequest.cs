﻿using Ermis.Core.InternalDTO.Requests;

namespace Ermis.Core.LowLevelClient.Requests
{
    public class FieldRequest : RequestObjectBase, ISavableTo<FieldRequestInternalDTO>
    {
        public bool Short { get; set; }

        public string Title { get; set; }

        public string Value { get; set; }

        FieldRequestInternalDTO ISavableTo<FieldRequestInternalDTO>.SaveToDto() =>
            new FieldRequestInternalDTO
            {
                Short = Short,
                Title = Title,
                Value = Value,
                AdditionalProperties = AdditionalProperties
            };
    }
}