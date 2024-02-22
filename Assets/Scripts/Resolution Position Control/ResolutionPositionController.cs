using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionPositionController : MonoBehaviour
{
    private void LateUpdate() {
        transform.position = new Vector3(AdjustCoordinate(transform.position.x),AdjustCoordinate(transform.position.y),transform.position.z);
    }

    private float AdjustCoordinate(float coordinate) {
        if(coordinate % 1!=0) {
            coordinate = Mathf.Round(coordinate);
        }
        return coordinate;
    }
}
