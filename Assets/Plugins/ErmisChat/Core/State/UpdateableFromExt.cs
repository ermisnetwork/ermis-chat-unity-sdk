using Ermis.Core.State.Caches;

namespace Ermis.Core.State
{
    internal static class UpdateableFromExt
    {
        public static void TryUpdateFromDto<TDto, TTrackedObject>(this IUpdateableFrom<TDto, TTrackedObject> updateable, TDto dto, ICache cache)
            where TTrackedObject : IErmisStatefulModel, IUpdateableFrom<TDto, TTrackedObject>
        {
            updateable?.UpdateFromDto(dto, cache);
        }
    }
}