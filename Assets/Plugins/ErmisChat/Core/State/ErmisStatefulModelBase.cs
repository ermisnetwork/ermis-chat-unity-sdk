﻿using System;
using System.Collections.Generic;
using Ermis.Core.LowLevelClient;
using Ermis.Core.State.Caches;
using Ermis.Libs.Logs;

namespace Ermis.Core.State
{
    /// <summary>
    /// Base class for <see cref="IErmisStatefulModel"/>
    /// </summary>
    /// <typeparam name="TStatefulModel">Type of tracked object</typeparam>
    internal abstract class ErmisStatefulModelBase<TStatefulModel> : IErmisStatefulModel
        where TStatefulModel : class, IErmisStatefulModel
    {
        public IErmisCustomData CustomData => _customData;

        string IErmisStatefulModel.UniqueId => InternalUniqueId;

        internal ErmisStatefulModelBase(string uniqueId, ICacheRepository<TStatefulModel> repository,
            IStatefulModelContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Client = context.Client ?? throw new ArgumentNullException(nameof(context.Client));
            Logs = context.Logs ?? throw new ArgumentNullException(nameof(context.Logs));
            Cache = context.Cache ?? throw new ArgumentNullException(nameof(context.Cache));
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));

            _customData = new ErmisCustomData(_additionalProperties, context.Serializer);

            InternalUniqueId = uniqueId;
            Repository.Track(Self);
        }
        
        //ErmisTodo: wrap completely the _additionalProperties in ErmisCustomData and not operate on both
        protected Dictionary<string, object> GetInternalAdditionalPropertiesDictionary() => _additionalProperties; 

        protected abstract string InternalUniqueId { get; set; }

        protected abstract TStatefulModel Self { get; }
        protected ErmisChatClient Client { get; }
        protected ErmisChatLowLevelClient LowLevelClient => Client.InternalLowLevelClient;
        protected ILogs Logs { get; }
        protected ICache Cache { get; }
        protected ICacheRepository<TStatefulModel> Repository { get; }

        protected void LoadAdditionalProperties(Dictionary<string, object> additionalProperties)
        {
            //ErmisTodo: investigate if there's a case we don't want to clear here
            //Without clear channel full update or partial update unset won't work because we'll ignore that WS sent channel without custom data
            
            //ErmisTodo: 2, wrap into _customData.Sync(additionalProperties); instead of having a collection here

            _additionalProperties.Clear();
            foreach (var keyValuePair in additionalProperties)
            {
                if (_additionalProperties.ContainsKey(keyValuePair.Key))
                {
                    _additionalProperties[keyValuePair.Key] = keyValuePair.Value;
                    continue;
                }

                _additionalProperties.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }

        protected static bool TrySet<T>(ref T storage, T value)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            return true;
        }

        protected static T GetOrDefault<T>(T? source, T defaultValue)
            where T : struct
            => source ?? defaultValue;

        protected static T? GetOrDefault<T>(T? source, T? defaultValue)
            where T : struct
            => source ?? defaultValue;

        protected static T GetOrDefault<T>(T source, T defaultValue)
            where T : class
            => source ?? defaultValue;

        private readonly ErmisCustomData _customData;
        private readonly Dictionary<string, object> _additionalProperties = new Dictionary<string, object>();
    }
}