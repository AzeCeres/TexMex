using UnityEngine;
namespace System {
    public class ShakeAll : MonoBehaviour {
        private ObjectShake[] objectsToShake;
        public bool shake;

        private void Start()
        {
            objectsToShake = UnityEngine.Object.FindObjectsOfType<ObjectShake>();
        }


        private void Update() {
            if (shake) ShakeAllObjects();
        }
        public void ShakeAllObjects() {
            for (int i = 0; i < objectsToShake.Length; i++) {
                objectsToShake[i].Shake();
            }
        }
    }
}
