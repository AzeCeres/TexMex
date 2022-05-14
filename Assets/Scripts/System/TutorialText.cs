using JetBrains.Annotations;
using Player;
using TMPro;
using UnityEngine;
namespace System {
    public class TutorialText : MonoBehaviour {
        public float timeBeforeText;
        public string tutorialText;
        private float _finalTime;
        public float textLingerTime;
        private float _lingeredTime;
        private TMP_Text _textField;
        private bool _hasTaught = false;
        public bool isDeathTutorial, isSplitTutorial, isSwitchTutorial;
        [CanBeNull] public GameObject nextTutorial;
        [SerializeField]private Split _split;
        [SerializeField]private int insideCount;
        private void Awake() {
            _textField = GetComponentInChildren<TMP_Text>();
        }
        private void Start() {
            _textField.enabled = false;
        }
        private void LateUpdate()
        {
            if (isSplitTutorial && _split.hasSplit || isSwitchTutorial && _split.hasSwitched || isDeathTutorial && _split.hasDied) {
                _textField.enabled = false;
                _hasTaught = true;
                if (nextTutorial != null)
                    nextTutorial.SetActive(true);
            }
        }
        private void OnEnable() {
            _finalTime = Time.realtimeSinceStartup + timeBeforeText;
            _lingeredTime = _finalTime + textLingerTime;
        }
        private void OnTriggerEnter2D(Collider2D other) {
            if (!other.gameObject.CompareTag("Player")) return;
            insideCount++;
            print(this.enabled);
            if (!this.enabled) return;
            if (_hasTaught) return;
            _finalTime = Time.realtimeSinceStartup + timeBeforeText;
            _lingeredTime = _finalTime + textLingerTime;
        }
        private void OnTriggerStay2D(Collider2D other) {
            if (!other.gameObject.CompareTag("Player")) return;
            print(name);
            if (Time.realtimeSinceStartup >= _finalTime) {
                SetText();
                _hasTaught = true;
            } if (Time.realtimeSinceStartup >= _lingeredTime) {
                _textField.enabled = false;
            }
        }
        private void SetText() {
            _textField.text = tutorialText;
            _textField.enabled = true;
            if (nextTutorial != null)
                nextTutorial.SetActive(true);
        }
        private void OnTriggerExit2D(Collider2D other) {
            if (!other.gameObject.CompareTag("Player")) return;
            insideCount--;
        }
    }
}
