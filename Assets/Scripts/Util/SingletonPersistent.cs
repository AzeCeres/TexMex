using UnityEngine;
using UnityEngine.SceneManagement;

namespace OmniDi.Library.Util
{
    /// <summary>
    /// An abstract class for creating MonoBehaviour classes that follow the singleton design pattern and should persist over the lifetime of the game.
    /// </summary>
    /// <typeparam name="T">The type of the inheriting class</typeparam>
    public abstract class SingletonPersistent<T> : MonoBehaviour where T : Component
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

                var activeScene = SceneManager.GetActiveScene();
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("Persistent"));
                
                _instance = FindObjectOfType<T>();
                if (_instance != null)
                {
                    SceneManager.SetActiveScene(activeScene);
                    return _instance;
                }

                var gameObject = new GameObject
                {
                    name = typeof(T).Name
                };
                _instance = gameObject.AddComponent<T>();
                SceneManager.SetActiveScene(activeScene);
                
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