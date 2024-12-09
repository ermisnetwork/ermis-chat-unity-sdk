﻿using UnityEngine;

namespace Ermis.Libs.Auth
{
    /// <summary>
    /// Asset to keep auth credentials
    /// </summary>
    [CreateAssetMenu(fileName = "AuthCredentials", menuName = "Plugins/Ermis/Config/Create auth credentials asset", order = 1)]
    public class AuthCredentialsAsset : ScriptableObject
    {
        public AuthCredentials Credentials => new AuthCredentials(_apiKey, _testUserId, _testUserToken);

        public void SetData(AuthCredentials authCredentials)
        {
            _apiKey = authCredentials.ApiKey;
            _testUserId = authCredentials.UserId;
            _testUserToken = authCredentials.UserToken;
        }

        [Header("Your `Api Key`")]
        [Tooltip("You can find it in Ermis Dashboard")]
        [SerializeField]
        private string _apiKey;

        [SerializeField]
        private string _testUserId;

        [SerializeField]
        private string _testUserToken;

    }
}