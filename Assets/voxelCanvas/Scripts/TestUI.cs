using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestUI : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    TextMeshProUGUI text;

    public bool isDrawing = true;
    void Start()
    {
        text.text = "Drawing";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ModeBtn()
    {
        if (isDrawing)
            text.text = "Erasing";
        else
            text.text = "Drawing";
        isDrawing = !isDrawing;
    }
}
