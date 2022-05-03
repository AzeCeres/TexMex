
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class LayerManager : MonoBehaviour
    {
        //todo // rank clones based on y value, where the clone with highest y value having lowest layer
        [SerializeField] private List<GameObject> clones;
        private List<SpriteRenderer> cloneRenderer= new List<SpriteRenderer>();
        private float highestY, lowestY;
        private int highestIndex, lowestIndex, lastIndex;
        [SerializeField] private int layerHeight;
        private void Awake() {
            foreach (var clone in clones) {
                var renderer = clone.GetComponent<SpriteRenderer>();
                cloneRenderer.Add(renderer);
            }
        }
        private void FixedUpdate() {
            GetLayerOrder();
            SetLayerHeight();
        }
        void GetLayerOrder() {
            highestY = float.MinValue;
            lowestY = float.MaxValue;
            
            for (int i = 0; i < clones.Count; i++) {
                if (clones[i].transform.position.y >= highestY) {
                    highestY = clones[i].transform.position.y;
                    highestIndex = i;
                }
                if (clones[i].transform.position.y <= lowestY) {
                    lowestY = clones[i].transform.position.y;
                    lowestIndex = i;
                }
            }
            for (int i = 0; i < clones.Count; i++) {
                if (i == highestIndex || i == lowestIndex) continue;
                lastIndex = i;
            }
        }
        void SetLayerHeight(){
            cloneRenderer[highestIndex].sortingOrder = layerHeight - 1;
            cloneRenderer[lowestIndex].sortingOrder = layerHeight + 1;
            cloneRenderer[lastIndex].sortingOrder = layerHeight;
        }
    }
}
