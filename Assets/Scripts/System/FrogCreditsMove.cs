using UnityEngine;
namespace System
{
    public class FrogCreditsMove : MonoBehaviour
    {
        [SerializeField] private float creditsMoveSpeed = 4f;
    
    
        void Update()
        {
            Vector3 creditsVelocity = Vector3.right * creditsMoveSpeed;
            transform.Translate(creditsVelocity * Time.deltaTime);
        }
    }
}
