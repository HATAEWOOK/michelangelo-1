using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureManager : MonoBehaviour
{
    public static TextureManager instance;
    [SerializeField]
    private Texture2D defaultTexture;
    private Texture2D coppiedTexture;
    public int row = 14;
    public int col = 0;
    public bool isUpdate = false;
    public bool isFromHome = false;
    public GameObject sceneObj;

    public Texture2D DefaultTexture
    {
        get
        {
            return coppiedTexture;
        }
        set
        {
            coppiedTexture = value;
        }
    }

    public Texture2D OriginTexture
    {
        get
        {
            return defaultTexture;
        }

    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);      
        
        coppiedTexture = new Texture2D(defaultTexture.width, defaultTexture.height);
        coppiedTexture.SetPixels(defaultTexture.GetPixels());
        coppiedTexture.Apply();

        sceneObj = GameObject.Find("Scene");
    }

    private void Update()
    {
        if (sceneObj == null)
            sceneObj = GameObject.Find("Scene");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
}
