using UnityEngine;
// using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;


public class DoorScript : MonoBehaviour
{
    public bool isOn = false;

    private void Update()
    {

        if (transform.parent.childCount > 1)
        {
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                Transform child = transform.parent.GetChild(i);

                if (child.CompareTag("Player"))
                {
                    int sceneIndex = SceneManager.GetActiveScene().buildIndex;
                    int maxSceneIndex = SceneManager.sceneCountInBuildSettings - 1;

                    if (sceneIndex < maxSceneIndex && isOn)
                    {
                        SceneManager.LoadScene(sceneIndex + 1);
                    }
                }
            }
        }
    }
}
