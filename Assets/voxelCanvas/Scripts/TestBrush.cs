using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.Mesh;

public class TestBrush : MonoBehaviour
{
    public VoxelCanvas voxelCanvas;

    [SerializeField]
    TestUI testUI;
    [SerializeField]
    TestMode testMode;
    [SerializeField]
    GameObject saveChunk;
    Vector3Int objectPosition;

    private bool isInputVisible = false;
    private string userInput = "";
    public static string savedText = "";

    // Start is called before the first frame update
    void Start()
    {
        transform.position = voxelCanvas.transform.localPosition;
        objectPosition = new Vector3Int(0, 0, 0);
        Debug.Log("로컬 좌표: " + transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tmp = voxelCanvas.transform.localPosition;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            objectPosition.z += 1;
            transform.position = voxelCanvas.transform.TransformPoint(objectPosition);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            objectPosition.z -= 1;
            transform.position = voxelCanvas.transform.TransformPoint(objectPosition);
        }
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

        if (testMode.drawState == TestMode.DrawState.draw && Input.GetKeyDown(KeyCode.Return))
        {
            Vector3 cubePosition = transform.position;
            voxelCanvas.SetBlock(objectPosition.x, objectPosition.y, objectPosition.z, new BlockFull());
            voxelCanvas.GetBlock(objectPosition.x, objectPosition.y, objectPosition.z).SetTiles(voxelCanvas.DrawWholeColor(objectPosition.x, objectPosition.y, objectPosition.z, voxelCanvas.drawColorX, voxelCanvas.drawColorY));
            Debug.Log($"{testMode.drawState}");
        }
        if (testMode.drawState == TestMode.DrawState.erase && Input.GetKeyDown(KeyCode.Return))
        {
            Vector3 cubePosition = transform.position;
            voxelCanvas.SetBlock(objectPosition.x, objectPosition.y, objectPosition.z, new BlockEmpty());
            Debug.Log($"{testMode.drawState}");
        }
        if (testMode.drawState == TestMode.DrawState.draw && Input.GetKeyDown(KeyCode.Return))
        {
            Vector3 cubePosition = transform.position;
            voxelCanvas.SetBlock(objectPosition.x, objectPosition.y, objectPosition.z, new BlockFull());
            voxelCanvas.GetBlock(objectPosition.x, objectPosition.y, objectPosition.z).SetTiles(voxelCanvas.DrawWholeColor(objectPosition.x, objectPosition.y, objectPosition.z, voxelCanvas.drawColorX, voxelCanvas.drawColorY));
            Debug.Log($"{testMode.drawState}");
        }
        if (testMode.drawState == TestMode.DrawState.select && Input.GetKeyDown(KeyCode.Return))
        {
            Vector3 cubePosition = transform.position;
            voxelCanvas.AddVoxelToSelection(objectPosition.x, objectPosition.y, objectPosition.z);
            Debug.Log($"{testMode.drawState}");
        }
        if (testMode.drawState == TestMode.DrawState.deselect && Input.GetKeyDown(KeyCode.Return))
        {
            Vector3 cubePosition = transform.position;
            voxelCanvas.DeselectVoxelToSelection(objectPosition.x, objectPosition.y, objectPosition.z);
            Debug.Log($"{testMode.drawState}");
        }
        if (testMode.drawState == TestMode.DrawState.save && Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log($"{testMode.drawState}");


            isInputVisible = !isInputVisible;
            if (!isInputVisible)
            {
                savedText = userInput;
                userInput = "";
            }


            Chunk.save = true;
            Debug.Log("Your model was Saved");
        }
        //if (testMode.drawState == TestMode.DrawState.load && Input.GetKeyDown(KeyCode.L))
        //{
        //    Debug.Log($"{testMode.drawState}");
        //    ObjLoader objLoader = new ObjLoader();

        //}
    }
    private void OnGUI()
    {
        if (isInputVisible)
        {
            userInput = GUI.TextField(new Rect(10, 10, 200, 20), userInput);

            if (GUI.Button(new Rect(220, 10, 100, 20), "Save"))
            {
                SaveStringData(userInput);
                isInputVisible = false;
            }

            if (GUI.Button(new Rect(330, 10, 100, 20), "Cancel"))
            {
                isInputVisible = false;
                userInput = "";
            }
        }
        else
        {
            if (GUI.Button(new Rect(10, 10, 100, 20), "Press S Key"))
            {
                isInputVisible = true;
            }
        }

        GUI.Label(new Rect(10, 50, 200, 20), "Saved Text: " + savedText);
    }
    private void SaveStringData(string data)
    {
        // 입력한 텍스트를 저장
        savedText = data;
    }
}
