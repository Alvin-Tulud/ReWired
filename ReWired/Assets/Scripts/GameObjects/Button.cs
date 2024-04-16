using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonScript : MonoBehaviour
{
    public bool isOn = false;

    private void Awake() {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Enter!");
        isOn = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        isOn = false;
    }
}
