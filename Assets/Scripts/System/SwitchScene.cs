using UnityEngine;
namespace System {
    public class SwitchScene : MonoBehaviour {
        [SerializeField] private int sceneIndex = -1;
        private SceneManager m_SceneManager;
        private void Awake() {
            var obj = GameObject.FindGameObjectWithTag("SceneManager");
            m_SceneManager = obj.GetComponent<SceneManager>();
        }
        private void OnTriggerEnter2D(Collider2D other) {
            if (sceneIndex <= -1) {
                throw new Exception("Yo, u kinda forgot to set a scene, which means the player won't progress");
            }
            m_SceneManager.ChangeScene(sceneIndex);
        }
        public void SceneSwitch(int sceneIndex) {
            m_SceneManager.ChangeScene(sceneIndex);
        }
    }
}
