using UnityEngine;
namespace System
{
    public class CreditsMove : MonoBehaviour
    {
        [SerializeField] private float creditsMoveSpeed = 2f;
    
    
        void LateUpdate()
        {
            Vector3 creditsVelocity = Vector3.up * creditsMoveSpeed;
            transform.Translate(creditsVelocity * Time.deltaTime);
        }
    }
}
