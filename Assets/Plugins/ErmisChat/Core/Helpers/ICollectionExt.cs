using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Ermis.Core.LowLevelClient;

namespace Ermis.Core.Helpers
{
    /// <summary>
    /// <see cref="ICollection"/> extensions
    /// </summary>
    internal static class ICollectionExt
    {

    
        [Pure]
        public static bool ContainsNoAlloc<TItem>(this List<TItem> source, TItem item)
        {
            for (var i = 0; i < source.Count; i++)
            {
                if (EqualityComparer<TItem>.Default.Equals(source[i], item))
                {
                    return true;
                }
            }

            return false;
        }
        
        [Pure]
        public static List<TDto> TrySaveToDtoCollection<TSource, TDto>(this List<TSource> source)
            where TSource : ISavableTo<TDto>
        {
            if (source == null)
            {
                return null;
            }

            var dtos = new List<TDto>(source.Count);

            foreach (var item in source)
            {
                if (item == null)
                {
                    continue;
                }
                dtos.Add(item.SaveToDto());
            }

            return dtos;
        }

        [Pure]
        public static List<TSource> TryLoadFromDtoCollection<TDto, TSource>(this List<TSource> _, List<TDto> dtos)
            where TSource : ILoadableFrom<TDto, TSource>, new()
        {
            if (dtos == null)
            {
                return null;
            }

            var items = new List<TSource>(dtos.Count);

            foreach (var dto in dtos)
            {
                items.Add(new TSource().LoadFromDto(dto));
            }

            return items;
        }
    }
}