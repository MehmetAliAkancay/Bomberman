using UnityEngine;

public class AnimatedSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite idleSprite;
    public Sprite[] animationSprite;
    public float animationTime = 0.25f;
    private int animationFrame;
    public bool loop =true;
    public bool idle = true;
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start() {
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime);
    }
    private void OnEnable() {
        spriteRenderer.enabled = true;
    }
    private void OnDisable() {
        spriteRenderer.enabled = false;
    }
    private void NextFrame()
    {
        animationFrame++;
        if(loop && animationFrame >= animationSprite.Length)
        {
            animationFrame = 0;
        }
        if(idle)
        {
            spriteRenderer.sprite = idleSprite;
        }
        else if (animationFrame >= 0 && animationFrame < animationSprite.Length){
            spriteRenderer.sprite = animationSprite[animationFrame];
        }
    }
}
