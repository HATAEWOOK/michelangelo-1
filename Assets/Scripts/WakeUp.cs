using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
public class WakeUp : MonoBehaviour
{
    public TextMeshProUGUI consoleText;
    private string consoleString;
    private string filePath => $"/storage/emulated/0/Android/data/com.Team.Michelangelo.MichelangeloGlasses/files/ObjFolder/voxel_obj_0914_200658.obj";
    public string serverURL = "http://43.201.109.250/getObjList";
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void wakeup()
    {
        Debug.Log("hey");
        StartCoroutine(UploadFile());
        Debug.Log("Finished");
    }
    void HandleLog(string message, string stackTrace, LogType type)
    {
        consoleString = consoleString + "\n" + message;
        consoleText.text = consoleString;
    }
    IEnumerator UploadFile()
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        //byte[] fileData = new System.Text.UTF8Encoding().GetBytes(myFile);
        string fileName = Path.GetFileName(filePath);
        //jsonFile = JsonUtility.ToJson(fileData);
        Application.logMessageReceived += HandleLog;
        UnityEngine.Debug.Log("Request");
        Application.logMessageReceived += HandleLog;
        using (UnityWebRequest www = new UnityWebRequest(serverURL, "POST"))
        {
            Application.logMessageReceived += HandleLog;
            www.uploadHandler = new UploadHandlerRaw(fileData);
            Application.logMessageReceived += HandleLog;
            www.downloadHandler = new DownloadHandlerBuffer();
            Application.logMessageReceived += HandleLog;
            // ���� �̸��� ���� ����
            www.SetRequestHeader("Content-Type", "application/octet-stream");
            www.SetRequestHeader("X-FileName", fileName);
            UnityEngine.Debug.Log("Sending");
            // ���ε� ����
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Upload completed!");
                Application.logMessageReceived += HandleLog;
            }
            else
            {
                Debug.Log("Error: " + www.error);
                Application.logMessageReceived += HandleLog;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
