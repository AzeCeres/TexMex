using System;
using System.Collections.Generic;
using UnityEngine;
namespace Player { 
    [RequireComponent(typeof(PlayerAudio))]
    [RequireComponent(typeof(PlayerController))]
    public class Split : MonoBehaviour {
        [Tooltip("All the clone gameObjects, the master list of the clones, used to check up against")]
        public GameObject[] clones{ get; private set; }
        [Tooltip("The layerChanger has to be in the same order as the Clone Array, Makes sure the clones are on different layers when they are spawned in")]
        [SerializeField] private PlayerLayerChanger[] layerChanger;
        [Tooltip("Bool version of the Clones, Indicates if they are alive or not")]
        public bool[] activeClones{ get; private set; } = new bool[4];
        [Tooltip("List of clones in the order they were spawned, not to be changed, fixed automatically")]
        public List<GameObject> mainClones{ get; private set; } = new List<GameObject>(4); 
        public List<GameObject> secondClones{ get; private set; } = new List<GameObject>(2);
        [Tooltip("The index of which mainClone is currently in use, not to be changed in editor")]
        public int selectedMain { get; private set; }
        [Tooltip("The index of which secondClone is currently in use, not to be changed in editor")]
        public int selectedSecond { get; private set; }
        // [Tooltip("Grandfathered option from when we were transitioning from a twin-stick system, Should always be on Until properly removed")]
        // public bool alternativeControls = true;
        [Tooltip("The amount of clones that can be active at once in the scene")]
        [Range(1,3)][SerializeField] private int maxClones = 3;
        [Tooltip("The index of the clone that was active last, used when a clone dies to go back to the same clone the player last used")]
        private int _previouslySelectedMain = 0;

        //Audio
        private PlayerAudio _playerAudio;
        
        //Tutorial Bools, Only used in the first level to determine if the player has done said action yet
        [HideInInspector] public bool hasSplit=false, hasSwitched=false, hasDied=false;

        private void Awake() {
            _playerAudio = GetComponent<PlayerAudio>();
        }

        private void Update() {
            //A collection of checks that should prevent edgeCases from happening, Such as not allowing the index to be higher than the list lenght
            //and also prevents a weird occurence where a clone doesn't fully die.
            OopsProtection();
            //A grandfathered function that should transition to and from alternative controls, going as far as to rearrange the lists to make hotSwitching possible
            // AlternativeControlsCheck();
        }


        private void OopsProtection() {
            //Checks if secondClones index is outside of the list, and rights it if it is.
            if (secondClones.Count-1 < selectedSecond) {
                selectedSecond = secondClones.Count-1;
            }
            //Checks if mainClones index is outside of the list, and rights it if it is.
            if (mainClones.Count-1 < selectedMain) {
                selectedMain = mainClones.Count-1;
            } 
            //for an edgeCase that left the index at -1, here it makes sure that the index when theres only one clone is always 0
            if (mainClones.Count == 1)
                 selectedMain = 0;
            //for an edgeCase that doesn't fully kill the player, and the clone is stuck disabled but still remains in the MainClones list. Here it finds and removes them.
            for (int i = 0; i < activeClones.Length; i++) {
                if (activeClones[i]) continue;
                for (int j = 0; j < mainClones.Count; j++) {
                    if (clones[i] == mainClones[j]) {
                        mainClones.Remove(mainClones[j]);
                    }
                }
            }
        }
        //Called from PlayerController, Spawns a new clone and changes as to make the index the same as that of the new clone
        public void AlternativeSplit() {
            hasSplit = true; // tutorial purposes
            _playerAudio.PlayCloneCreateAudio();
            if (mainClones.Count < maxClones) {
                SpawnClone(selectedMain, mainClones, mainClones[selectedMain]); //spawns a clone, Doesn't need the index any longer, But needs the list to add the new clone in,
                //and the game object to get the position of the clone it wants to spawn at
                AlternativeSwitch(Int32.MinValue); //switches so far back the alternativeSwitch function chooses to go to the very end of the list,
                                                   //ensuring it is always the newest clone that gets chosen
                hasSwitched = false; // To avoid it removing the second tutorial in Level 1. Does nothing else.
            } else {
                //print("There are no more clones to be spawned");
            }
        }
        //Called from PlayerController, Switches to the next clone in the list. Can go  through the list in both directions, Having checks to keep the index inside the list lenght
        public void AlternativeSwitch(int switchValue) {
            hasSwitched = true; // for Tutorial purposes
            _playerAudio.PlayCloneSwitchAudio();
            _previouslySelectedMain = selectedMain; // Sets the index you had to be previously selected, as to be able to go back to the same clone you were in, when you die
            if (switchValue + selectedMain > mainClones.Count-1) {//goes back to 0 if the current index + the value of the switch is higher than the amount of clones 
                selectedMain = 0;
            } else if (switchValue + selectedMain < 0) {// Goes to the max index of the list, if the index + switchValue are negative
                selectedMain = mainClones.Count-1;
            } else {
                selectedMain += switchValue; //otherwise it just adds the switchValue to the current index
            }
        }
        
