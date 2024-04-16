using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlowReceiverParent : MonoBehaviour
{
    public int powered;

    // Start is called before the first frame update
    void Start()
    {
        //apply
    }

    //Update: Changes power state

    //WireUpdate: for wires only and does not go here

    //Apply: Abstract class that gets defined per flow receiver
}
