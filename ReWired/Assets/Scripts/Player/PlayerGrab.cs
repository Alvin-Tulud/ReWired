using UnityEngine;
using compTypes = GridObjects.Components.CompTypes;

public class PlayerGrab : MonoBehaviour
{
    private PlayerMovement pControl;
    private GameObject heldObject = null;
    private bool isSubbed = false;

    void Start()
    {
        pControl = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        int playerDir = pControl.getDirection();
        //Debug.Log(playerDir);

        if (Input.GetKeyDown(KeyCode.C))
        {
            if(!isSubbed)
                SendRayCircle2D(GetDirectionVector(playerDir));
            else
                UpdateHoldObject();
        }
    }

    void SendRayCircle2D(Vector2 direction)
    {
        Vector2 startPoint = new Vector2(transform.position.x + direction.x, transform.position.y + direction.y);
        LayerMask editorLayerMask = 1 << 31;
        RaycastHit2D hit = Physics2D.CircleCast(startPoint, 0.4f, direction, 1, editorLayerMask);
        Debug.DrawLine(new Vector2(startPoint.x + direction.x, startPoint.y + direction.y), startPoint, Color.white, 2f);
        if(hit.collider != null && hit.collider.transform.childCount != 0)
        {
            for(int i = 0; i < hit.collider.transform.childCount; i++)
            {
                GameObject child = hit.collider.transform.GetChild(i).gameObject;
                if (child.CompareTag("Battery") || child.CompareTag("WireNub"))
                {
                    //Debug.Log("Battery picked up: " + hit.collider.name);
                    subscribe(child, direction);
                }
            }
        }
    }

    Vector2 GetDirectionVector(int playerDir)
    {
        switch (playerDir)
        {
            case 0:
                return Vector2.up;
            case 1:
                return Vector2.right;
            case 2:
                return Vector2.down;
            case 3:
                return Vector2.left;
            default:
                return Vector2.zero;
        }
    }

    private void subscribe(GameObject hold, Vector2 dir)
    {
        if(hold.transform.parent.transform.childCount > 0)
        {
            if(hold.transform.parent.GetChild(0).gameObject.CompareTag("BatteryContainer"))
            {
                Debug.Log("BatteryContainer: Off");
                //Andrew: Updates the battery container
                Debug.Log(hold.transform.parent.GetChild(0).transform.name);
                hold.transform.parent.GetChild(0).gameObject.GetComponent<BatteryContainerCheck>().updateFlowSource(false);
            }
            else if (hold.transform.parent.GetChild(0).gameObject.CompareTag("Floor"))
            {
                if (hold.transform.parent.GetChild(0).gameObject.GetComponent<DrawWire>() == null)
                {
                    hold.transform.parent.GetChild(0).gameObject.GetComponent<DrawWire>().grabbed();
                }
            }

        }
        hold.transform.parent.GetComponent<compTypes.CanWalk>().setWalkable(true);
        hold.transform.parent = this.transform;
        pControl.setSubscribed(true, dir);
        isSubbed = true;
        heldObject = hold;
    }

    private void unsubscribe(GameObject unhold, GameObject floorTile)
    {
        unhold.transform.parent = floorTile.transform;
        unhold.transform.parent.GetComponent<compTypes.CanWalk>().setWalkable(false);
        if(floorTile.transform.childCount > 0)
        {
            if(floorTile.transform.GetChild(0).gameObject.CompareTag("BatteryContainer"))
            {
                Debug.Log("BatteryContainer: On");
                //Andrew: Updates the battery container
                floorTile.transform.GetChild(0).gameObject.GetComponent<BatteryContainerCheck>().updateFlowSource(true);
            }
            else if (floorTile.transform.GetChild(0).gameObject.CompareTag("BatteryContainer"))
            {
                if(floorTile.transform.GetChild(0).gameObject.GetComponent<DrawWire>() == null)
                {
                    floorTile.transform.GetChild(0).gameObject.GetComponent<DrawWire>().notGrab();
                }
            }
        }
        pControl.setSubscribed(false, Vector2.zero);
        heldObject = null;
        isSubbed = false;
    }

    private void UpdateHoldObject()
    {
        Vector2 startPoint = new Vector2(heldObject.transform.position.x, heldObject.transform.position.y);
        LayerMask editorLayerMask = 1 << 31;
        RaycastHit2D hit = Physics2D.CircleCast(startPoint, 0.4f, Vector2.zero, 1, editorLayerMask);
        if(hit.collider != null)
            unsubscribe(heldObject,hit.collider.gameObject);
    }
}