using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBrush : MonoBehaviour
{
    public VoxelCanvas voxelCanvas;

    [SerializeField]
    TestUI testUI;
    Vector3Int objectPosition;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = voxelCanvas.transform.localPosition;
        objectPosition = new Vector3Int(0, 0, 0);
        Debug.Log("·ÎÄÃ ÁÂÇ¥: " + transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tmp = voxelCanvas.transform.localPosition;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            objectPosition.x += 1;
            transform.position = voxelCanvas.transform.TransformPoint(objectPosition);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            objectPosition.x -= 1;
            transform.position = voxelCanvas.transform.TransformPoint(objectPosition);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            objectPosition.y += 1;
            transform.position = voxelCanvas.transform.TransformPoint(objectPosition);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            objectPosition.y -= 1;
            transform.position = voxelCanvas.transform.TransformPoint(objectPosition);
        }

        if (testUI.isDrawing && Input.GetKeyDown(KeyCode.Return))
        {
            Vector3 cubePosition = transform.position;
            voxelCanvas.SetBlock(objectPosition.x, objectPosition.y, objectPosition.z, new BlockFull());
            voxelCanvas.GetBlock(objectPosition.x, objectPosition.y, objectPosition.z).SetTiles(voxelCanvas.DrawWholeColor(objectPosition.x, objectPosition.y, objectPosition.z, 0, 0));
        }
        if (!testUI.isDrawing && Input.GetKeyDown(KeyCode.Return))
        {
            Vector3 cubePosition = transform.position;
            voxelCanvas.SetBlock(objectPosition.x, objectPosition.y, objectPosition.z, new BlockEmpty());
        }
    }
}
