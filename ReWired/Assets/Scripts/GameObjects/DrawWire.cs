using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWire : MonoBehaviour
{
    Transform otherWireNode;
    int childNodeIndex;
    LineRenderer wireLine;
    // Start is called before the first frame update
    void Start()
    {
        childNodeIndex = transform.GetSiblingIndex();
        if (childNodeIndex == 0)
            otherWireNode = transform.parent.GetChild(1);
        else
            otherWireNode = transform.parent.GetChild(0);


        wireLine = GetComponent<LineRenderer>();
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
