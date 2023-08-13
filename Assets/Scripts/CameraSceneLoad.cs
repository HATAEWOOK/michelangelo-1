using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSceneLoad : MonoBehaviour
{
    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("camera");
    }
}
