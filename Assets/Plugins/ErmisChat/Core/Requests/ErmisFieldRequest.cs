using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient;

namespace Ermis.Core.Requests
{
    public class ErmisFieldRequest : ISavableTo<FieldRequestInternalDTO>
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
            };
    }
}