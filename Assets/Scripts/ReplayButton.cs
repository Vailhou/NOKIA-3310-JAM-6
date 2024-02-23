using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReplayButton : MonoBehaviour
{  
    // Kenttä, johon syötetään seuraavan scenen ID.
    public string nextSceneName;
    // Kenttä, johon vedetään replay-painike.
    public Button replayButton;

    // Vaihtaa scenen pelaajan liikkuessa siirtymäalueelle.
    private void ButtonOnClick()
    {
        replayButton.onClick.AddListener(DoOnClick);
    }

    private void DoOnClick()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}