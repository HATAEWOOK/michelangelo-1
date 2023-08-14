using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BrushSizeAction : MonoBehaviour
{
    private bool plusSize = false;
    private bool minusSize = false;
    private string sizeValue;
    private float sizeValueFloat;
    private float prevValue;


    private float stepSize = 0.1f;

    private int maximumSize = 8;

    private int minimumSize = 1;
    private Brushes br;

    public float SizeValueFloat { get => sizeValueFloat; set => sizeValueFloat = value; }

    // Start is called before the first frame update
    void Start()
    {
        //sizeValue = this.GetComponent<TextMeshPro>().text;
        //sizeValueFloat = Convert.ToSingle(sizeValue);
        this.GetComponent<TextMeshPro>().text = ((int)sizeValueFloat).ToString();
        prevValue = sizeValueFloat;
        br = GameObject.Find("Brushes").GetComponent<Brushes>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (plusSize)
        //{
        //    if ((int)sizeValueFloat < maximumSize)
        //    {
        //        sizeValueFloat += stepSize;
        //    }
        //}

        //if (minusSize)
        //{
        //    if ((int)sizeValueFloat > minimumSize)
        //    {
        //        sizeValueFloat -= stepSize;
        //    }
        //}

        //if (prevValue != sizeValueFloat)
        //{
        //    br.OnScaleChanged();
        //    prevValue = sizeValueFloat;
        //}
        this.GetComponent<TextMeshPro>().text = ((int)sizeValueFloat).ToString();
    }

    public void OnPlusClicked()
    {
        if ((int)sizeValueFloat < maximumSize)
        {
            sizeValueFloat += 1;
            br.OnScaleChanged();
        }
    }

    public void OnMinusClicked()
    {
        if ((int)sizeValueFloat > minimumSize)
        {
            sizeValueFloat -= 1;
            br.OnScaleChanged();
        }
    }

    public void OnPlusEnter()
    {
        plusSize = true;
    }

    public void OnPlusExit()
    {
        plusSize = false;
    }

    public void OnMinusEnter()
    {
        minusSize = true;
    }

    public void OnMinusExit()
    {
        minusSize = false;
    }
}
