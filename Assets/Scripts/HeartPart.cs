using UnityEngine;

public class HeartPart : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    private Color originalColor;
    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
    }

    public void Highlight()
    {
        renderer.material.color = highlightColor;
    }

    public void RemoveHighlight()
    {
        renderer.material.color = originalColor;
    }
}