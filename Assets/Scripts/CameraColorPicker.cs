using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CameraColorPicker : MonoBehaviour
{
    public Texture2D cameraTexture;

    public void AdjustColorPallete()
    {
        Texture2D defaultTexture = TextureManager.instance.DefaultTexture;
        int r = TextureManager.instance.row;
        int c = TextureManager.instance.col;
        Texture2D crropedTexture = new Texture2D(64, 64);
        int width = (int)cameraTexture.width;
        int height = (int)cameraTexture.height;
        int rx = width / 2 - 32;
        int ry = height / 2 - 32;

        int cx = 0;
        int cy = 0;
        for (int x = rx; x < rx + 64; x++)
        {
            for (int y = ry; y < ry + 64; y++)
            {
                crropedTexture.SetPixel(cx, cy, cameraTexture.GetPixel(x, y));
                if (cy < 63)
                {
                    cy++;
                }
                else
                {
                    cy = 0;
                    cx  ++;
                }
            }
        }

        crropedTexture.Apply();
        Texture2D resizedTexture = ResizeTexture(crropedTexture, 32, 32);
        //crropedTexture.Reinitialize(32, 32, TextureFormat.RGBA32, true);

        int dx = 0;
        int dy = 0;

        for (int x = r*32; x < r*32 + 32; x++)
        {
            for (int y = c*32; y < c*32 + 32; y++)
            {
                defaultTexture.SetPixel(x, y, resizedTexture.GetPixel(dx, dy));
            }

            if (dy < 31)
            {
                dy++;
            }
            else
            {
                dy = 0;
                dx++;
            }
        }

        defaultTexture.Apply();

        TextureManager.instance.DefaultTexture = defaultTexture;
        if (c < 15)
        {
            TextureManager.instance.col++;
        }
        else
        {
            TextureManager.instance.col = 0;
            if (r < 15)
                TextureManager.instance.row++;
            else
                TextureManager.instance.row = 14;
        }
        TextureManager.instance.isUpdate = true;
    }

    Texture2D ResizeTexture(Texture2D tex, int w, int h)
    {
        Texture2D result = new Texture2D(100, 100, TextureFormat.RGBA32, true);
        Color[] rpixels = result.GetPixels(0);
        float incX = (1.0f / (float)100);
        float incY = (1.0f / (float)100);
        for (int px = 0; px < rpixels.Length; px++)
        {
            rpixels[px] = tex.GetPixelBilinear(incX * ((float)px % 100), incY * ((float)Mathf.Floor(px / 100)));
        }
        result.SetPixels(rpixels, 0);
        result.Apply();
        return result;
    }

}
