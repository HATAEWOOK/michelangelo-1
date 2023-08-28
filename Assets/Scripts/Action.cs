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

    public void HomeAction2()
    {
        SceneManager.LoadScene("home");
    }

    public void MakingZoneAction()
    {
        SceneManager.LoadScene("main");
    }

    public void MakingZoneActionAtHome()
    {
        TextureManager.instance.isFromHome = true;
        SceneManager.LoadScene("main");
    }

    public void MyGalleryAction()
    {
        SceneManager.LoadScene("gallery");
    }

    public void GalleryUIAction()
    {
       GameObject garrUI = GameObject.Find("GalleryUI");
       garrUI.SetActive(!garrUI.activeSelf);
    }

    public void PlayZoneAction()
    {
        SceneManager.LoadScene("play");
    }

    public void CameraZoneAction()
    {
        SceneManager.LoadScene("camera");
    }

    public void Test()
    {
        Debug.Log("Btn cliicked");
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
