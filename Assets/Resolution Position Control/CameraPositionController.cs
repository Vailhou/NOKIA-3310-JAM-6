using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate() {
        transform.position = new Vector3(AdjustCoordinate(transform.position.x),AdjustCoordinate(transform.position.y),transform.position.z);
    }

    private float AdjustCoordinate(float coordinate) {
        coordinate += 0.5f;
        if(coordinate % 1 != 0) {
            coordinate = Mathf.Round(coordinate);
        }
        return coordinate-0.5f;
    }
}
