using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class ChunkToObj : MonoBehaviour
{
    private bool isUpdate = false;
    public bool IsUpdate { get { return isUpdate; } set { isUpdate = value; } }

    private string RootPath
    {
        get
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            return Application.dataPath;
#elif UNITY_ANDROID
            return $"/storage/emulated/0/DCIM/{Application.productName}/";
            //return Application.persistentDataPath;
#endif
        }
    }

    private string folderName = "ObjFolder";
    private string fileName = "voxel_obj";
    private string folderPath => $"{RootPath}/{folderName}";
    private string TotalPath => $"{folderPath}/{fileName}_{DateTime.Now.ToString("MMdd_HHmmss")}.obj";

    // Start is called before the first frame update
    void Start()
    {
        if (Directory.Exists(folderPath) == false)
            Directory.CreateDirectory(folderPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MergeChunk()
    {
        GameObject vc = GameObject.Find("VoxelCanvas");
        List<GameObject> chunks = new List<GameObject>();
        vc.GetChildGameObjects(chunks);

        List<CombineInstance> combine = new List<CombineInstance>();

        MeshFilter[] meshFilters = vc.GetComponentsInChildren<MeshFilter>();
        foreach (GameObject chunk in chunks)
        {
            if (chunk.tag != "TextureTarget")
                continue;
            MeshFilter filter = chunk.GetComponent<MeshFilter>();
            CombineInstance ci = new CombineInstance();
            ci.mesh = filter.sharedMesh;
            ci.transform = filter.transform.localToWorldMatrix;
            combine.Add(ci);
        }

        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();
        MeshCollider mc = gameObject.AddComponent<MeshCollider>();
        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine.ToArray());
        mf.mesh = mesh;
        mr.material = chunks[0].GetComponent<MeshRenderer>().material;
        mc.sharedMesh = mesh;
        mr.enabled = false;

        //Save mesh to obj file
        MeshData meshData = new MeshData();
        meshData.vertices.AddRange(mesh.vertices);
        meshData.triangles.AddRange(mesh.triangles);
        meshData.uv.AddRange(mesh.uv);
        SaveAsObjFile(meshData, TotalPath);

        vc.GetComponent<VoxelCanvas>().ResetCanvas();
        isUpdate = true;
    }

    public void SaveAsObjFile(MeshData meshData, string fileName)
    {
        string filePath = fileName;
        using (StreamWriter sw = new StreamWriter(filePath))
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

            Debug.Log("Mesh data exported to: " + filePath);
        }
    }
}