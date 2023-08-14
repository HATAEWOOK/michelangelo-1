using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class test : MonoBehaviour
{
    private string RootPath
    {
        get
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            return Application.dataPath;
#elif UNITY_ANDROID
            return $"/storage/emulated/0/DCIM/{Application.productName}/";
            //return Application.persistentDataPath;
#endif
        }
    }

    [SerializeField]
    private RawImage cameraView;
    private Texture2D cameraTexture;

    [SerializeField]
    private SpriteRenderer totalOut;
    [SerializeField]
    private SpriteRenderer altOut;
    [SerializeField]
    private SpriteRenderer imgOut;
    [SerializeField]
    private SpriteRenderer cameraOut;

    public string folderName = "ScreenShots";
    public string fileName = "MyScreenShot";
    public string extName = "png";
    private string FolderPath => $"{RootPath}/{folderName}";
    private string TotalPath => $"{FolderPath}/screenshot.png";

    public void ProcImg()
    {
        Debug.Log("Button Pressed");
        string totalPath = TotalPath;
        string imgPath = $"{FolderPath}/Image001.png";

        Debug.Log($"TotalPath: {totalPath}");
        Debug.Log($"ImgPath: {imgPath}");

        cameraTexture = cameraView.texture as Texture2D;
        byte[] bytes = cameraTexture.EncodeToPNG();
        File.WriteAllBytes(totalPath, bytes);

        Rect cameraRect = new Rect(0, 0, cameraTexture.width, cameraTexture.height);
        Sprite cameraSprite = Sprite.Create(cameraTexture, cameraRect, new Vector2(0.5f, 0.5f));
        cameraOut.sprite = cameraSprite;
        

        if (File.Exists(imgPath))
        {
            Debug.Log("Imgpath exists");
            Debug.Log(imgPath);
            Texture2D imgTex = LoadPNG(imgPath);
            Rect imgRect = new Rect(0, 0, imgTex.width, imgTex.height);
            Sprite imgSprite = Sprite.Create(imgTex, imgRect, new Vector2(0.5f, 0.5f));
            imgOut.sprite = imgSprite;
        }
        else
        {
            Debug.Log("Imgpath does not exist");
            Debug.Log(imgPath);
        }

        if (File.Exists(totalPath))
        {
            Debug.Log("total path exists");
            Debug.Log(totalPath);
            Texture2D totalTex = LoadPNG(totalPath);
            Rect totalRect = new Rect(0, 0, totalTex.width, totalTex.height);
            Sprite totalSprite = Sprite.Create(totalTex, totalRect, new Vector2(0.5f, 0.5f));
            totalOut.sprite = totalSprite;
        }
        else
        {
            Debug.Log("total path does not exist");
            Debug.Log(totalPath);
        }        
    }

    public static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(1, 1, TextureFormat.RGB24, false);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }
}
