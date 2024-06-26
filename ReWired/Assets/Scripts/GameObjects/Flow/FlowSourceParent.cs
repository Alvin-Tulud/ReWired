using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlowSourceParent : MonoBehaviour
{
    //Flow source is buttons/etc. that power flow wires

    bool powered;
    private List<GameObject> connectedWires = new List<GameObject>(); //List of connected flow segments, populated in Awake()
    private LayerMask FlowLayerMask;


    //Right before start we gotta use a circle raycast to get the list of connected flow segments
    void Awake()
    {
        //layermask to only interact with flow wires
        FlowLayerMask = 1 << 10;
        //Get all the flow wires touching the flow source
        RaycastHit2D[] flowRaycastList = Physics2D.CircleCastAll(gameObject.transform.position, 0.5f, Vector2.zero, 0, FlowLayerMask);

        //convert the flow wires from RaycastHit2D to normal GameObject
        for(int i=0; i<flowRaycastList.Length; i++)
        {
            //convert the RaycastHit2D object into GameObject
            GameObject g = flowRaycastList[i].transform.gameObject;
            Debug.Log(g);
            //add the GameObject to connectedWires
            connectedWires.Add(g);
        }
 
    }

    //Update: 1.Change this thing's power state 2.trigger connected wires' recursive update
    //this should probably be called by collision enter/exit function
    public void updateFlowSource(bool p)
    {
        int val; //1 or -1 depending on powering on or off
        if (p) //if powering ON
        {
            val = 1;
        }
        else //if powering OFF
        {
            val = -1;
        }

        //change this thing's own state
        //(should probably involve sprite/animation change)
        apply(p);

        //trigger wireUpdate on connected flow wires
        foreach (GameObject w in connectedWires)
        {
            //the script of the flow wire
            FlowWire script = w.GetComponent<FlowWire>();
            Debug.Log(w.GetComponent<FlowWire>().getUpdated());

            //Only update the wire if it hasn't been yet
            if (script.getUpdated() == false) //if updated==false
            {
                script.powerUpdate(val); //recursive update the connected wire
            }
        }
    }

    //Applies state change based on power level (powerUpdate only updates power level but doesn't trigger effects)
    protected virtual void apply(bool p)
    {
        //hello bro
    }

}