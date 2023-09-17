using Microsoft.MixedReality.Toolkit.UX;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using Dummiesman;

public class GalleryObject : MonoBehaviour
{
    public static GalleryObject instance;
    private int idx = 2;
    public GameObject[] objTar;
    private bool sceneChanged = false;

    private string RootPath
    {
        get
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            return Application.dataPath;
#elif UNITY_ANDROID
            //return $"/storage/emulated/0/DCIM/{Application.productName}/";
            return Application.persistentDataPath;
#endif
        }
    }

    private string folderName = "ObjFolder";
    private string fileName = "voxel_obj";
    private string folderPath => $"{RootPath}/{folderName}";

    public Texture2D defaultTexture;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject objTarObj in objTar)
        {
            objTarObj.GetComponent<MeshRenderer>().enabled = false;
        }
        for (int i=0; i < 1; i++)
        {
            string default_obj = folderPath + "/" + $"Default_{i}.obj";
            GameObject loadedObject = new OBJLoader().Load(default_obj);
            MeshFilter mf = objTar[i].GetComponent<MeshFilter>();
            MeshRenderer mr = objTar[i].GetComponent<MeshRenderer>();
            MeshCollider mc = objTar[i].GetComponent<MeshCollider>();
            mf.mesh = loadedObject.GetComponentInChildren<MeshFilter>().mesh;
            mr.material = loadedObject.GetComponentInChildren<MeshRenderer>().material;
            mr.material.mainTexture = defaultTexture;
            mr.enabled = true;
            objTar[i].transform.localPosition = new Vector3(0f, 0.005f, -0.05f);
            Destroy(loadedObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "gallery")
        {
            if (sceneChanged)
            {
                List<GameObject> childs_ = new List<GameObject>();
                gameObject.GetChildGameObjects(childs_);
                foreach (GameObject child in childs_)
                {
                    if (child.activeSelf == false)
                        child.SetActive(true);
                }
                sceneChanged = false;
            }

            List<GameObject> childs = new List<GameObject>();
            gameObject.GetChildGameObjects(childs);

            GameObject cloneChunk = GameObject.Find("CloneChunk");
            if (cloneChunk == null)
                return;
            ChunkToObj cto = cloneChunk.GetComponent<ChunkToObj>();
            if (cto.IsUpdate)
            {
                //Instantiate(cloneChunk, objTar[idx].transform.position, Quaternion.identity);
                MeshFilter mf = objTar[idx].GetComponent<MeshFilter>();
                MeshRenderer mr = objTar[idx].GetComponent<MeshRenderer>();
                MeshCollider mc = objTar[idx].GetComponent<MeshCollider>();

                mf.mesh = cloneChunk.GetComponent<MeshFilter>().mesh;
                mr.material = cloneChunk.GetComponent<MeshRenderer>().material;
                mc.sharedMesh = cloneChunk.GetComponent<MeshCollider>().sharedMesh;
                mr.enabled = true;
                objTar[idx].transform.localPosition = new Vector3(0f, 0.005f, -0.05f);
                cto.IsUpdate = false;
                if (idx < 8)
                    idx++;
                else
                    idx = 0;
            }
        }
        else
        {
            if (!sceneChanged)
            {
                List<GameObject> childs = new List<GameObject>();
                gameObject.GetChildGameObjects(childs);
                foreach (GameObject child in childs)
                {
                    if (child.activeSelf == true)
                        child.SetActive(false);
                }
                sceneChanged = true;
            }
        }
    }
}
