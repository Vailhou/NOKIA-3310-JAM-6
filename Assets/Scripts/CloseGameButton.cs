using UnityEngine;
using UnityEngine.UI;

public class CloseGameButton : MonoBehaviour
{  
    // Kenttä, johon vedetään Close-painike.
    public Button closeButton;

    // Vaihtaa scenen pelaajan liikkuessa siirtymäalueelle.
    private void ButtonOnClick()
    {
        closeButton.onClick.AddListener(DoOnClick);
    }

    private void DoOnClick()
    {
        Application.Quit();
    }
}