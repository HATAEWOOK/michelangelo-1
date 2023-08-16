using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UX;

public class ColorPickerEdit : MonoBehaviour
{
    private float bx = 0f;
    private float by = 0f;

    [SerializeField]
    private float stepSize = 0.32f;

    [SerializeField]
    private ColorQuad cq;

    RectTransform rt;

    private float prevX;
    private float prevY;

    private bool incX = false;
    private bool decX = false;
    private bool incY = false;
    private bool decY = false;

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(bx, by);
        prevX = bx;
        prevY = by;
    }

    // Update is called once per frame
    void Update()
    {
        if (incX)
        {
            if (bx < 0.32f*15)
            {
                bx += stepSize;
            }
        }

        if (decX)
        {
            if (bx > 0)
            {
                bx -= stepSize;
            }
        }

        if (incY)
        {
            if (by < 0.32f*15)
            {
                by += stepSize;
            }
        }

        if (decY)
        {
            if (by > 0)
            {
                by -= stepSize;
            }
        }

        if (bx != prevX || by != prevY)
        {
            Debug.Log((bx / 0.32f) * 0.32f);
            rt.anchoredPosition = new Vector2(((int)(bx / 0.32f))*0.32f, ((int)(by / 0.32f))*0.32f);
            cq.curPoint = new Vector2((bx / 0.32f), (by / 0.32f));
            prevX = bx;
            prevY = by;
        }
    }

    public void CameraColorApply()
    {
        bx = TextureManager.instance.row * 0.32f;
        by = TextureManager.instance.col * 0.32f;
    }

    public void PlusX()
    {
        if (bx < 0.32f * 15)
        {
            bx += 0.32f;
        }
    }

    public void MinusX()
    {
        if (bx > 0)
        {
            bx -= 0.32f;
        }
    }

    public void PlusY()
    {
        if (by < 0.32f * 15)
        {
            by += 0.32f;
        }
    }

    public void MinusY()
    {
        if (by > 0)
        {
            bx -= 0.32f;
        }
    }

    public void IncX()
    {
        if (incX)
        {
            incX = false;
        }
        else
        {
            incX = true;
        }
    }

    public void DecX()
    {
        if (decX)
        {
            decX = false;
        }
        else
        {
            decX = true;
        }
    }

    public void IncY()
    {
        if (incY)
        {
            incY = false;
        }
        else
        {
            incY = true;
        }
    }

    public void DecY()
    {
        if (decY)
        {
            decY = false;
        }
        else
        {
            decY = true;
        }
    }
}
