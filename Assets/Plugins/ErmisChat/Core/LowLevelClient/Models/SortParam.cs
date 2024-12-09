using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient.Requests;

namespace Ermis.Core.LowLevelClient.Models
{
    public class SortParam : RequestObjectBase, ISavableTo<SortParamRequestInternalDTO>
    {
        public int? Direction { get; set; }

        public string Field { get; set; }

        SortParamRequestInternalDTO ISavableTo<SortParamRequestInternalDTO>.SaveToDto() =>
            new SortParamRequestInternalDTO
            {
                Direction = Direction,
                Field = Field,
                AdditionalProperties = AdditionalProperties
            };
    }
}