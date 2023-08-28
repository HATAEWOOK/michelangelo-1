using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextureLoader : MonoBehaviour
{
    private GameObject[] textureTargetObj;
    [SerializeField]
    private GameObject voxelCanvas;
    [SerializeField]
    private GameObject ui;
    [SerializeField]
    private GameObject brushes;
    [SerializeField]
    private GameObject cloneChunk;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "main")
        {
            voxelCanvas.SetActive(true);
            if (!TextureManager.instance.isUpdate)
            {
                voxelCanvas.GetComponent<VoxelCanvas>().ResetCanvas();
                voxelCanvas.transform.position = new Vector3(0, 0, 0);
                voxelCanvas.transform.rotation = Quaternion.identity;
                voxelCanvas.transform.localScale = new Vector3(1, 1, 1);
            }
            ui.SetActive(true);
            brushes.SetActive(true);
            cloneChunk.SetActive(true);
        }
    }

    public void OnSceneLoaded()
    {
        textureTargetObj = GameObject.FindGameObjectsWithTag("TextureTarget");
        Debug.Log(textureTargetObj.Length);
        Texture2D modifiedTexture = TextureManager.instance.DefaultTexture;
        foreach (GameObject texObj in textureTargetObj)
        {
            SpriteRenderer sr = texObj.GetComponent<SpriteRenderer>();
            MeshRenderer mr = texObj.GetComponent<MeshRenderer>();

            if (sr != null)
            {
                Rect rect = new Rect(0, 0, modifiedTexture.width, modifiedTexture.height);
                Sprite sprite = Sprite.Create(modifiedTexture, rect, new Vector2(0.5f, 0.5f));
                sr.sprite = sprite;
            }

            if (mr != null)
            {
                Material mat = new Material(Shader.Find("Standard"));
                mat.mainTexture = modifiedTexture;
                mr.material = mat;
            }
        }
    }

    public void OnSceneLoadedReset()
    {
        textureTargetObj = GameObject.FindGameObjectsWithTag("TextureTarget");
        Debug.Log(textureTargetObj.Length);
        Texture2D originTexture = TextureManager.instance.OriginTexture;
        foreach (GameObject texObj in textureTargetObj)
        {
            SpriteRenderer sr = texObj.GetComponent<SpriteRenderer>();
            MeshRenderer mr = texObj.GetComponent<MeshRenderer>();

            if (sr != null)
            {
                Rect rect = new Rect(0, 0, originTexture.width, originTexture.height);
                Sprite sprite = Sprite.Create(originTexture, rect, new Vector2(0.5f, 0.5f));
                sr.sprite = sprite;
            }

            if (mr != null)
            {
                Material mat = new Material(Shader.Find("Standard"));
                mat.mainTexture = originTexture;
                mr.material = mat;
            }
        }
    }

    private void Update()
    {
        if (TextureManager.instance.isUpdate)
        {
            if (!TextureManager.instance.sceneObj.activeSelf)
                TextureManager.instance.sceneObj.SetActive(true);
            OnSceneLoaded();
            TextureManager.instance.isUpdate = false;
        }
        if (TextureManager.instance.isFromHome) 
        {
            if (!TextureManager.instance.sceneObj.activeSelf)
                TextureManager.instance.sceneObj.SetActive(true);
            OnSceneLoadedReset();
            TextureManager.instance.isFromHome = false;
        }
    }
}
