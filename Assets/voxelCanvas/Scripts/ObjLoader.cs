using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ObjLoader : MonoBehaviour
{
    public TestMode testMode;
    public string objFileName = "Tetris";  // .obj 파일 이름

    void Update()
    {
        GameObject obj = GameObject.Find("BrushCube");
        Vector3 position = obj.transform.position;
        if (testMode.drawState == TestMode.DrawState.load && Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log($"{testMode.drawState}");
            string txtPath = Path.Combine(Application.streamingAssetsPath, objFileName + ".obj");
            string objText = File.ReadAllText(txtPath);
            GameObject loadedObject = ParseObjFile(objText);
            loadedObject.transform.position = position;
            loadedObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    GameObject ParseObjFile(string objText)
    {
        GameObject obj = new GameObject("ParsedObject");
        MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();
        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        string[] lines = objText.Split('\n');
        foreach (string line in lines)
        {
            string[] tokens = line.Trim().Split(' ');
            if (tokens.Length == 0)
                continue;

            if (tokens[0] == "v")
            {
                // 정점 정보 파싱
                float x = float.Parse(tokens[1]);
                float y = float.Parse(tokens[2]);
                float z = float.Parse(tokens[3]);
                vertices.Add(new Vector3(x, y, z));
                Debug.Log(vertices);
            }
            else if (tokens[0] == "f")
            {
                // 면 정보 파싱
                for (int i = 1; i < tokens.Length; i++)
                {
                    int vertexIndex = int.Parse(tokens[i]) - 1;
                    triangles.Add(vertexIndex);
                    Debug.Log(triangles);
                }
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        meshFilter.mesh = mesh;
        meshRenderer.material = new Material(Shader.Find("Standard"));

        return obj;
    }
}