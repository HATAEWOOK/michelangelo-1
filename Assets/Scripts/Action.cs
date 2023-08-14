using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Action : MonoBehaviour
{
    public void HomeAction()
    {
        SceneManager.LoadScene("home");
    }

    public void MakingZoneAction()
    {
        SceneManager.LoadScene("main");
    }

    public void MyGalleryAction()
    {
        SceneManager.LoadScene("gallery");
    }

    public void PlayZoneAction()
    {
        SceneManager.LoadScene("play");
    }

    public void CameraZoneAction()
    {
        SceneManager.LoadScene("camera");
    }

    public void BtnCliicked()
    {
        if (this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
