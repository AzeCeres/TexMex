using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace System {
    public class SwitchScene : MonoBehaviour {
        [SerializeField] private int sceneIndex = -1;
        [SerializeField] private Image fadeImage;
        [SerializeField] private Color fadeColor;
        
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

            StartCoroutine(StartSceneTransition());
        }

        IEnumerator StartSceneTransition()
        {
            yield return StartCoroutine(FadeToColor());
            m_SceneManager.ChangeScene(sceneIndex);
        }
        
        IEnumerator FadeToColor()
        {
            fadeImage.color = fadeColor;
            
            Color color = fadeImage.color;
            for (float alpha = 0f; alpha < 1; alpha += 0.1f)
            {
                color.a = alpha;
                fadeImage.color = color;
                
                yield return new WaitForSeconds(.1f);
            }
        }
    }
}