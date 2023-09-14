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

public class ChunkToObj : MonoBehaviour
{
    private bool isUpdate = false;
    public bool IsUpdate { get { return isUpdate; } set { isUpdate = value; } }
    public TextMeshProUGUI consoleText;
    private string consoleString;

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    public void HandleLog(string message, string stackTrace, LogType type)
    {
        consoleString = consoleString + "\n" + message;
        consoleText.text = consoleString;
    }
    public string serverURL = "http://43.201.109.250/getObjList";

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
    private string folderPath => $"/{RootPath}/{folderName}";
    private string TotalPath => $"/{folderPath}/{fileName}_{DateTime.Now.ToString("MMdd_HHmmss")}.obj";
    private string filePath => $"/storage/emulated/0/Android/data/com.Team.Michelangelo.MichelangeloGlasses/files/ObjFolder/{fileName}_{DateTime.Now.ToString("MMdd_HHmmss")}.obj";

    void Start()
    {
        if (Directory.Exists(folderPath) == false)
            Directory.CreateDirectory(folderPath);
        Debug.Log("stream" + TotalPath);
    }

    void Update()
    {

    }

    public void MergeChunk()
    {
        GameObject vc = GameObject.Find("VoxelCanvas");
        List<GameObject> chunks = new List<GameObject>();
        List<GameObject> _chunks = new List<GameObject>();
        vc.GetChildGameObjects(chunks);

        List<CombineInstance> combine = new List<CombineInstance>();

        MeshFilter[] meshFilters = vc.GetComponentsInChildren<MeshFilter>();
        foreach (GameObject chunk in chunks)
        {
            if (chunk.tag != "TextureTarget")
                continue;
            _chunks.Add(chunk);
            MeshFilter filter = chunk.GetComponent<MeshFilter>();
            CombineInstance ci = new CombineInstance();
            ci.mesh = filter.sharedMesh;
            ci.transform = filter.transform.localToWorldMatrix;
            combine.Add(ci);
        }

        //if (GetComponent<MeshFilter>() != null)
        //    Destroy(GetComponent<MeshFilter>());
        //if (GetComponent<MeshRenderer>() != null)
        //    Destroy(GetComponent<MeshRenderer>());
        //if (GetComponent<MeshCollider>() != null)
        //    Destroy(GetComponent<MeshCollider>());

        //Debug.Log(_chunks.Count);
        //MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        //MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();
        //MeshCollider mc = gameObject.AddComponent<MeshCollider>();
        MeshFilter mf = GetComponent<MeshFilter>();
        MeshRenderer mr = GetComponent<MeshRenderer>();
        MeshCollider mc = GetComponent<MeshCollider>();
        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine.ToArray());
        mf.mesh = mesh;
        mr.material = _chunks[0].GetComponent<MeshRenderer>().material;
        mc.sharedMesh = mesh;
        mr.enabled = false;

