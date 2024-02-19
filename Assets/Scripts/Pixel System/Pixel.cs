using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pixel : MonoBehaviour
{
    Image m_Image;
    private Color m_Color;
    private int overlapCount;

    private void Awake() {
        m_Image = GetComponent<Image>();
        m_Color = m_Image.color;
        flipPixel(false);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void flipPixel(bool m_bool) {
        int boolInt = m_bool ? 1 : 0;
        m_Image.color = new Color(m_Color.r,m_Color.g,m_Color.b,boolInt);
    }

    private void UpdateCount(bool m_bool) {
        int boolInt = m_bool ? 1 : -1;
        overlapCount += boolInt;
        if(overlapCount == 0 && m_Image.color.a == 1) {
            flipPixel(false);
        }
        if(overlapCount > 0 && m_Image.color.a == 0) {
            flipPixel(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        UpdateCount(true);

    }

    private void OnTriggerExit2D(Collider2D collision) {
        UpdateCount(false);
    }
}
