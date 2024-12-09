using Ermis.Core.InternalDTO.Models;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient;

namespace Ermis.Core.Requests
{
    public sealed class ErmisSortParam : ISavableTo<SortParamInternalDTO>,  ISavableTo<SortParamRequestInternalDTO>
    {
        public int? Direction { get; set; }

        public string Field { get; set; }

        SortParamInternalDTO ISavableTo<SortParamInternalDTO>.SaveToDto() =>
            new SortParamInternalDTO
            {
                Direction = Direction,
                Field = Field,
            };
        
        SortParamRequestInternalDTO ISavableTo<SortParamRequestInternalDTO>.SaveToDto() =>
            new SortParamRequestInternalDTO
            {
                Direction = Direction,
                Field = Field,
            };
    }
}