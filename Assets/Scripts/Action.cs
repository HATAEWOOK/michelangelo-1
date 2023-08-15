using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
        GameObject[] menus = GameObject.FindGameObjectsWithTag("SubMenu");
        foreach (GameObject menu in menus) 
        {
            menu.SetActive(false);
        }
        
        this.gameObject.SetActive(true);

    }
}
