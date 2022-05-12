using UnityEngine;
namespace Player {
    public class ActivationScript : MonoBehaviour {
        private PlayerLayerChanger _layerChanger;
        private void Awake() {
            _layerChanger = GetComponentInParent<PlayerLayerChanger>();
        }
        private void OnTriggerExit2D(Collider2D other) {
            if(!other.CompareTag("Player"))
                return;
            if (transform.parent.gameObject.layer == 7)
            {
                _layerChanger.ChangeLayerToNormal();
            }
        }
    }
}