        public void SpawnClone(int selected, List<GameObject> cloneGroup, GameObject sourceClone ) {
            for (int i = 0; i < activeClones.Length; i++) {
                if (activeClones[i]) // "Returns" if the clone is already active
                    continue;
                activeClones[i] = true; // if the clone isn't active, it is set to be, as it is the one being spawned in.
                clones[i].transform.position = new Vector2(sourceClone.transform.position.x, 
                    sourceClone.transform.position.y - 0.05f); // sets the new clones position to that of the "Source clone", aka teh clone that was used when it was cloned
                clones[i].SetActive(true); // Then it sets the clone of the same index's gameObject to active
                cloneGroup.Add(clones[i]); // Adds the clone to the specified list
                layerChanger[i].ChangeLayerToNonCollision(); // Changes the layer of the spawned clone,
                                                             // so that the clones don't collide with each other before leaving each others colliders 
                return;
            }
        }
        // Called from the animator script, removes the clone from the list, sets the clone to inactive
        public void KillClone(GameObject cloneToKill) { 
            hasDied = true; // For tutorial purposes, not used for anything else
            _playerAudio.PlayCloneDeathAudio();
            for (int i = 0; i < clones.Length; i++) { //loops through the clones, checking if they are the same as the cloneToKill
                if (clones[i] != cloneToKill)
                    continue;
                activeClones[i] = false; //sets the clone to unActive
                for (int j = 0; j < mainClones.Count; j++) {
                    // Loops through all the mainClones to remove them out of the list of mainClones
                    if (mainClones[j] != cloneToKill)// continues if its the wrong clone
                        continue;
                    mainClones.Remove(mainClones[j]); // removes the clone from the list
                    if (/*alternativeControls && */ _previouslySelectedMain <= mainClones.Count - 1) {
                        selectedMain = _previouslySelectedMain;
                    }
                }
                // for (int j = 0; j < secondClones.Count; j++) {
                //     if (secondClones[j] == cloneToKill) {
                //         secondClones.Remove(secondClones[j]);
                //     }
                // }
            }
        }
        //Called from the death animation, kills of the clone, turning the gameObject off
        public void DeActivateClone(GameObject cloneToKill) {
            for (int i = 0; i < clones.Length; i++) {
                if (clones[i] != cloneToKill)
                    continue;
                clones[i].SetActive(false);
                activeClones[i] = false;
            }
        }
        
        
        
        /*
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
        private void SwitchMain() => selectedMain = selectedMain == 0 ? 1 : 0;
        private void SwitchSecond() => selectedSecond = selectedSecond == 0 ? 1 : 0;
        */
        
        // void AlternativeControlsCheck() {
        //     switch (alternativeControls) {
        //         case true: {
        //             for (int i = 0; i < secondClones.Count; i++) {
        //                 mainClones.Add(secondClones[i]);
        //                 secondClones.Remove(secondClones[i]);
        //             }
        //             break;
        //         } case false: {
        //             for (int i = 0; i < mainClones.Count; i++) {
        //                 if (mainClones.Count >= 3) {
        //                     secondClones.Add(mainClones[i]);
        //                     mainClones.Remove(mainClones[i]);
        //                 }
        //             }
        //             break;
        //         }
        //     }
        // }
    }
}