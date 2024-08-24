using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player1")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

        }
    }
}
