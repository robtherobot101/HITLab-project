using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Utils
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    throw new UninstantiatedException($"The singleton {typeof(T)} has" +
                                                      " not been instantiated. Make sure it is" +
                                                      " attached to a GameObject.");
                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this as T;
            Init();
        }

        /// <summary>
        ///     Called during Awake function
        /// </summary>
        protected virtual void Init()
        {
        }
    }

    [Serializable]
    public class UninstantiatedException : Exception
    {
        public UninstantiatedException()
        {
        }

        public UninstantiatedException(string message) : base(message)
        {
        }

        public UninstantiatedException(string message, Exception inner) : base(message, inner)
        {
        }

        protected UninstantiatedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}