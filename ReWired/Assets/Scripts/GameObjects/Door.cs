using UnityEngine;
// using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(SpriteRenderer))]
public class DoorScript : MonoBehaviour
{
    public bool isOn = false;

    // private void Awake() {
        
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Enter!");
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        int maxSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
        
        if (sceneIndex < maxSceneIndex) {
            SceneManager.LoadScene( sceneIndex + 1);
        }

        // gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("")
        
    }
}
