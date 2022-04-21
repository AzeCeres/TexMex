using UnityEngine;
namespace Puzzle
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Button[] button;
        public bool inverted;
        private bool _mOpen;
        private BoxCollider2D _mDoorCollider;
        private SpriteRenderer _mDoorRenderer;
        private void Awake()
        {
            _mDoorCollider = GetComponentInChildren<BoxCollider2D>();
            _mDoorRenderer = GetComponentInChildren<SpriteRenderer>();
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
                _mOpen = activeCount == button.Length;
            }
            if (_mOpen && !inverted || !_mOpen && inverted)
            {
                _mDoorRenderer.enabled = false;
                if (_mDoorCollider == null) return;
                _mDoorCollider.enabled = false;
            }
            else //if (m_Open && inverted || m_Open && inverted)
            {
                _mDoorRenderer.enabled = true;
                if (_mDoorCollider == null) return;
                _mDoorCollider.enabled = true;
            }
        }
    }
}