        //Save mesh to obj file
        vc.GetComponent<VoxelCanvas>().ResetCanvas();
        isUpdate = true;
        chunks.Clear();
        _chunks.Clear();
        combine.Clear();
        ObjExporter.SaveMeshToFile(mesh, mr, "Saved obj", TotalPath);
        Application.logMessageReceived += HandleLog;
        Debug.Log("hey");
        StartCoroutine(UploadFile());
        Debug.Log("Finished");
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
                Debug.Log("Upload complete!");
                Application.logMessageReceived += HandleLog;
            }
            else
            {
                Debug.Log("Error: " + www.error);
                Application.logMessageReceived += HandleLog;
            }
        }
    }
    public void SaveAsObjFile(MeshData meshData, string fileName)
    {
        string filePath = fileName;
        FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        using (StreamWriter sw = new StreamWriter(file))
        {
            foreach (Vector3 vertex in meshData.vertices)
            {
                sw.WriteLine("v " + vertex.x + " " + vertex.y + " " + vertex.z);
            }

            for (int i = 0; i < meshData.triangles.Count; i += 3)
            {
                int v1 = meshData.triangles[i] + 1;
                int v2 = meshData.triangles[i + 1] + 1;
                int v3 = meshData.triangles[i + 2] + 1;
                int uv1 = meshData.triangles[i] + 1;
                int uv2 = meshData.triangles[i + 1] + 1;
                int uv3 = meshData.triangles[i + 2] + 1;

                sw.WriteLine("f " + v1 + "/" + uv1 + " " + v2 + "/" + uv2 + " " + v3 + "/" + uv3);
            }
            sw.Close();
            file.Close();
            Debug.Log("Mesh data exported to: " + filePath);
        }
    }

    public static class ObjExporter
    {
        public static string MeshToString(Mesh mesh, MeshRenderer mr, string name)
        {
            Material[] mats = mr.sharedMaterials;
            StringBuilder sb = new StringBuilder();

            sb.Append($"# UnityEngine - Rito Mesh Editor\n");
            sb.Append($"# File Created : {DateTime.Now}\n");
            sb.Append("\n");

            sb.Append($"# {mesh.vertices.Length} Vertices\n");
            sb.Append($"# {mesh.normals.Length} Vertex Normals\n");
            sb.Append($"# {mesh.uv.Length} Texture Coordinates\n");
            sb.Append($"# {mesh.subMeshCount} Submeshes\n");
            sb.Append($"# {mesh.triangles.Length} Polygons\n");
            sb.Append("\n");

            // 1. Name
            sb.Append("g ").Append(name).Append("\n\n");

            // 2. Vertices
            foreach (Vector3 v in mesh.vertices)
            {
                // 유니티는 좌표계가 달라서 x 반전시켜야 함
                sb.Append(string.Format("v {0:F4} {1:F4} {2:F4}\n", -v.x, v.y, v.z));
            }
            sb.Append("\n");

            // 3. Normals
            foreach (Vector3 v in mesh.normals)
            {
                // x 반전
                sb.Append(string.Format("vn {0:F4} {1:F4} {2:F4}\n", -v.x, v.y, v.z));
            }
            sb.Append("\n");

            // 4. UVs
            foreach (Vector3 v in mesh.uv)
            {
                sb.Append(string.Format("vt {0:F4} {1:F4}\n", v.x, v.y));
            }

            // 5. Triangles
            for (int material = 0; material < mesh.subMeshCount; material++)
            {
                sb.Append("\n");
                sb.Append("usemtl ").Append(mats[material].name).Append("\n");
                sb.Append("usemap ").Append(mats[material].name).Append("\n");

                int[] triangles = mesh.GetTriangles(material);
                for (int i = 0; i < triangles.Length; i += 3)
                {
                    // x 반전
                    sb.Append(string.Format("f {1}/{1}/{1} {0}/{0}/{0} {2}/{2}/{2}\n",
                        triangles[i] + 1, triangles[i + 1] + 1, triangles[i + 2] + 1));
                }
            }
            return sb.ToString();
        }

        public static void SaveMeshToFile(Mesh mesh, MeshRenderer mr, string meshName, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter streamwriter = new StreamWriter(file);
            streamwriter.WriteLine(MeshToString(mesh, mr, meshName));
            streamwriter.Close();
            file.Close();
        }
    }
}

//public static void SaveAsAsset(GameObject gameObject)
//{
//    string path = "Assets/Prefab/Saved/New Item.prefab";
//    var prefab = PrefabUtility.SaveAsPrefabAsset(gameObject, path);
//    prefabToUpload = (GameObject)prefab;

//    Item item = ScriptableObject.CreateInstance<Item>();
//    item.itemName = "New Item";
//    item.itemPrefab = prefabToUpload;
//    scriptableObjectToUpload = item;

//    AssetDatabase.CreateAsset(item, "Assets/Item/New Item.asset");
//    AssetDatabase.SaveAssets();
//    AssetDatabase.Refresh();
//    EditorUtility.FocusProjectWindow();
//    Selection.activeObject = item;

//    PrefabUtility.UnpackPrefabInstance(gameObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);

//}

//public void SendDataToServer()
//{
//    string prefabData = JsonUtility.ToJson(prefabToUpload);
//    string scriptableObjectData = JsonUtility.ToJson(scriptableObjectToUpload);
//    StartCoroutine(SendDataCoroutine(prefabData, scriptableObjectData));
//}

//private IEnumerator SendDataCoroutine(string prefabData, string scriptableObjectData)
//{
//    WWWForm form = new WWWForm();
//    form.AddField("prefabData", prefabData);
//    form.AddField("scriptableObjectData", scriptableObjectData);

//    using (UnityWebRequest request = UnityWebRequest.Post(serverURL, form))
//    {
//        yield return request.SendWebRequest();

//        if (request.result != UnityWebRequest.Result.Success)
//        {
//            Debug.LogError("Error sending data: " + request.error);
//        }
//        else
//        {
//            Debug.Log("Data sent successfully!");
//        }
//    }
//}