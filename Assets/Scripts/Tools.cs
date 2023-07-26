using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{
    [SerializeField]
    private VoxelCanvas voxelCanvas;

    private Brush brushTool = new Brush();
    private Eraser eraseTool = new Eraser();

    private Vector3 targetPosition;
    private Vector3Int targetPositionInt;
    private bool isPinching = false;

    [SerializeField]
    private float pinchTreshhold = 0.8f;

    private enum mode
    {
        Brush,
        Eraser
    };

    [SerializeField]
    private mode selectMode = mode.Brush;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selectMode == mode.Eraser)
        {
            if (isPinching)
                eraseTool.erase(targetPositionInt, voxelCanvas);
        }
        else if (selectMode == mode.Brush)
        {
            if (isPinching)
                brushTool.draw(targetPositionInt, voxelCanvas);
        }
    }

    public void setTargetValue(Vector3 getPosition, float pinchAmount)
    {
        targetPosition = voxelCanvas.transform.InverseTransformPoint(getPosition);
        targetPositionInt = Vector3Int.FloorToInt(targetPosition);
        //Debug.Log("[hatw] targetPosition: " + targetPosition);
        if (pinchAmount > pinchTreshhold)
            isPinching = true;
        else
            isPinching = false;
    }
}
