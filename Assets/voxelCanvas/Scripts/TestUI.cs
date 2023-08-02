using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestUI : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    TextMeshProUGUI drawText;
    [SerializeField]
    TextMeshProUGUI selectText;
    [SerializeField]
    TextMeshProUGUI saveText;
    [SerializeField]
    TextMeshProUGUI loadText;

    [SerializeField]
    TestMode testMode;
    void Start()
    {
        drawText.text = "Drawing";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawEraseBtn()
    {
        if (testMode.drawState != TestMode.DrawState.erase)
        {
            drawText.text = "Erase";
            testMode.drawState = TestMode.DrawState.erase;
        }
        else if (testMode.drawState != TestMode.DrawState.draw)
        {
            drawText.text = "Draw";
            testMode.drawState = TestMode.DrawState.draw;
        }
    }

    public void SelectBtn()
    {
        if (testMode.drawState != TestMode.DrawState.select)
        {
            selectText.text = "Select";
            testMode.drawState = TestMode.DrawState.select;
        }
        else if (testMode.drawState != TestMode.DrawState.deselect)
        {
            selectText.text = "Deselect";
            testMode.drawState = TestMode.DrawState.deselect;
        }
    }

    public void SaveBtn()
    {
        if (testMode.drawState != TestMode.DrawState.save)
        {
            saveText.text = "Save";
            testMode.drawState = TestMode.DrawState.save;
        }
    }
    public void LoadBtn()
    {
        if (testMode.drawState != TestMode.DrawState.load)
        {
            loadText.text = "Load";
            testMode.drawState = TestMode.DrawState.load;
        }
    }
}
