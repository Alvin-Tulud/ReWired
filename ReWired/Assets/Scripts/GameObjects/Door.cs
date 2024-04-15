using UnityEngine;
// using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;
public class DoorScript : MonoBehaviour
{
    public bool isOn = false;

    // private void Awake() {
        
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Enter!");
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        if (sceneIndex >= sceneCount) {
            SceneManager.LoadScene( sceneIndex + 1);
        }
        
    }
}
