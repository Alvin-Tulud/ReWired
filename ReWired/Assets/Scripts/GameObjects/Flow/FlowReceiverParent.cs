using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlowReceiverParent : MonoBehaviour
{
    public int powered;
    private bool updated;

    public bool getUpdated()
    {
        return updated;
    }
    public void setUpdated(bool u)
    {
        updated = u;
    }

 

    //Update: Changes power state then apply
    //p is supposed to only be 1 or -1
    public void powerUpdate(int p)
    {
        //update powered state
        powered += p;

        apply();
    }

    //WireUpdate: for wires only and does not go here

    //Apply: Abstract class that gets defined per flow receiver
    public abstract void apply();
}
