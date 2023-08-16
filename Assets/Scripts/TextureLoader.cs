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
                Debug.Log(mr.material.name);
                Material mat = new Material(Shader.Find("Standard"));
                mat.mainTexture = modifiedTexture;
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
    }
}
