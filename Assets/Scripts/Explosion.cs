using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AnimatedSpriteRenderer start;
    public AnimatedSpriteRenderer middle;
    public AnimatedSpriteRenderer end;
    public void SetActiveRenderer(AnimatedSpriteRenderer spriteRenderer)
    {
        start.enabled = spriteRenderer == start;
        middle.enabled = spriteRenderer == middle;
        end.enabled = spriteRenderer == end;
    }
    public void SetDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }
    public void DestroyAfter(float seconds)
    {
        Destroy(gameObject, seconds);
    }
}
