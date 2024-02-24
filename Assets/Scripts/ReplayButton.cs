using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReplayButton : MonoBehaviour
{  
    // Kenttä, johon syötetään seuraavan scenen ID.
    public string nextSceneName;

    // Aloittaa pelin 1. taosta.
    public void RestartGame()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}