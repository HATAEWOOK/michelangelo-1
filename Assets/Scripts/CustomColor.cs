using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CustomColor: MonoBehaviour
{
    private string source_path = "/ColorTexture/TileTexture_colors.png";
    private string new_source_path = "/ColorTexture/TileTexture_colors.png";
    private string target_path = "/Image/Image001.png";
    private int row = 14;
    private int col = 0;


    private void Start()
    {
        
    }


    //Texture2D source_t2d = LoadPNG(Application.dataPath + source_path);
    //Texture2D target_t2d = LoadPNG(Application.dataPath + target_path);
    //source_mat = OpenCvSharp.Unity.TextureToMat(source_t2d);
    //    target_mat = OpenCvSharp.Unity.TextureToMat(target_t2d);

    public static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }
}
