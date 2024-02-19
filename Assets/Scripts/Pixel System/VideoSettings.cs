using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoSettings : MonoBehaviour
{

    private void Awake() {

    }
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 15;
        Screen.SetResolution(504,288,false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
