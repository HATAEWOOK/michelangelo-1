using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using OpenCvSharp;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CustomColor: MonoBehaviour
{
    private string source_path = "/ColorTexture/TileTexture_colors.png";
    private string new_source_path = "/ColorTexture/TileTexture_colors.png";
    private string target_path = "/Image/Image001.png";
    private Mat source_mat;
    private Mat target_mat;
    private int row = 14;
    private int col = 0;


    private void Start()
    {
        
    }


    //Texture2D source_t2d = LoadPNG(Application.dataPath + source_path);
    //Texture2D target_t2d = LoadPNG(Application.dataPath + target_path);
    //source_mat = OpenCvSharp.Unity.TextureToMat(source_t2d);
    //    target_mat = OpenCvSharp.Unity.TextureToMat(target_t2d);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ImgProc(row, col);
            if (col < 15)
            {
                col++;
            }
            else
            {
                col = 0;
                row++;
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene("michel_glss");
        }
    }

    public void ImgProc(int r, int c)
    {
        Size imgSize = target_mat.Size();
        int width = (int)imgSize.Width;
        int height = (int)imgSize.Height;
        OpenCvSharp.Rect roi = new OpenCvSharp.Rect(width / 2 - 32, height / 2 - 32, 64, 64);
        Mat croppedTarget = target_mat[roi];
        Cv2.Resize(croppedTarget, croppedTarget, new Size(32, 32));
        Texture2D croppedText = OpenCvSharp.Unity.MatToTexture(croppedTarget);
        byte[] bytes = croppedText.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/Image/Image002.png", bytes);

        OpenCvSharp.Rect textureRoi = new OpenCvSharp.Rect(r*32, c, 32, 32);
        Mat newSource = new Mat();
        source_mat.CopyTo(newSource);
        croppedTarget.CopyTo(newSource[textureRoi]);
        Texture2D newSourceText = OpenCvSharp.Unity.MatToTexture(newSource);
        byte[] newSourceBytes = newSourceText.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + new_source_path, newSourceBytes);
    }

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
