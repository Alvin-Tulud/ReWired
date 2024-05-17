using GridObjects.Components.CompTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawWire : MonoBehaviour
{
    public Transform otherWireNode;
    public LineRenderer wireLine;
    public Color32 wireColor;


    public RaycastHit2D[] tilesStates;
    bool isGrabbed;
    

    // Start is called before the first frame update
    void Start()
    {
        wireLine = GetComponent<LineRenderer>();
        wireLine.startColor = wireColor;
        wireLine.endColor = wireColor;
        wireLine.startWidth = transform.localScale.x;
        wireLine.endWidth = transform.localScale.x;


        isGrabbed = false;
    }

    // Update is called once per frame
    void Update()
    { 
        wireLine.SetPosition(0, transform.position);
        wireLine.SetPosition(1, otherWireNode.position);

        Vector2 direction = otherWireNode.position - transform.position;
        float distance = Mathf.Pow(Mathf.Pow(otherWireNode.position.x - transform.position.x, 2) + 
            Mathf.Pow(otherWireNode.position.y - transform.position.y, 2), 0.5f);

        tilesStates = Physics2D.CircleCastAll(transform.position, 0.35f, direction, distance, 1 << 8 | 1 << 31);
        


        Debug.DrawRay(transform.position, direction, Color.blue, distance);

        if (!isGrabbed)
        {
            foreach (var tile in tilesStates)
            {
                if (tile.transform.gameObject.GetComponent<CanWalk>() != null)
                {
                    tile.transform.gameObject.GetComponent<CanWalk>().setWalkable(false);
                }
            }
        }
        else
        {
            foreach (var tile in tilesStates)
            {
                if (tile.transform.gameObject.GetComponent<CanWalk>() != null)
                {
                    tile.transform.gameObject.GetComponent<CanWalk>().setWalkable(true);
                }
            }
        }
    }

    public void grabbed(bool grab)
    {
        isGrabbed = grab;
        Debug.Log("Grabbed: " + isGrabbed + " Gameobject: " + this.transform.name);
    }

    public void setother(bool grab)
    {
        otherWireNode.gameObject.GetComponent<DrawWire>().grabbed(grab);
    }
}
