using UnityEngine;

[ExecuteInEditMode]
public class ObjectColorChanger : MonoBehaviour
{
    private Color originalColor;
    private SpriteRenderer spriteRenderer;

    // Changes the color of the object
    public void ChangeColor(Color newColor)
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        spriteRenderer.color = newColor;
    }

    // Restores the original color of the object
    public void RestoreColor()
    {
        spriteRenderer.color = originalColor;
    }
}