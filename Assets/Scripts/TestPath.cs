using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TestPath : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer obj2;
    private Texture2D input;

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

    public string folderName = "ScreenShots";
    public string fileName = "MyScreenShot";
    public string extName = "png";
    private string FolderPath => $"{RootPath}/{folderName}";
    private string TotalPath => $"{FolderPath}/{fileName}_{DateTime.Now.ToString("MMdd_HHmmss")}.{extName}";

    private Texture2D texture;
    // Start is called before the first frame update
    void Start()
    {
        saveImg();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void saveImg()
    {
        string totalPath = TotalPath;
        input = LoadPNG(FolderPath + "/Image001.png");
        Rect rect = new Rect(0, 0, input.width, input.height);
        Sprite sprite = Sprite.Create(input, rect, new Vector2(0.5f, 0.5f));
        obj2.sprite = sprite;
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
