using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ermis.Core.StatefulModels;

namespace Ermis.Core.Requests
{
    /// <summary>
    /// Request object to set Custom Data (max 5KB) that you can assign to:
    /// - <see cref="IErmisChannel"/>
    /// - <see cref="IErmisMessage"/>
    /// - <see cref="IErmisUser"/>
    /// - <see cref="ErmisChannelMember"/>
    /// If you want to have images or files as custom data, upload them using <see cref="IErmisChannel.UploadFileAsync"/> and <see cref="IErmisChannel.UploadImageAsync"/> and put only file URL as a custom data
    /// </summary>
    public class ErmisCustomDataRequest : IEnumerable
    {
        /// <summary>
        /// Default constructor. If you wish to inject your own internal dictionary e.g. from object pool you can use the other constructor
        /// </summary>
        public ErmisCustomDataRequest()
            : this(new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Constructor that allows you to inject your own dictionary to use by this object
        /// </summary>
        /// <param name="internalDictionary">Inject your dictionary to use by request</param>
        /// <exception cref="ArgumentNullException">if passed dictionary is null</exception>
        public ErmisCustomDataRequest(IDictionary<string, object> internalDictionary)
        {
            InternalCustomData = internalDictionary ?? throw new ArgumentNullException(nameof(internalDictionary));
        }

        public ErmisCustomDataRequest(IErmisCustomData customData) : this()
        {
            foreach (var key in customData.Keys)
            {
                Add(key, customData.Get<object>(key));
            }
        }

        public void Add(string key, object value) => InternalCustomData.Add(key, value);

        public IEnumerator GetEnumerator() => InternalCustomData.GetEnumerator();

        internal IDictionary<string, object> InternalCustomData { get; }

        internal Dictionary<string, object> ToDictionary() => InternalCustomData.ToDictionary(k => k.Key, k => k.Value);
    }
}