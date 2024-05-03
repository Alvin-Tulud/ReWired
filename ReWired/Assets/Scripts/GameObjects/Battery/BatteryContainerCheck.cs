using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using comptypes = GridObjects.Components.CompTypes;
public class BatteryContainerCheck : FlowSourceParent
{

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("should do SOMETHING");
        if (collision.transform.CompareTag("Battery"))
        {
            Debug.Log("should turn on");
            //do thing
            updateFlowSource(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Battery"))
        {
            Debug.Log("should turn off");
            //undo thing
            updateFlowSource(false);
        }
    }
    */
    protected override void apply(bool p)
    {
        

        if(p)
        {
            Debug.Log("button pressed ON");
        }
        else
        {
            Debug.Log("button pressed OFF");
        }
    }
}
