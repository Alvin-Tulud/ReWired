using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using comptypes = GridObjects.Components.CompTypes;
public class BatteryContainerCheck : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Battery"))
        {
            //do thing
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Battery"))
        {
            //undo thing
        }
    }
}
