using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWire : MonoBehaviour
{
    public Transform otherWireNode;
    LineRenderer wireLine;
    public Color32 wireColor;
    LayerMask wireLayerMask;

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


        //RaycastHit2D[] ray;
        //ray = Physics2D.RaycastAll(transform.position, transform.position, 1000f,wireLayerMask);
        //foreach(RaycastHit2D WireNub in ray){
        //    Debug.Log(WireNub.transform.name + " || " + WireNub.transform.position);
        //    if (wireLine.startColor == WireNub.transform.GetComponent<LineRenderer>().startColor) {
        //        otherWireNode = WireNub.transform;
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    { 
        wireLine.SetPosition(0, transform.position);
        wireLine.SetPosition(1, otherWireNode.position);

        //if an if else for moving turn it on and else turn it off
        //make sure it is always pointing towards the other wire so like rotate it
    }


}
