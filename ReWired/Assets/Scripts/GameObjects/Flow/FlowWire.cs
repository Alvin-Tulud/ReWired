using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowWire : FlowReceiverParent
{
    /* The class for flow wires
     * Used by all flow wires
     * 
     * 
     * if you're reading this, I AM FIXING IT AND WILL COME BACK LATER, trust :fire:
     */

    //TODO: 
    // - New tag for the flow object so it knows whether colliding objects are flow wires

    //int powered and bool updated inherited from parent class
    private List<GameObject> connectedWires; //List of connected flow segments, populated by OnTriggerEnter2D




    public void wireUpdate(int p)
    {
        //powerUpdate self
        powerUpdate(p);

        //trigger wireUpdate on connected flow wires who are not yet updated

        //^After recursive update is done, reset updated status
        apply();
    }

    public override void apply()
    {
        //apply implementation
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //If the collider is a wire, then add to the list of connectedwires
    }


}
