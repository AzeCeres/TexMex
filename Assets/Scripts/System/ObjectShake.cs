using UnityEngine;
namespace System {
    [Tooltip("Place on any Object you want to Shake")]
    public class ObjectShake : MonoBehaviour {
        [Header("StartPos")]
        private Vector3 _originPosition;
        private Quaternion _originRotation;
        [Header("Effects")]
        //todo put in a range
        [Range(0.0001f, 0.01f)] [Tooltip("How quickly the shake stops.")]
        public float shakeDecay = 0.003f;
        [Range(0.01f, 0.3f)] [Tooltip("Generally good to have lower intensity if the shake affects multiple objects")]
        public float shakeIntensity = .2f;
        private bool _isShaking = false;
        private float _tempShakeIntensity = 0;

        void Update() {
            if (_tempShakeIntensity > 0 && _isShaking == true) {
                transform.position = _originPosition + UnityEngine.Random.insideUnitSphere * _tempShakeIntensity;
                transform.rotation = new Quaternion(
                    _originRotation.x + UnityEngine.Random.Range(-_tempShakeIntensity, _tempShakeIntensity) * .2f,
                    _originRotation.y + UnityEngine.Random.Range(-_tempShakeIntensity, _tempShakeIntensity) * .2f,
                    _originRotation.z + UnityEngine.Random.Range(-_tempShakeIntensity, _tempShakeIntensity) * .2f,
                    _originRotation.w + UnityEngine.Random.Range(-_tempShakeIntensity, _tempShakeIntensity) * .2f);
                _tempShakeIntensity -= shakeDecay;
            } else {
                _isShaking = false;
            }
        }

        public void Shake() {
            if (!_isShaking) {
                _isShaking = true;
                _originPosition = transform.position;
                _originRotation = transform.rotation;
                _tempShakeIntensity = shakeIntensity;
            }
        }
    }
}

