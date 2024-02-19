using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPixels : MonoBehaviour
{
    public int numberOfRows = 48;
    public int numberOfColumns = 84;
    public GameObject rowPrefab;
    private GameObject[] rows;
    private int pixelSize;

    private void Awake() {
        float pixelWidth = rowPrefab.GetComponent<PixelRow>().pixel.GetComponent<RectTransform>().rect.width;
        float pixelHeight = rowPrefab.GetComponent<PixelRow>().pixel.GetComponent<RectTransform>().rect.height;
        if(pixelWidth != pixelHeight) {
            Debug.LogError("pixel prefab does not have square dimensions");
        }
        if(pixelWidth % 1 != 0 || pixelHeight % 1 != 0) {
            Debug.LogError("pixel prefab dimensions are not integers");
        }
        pixelSize = (int)rowPrefab.GetComponent<PixelRow>().pixel.GetComponent<RectTransform>().rect.width;
    }

    // Start is called before the first frame update
    void Start()
    {
        rows = new GameObject[numberOfRows];
        for(int i = 0; i < rows.Length; i++) {
            GameObject row = Instantiate(rowPrefab,transform);
            row.name = "Row " + i;
            row.GetComponent<RectTransform>().localPosition = new Vector3(0,(numberOfRows * pixelSize / 2f) - (pixelSize / 2f)-(i*pixelSize),0);
            rows[i] = row;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
