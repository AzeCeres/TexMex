using System;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class Split : MonoBehaviour 
    {
    
        public GameObject[] clones;
        public bool[] activeClones = new bool[4];

        //todo remove serialize field after troubleshooting
        [SerializeField] public List<GameObject> mainClones = new List<GameObject>(2);
        [SerializeField] public List<GameObject> secondClones = new List<GameObject>(2);
        [SerializeField] public int selectedMain;
        [SerializeField] public int selectedSecond = 1;

        private void Update() {
            OopsProtection();
        }
        public void MainSplit() {
            if (mainClones.Count == 2 && secondClones.Count > 0) {
                SwitchMain();
            }
            //todo make it so you can switch clone even if both seconds are dead? or switch one of them over to be a second instead
            //if only 1 active clone, add second, become SelectedSecond (run SecondSplit?) and return;
            else if (mainClones.Count >= 1 && secondClones.Count == 0) {
                SpawnClone(selectedSecond, secondClones, mainClones[selectedMain]);
                SwitchSecond();
            }
            //if has SelectedSecond, become mainClone, transfer SelectedSecond
            else if (mainClones.Count == 1 && secondClones.Count > 0) {
                SpawnClone(selectedMain, mainClones, mainClones[selectedMain]);
                SwitchMain();
            }
        }
        public void SecondSplit() {
            if (secondClones.Count == 2 && mainClones.Count > 0) {
                SwitchSecond();
            }
            //if no second, make second from main
            else if (secondClones.Count >= 1 && mainClones.Count == 0) {
                SpawnClone(selectedMain, mainClones, secondClones[selectedSecond]);
                SwitchMain();
            }
            //if has SelectedSecond, become mainClone, transfer SelectedSecond
            else if (secondClones.Count == 1 && mainClones.Count > 0) {
                SpawnClone(selectedSecond, secondClones, secondClones[selectedSecond]);
                SwitchSecond();
            } 
            //if both, nothing happens
        }

        public void SpawnClone(int selected, List<GameObject> cloneGroup, GameObject sourceClone ) {
        
            for (int i = 0; i < activeClones.Length; i++)
            {
                if (activeClones[i])
                    continue;
                //todo move them in front of the player, making sure theres space there.
                activeClones[i] = true;
                clones[i].transform.position = sourceClone.transform.position; 
                clones[i].SetActive(true);
                cloneGroup.Add(clones[i]);
                return;
            }
        }
        private void SwitchMain() => selectedMain = selectedMain == 0 ? 1 : 0;
        private void SwitchSecond() => selectedSecond = selectedSecond == 0 ? 1 : 0;

        private void OopsProtection() {
            if (secondClones.Count == 1) {
                selectedSecond = 0;
            }
            if (mainClones.Count == 1) {
                selectedMain = 0;
            }
        }
        //todo; function for killing clone, changing who's selected

        public void KillClone(GameObject cloneToKill) {
            for (int i = 0; i < clones.Length; i++)
            {
                if (clones[i] != cloneToKill)
                    continue;
                clones[i].SetActive(false);
                activeClones[i] = false;
                for (int j = 0; j < mainClones.Count; j++) {

                    if (mainClones[j] == cloneToKill) {
                        mainClones.Remove(mainClones[j]);
                    }
                }
                for (int j = 0; j < secondClones.Count; j++) {

                    if (secondClones[j] == cloneToKill)
                    {
                        secondClones.Remove(secondClones[j]);
                    }
                }
            }
        }
    }
}
