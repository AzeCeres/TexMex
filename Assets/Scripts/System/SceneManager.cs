using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using Player;
using Puzzle.Pots;
using UnityEngine;
using UnityEngine.UI;
using Button = Puzzle.Button;

namespace System { 
    public class SceneManager : MonoBehaviour {
        private GameObject m_StartPos;
        private Split m_Split;
        private List<Button> buttons = new List<Button>();
        [CanBeNull]private List<PotBreakingScript> pots = new List<PotBreakingScript>();
        [CanBeNull]private List<PlantSkullBreaking> skullPots = new List<PlantSkullBreaking>();
        [SerializeField] [CanBeNull] private List<Button> excludedButtons = new List<Button>();
        [SerializeField] private Image fadeImage;
        private bool hasWarned = false;
        private void Start() {
            var obj = GameObject.FindGameObjectWithTag("PlayerController");
            m_Split = obj.GetComponent<Split>();
            m_StartPos = obj; 
            GetAllScripts();
            
            if (fadeImage != null) {
                StartCoroutine(FadeFromColor());
            }
        }
        private void GetAllScripts() {
            GameObject[] gameObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (var go in gameObjects) { 
                if (go.CompareTag("Button")) {     
                    buttons.Add(go.GetComponent<Button>());
                } if (go.CompareTag("Pot")) {
                    pots.Add(go.GetComponent<PotBreakingScript>());
                } if (go.CompareTag("SkullPlant")) {
                    skullPots.Add(go.GetComponent<PlantSkullBreaking>());
                }
            }
            if (excludedButtons == null) return;
            RemoveExcluded();
        }
        private void RemoveExcluded() {
            List<int> excludedArray = new List<int>();
            foreach (var exButt in excludedButtons) {
                for (var i = 0; i < buttons.Count; i++) {
                    var butt = buttons[i];
                    if (butt == exButt) {
                        excludedArray.Add(i);
                    }
                }
            }
            for (int i = excludedArray.Count; i-- > 0; ) {
                print(i);
                buttons.Remove(buttons[i]);
            }
        }
        private void Update() {
            if (m_Split != null) 
                CheckPlayer();
            else {
                if (hasWarned) return;
                hasWarned = true;
                throw new WarningException("PlayerController might be missing or not have the right tag!");
            }
        }
        void CheckPlayer() {
            var count = 0;
            for (int i = 0; i < m_Split.activeClones.Length; i++) {
                if (!m_Split.activeClones[i]) {
                    count++;
                } if (count == m_Split.activeClones.Length) {
                    Reset();
                }
            }
        }
        public void CreateCheckpoint(Vector2 coordinates) {
            m_StartPos.transform.position = coordinates;
        }
        private void Reset() {
            for (int i = 0; i < buttons.Count; i++) {
                buttons[i].Reset();
            }
            if (pots != null) {
                for (int i = 0; i < pots.Count; i++)
                {
                    pots[i].Reset();
                }
            }
            if (skullPots != null)
            {
                for (int i = 0; i < skullPots.Count; i++)
                {
                    skullPots[i].Reset();   
                }
            }
            m_Split.SpawnClone(0, m_Split.mainClones, m_StartPos);
        }
        public void QuitApplication() {
            UnityEngine.Application.Quit();
        }
        public void QuitToMainMenu() {
            ChangeScene(0);
        }
        public void ChangeScene(int sceneIndex) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
        }
        
        IEnumerator FadeFromColor() {
            Color color = fadeImage.color;
            for (float alpha = 1f; alpha > -1f; alpha -= 0.1f)
            {
                color.a = alpha;
                fadeImage.color = color;

                yield return new WaitForSeconds(.1f);
            }
        }
    }
}