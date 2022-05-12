using UnityEngine;
using UnityEngine.Tilemaps;
public class ActivateMaterial : MonoBehaviour {
    [SerializeField] private TilemapRenderer[] tRenderers;
    private static readonly int s_Active = Shader.PropertyToID("_Active");
    private void Start() {
        foreach (var tRenderer in tRenderers) {
            tRenderer.material.SetFloat(s_Active, 1f);
        }
    }
}
