using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelRow : MonoBehaviour
{
    public GameObject pixel;
    private GameObject[] pixels;
    // Start is called before the first frame update
    void Start()
    {
        int pixelSize = (int)pixel.GetComponent<RectTransform>().rect.width;
        int rowNumber = int.Parse(name.Remove(0,4));
        pixels = new GameObject[GetComponentInParent<SpawnPixels>().numberOfColumns];
        for(int i = 0; i < pixels.Length; i++) {
            GameObject m_pixel = Instantiate(pixel, transform);
            m_pixel.GetComponent<RectTransform>().position = new Vector3( (i * pixelSize) - (pixels.Length * pixelSize / 2f) + (pixelSize / 2f),transform.position.y,0);
            m_pixel.name = "Pixel (" + rowNumber + "," + i + ")";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
