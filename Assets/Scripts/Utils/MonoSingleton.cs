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
                if (_instance == null) Debug.Log(typeof(T) + " is NULL.");
                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this as T;
            Init();
        }

        /// <summary>
        /// Called during Awake function
        /// </summary>
        protected virtual void Init() { }

    }
}
