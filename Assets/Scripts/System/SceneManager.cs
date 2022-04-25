using System.Collections.Generic;
using Player;
using Puzzle;
using UnityEngine;
namespace System
{
    public class SceneManager : MonoBehaviour
    {
        //todo Check if all clones are dead - Reset all Buttons
        private GameObject startPos;
        private Split m_Split;
        private List<Button> buttons = new List<Button>();
        private void Start() {
            var obj = GameObject.FindGameObjectWithTag("PlayerController");
            m_Split = obj.GetComponent<Split>();
            startPos = obj; 
            GetAllButtons();
        }
        void GetAllButtons() {
            GameObject[] gameObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (var go in gameObjects) { 
                if (go.CompareTag("Button")) {     
                    buttons.Add(go.GetComponent<Button>());
                }
            }
        }
        private void Update() {
            CheckPlayer();
        }
        void CheckPlayer()
        {
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
            startPos.transform.position = coordinates;
        }
        private void Reset() {
            for (int i = 0; i < buttons.Count; i++) {
                buttons[i].active = false;
            }
            m_Split.SpawnClone(0, m_Split.mainClones, startPos);
        }
    }
}
