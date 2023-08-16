using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Mesh;

public class ColorQuad : MonoBehaviour
{
    Mesh m;
    MeshFilter mf;

    List<Vector3> vertices = new List<Vector3>();
    List<Vector2> uvs = new List<Vector2>();
    List<int> triangles = new List<int>();
    public struct Tile { public int x; public int y; }
    const float tileSize = 0.0625f;

    [SerializeField] private int bx = 0;
    [SerializeField] private int by = 0;
    public float tempsizey = 0.1f;
    public float tempsizex = 0.1f;

    private int prevx;
    private int prevy;

    public Vector2 curPoint { get { return new Vector2(bx, by); }  set { bx = (int)value.x; by = (int)value.y; } }

    // Use this for initialization
    void Start()
    {
        Vector3 thisPos = transform.position;
        thisPos.x += tempsizex;
        thisPos.y += tempsizey;
        mf = GetComponent<MeshFilter>();
        mf.mesh.Clear();
        vertices.Add(new Vector3(thisPos.x - 0.5f, thisPos.y - 0.5f, 0f));
        vertices.Add(new Vector3(thisPos.x - 0.5f, thisPos.y + 0.5f, 0f));
        vertices.Add(new Vector3(thisPos.x + 0.5f, thisPos.y + 0.5f, 0f));
        vertices.Add(new Vector3(thisPos.x + 0.5f, thisPos.y - 0.5f, 0f));

        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 2);

        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 1);

        uvs.AddRange(FaceUVs(bx, by));

        mf.mesh.vertices = vertices.ToArray();
        mf.mesh.triangles = triangles.ToArray();
        mf.mesh.uv = uvs.ToArray();
        mf.mesh.RecalculateNormals();

        prevx = bx; prevy = by;
        //drawCircle();

        //gameObject.GetComponent<MeshCollider>().sharedMesh = m;
        //gameObject.GetComponent<MeshCollider>().convex = true;
        //AssetDatabase.CreateAsset(m, "Assets/smallCircle.asset");
    }

    private void Update()
    {
        if (prevx != bx || prevy != by)
        {
            prevx = bx; prevy = by;
            uvs.Clear();
            uvs.AddRange(FaceUVs(bx, by));
            mf.mesh.uv = uvs.ToArray();
            mf.mesh.RecalculateNormals();
        }
    }

    public virtual Vector2[] FaceUVs(int x, int y)
    {
        Vector2[] UVs = new Vector2[4];
        Tile tilePos;
        tilePos.x = x;
        tilePos.y = y;

        UVs[3] = new Vector2(tileSize * tilePos.x + tileSize,
            tileSize * tilePos.y);
        UVs[2] = new Vector2(tileSize * tilePos.x + tileSize,
            tileSize * tilePos.y + tileSize);
        UVs[1] = new Vector2(tileSize * tilePos.x,
            tileSize * tilePos.y + tileSize);
        UVs[0] = new Vector2(tileSize * tilePos.x,
            tileSize * tilePos.y);

        return UVs;
    }
}
