using GridObjects.Components.CompTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawWire : MonoBehaviour
{
    public Transform otherWireNode;
    LineRenderer wireLine;
    public Color32 wireColor;
    LayerMask wireLayerMask;


    //RaycastHit2D[] rayright;
    //RaycastHit2D[] rayleft;
    public RaycastHit2D[] tilesStates;
    bool isGrabbed;


    static int Connect = 0;
    private int WireID;

    private void Awake()
    {
        //talking between all wirenubs to assign them ids
        WireID = Connect;
        Connect++;
    }

    // Start is called before the first frame update
    void Start()
    {
        wireLayerMask = 1 << 8;


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

        tilesStates = Physics2D.CircleCastAll(transform.position, transform.localScale.x, otherWireNode.position, 1000f, 1 << 31);

        Debug.DrawLine(transform.position, otherWireNode.position,Color.red, 1000f);


        //Vector2 raydirright = transform.position - otherWireNode.position;
        //raydirright = Vector2.Perpendicular(raydirright);
        //raydirright.Normalize();
        //raydirright = raydirright * wireLine.endWidth/2;
        //raydirright.y += 0.1f;
        //
        //Vector2 raydirleft = transform.position - otherWireNode.position;
        //raydirleft = Vector2.Perpendicular(raydirleft);
        //raydirleft.Normalize();
        //raydirleft = raydirleft * wireLine.endWidth/2;
        //raydirleft.y -= 0.1f;

        //get left n right of the otherwirenode somehow
        if (!isGrabbed)
        {
            foreach (var tile in tilesStates)
            {
                if (tile.transform.gameObject.GetComponent<CanWalk>() != null)
                {
                    tile.transform.gameObject.GetComponent<CanWalk>().setWalkable(false);
                }
            }

            //rayright = Physics2D.RaycastAll(raydirright, (Vector2)otherWireNode.position + raydirright, 1000f, 1 << 31);
            //
            //Debug.DrawRay(raydirright, (Vector2)otherWireNode.position + raydirright, Color.red, 5f);
            //
            //foreach (RaycastHit2D ray in rayright)
            //{
            //    if (ray.transform.gameObject.GetComponent<CanWalk>() != null)
            //    {
            //        ray.transform.gameObject.GetComponent<CanWalk>().setWalkable(false);
            //    }
            //
            //}
            //
            //rayleft = Physics2D.RaycastAll(raydirleft, (Vector2)otherWireNode.position + raydirleft, 1000f, 1 << 31);
            //
            //Debug.DrawRay(raydirleft, (Vector2)otherWireNode.position + raydirleft, Color.blue, 5f);
            //
            //foreach (RaycastHit2D ray in rayleft)
            //{
            //    if (ray.transform.gameObject.GetComponent<CanWalk>() != null)
            //    {
            //        ray.transform.gameObject.GetComponent<CanWalk>().setWalkable(false);
            //    }
            //
            //}
        }
        //if an if else for moving turn it on and else turn it off
        //make sure it is always pointing towards the other wire so like rotate it
    }

    public void grabbed()
    {
        isGrabbed = true;

        foreach (var tile in tilesStates)
        {
            if (tile.transform.gameObject.GetComponent<CanWalk>() != null)
            {
                tile.transform.gameObject.GetComponent<CanWalk>().setWalkable(true);
            }
        }

        //foreach (RaycastHit2D ray in rayright)
        //{
        //    if (ray.transform.gameObject.GetComponent<CanWalk>() != null)
        //    {
        //        ray.transform.gameObject.GetComponent<CanWalk>().setWalkable(true);
        //    }
        //
        //}
        //
        //foreach (RaycastHit2D ray in rayleft)
        //{
        //    if (ray.transform.gameObject.GetComponent<CanWalk>() != null)
        //    {
        //        ray.transform.gameObject.GetComponent<CanWalk>().setWalkable(true);
        //    }
        //
        //}
    }

    public void notGrab()
    {
        isGrabbed = false;
    }
}
