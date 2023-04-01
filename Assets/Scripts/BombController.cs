using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    public KeyCode inputKey = KeyCode.Space;
    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    [Header("Bomb")]
    public GameObject bombPrefab;
    public float bombFuseTime = 3f;
    public int bombAmount = 1;
    private int bombsRemaining;

    [Header("Destructible")]
    public Tilemap destructibleTiles;
    public Destructible destructiblePrefab;
    private void Update() {
        if(bombsRemaining > 0 && Input.GetKeyDown(inputKey)){
            StartCoroutine(PlaceBomb());
        }
    }
    private void OnEnable() {
        bombsRemaining = bombAmount;
    }
    public void AddBomb()
    {
        bombAmount++;
        bombsRemaining++;
    }
    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        GameObject newBomb = Instantiate(bombPrefab, position, Quaternion.identity);
        bombsRemaining--;

        yield return new WaitForSeconds(bombFuseTime);

        position = newBomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Explosion newExplosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        newExplosion.SetActiveRenderer(newExplosion.start);
        newExplosion.DestroyAfter(explosionDuration);

        Explode(Vector2.up,position,explosionRadius);
        Explode(Vector2.down,position,explosionRadius);
        Explode(Vector2.left,position,explosionRadius);
        Explode(Vector2.right,position,explosionRadius);

        Destroy(newBomb);
        bombsRemaining++;
    }
    private void Explode(Vector2 direction, Vector2 position, int length)
    {
        if(length <=0)
            return;
        
        position += direction;

        if(Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
        {
            ClearDestructible(position);
            return;
        }
        
        Explosion newExplosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        newExplosion.SetActiveRenderer(length > 1 ? newExplosion.middle : newExplosion.end);
        newExplosion.SetDirection(direction);
        newExplosion.DestroyAfter(explosionDuration);

        Explode(direction,position,length-1);
    }
    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cell = destructibleTiles.WorldToCell(position);
        TileBase tile = destructibleTiles.GetTile(cell);

        if(tile != null)
        {
            Instantiate(destructiblePrefab, position, Quaternion.identity);
            destructibleTiles.SetTile(cell, null);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
    }
}
