using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;


public class CreateObject : MonoBehaviour
{
    private const string serverURL = "http://localhost:5000/";
    public static Item scriptableObjectToUpload;
    public static GameObject prefabToUpload;
    public static void SaveAsAsset(GameObject gameObject)
    {
        string path = "Assets/Prefab/Saved/New Item.prefab";
        Object prefab = PrefabUtility.SaveAsPrefabAsset(gameObject, path);
        prefabToUpload = (GameObject)prefab;

        Item item = ScriptableObject.CreateInstance<Item>();
        item.itemName = "New Item";
        item.itemPrefab = prefabToUpload;
        scriptableObjectToUpload = item;

        AssetDatabase.CreateAsset(item, "Assets/Item/New Item.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = item;

        PrefabUtility.UnpackPrefabInstance(gameObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);

    }

    public void SendDataToServer()
    {
        string prefabData = JsonUtility.ToJson(prefabToUpload);
        string scriptableObjectData = JsonUtility.ToJson(scriptableObjectToUpload);
        StartCoroutine(SendDataCoroutine(prefabData, scriptableObjectData));
    }

    private IEnumerator SendDataCoroutine(string prefabData, string scriptableObjectData)
    {
        WWWForm form = new WWWForm();
        form.AddField("prefabData", prefabData);
        form.AddField("scriptableObjectData", scriptableObjectData);

        using (UnityWebRequest request = UnityWebRequest.Post(serverURL, form))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error sending data: " + request.error);
            }
            else
            {
                Debug.Log("Data sent successfully!");
            }
        }
    }
}