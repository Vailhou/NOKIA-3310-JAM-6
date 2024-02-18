using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenVisibility : MonoBehaviour
{
    int invisibleToScreen = 7;
    int originalLayer;
    // Start is called before the first frame update
    void Start()
    {
        originalLayer = gameObject.layer;
        if(LayerMask.NameToLayer("Screen Invisible") != invisibleToScreen) {
            Debug.LogWarning("\"Screen Invisible\" layer is not matching in " + this);
            invisibleToScreen = LayerMask.NameToLayer("Screen Invisible");
        }
    }

    public void Visible(bool m_bool) {
        if(m_bool) {
            gameObject.layer = originalLayer;
        } else {
            gameObject.layer = invisibleToScreen;
        }
    }

    private void OnEnable() {
        Visible(true);
    }

    private void OnDisable() {
        Visible(false);
    }
}
