using UnityEngine;
namespace Puzzle
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Button[] button;
        public bool inverted;
        private bool m_Open;
        private BoxCollider2D m_DoorCollider;
        private SpriteRenderer m_DoorRenderer;
        private void Awake()
        {
            m_DoorCollider = GetComponentInChildren<BoxCollider2D>();
            m_DoorRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        private void Update()
        {
            Open();
        }
        private void Open()
        {
            int activeCount = 0;
            for (int i = 0; i < button.Length; i++)
            {
                if (button[i].active)
                {
                    activeCount++;
                }
                m_Open = activeCount == button.Length;
            }
            if (m_Open && !inverted || !m_Open && inverted)
            {
                m_DoorRenderer.enabled = false;
                if (m_DoorCollider == null) return;
                m_DoorCollider.enabled = false;
            }
            else //if (m_Open && inverted || m_Open && inverted)
            {
                m_DoorRenderer.enabled = true;
                if (m_DoorCollider == null) return;
                m_DoorCollider.enabled = true;
            }
        }
    }
}
