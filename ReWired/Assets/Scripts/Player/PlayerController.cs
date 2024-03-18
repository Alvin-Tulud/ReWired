using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask floorLayer;

    private Rigidbody2D rb;
    private Vector2Int currentTile;
    private Vector2 moveDirection = new Vector2(0,0);
    private bool canMove = true;
    private float timeSinceLastMove = 0f;
    private float pauseTime = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentTile = GetTilePosition(transform.position);
    }

    void Update()
    {
        if (!canMove)
        {
            timeSinceLastMove += Time.deltaTime;

            if (timeSinceLastMove >= pauseTime)
            {
                canMove = true;
                timeSinceLastMove = 0f;
            }
        }
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 movetempDirection = new Vector2(moveX, moveY).normalized;

        if(moveDirection == Vector2.zero)
        {
            //reset move
        }
        if (canMove)
        {
            bool canWalk = IsTileWalkable(currentTile, moveDirection);

            if(movetempDirection != Vector2.zero && moveDirection != movetempDirection && canWalk)
            {
                // Add a directional movement not adding rb anymore
            }
            else if (movetempDirection != Vector2.zero && canWalk)
            {
                rb.velocity = moveDirection * moveSpeed;
                currentTile += Vector2Int.RoundToInt(moveDirection);

            }
        }
    }

    private bool IsTileWalkable(Vector2Int currentTile, Vector2 direction)
    {
        float tileSize = 1f;

        //changed tiling

        // RaycastHit2D hit = Physics2D.Raycast(currentTile * tileSize, direction, tileSize, floorLayer);
        // if (hit.collider != null)
        // {
        //     FloorTile floorTile = hit.collider.GetComponent<FloorTile>();
        //     if (floorTile != null)
        //     {
        //         return floorTile.IsWalkable();
        //     }
        // }
        return false;
    }

    private Vector2Int GetTilePosition(Vector2 position)
    {
        return new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));
    }
}