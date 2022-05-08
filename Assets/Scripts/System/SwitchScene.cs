using System.Collections;
using Audio;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace System {
    [RequireComponent(typeof(AudioSource))]
    public class SwitchScene : MonoBehaviour {
        [SerializeField] private int sceneIndex = -1;
        
        [SerializeField] private Image fadeImage;
        [SerializeField] private Color fadeColor;
        [SerializeField] [Range(0f, 0.1f)] private float fadeStep = 0.1f;
        [SerializeField] private bool finalLevel;
        private MusicController _musicController;

        [CanBeNull]private SceneManager m_SceneManager;

        private void Awake() {
            var sceneManager = GameObject.FindGameObjectWithTag("SceneManager");
            m_SceneManager = sceneManager.GetComponent<SceneManager>();
            
            var mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            _musicController = mainCamera.GetComponentInChildren<MusicController>();
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

        private IEnumerator StartSceneTransition()
        {
            if (finalLevel)
            {
                _musicController.FadeMusic();
                yield return StartCoroutine(FadeToColor());

                for (int i = 0; i < 20; i++)
                {
                    yield return new WaitForSeconds(1f);
                }
            }
            
            m_SceneManager.ChangeScene(sceneIndex);
        }
        
        private IEnumerator FadeToColor()
        {
            fadeImage.color = fadeColor;
            
            Color color = fadeImage.color;
            for (float alpha = 0f; alpha < 1; alpha += fadeStep)
            {
                color.a = alpha;
                fadeImage.color = color;
                
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}