using System.Collections.Generic;
using UnityEngine;
namespace Player
{ public class Split : MonoBehaviour {
    
        //[HideInInspector] 
        public GameObject[] clones;
        //[HideInInspector]
        public bool[] activeClones = new bool[4];

        //todo remove serialize field after troubleshooting
        //[HideInInspector]
        [SerializeField] public List<GameObject> mainClones = new List<GameObject>(4);
        [HideInInspector][SerializeField] public List<GameObject> secondClones = new List<GameObject>(2);
        [HideInInspector][SerializeField] public int selectedMain;
        [HideInInspector][SerializeField] public int selectedSecond = 1;
        private string[] test = new string[1];
        //in settings, switch alternative controls over to true to activate single stick controls 
        public bool alternativeControls = true;
        [Range(1,4)][SerializeField] private int maxClones = 4;
        private int previousSelectedMain = 0;
        
        private void Update() {
            OopsProtection();
            AlternativeControlsCheck();
        }
        //todo put in max clones, that is possible per stage. Done for alternative controls
        public void MainSplit() {
            if (mainClones.Count == 2 && secondClones.Count > 0) {
                SwitchMain();
            }
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
            for (int i = 0; i < activeClones.Length; i++) {
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
            if (secondClones.Count-1 < selectedSecond) {
                selectedSecond = secondClones.Count-1;
            } if (mainClones.Count-1 < selectedMain) {
                selectedMain = mainClones.Count-1;
            }
        }
        void AlternativeControlsCheck() {
            switch (alternativeControls) {
                case true: {
                    for (int i = 0; i < secondClones.Count; i++) {
                        mainClones.Add(secondClones[i]);
                        secondClones.Remove(secondClones[i]);
                    }
                    break;
                } case false: {
                    for (int i = 0; i < mainClones.Count; i++) {
                        if (mainClones.Count >= 3) {
                            secondClones.Add(mainClones[i]);
                            mainClones.Remove(mainClones[i]);
                        }
                    }
                    break;
                }
            }
        }
        public void AlternativeSplit() {
            if (mainClones.Count < maxClones) {
                SpawnClone(selectedMain, mainClones, mainClones[selectedMain]);
                AlternativeSwitch(-10);
            } else {
                //print("There are no more clones to be spawned");
            }
        }
        public void AlternativeSwitch(int switchValue) { 
            previousSelectedMain = selectedMain;
            if (switchValue + selectedMain > mainClones.Count-1) {
                selectedMain = 0;
            } else if (switchValue + selectedMain < 0) {
                selectedMain = mainClones.Count-1;
            } else {
                selectedMain += switchValue;
            }
        }
        public void KillClone(GameObject cloneToKill) {
            for (int i = 0; i < clones.Length; i++) {
                if (clones[i] != cloneToKill)
                    continue;
                for (int j = 0; j < mainClones.Count; j++) {
                    if (mainClones[j] == cloneToKill) {
                        mainClones.Remove(mainClones[j]); //todo//send death to animator, make the character uncontrollable during//Start animation , then have the animation call this script
                        if (alternativeControls && previousSelectedMain <= mainClones.Count - 1) {
                            selectedMain = previousSelectedMain;
                        }
                    }
                }
                for (int j = 0; j < secondClones.Count; j++) {
                    if (secondClones[j] == cloneToKill) {
                        secondClones.Remove(secondClones[j]);
                    }
                }
            }
        }
        public void DeActivateClone(GameObject cloneToKill) {
            for (int i = 0; i < clones.Length; i++) {
                if (clones[i] != cloneToKill)
                    continue;
                clones[i].SetActive(false);
                activeClones[i] = false;
            }
        }
    }
}
