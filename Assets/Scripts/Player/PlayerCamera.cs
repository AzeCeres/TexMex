using Cinemachine;
using Player;
using UnityEngine;

[RequireComponent(typeof(Split))]
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] cameras;

    private Split _split;

    // Start is called before the first frame update
    void Start()
    {
        _split = GetComponent<Split>();
    }

    // Update is called once per frame
    void Update()
    {
        cameras[_split.selectedMain].m_Priority = 10;
        for (int i = 0; i < cameras.Length; i++)
        {
            if (i == _split.selectedMain) break;
            cameras[i].m_Priority = 0;
        }
    }
}