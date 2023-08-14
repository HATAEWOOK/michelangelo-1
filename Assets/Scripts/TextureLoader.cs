using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextureLoader : MonoBehaviour
{
    private GameObject[] textureTargetObj;
    [SerializeField]
    private GameObject sceneObj;

    //private void Awake()
    //{
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    textureTargetObj = GameObject.FindGameObjectsWithTag("TextureTarget");
    //    Debug.Log(textureTargetObj.Length); 
    //    Texture2D modifiedTexture = TextureManager.instance.DefaultTexture;
    //    foreach (GameObject texObj in textureTargetObj)
    //    {
    //        SpriteRenderer sr = texObj.GetComponent<SpriteRenderer>();
    //        MeshRenderer mr = texObj.GetComponent<MeshRenderer>();

    //        if (sr != null)
    //        {
    //            Rect rect = new Rect(0, 0, modifiedTexture.width, modifiedTexture.height);
    //            Sprite sprite = Sprite.Create(modifiedTexture, rect, new Vector2(0.5f, 0.5f));
    //            sr.sprite = sprite;
    //        }

    //        if (mr != null)
    //        {
    //            Material mat = new Material(Shader.Find("Standard"));
    //            mat.mainTexture = modifiedTexture;
    //            mr.material = mat;
    //        }
    //    }

    //    if (!sceneObj.activeSelf)
    //        sceneObj.SetActive(true);
    //}

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
