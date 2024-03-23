using UnityEngine;
using compTypes = GridObjects.Components.CompTypes;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2000f;
    private LayerMask floorLayer;
    private Vector2 moveDirection = Vector2.zero;
    private Vector2 animationMove = Vector2.zero;
    private bool canMove = true;
    private bool playerMoving = false;
    private float pauseTime = 0.3f;
    private float time = 0;
    private Transform currentFloorTile;

    private int facingDir = 0; //0 = up 1 = right 2 = down 3 = left

    private bool isSubscribed = false;
    private Vector2 subDirection = Vector2.zero;


    void Awake()
    {
        floorLayer = 1 << LayerMask.NameToLayer("Floor");
    }

    void Update()
    {
        if (!canMove)
        {
            time = time + Time.deltaTime;
            canMove = time >= pauseTime;
        }
        else time = 0f;

        Vector2 movetempDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movetempDirection = Vector2.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movetempDirection = Vector2.right;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            movetempDirection = Vector2.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            movetempDirection = Vector2.down;
        }

        //Debug.Log(movetempDirection + " , " + canMove);

        if (movetempDirection == Vector2.zero)
        {
            canMove = true;
            moveDirection = Vector2.zero;
            animationMove= Vector2.zero;
        }
        else if (canMove && !playerMoving)
        {
            if(currentFloorTile == null)
                currentFloorTile = this.transform.parent;
            bool canWalk = IsTileWalkable(currentFloorTile);
            if (movetempDirection != Vector2.zero && moveDirection != movetempDirection && canWalk)
            {
                moveDirection = movetempDirection;
                TryMove(movetempDirection);
                canMove = false;
            }
            else if (movetempDirection != Vector2.zero && canWalk)
            {
                TryMove(movetempDirection);
            }
        }

        if (playerMoving)
        {
            float step = moveSpeed * Time.deltaTime;
            animationMove = Vector2.Lerp(animationMove, Vector2.zero, step);
            if (Vector3.Distance(animationMove, Vector2.zero) < 0.65f)
            {
                transform.localPosition = Vector2.zero;
                currentFloorTile = null;
                playerMoving = false;
            }
        }
    }

    private void TryMove(Vector2 direction)
    {
        //Debug.Log("hit: " + nextFloorTile.gameObject.tag);
        
        Transform playerDir = transform.GetChild(0).transform;
        if(direction.x > .1f)
        {
            playerDir.rotation = Quaternion.Euler(0f, 0f, -90f);
            playerDir.localPosition = new Vector2(.1f,0f);
            facingDir = 1;
        }
        else if(direction.x < -.1f)
        {
            playerDir.rotation = Quaternion.Euler(0f, 0f, 90f);
            playerDir.localPosition = new Vector2(-.1f,0f);
            facingDir = 3;
        }
        else if(direction.y < -.1f)
        {
            playerDir.rotation = Quaternion.Euler(0f, 0f, 180f);
            playerDir.localPosition = new Vector2(0f,-0.1f);
            facingDir = 2;
        }
        else if(direction.y > .1f)
        {
            playerDir.rotation = Quaternion.Euler(0f, 0f, 0f);
            playerDir.localPosition = new Vector2(0f,0.1f);
            facingDir = 0;
        }

        if(isSubscribed)
        {
            direction = direction + subDirection;
        }

        Transform nextFloorTile = GetFloorTileTransform(direction);

        if(isSubscribed && !tagConditons(nextFloorTile))
        {
            direction = direction - subDirection;
            nextFloorTile = GetFloorTileTransform(direction);
            return;
        }

        if (nextFloorTile != null && (IsTileWalkable(nextFloorTile) || (isSubscribed && nextFloorTile.GetChild(0).gameObject.CompareTag("BatteryContainer"))))
        {
            if(isSubscribed)
            {
                direction = direction - subDirection;
                nextFloorTile = GetFloorTileTransform(direction);
                if(!IsTileWalkable(nextFloorTile))
                    return;
            }
            currentFloorTile = nextFloorTile;
            transform.SetParent(currentFloorTile);
            animationMove = transform.localPosition;
            playerMoving = true;
        }
    }

    private bool IsTileWalkable(Transform floorTile)
    {
        return floorTile != null && floorTile.GetComponent<compTypes.CanWalk>().isWalkable() == true;
    }

    private Transform GetFloorTileTransform(Vector2 direction)
    {
        //Debug.Log(direction);
        RaycastHit2D hit = Physics2D.CircleCast(new Vector2(transform.position.x, transform.position.y) + direction, 0.45f, direction, 0.0f, floorLayer);
        //Debug.DrawLine(new Vector2(transform.position.x, transform.position.y) + direction, new Vector2(transform.position.x + direction.x, transform.position.y + direction.y), Color.blue);
        return hit.collider.transform != null && hit.collider.CompareTag("Floor") ? hit.transform : null;
    }

    bool tagConditons(Transform fTile)
    {
        if(fTile.childCount > 1)
            return false;
        return true;
    }

    public int getDirection()
    {
        return facingDir;
    }

    public void setSubscribed(bool subscribed, Vector2 subDir)
    {
        isSubscribed = subscribed;
        subDirection = subDir;
    }

}