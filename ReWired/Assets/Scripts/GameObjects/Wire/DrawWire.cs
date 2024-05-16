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
    public List<GameObject> tilesStateSwitched;
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

        Vector2 direction = otherWireNode.position - transform.position;
        float distance = Mathf.Pow(Mathf.Pow(otherWireNode.position.x - transform.position.x, 2) + 
            Mathf.Pow(otherWireNode.position.y - transform.position.y, 2), 0.5f);

        tilesStates = Physics2D.CircleCastAll(transform.position, 0.35f, direction, distance, 1 << 8 | 1 << 31);
        foreach (RaycastHit2D ray in tilesStates)
        {
            if (!tilesStateSwitched.Contains(ray.transform.gameObject))
            {
                tilesStateSwitched.Add(ray.transform.gameObject);
            }
        }

        

        Debug.DrawLine(transform.position, otherWireNode.position,Color.red, distance);
        Debug.DrawRay(transform.position, otherWireNode.position, Color.black, distance);
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
    }

    public void notGrab()
    {
        isGrabbed = false;
    }
}
