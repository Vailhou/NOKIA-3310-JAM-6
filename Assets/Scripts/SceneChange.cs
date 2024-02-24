using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{  
    // Kenttä, johon syötetään seuraavan scenen ID.
    public string nextSceneName;
    public GameObject player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == player)
            SceneManager.LoadScene(nextSceneName);
    }
}