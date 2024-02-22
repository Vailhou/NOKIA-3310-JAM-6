using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class VideoSettings : MonoBehaviour
{

    private void Awake() {

    }
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 15;
        Screen.SetResolution(84,48,false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
