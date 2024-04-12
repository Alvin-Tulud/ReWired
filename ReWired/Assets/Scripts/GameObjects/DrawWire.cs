using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWire : MonoBehaviour
{
    Transform otherWireNode;
    int childNodeIndex;
    LineRenderer wireLine;
    public Color32 wireColor;

    // Start is called before the first frame update
    void Start()
    {
        wireLine = GetComponent<LineRenderer>();

        wireLine.startColor = wireColor;
        GameObject[] found = GameObject.FindGameObjectsWithTag(transform.tag);
        foreach(GameObject WireNub in found){
            if (wireLine.startColor == WireNub.GetComponent<LineRenderer>().startColor) {
                otherWireNode = WireNub.transform;
            }
        }
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
