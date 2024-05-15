using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire_Knob : MonoBehaviour
{
    bool isPowered;
    bool hasNub;
    public Color WireColor;
    string WireHex;
    string NubHex;
    // Start is called before the first frame update
    void Start()
    {
        WireHex = ColorUtility.ToHtmlStringRGBA(WireColor);
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

                if (child.CompareTag("WireNub"))
                {
                    NubHex = ColorUtility.ToHtmlStringRGBA(child.GetComponent<LineRenderer>().startColor);

                    if (WireHex.CompareTo(NubHex) == 0)
                    {
                        hasNub = true;
                        foundNub = true;
                    }
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
