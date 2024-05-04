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
    private List<GameObject> connectedWires = new List<GameObject>(); //List of connected flow segments, populated by OnTriggerEnter2D
    private List<GameObject> connectedFlowObjects = new List<GameObject>(); //List of connected flow objects that aren't wires ^
    private bool updated; //base case for recursive update
    public SpriteRenderer spriteRenderer; //sprite renderer for changing the color

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //darken wire color initially to signify it is off
        spriteRenderer.material.color = new Color32(127, 127, 127, 255);
        //^ (May change this in favor of having a dark sprite then lightening it upon initial power)

        //Populates the wire's list of connected wires / flow objects

        //Get everything touching the wire
        RaycastHit2D[] flowRaycastList = Physics2D.CircleCastAll(gameObject.transform.position, 0.5f, Vector2.zero, 0);

        //Splits the relevant RaycastHit2D's into either wires or flow receivers
        for (int i = 0; i < flowRaycastList.Length; i++)
        {
            //convert the RaycastHit2D object into GameObject
            GameObject g = flowRaycastList[i].transform.gameObject;
            
            if(g.GetComponent<FlowReceiverParent>() != null)
            {
                //If the obj is a flow wire, then add to the list of connectedwires
                if ((g.tag == "Flow") && (g != this.gameObject)) //2nd part of the if() is to prevent it from adding itself
                {
                    connectedWires.Add(g);
                }
                //Otherwise, it's a non-wire flow receiver like a door
                else
                {
                    connectedFlowObjects.Add(g);
                }
            }
        }
    }

    public bool getUpdated()
    {
        return updated;
    }
    public void setUpdated(bool u)
    {
        updated = u;
    }

    //recursive wire update
    public new void powerUpdate(int p)
    {
        //powerUpdate self
        powered += p;
        setUpdated(true);
        //Debug.Log("Power: " + powered);

        //trigger wireUpdate on connected flow wires who are not yet updated
        foreach (GameObject w in connectedWires)
        {
            //the script of the flow wire
            FlowWire script = w.GetComponent<FlowWire>();

            //Only update the wire if it hasn't been yet
            if (!script.getUpdated()) //if updated==false
            {
                script.powerUpdate(p); //recursive update the connected wire
            }
        }

        //^After recursive update is done, reset updated status and update wire status
        apply();
    }

    //1. Updates the wire itself, 2. Updates connected flow objects
    protected override void apply()
    {
        //p determines whether the flow's change was +1 or -1 so it knows what to update connected obj's with
        int p = 0;


        if (powered > 0) //if the wire is on
        {
            p = 1;
            //lighten wire color if on
            spriteRenderer.material.color = new Color32(255, 255, 255, 255);
        }
        else //if the wire is off
        {
            p = -1;
            //darken wire color if off
            spriteRenderer.material.color = new Color32(127, 127, 127, 255);
        }

        Debug.Log("CONNECTED FLOW: " + connectedWires.Count);
        //Spreads flow's updated status to connected flow objects (not flow wires)
        foreach (GameObject o in connectedFlowObjects)
        {
            if (o.tag != "Flow")
            {
                FlowReceiverParent script = o.gameObject.GetComponent<FlowReceiverParent>();

                script.powerUpdate(p);
            }
            
        }

        updated = false; //Lastly reset updated status
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //I might replace all of this with the layermask stuff in FlowSourceParent

        //script is the parent script, used to check if the collided object is part of flow receivers
        //it has occurred to me that I could have just done this with layers but w/e
        FlowReceiverParent script = col.gameObject.GetComponent<FlowReceiverParent>();

        if(script != null)
        {
            //If the collider obj is a flow wire, then add to the list of connectedwires
            if (col.gameObject.tag == "Flow")
            {
                connectedWires.Add(col.gameObject);
            }
            //Otherwise, it's a non-wire flow receiver like a door
            else
            {
                connectedFlowObjects.Add(col.gameObject);
            }
        }
        
    }


}
