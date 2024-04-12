using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire_Knob : MonoBehaviour
{
    bool isPowered;
    bool hasNub;
    public Color32 WireColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    } 



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("WireNub"))
        {
            if (collision.gameObject.GetComponent<LineRenderer>().startColor == WireColor)
            {
                hasNub = true;
                //If powered, flip bool isPowered
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        hasNub= false;
    }
}
