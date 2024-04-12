using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowWireParent : MonoBehaviour, IFlowReceiver
{
    /* The class for flow wires
     * Used by all flow wires
     * 
     * if you're reading this, I AM FIXING IT AND WILL COME BACK LATER, trust :fire:
     */

    //TODO: I need to add:
    // - New tag for the flow object so it knows whether colliding objects are flow wires

    bool poweredState; //Whether the flow is on or off
    bool updated; //Keeps track of whether a flow wire is updatedFalse, true when updating (so below function doesn't loop back into itself), false again when done updating

    void Start()
    {
        updated = false;
    }

    //Updates the whole connected flow wire's state recursively.
    public void updateWire(bool state)
    {
        selfUpdate(state);

        //for every other flow wire touching this one:
        //If its updated=false, updateWire it so it can update the others
    }

    //Updates THIS flow tile's state only
    public void selfUpdate(bool state)
    {
        poweredState = state;
        updated = true;
    }
}
