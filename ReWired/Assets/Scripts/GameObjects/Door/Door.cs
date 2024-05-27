using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;


public class DoorScript : MonoBehaviour
{
    public bool isOn = true;

    private void Update()
    {
        GameObject[] knobs = GameObject.FindGameObjectsWithTag("WireKnob");

        isOn = true;


        foreach (var v in knobs)
        {
            if (!v.GetComponent<Wire_Knob>().getNub())
            {
                isOn = false;
            }
        }




        if (transform.parent.childCount > 1)
        {
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                Transform child = transform.parent.GetChild(i);

                if (child.CompareTag("Player"))
                {
                    Debug.Log("Player Here");
                    int sceneIndex = SceneManager.GetActiveScene().buildIndex;
                    int maxSceneIndex = SceneManager.sceneCountInBuildSettings - 1;

                    if (sceneIndex < maxSceneIndex && isOn)
                    {
                        Debug.Log("Player Win");
                        SceneManager.LoadScene(sceneIndex + 1);
                    }
                    else if (sceneIndex == maxSceneIndex && isOn)
                    {
                        Debug.Log("Player Win");
                        SceneManager.LoadScene(0);
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
