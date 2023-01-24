using System;
using System.Linq;
using OmniDi.Library.Util;
using OmniDi.Library.Wrappers;
using UnityEngine;
using UnityEngine.SceneManagement;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace OmniDi.Library.Controllers
{
    /// <summary>
    /// The class that handles the control over the currently open scenes.
    /// </summary>
    public class SceneController : SingletonPersistent<SceneController>
    {
        [Tooltip("The scenes that should never be set as active.")]
        [SerializeField] private SerializedScene[] inactiveScenes;
        [Tooltip("The scenes that should be opened as the game starts.")]
        [SerializeField] private SerializedScene[] openOnLoad;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Startup()
        {
            SceneManager.LoadSceneAsync("Scenes/Persistent", LoadSceneMode.Additive);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

            if (openOnLoad.Length < 1) return;
            foreach (var scene in openOnLoad)
            {
                OpenSceneAdditive(scene);
            }
        }


        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }


        /// <summary>
        /// Unloads the currently active scene and loads the new scene at the specified index.
        /// </summary>
        /// <param name="sceneIndex">The build index of the scene to load.</param>
        public static void ChangeScene(int sceneIndex)
        {
            ValidateScene(sceneIndex);
            
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        }

        /// <summary>
        /// Unloads the currently active scene and loads the new scene at the specified index.
        /// </summary>
        /// <param name="scene">The <see cref="SerializedScene"/> to load.</param>
        public static void ChangeScene(SerializedScene scene)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.LoadSceneAsync(scene.BuildIndex, LoadSceneMode.Additive);
        }

        /// <summary>
        /// Loads a new scene on top of the existing one(s).
        /// </summary>
        /// <param name="sceneIndex">The build index of the scene to load.</param>
        public static void OpenSceneAdditive(int sceneIndex)
        {
            ValidateScene(sceneIndex);
            
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        }

        /// <summary>
        /// Loads a new scene on top of the existing one(s).
        /// </summary>
        /// <param name="scene">The <see cref="SerializedScene"/> to load.</param>
        public static void OpenSceneAdditive(SerializedScene scene)
        {
            SceneManager.LoadSceneAsync(scene.BuildIndex, LoadSceneMode.Additive);
        }

        /// <summary>
        /// Unloads an additively loaded scene at the specified index.
        /// </summary>
        /// <param name="sceneIndex">The build index of the scene to unload.</param>
        public static void CloseAdditiveScene(int sceneIndex)
        {
            ValidateScene(sceneIndex);
            
            var previousScene = SceneManager.GetSceneAt(1);
            SceneManager.SetActiveScene(previousScene);

            SceneManager.UnloadSceneAsync(sceneIndex);
        }

        /// <summary>
        /// Unloads an additively loaded scene at the specified index.
        /// </summary>
        /// <param name="scene">The <see cref="SerializedScene"/> to load.</param>
        public static void CloseAdditiveScene(SerializedScene scene)
        {
            var previousScene = SceneManager.GetSceneAt(1);
            SceneManager.SetActiveScene(previousScene);

            SceneManager.UnloadSceneAsync(scene.BuildIndex);
        }

        /// <summary>
        /// Closes the game.
        /// </summary>
        public static void QuitApplication()
        {
            Application.Quit();
        }

        /// <summary>
        /// Validates that the scene about to be loaded is within the scene build index.
        /// </summary>
        /// <param name="sceneIndex">The scene to validate.</param>
        /// <exception cref="ArgumentException">Is thrown if the scene that is about to be loaded is not within the bounds of the scene build index.</exception>
        private static void ValidateScene(int sceneIndex)
        {
            if (sceneIndex > SceneManager.sceneCountInBuildSettings)
            {
                throw new ArgumentException($"The scene index ({sceneIndex}) is higher than the amount of scenes in the build index ({SceneManager.sceneCountInBuildSettings}");
            }
            if (sceneIndex < 0)
            {
                throw new ArgumentException($"The scene index ({sceneIndex}) is invalid, expected a value between 0 and {SceneManager.sceneCountInBuildSettings}");
            }
        }


        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (inactiveScenes.Any(inactiveScene => inactiveScene.Scene == scene)) return;

            SceneManager.SetActiveScene(scene);
        }
    }
}