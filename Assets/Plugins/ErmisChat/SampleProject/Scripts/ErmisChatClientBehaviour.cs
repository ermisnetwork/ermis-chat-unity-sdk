#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using Ermis.Core;
using Ermis.Core.Configs;
using Ermis.Core.Exceptions;
using Ermis.Libs.Auth;
using ErmisChat.SampleProject.Configs;
using ErmisChat.SampleProject.Inputs;
using ErmisChat.SampleProject.Utils;
using ErmisChat.SampleProject.Views;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;
using System.Threading.Tasks;
using Ermis.Core.InternalDTO.Responses;
using Newtonsoft.Json;

namespace ErmisChat.SampleProject
{
    /// <summary>
    /// Ermis Chat Client MonoBehaviour
    /// </summary>
    public class ErmisChatClientBehaviour : MonoBehaviour
    {

        [SerializeField]
        private string _apiKey;

        [SerializeField]
        private string _tokenHeader;

        [SerializeField]
        private string _projectId;

        [SerializeField]
        private bool _isAuth;


        [SerializeField]
        private string _address;

        private TokenResponeDto _tokenResponeDto;

        protected void Awake()
        {

        }

        private IErmisChatClient _client;

        [SerializeField]
        private RootView _rootView;

        [SerializeField]
        private AuthCredentialsAsset _authCredentialsAsset;

        [FormerlySerializedAs("appConfig")]
        [SerializeField]
        private AppConfig _appConfig;

        [SerializeField]
        private Transform _popupsContainer;

#if UNITY_EDITOR
        private IEnumerator BlinkProjectAsset(Object target, Object owner)
        {
            EditorUtility.FocusProjectWindow();

            while (owner != null)
            {
                EditorGUIUtility.PingObject(target);

                yield return new WaitForSeconds(1);
            }
        }
#endif

        private void Start()
        {
            GetUserToken(Connect);
        }


        private async Task GetUserToken(Action callback)
        {
            var tokenProvider = ErmisChatClient.CreateDefaultTokenProvider(TokenUriHandle);

            if (_isAuth)
            {
                var _userToken = await tokenProvider.GetTokenAsync(_apiKey, _tokenHeader);
                _tokenResponeDto = JsonConvert.DeserializeObject<TokenResponeDto>(_userToken);
                Debug.Log(_userToken);
                callback?.Invoke();
            }
            else
            {
                var _userToken = await tokenProvider.GetTokenAsync(_apiKey, "");
                _tokenResponeDto = JsonConvert.DeserializeObject<TokenResponeDto>(_userToken);
                Debug.Log(_userToken);
                callback?.Invoke();
            }

        }

        private Uri TokenUriHandle(string apiKey)
        {

            if (_isAuth)
            {
                Uri uri = new Uri($"https://api-dev.ermis.network/uss/v1/get_token/external_auth?apikey={apiKey}");
                return uri;
            }
            else
            {
                Uri uri = new Uri($"https://api-dev.ermis.network/uss/v1/get_token?apikey={apiKey}&address={_address}");
                return uri;
            }
        }    

        private void Connect()
        {
            var inputSystemFactory = new InputSystemFactory();
            var defaultInputSystem = inputSystemFactory.CreateDefault();

            var viewFactory = new ViewFactory(_appConfig, _popupsContainer);

            TrySetEmojisSpriteAtlas();

            try
            {
                var config = new ErmisClientConfig
                {
#if ERMIS_DEBUG_ENABLED
                    LogLevel = ErmisLogLevel.Debug
#endif
                };
                _client = ErmisChatClient.CreateDefaultClient(config);
                AuthCredentials authCredentials = new AuthCredentials(_apiKey, _projectId, _tokenResponeDto.Token);
                _client.ConnectUserAsync(authCredentials);

                var viewContext = new ChatViewContext(_client, new UnityImageWebLoader(), viewFactory,
                    defaultInputSystem, _appConfig);

                viewFactory.Init(viewContext);
                _rootView.Init(viewContext);
            }
            catch (ErmisMissingAuthCredentialsException e)
            {
                Debug.LogError(e.Message);
                var popup = viewFactory.CreateFullscreenPopup<ErrorPopup>();
                popup.SetData("Invalid Authorization Credentials",
                                        $"Please provide valid authorization data into `{_authCredentialsAsset.name}` asset. " +
                                        $"Register Ermis Account and visit <b>Dashboard</b> to get your `API_KEY` and use <b>chat explorer</b> to create your first user. " +
                                        $"You can then click here -> <link=\"TokenGenerator\"><u>Tokens & Authorization</u></link> to visit online auth token generator.",
                                        new Dictionary<string, string>()
                                        {
                                    {
                                        "TokenGenerator",
                                        ""
                                    }
                                        });

#if UNITY_EDITOR

                StartCoroutine(BlinkProjectAsset(_authCredentialsAsset, popup));

#endif
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        private void TrySetEmojisSpriteAtlas()
        {
            var spriteAsset = _appConfig?.Emojis?.TMPSpriteAsset;
            if (spriteAsset != null)
            {
                if (TMP_Settings.defaultSpriteAsset == spriteAsset)
                {
                }
                else if (TMP_Settings.defaultSpriteAsset != null)
                {
                    var fallbackSpriteAssets = TMP_Settings.defaultSpriteAsset.fallbackSpriteAssets;

                    if (!fallbackSpriteAssets.Contains(spriteAsset))
                    {
                        fallbackSpriteAssets.Add(spriteAsset);
                    }

                    Debug.LogWarning(
                        $"`{spriteAsset.name}` sprite asset was added as a fallback to the default `{TMP_Settings.defaultSpriteAsset}`");
                }
                else
                {
                    Debug.LogError(
                        $"TMP_Settings Default sprite is not set. Emojis sprite will not be properly replaced. " +
                        $"Please either set the `{spriteAsset.name}` as a default sprite asset or set any default asset so that `{spriteAsset.name}` gets appended as a fallback");
                }
            }
        }
    }
}