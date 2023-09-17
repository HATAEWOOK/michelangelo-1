using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UX;


public class Brushes : MonoBehaviour
{
    public VoxelCanvas voxelCanvas;

    [SerializeField]
    public List<BrushSizeAction> brushSize;

    [SerializeField]
    public List<GameObject> brushObjects;


    [SerializeField]
    Mode mode;

    [SerializeField]
    Tools tools;


    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < brushObjects.Count; i++)
            brushObjects[i].SetActive(false);
        foreach (BrushSizeAction size in brushSize)
        {
            foreach (GameObject bobject in brushObjects)
                size.SizeValueFloat = bobject.transform.localScale.x * 100f;
        }
        ChangeBrushColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "VoxelCanvas" && mode.menuMode != Mode.MenuMode.none)
            for (int i = 0; i < brushObjects.Count; i++)
                if (i == (int)mode.brushType)
                    brushObjects[i].SetActive(true);
                else
                    brushObjects[i].SetActive(false);

    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "VoxelCanvas")
            for (int i = 0; i < brushObjects.Count; i++)
                brushObjects[i].SetActive(false);
    }

    public void BrushTypeCube()
    {
        mode.brushType = Mode.BrushType.cube;
        brushSize[(int)mode.menuMode].SizeValueFloat = brushObjects[(int)mode.brushType].transform.localScale.x * 100f;
        ChangeBrushColor();
    }

    public void BrushTypeSphere()
    {
        mode.brushType = Mode.BrushType.sphere;
        brushSize[(int)mode.menuMode].SizeValueFloat = brushObjects[(int)mode.brushType].transform.localScale.x * 100f;
        ChangeBrushColor();
    }


    public void OnScaleChanged()
    {
        Debug.Log("OnScaleChanged");
        if (mode.menuMode != Mode.MenuMode.none)
        {
            float scaleValue = brushSize[(int)mode.menuMode].SizeValueFloat / 100f;
            brushObjects[(int)mode.brushType].transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
        }
        
    }

    public void BrushSizeForCanvas(int canvasSize)
    {
        print("BrushSize");
        if (mode.menuMode != Mode.MenuMode.none)
        {
            print("BrushSize");
            float scaleValue = canvasSize / 100f;
            brushObjects[(int)mode.brushType].transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
            brushSize[(int)mode.menuMode].SizeValueFloat = canvasSize;
        }
    }


    public void DrawInCube(Vector3 brushPosition, float height, float width, float depth)
    {
        
        int[] pos = new int[] { (int)brushPosition.x, (int)brushPosition.y, (int)brushPosition.z };

        int halfx = (int)(0.5f * width / voxelCanvas.transform.lossyScale.x);
        int halfy = (int)(0.5f * height / voxelCanvas.transform.lossyScale.y);
        int halfz = (int)(0.5f * depth / voxelCanvas.transform.lossyScale.z);

        int xend = (int)(width / voxelCanvas.transform.lossyScale.x) - halfx;
        int yend = (int)(height / voxelCanvas.transform.lossyScale.y) - halfy;
        int zend = (int)(depth / voxelCanvas.transform.lossyScale.z) - halfz;



        for (int x = pos[0] - halfx; x < pos[0] + xend; x++)
        {
            for (int y = pos[1] - halfy; y < pos[1] + yend; y++)
            {
                for (int z = pos[2] - halfz; z < pos[2] + zend; z++)
                {
                    if (CheckSel(x, y, z) && (mode.drawMode == Mode.DrawMode.draw))
                    {
                        voxelCanvas.SetBlock(x, y, z, new BlockFull());
                        voxelCanvas.GetBlock(x, y, z).SetTiles(voxelCanvas.DrawWholeColor(x, y, z, voxelCanvas.DrawColors[0], voxelCanvas.DrawColors[1]));
                    }
                    else if (CheckSel(x, y, z) && (mode.drawMode == Mode.DrawMode.erase))
                    {
                        voxelCanvas.SetBlock(x, y, z, new BlockEmpty());
                    }
                    else if(mode.selectMode == Mode.SelectMode. select)
                    {
                        voxelCanvas.AddVoxelToSelection(x, y, z);
                    }
                    else if (mode.selectMode == Mode.SelectMode.deselect)
                    {
                        voxelCanvas.DeselectVoxelToSelection(x, y, z);
                    }
                }
            }
        }
    }

    public void DrawInSphere(Vector3 center, float radius)
    {
        int xend = voxelCanvas.GetComponent<VoxelCanvas>().VoxelCanvasDimensions[0] * voxelCanvas.GetComponent<VoxelCanvas>().chunkDimension;
        int yend = voxelCanvas.GetComponent<VoxelCanvas>().VoxelCanvasDimensions[1] * voxelCanvas.GetComponent<VoxelCanvas>().chunkDimension;
        int zend = voxelCanvas.GetComponent<VoxelCanvas>().VoxelCanvasDimensions[2] * voxelCanvas.GetComponent<VoxelCanvas>().chunkDimension;
        Vector3 c = center;

        for (int x = 0; x < xend; x++)
        {
            for (int y = 0; y < yend; y++)
            {
                for (int z = 0; z < zend; z++)
                {
                    if (Vector3.Distance(new Vector3(x, y, z), c) < radius)
                    {
                        if (mode.drawMode == Mode.DrawMode.draw && CheckSel(x, y, z))
                        {
                            voxelCanvas.GetComponent<VoxelCanvas>().SetBlock(x, y, z, new BlockFull());
                            voxelCanvas.GetComponent<VoxelCanvas>().GetBlock(x, y, z).SetTiles(voxelCanvas.GetComponent<VoxelCanvas>().DrawWholeColor(x, y, z, voxelCanvas.GetComponent<VoxelCanvas>().DrawColors[0], voxelCanvas.GetComponent<VoxelCanvas>().DrawColors[1]));
                        }
                        else if (mode.drawMode == Mode.DrawMode.erase && CheckSel(x, y, z))
                        {
                            voxelCanvas.GetComponent<VoxelCanvas>().SetBlock(x, y, z, new BlockEmpty());
                            //voxelCanvas.GetComponent<VoxelCanvas>().GetBlock(x, y, z).SetTiles(voxelCanvas.GetComponent<VoxelCanvas>().DrawWholeColor(x, y, z, voxelCanvas.GetComponent<VoxelCanvas>().DrawColors[0], voxelCanvas.GetComponent<VoxelCanvas>().DrawColors[1]));
                        }
                        if (mode.selectMode == Mode.SelectMode.select)
                        {
                            voxelCanvas.AddVoxelToSelection(x, y, z);
                        }
                        else if (mode.selectMode == Mode.SelectMode.deselect)
                        {
                            voxelCanvas.DeselectVoxelToSelection(x, y, z);
                        }
                    }
                }
            }
        }
    }

    //for checking for selections
    public bool CheckSel(int x, int y, int z)
    {
        if (voxelCanvas.SelectionListCount < 1)
        {
            return true;
        }
        return voxelCanvas.VoxelInSelection(x, y, z);
    }

    public void ChangeDisplayCube()
    {
        int[] drawCoord;
        if (mode.menuMode == Mode.MenuMode.select)
            drawCoord = new int[2] { 7, 4 };
        else
            drawCoord = voxelCanvas.DrawColors;
        float tileSize = voxelCanvas.GetBlock(0, 0, 0).TileSize;

        Mesh mesh = brushObjects[0].GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector2[] UVs = new Vector2[vertices.Length];

        for (int i = 0; i < UVs.Length - 3; i += 4)
        {
            UVs[i] = new Vector2(tileSize * drawCoord[0] + tileSize,
                tileSize * drawCoord[1]);
            UVs[i + 1] = new Vector2(tileSize * drawCoord[0] + tileSize,
                tileSize * drawCoord[1] + tileSize);
            UVs[i + 2] = new Vector2(tileSize * drawCoord[0],
                tileSize * drawCoord[1] + tileSize);
            UVs[i + 3] = new Vector2(tileSize * drawCoord[0],
                tileSize * drawCoord[1]);
        }

        brushObjects[0].GetComponent<MeshFilter>().mesh.uv = UVs;
        Debug.Log(drawCoord[0] + " " + drawCoord[1]);
    }

    public void ChangeDisplaySphere()
    {
        int[] drawCoord;
        if (mode.menuMode == Mode.MenuMode.select)
            drawCoord = new int[2] { 7, 4 };
        else
            drawCoord = voxelCanvas.DrawColors;
        float tileSize = voxelCanvas.GetBlock(0, 0, 0).TileSize;

        Mesh mesh = brushObjects[1].GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector2[] UVs = mesh.uv;

        for (int i = 0; i < UVs.Length; i++)
        {
            // 다음은 각 UV 포인트를 타일 내에 위치하도록 조정하는 예제입니다.
            // 실제로 원하는 UV 좌표 조정은 여기서 수행합니다.
            UVs[i] = new Vector2(UVs[i].x * tileSize + tileSize * drawCoord[0],
                                 UVs[i].y * tileSize + tileSize * drawCoord[1]);
        }

        brushObjects[1].GetComponent<MeshFilter>().mesh.uv = UVs;
        Debug.Log(drawCoord[0] + " " + drawCoord[1]);
    }


    public void ChangeBrushColor()
    {
        if (mode.brushType == Mode.BrushType.cube)
            ChangeDisplayCube();
        else if (mode.brushType == Mode.BrushType.sphere)
            ChangeDisplaySphere();
    }

}
