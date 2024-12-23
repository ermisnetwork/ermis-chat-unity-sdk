﻿#if ERMIS_TESTS_ENABLED
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using UnityEngine;

namespace ErmisChat.Tests.LowLevelClient
{
    internal class DTOTests
    {
        /// <summary>
        /// Ensure that DTOs do not use interfaces that would break JSON deserialization with IL2CPP
        /// </summary>
        [Test]
        public void DTOs_do_not_use_interfaces()
        {
            const string ErmisChatCoreAssemblyName = "Ermis.Core";
            const string DTOKeyword = "DTO";
            const string IgnoredNamespace = "AutoGeneratedClientReferenceOnlyDoNotUseInProduction";

            var ermisChatCoreAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .Single(_ => _.GetName().Name == ErmisChatCoreAssemblyName);

            var dtoTypes = new List<Type>();

            foreach (var type in ermisChatCoreAssembly.GetTypes())
            {
                if (type.Namespace != null && type.Namespace.IndexOf(DTOKeyword, StringComparison.InvariantCulture) != -1 &&
                    type.Namespace.IndexOf(IgnoredNamespace, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    dtoTypes.Add(type);
                }
            }

            var errorSb = new StringBuilder();

            foreach (var type in dtoTypes)
            {
                var properties = type.GetProperties();

                foreach (var p in properties)
                {
                    var propertyType = p.PropertyType;

                    if (propertyType.IsInterface)
                    {
                        var error =
                            $"DTO of type: `{type}` contains interface type property: `{p.Name}` of type `{propertyType}`";
                        errorSb.AppendLine(error);
                    }
                }
            }

            if (errorSb.Length > 0)
            {
                Debug.LogError(errorSb.ToString());
            }

            Assert.AreEqual(0, errorSb.Length);
        }
    }
}
#endif