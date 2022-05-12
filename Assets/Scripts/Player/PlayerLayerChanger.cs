using UnityEngine;
namespace Player {
    public class PlayerLayerChanger : MonoBehaviour {
        [SerializeField] private int normalLayerIndex, nonCollidingLayer;
        public void ChangeLayerToNonCollision() {
            transform.gameObject.layer = nonCollidingLayer;
        }
        public void ChangeLayerToNormal() {
            transform.gameObject.layer = normalLayerIndex;
        }
    }
}
