using Ermis.Core.State.Caches;

namespace Ermis.Core.State
{
    internal interface IUpdateableFrom<in TDto, out TTrackedObject>
        where TTrackedObject : IErmisStatefulModel, IUpdateableFrom<TDto, TTrackedObject>
    {
        void UpdateFromDto(TDto dto, ICache cache);
    }
}