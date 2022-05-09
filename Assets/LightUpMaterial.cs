using UnityEngine;

public class LightUpMaterial : MonoBehaviour
{
    private SpriteRenderer m_Renderer;
    private static readonly int s_Active = Shader.PropertyToID("Active");
    private void Awake()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        m_Renderer.material.SetFloat(s_Active, 1f);
    }
}
