using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using comptypes = GridObjects.Components.CompTypes;
public class BatteryContainerCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        comptypes.CanWalk canwalk = this.gameObject.transform.parent.transform.gameObject.GetComponent<comptypes.CanWalk>();
        canwalk.setWalkable(false);
        
    }

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
