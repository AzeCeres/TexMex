using UnityEngine;

namespace OmniDi.Library.Util
{
    /// <summary>
    /// An abstract class for creating MonoBehaviour classes that follow the singleton design pattern.
    /// </summary>
    /// <typeparam name="T">The type of the inheriting class</typeparam>
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        /// <summary>
        /// Returns an existing instance of the singleton.
        /// If no instance exists get one from an existing GameObject.
        /// If no GameObjects of the type exists, create one and return that.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                
                var singletonObject = FindObjectOfType<T>();
                if (singletonObject != null)
                {
                    _instance = singletonObject;
                    return _instance;
                }
                
                GameObject gameObject = new GameObject();
                gameObject.name = typeof(T).Name;
                _instance = gameObject.AddComponent<T>();
                return _instance;
            }
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
}