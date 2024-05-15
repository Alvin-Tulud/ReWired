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
        if (transform.parent.childCount > 1)
        {
            bool foundNub = false;
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                if (child.CompareTag("WireNub") &&
                    WireColor == child.GetComponent<LineRenderer>().startColor)
                {
                    hasNub = true;
                    foundNub = true;
                }
            }
            if (!foundNub)
            {
                hasNub = false;
            }
        }
        else
        {
            hasNub = false;
        }
    } 
}
