﻿#if ERMIS_TESTS_ENABLED
using System;
using System.Collections;
using System.Globalization;
using System.Threading.Tasks;
using Ermis.Core;
using Ermis.Core.LowLevelClient;
using UnityEditor;
using UnityEngine;

namespace ErmisChat.Tests
{
    /// <summary>
    /// Utils for testing purposes
    /// </summary>
    internal static class UnityTestUtils
    {
        public static IEnumerator RunAsIEnumerator<TResponse>(this Task<TResponse> task,
            Action<TResponse> onSuccess = null, Action<Exception> onFaulted = null)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }

            if (task.IsFaulted)
            {
                var ex = UnwrapAggregateException(task.Exception);
                if (onFaulted != null)
                {
                    onFaulted(ex);
                    yield break;
                }

                throw ex;
            }

            onSuccess?.Invoke(task.Result);
        }

        public static IEnumerator RunAsIEnumerator(this Task task,
            Action onSuccess = null, Action<Exception> onFaulted = null)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }

            if (task.IsFaulted)
            {
                var ex = UnwrapAggregateException(task.Exception);
                if (onFaulted != null)
                {
                    onFaulted(ex);
                    yield break;
                }

                throw ex;
            }

            onSuccess?.Invoke();
        }

        public static IEnumerator RunAsIEnumerator(this Task task,
            IErmisChatLowLevelClient lowLevelClient, Action onSuccess = null, Action<Exception> onFaulted = null)
        {
            while (!task.IsCompleted)
            {
                lowLevelClient?.Update(0.2f);
                yield return null;
            }

            if (task.IsFaulted)
            {
                var ex = UnwrapAggregateException(task.Exception);
                if (onFaulted != null)
                {
                    onFaulted(ex);
                    yield break;
                }

                throw ex;
            }

            onSuccess?.Invoke();
        }

        public static IEnumerator WaitForClientToConnect(this IErmisChatLowLevelClient lowLevelClient)
        {
            if (lowLevelClient.ConnectionState == ConnectionState.Connected)
            {
                yield break;
            }

            const float maxTimeToConnect = 15f;
            var timeStarted = EditorApplication.timeSinceStartup;

            while (true)
            {
                var elapsed = EditorApplication.timeSinceStartup - timeStarted;

                if (elapsed > maxTimeToConnect)
                {
                    Debug.LogError($"Waiting for connection exceeded max time, elapsed: {elapsed}. Terminating");
                    break;
                }

                if (lowLevelClient.ConnectionState == ConnectionState.Connecting)
                {
                    yield return null;
                }

                if (lowLevelClient.ConnectionState == ConnectionState.Connected)
                {
                    break;
                }

                if (lowLevelClient.ConnectionState == ConnectionState.Disconnected)
                {
                    Debug.LogError("Client disconnected when waiting for connection. Terminating");
                    break;
                }
            }
        }

        public static IEnumerator RunTaskAsEnumerator(this Task task)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }

            if (!task.IsFaulted)
            {
                yield break;
            }

            if (task.Exception is AggregateException aggregateException &&
                aggregateException.InnerExceptions.Count == 1)
            {
                throw task.Exception.InnerException;
            }

            throw task.Exception;
        }

        public static string ToRfc3339String(this DateTime dateTime)
            => dateTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz", DateTimeFormatInfo.InvariantInfo);

        private static Exception UnwrapAggregateException(Exception exception)
        {
            if (exception is AggregateException aggregateException &&
                aggregateException.InnerExceptions.Count == 1)
            {
                return aggregateException.InnerExceptions[0];
            }

            return exception;
        }
    }
}
#endif