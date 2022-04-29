using JetBrains.Annotations;
using UnityEngine;
namespace System {
    public class SwitchScene : MonoBehaviour {
        [SerializeField] private int sceneIndex = -1;
        [CanBeNull]private SceneManager m_SceneManager;
        private void Awake() {
            var obj = GameObject.FindGameObjectWithTag("SceneManager");
            m_SceneManager = obj.GetComponent<SceneManager>();
        }
        private void OnTriggerEnter2D(Collider2D other) {
            if (m_SceneManager == null) {
                throw new NullReferenceException("SCENEMANAGER HAS TO BE ADDED TO THE SCENE");
            }
            if (sceneIndex <= -1) {
                throw new Exception("Yo, u kinda forgot to set a scene, which means the player won't progress");
            }
            if(!other.gameObject.CompareTag("Player")) return;
            m_SceneManager.ChangeScene(sceneIndex);
        }
    }
}
