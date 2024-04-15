using UnityEngine;
// using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.TextCore.Text;
[RequireComponent(typeof(SpriteRenderer))]

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
        
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        
    }
}
