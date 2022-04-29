using UnityEngine;
namespace Puzzle
//todo // Collect colour from Accessibility settings, store colour, and have it be inheritable
{ public class Button : MonoBehaviour {
        [SerializeField] private bool staysDown;
        [SerializeField] private Sprite pressed;
        [SerializeField] private Sprite dePressed;
        public Color color;
        public bool active;
        private int m_InsideCount;
        private bool m_WasActive = false;
        private SpriteRenderer m_Renderer;
        private void Awake() {
            m_Renderer = GetComponent<SpriteRenderer>();
        }
        private void OnTriggerEnter2D(Collider2D other) {
            if(!other.gameObject.CompareTag("Player")) return;
            m_InsideCount++;
        }
        private void OnTriggerExit2D(Collider2D other) {
            if(!other.gameObject.CompareTag("Player")) return;
            if (staysDown) return;
            m_InsideCount--;
        }
        private void Update() { 
            m_WasActive = active;
            active = m_InsideCount>0;
            if (active && !m_WasActive) 
                Activate();
            else if (!active && m_WasActive)
                DeActivate();
            m_WasActive = active;
        }
        void Activate() {
            //todo Sound and Particles
            m_Renderer.sprite = pressed;
        }
        void DeActivate() {
            //todo Sound and Particles
            m_Renderer.sprite = dePressed;
        }
    }
}
