using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace OmniDi.Library.Wrappers
{
    [Serializable]
    public struct SerializedScene
    {
        [SerializeField] private int scene;

        public Scene Scene => SceneManager.GetSceneByBuildIndex(scene);
        public string Name => SceneManager.GetSceneByBuildIndex(scene).name;
        public int BuildIndex => scene;

        private SerializedScene(Scene buildScene)
        {
            scene = buildScene.buildIndex;
        }

        /// <summary>
        /// Returns a new Result with the SerializedScene based on the build index of a scene
        /// </summary>
        /// <param name="sceneIndex">The index of the scene to create a new SerializedScene from</param>
        /// <returns>Returns a Result with the SerializedScene if it's in the build index, otherwise a Result with an error</returns>
        public static Result<SerializedScene> New(int sceneIndex)
        {
            var sceneInBuildIndex = sceneIndex > 0 && sceneIndex < SceneManager.sceneCountInBuildSettings;
            return sceneInBuildIndex
                ? Result<SerializedScene>.Ok(new SerializedScene(SceneManager.GetSceneByBuildIndex(sceneIndex)))
                : Result<SerializedScene>.Err("Could not find the scene in the build index");
        }

        /// <summary>
        /// Returns a new Result with the SerializedScene based on the name of a scene
        /// </summary>
        /// <param name="sceneIndex">The index of the scene to create a new SerializedScene from</param>
        /// <returns>Returns a Result with the SerializedScene if it's in the build index, otherwise a Result with an error</returns>
        public static Result<SerializedScene> New(string sceneName)
        {
            var buildSettingScenePaths = EditorBuildSettings.scenes.Select(scene => scene.path);
            var sceneByName = SceneManager.GetSceneByName(sceneName);
            var sceneInBuildIndex = buildSettingScenePaths.Contains(sceneByName.path);

            return sceneInBuildIndex
                ? Result<SerializedScene>.Ok(new SerializedScene(sceneByName))
                : Result<SerializedScene>.Err("Could not find the scene in the build index");
        }

        /// <summary>
        /// Returns a new Result with the SerializedScene based on a scene
        /// </summary>
        /// <param name="sceneIndex">The index of the scene to create a new SerializedScene from</param>
        /// <returns>Returns a Result with the SerializedScene if it's in the build index, otherwise a Result with an error</returns>
        public static Result<SerializedScene> New(Scene unityScene)
        {
            var buildSettingScenePaths = EditorBuildSettings.scenes.Select(scene => scene.path);
            var sceneInBuildIndex = buildSettingScenePaths.Contains(unityScene.path);

            return sceneInBuildIndex
                ? Result<SerializedScene>.Ok(new SerializedScene(unityScene))
                : Result<SerializedScene>.Err("Could not find the scene in the build index");
        }
    }
}