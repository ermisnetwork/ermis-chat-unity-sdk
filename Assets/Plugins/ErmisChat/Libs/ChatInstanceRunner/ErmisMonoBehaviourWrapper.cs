using System;
using System.Collections;
using UnityEngine;

namespace Ermis.Libs.ChatInstanceRunner
{
    /// <summary>
    /// Wrapper to hide the <see cref="UnityErmisChatClientRunner"/> from Unity's inspector dropdowns and Unity search functions like Object.FindObjectsOfType<MonoBehaviour>(); 
    /// </summary>
    public sealed class ErmisMonoBehaviourWrapper
    {
        /// <summary>
        /// This is a MonoBehaviour wrapper that will pass Unity Engine callbacks to the Ermis Chat Client
        /// </summary>
        public sealed class UnityErmisChatClientRunner : MonoBehaviour, IErmisChatClientRunner
        {
            public void RunChatInstance(IErmisChatClientEventsListener ermisChatInstance)
            {
                if (!Application.isPlaying)
                {
                    Debug.LogWarning($"Application is not playing. The MonoBehaviour {nameof(UnityErmisChatClientRunner)} wrapper will not execute." +
                              $" You need to call Ermis Chat Client's {nameof(IErmisChatClientEventsListener.Update)} and {nameof(IErmisChatClientEventsListener.Destroy)} by yourself");
                    DestroyImmediate(gameObject);
                    return;
                }
                
                _ermisChatInstance = ermisChatInstance ?? throw new ArgumentNullException(nameof(ermisChatInstance));
                _ermisChatInstance.Disposed += OnErmisChatInstanceDisposed;
                StartCoroutine(UpdateCoroutine());
            }

            private IErmisChatClientEventsListener _ermisChatInstance;
            
            // Called by Unity
            private void Awake()
            {
                DontDestroyOnLoad(gameObject);
            }

            // Called by Unity
            private void OnDestroy()
            {
                if (_ermisChatInstance == null)
                {
                    return;
                }

                _ermisChatInstance.Disposed -= OnErmisChatInstanceDisposed;
                StopCoroutine(UpdateCoroutine());
                _ermisChatInstance.Destroy();
                _ermisChatInstance = null;
            }

            private IEnumerator UpdateCoroutine()
            {
                while (_ermisChatInstance != null)
                {
                    _ermisChatInstance.Update();
                    yield return null;
                }
            }

            private void OnErmisChatInstanceDisposed()
            {
                if (_ermisChatInstance == null)
                {
                    return;
                }

                _ermisChatInstance.Disposed -= OnErmisChatInstanceDisposed;
                _ermisChatInstance = null;
                StopCoroutine(UpdateCoroutine());

#if ERMIS_DEBUG_ENABLED
                Debug.Log($"Ermis Chat Client Disposed - destroy {nameof(UnityErmisChatClientRunner)} instance");
#endif
                Destroy(gameObject);
            }

        }
    }
}