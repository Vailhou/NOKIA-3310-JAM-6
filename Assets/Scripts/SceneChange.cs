using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{  
    // Kenttä, johon syötetään seuraavan scenen ID.
    public string nextSceneName;
    // Kenttä, johon vedetään scenen Player-objekti.
    public GameObject player;

    // Vaihtaa scenen pelaajan liikkuessa siirtymäalueelle.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        SceneManager.LoadScene(nextSceneName);
    }
}