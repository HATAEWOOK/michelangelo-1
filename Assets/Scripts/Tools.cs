using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{
    [SerializeField]
    private VoxelCanvas voxelCanvas;

    [SerializeField]
    private Brushes brushes;

    private Vector3 targetPosition;
    private Vector3Int targetPositionInt;
    private bool isPinching = false;

    [SerializeField]
    private float pinchTreshhold = 0.8f;

    [SerializeField]
    Mode mode;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isPinching)
        {
            if (mode.brushType == Mode.BrushType.cube)
                brushes.DrawInCube(targetPosition, brushes.brushObjects[(int)mode.brushType].transform.lossyScale.x, brushes.brushObjects[(int)mode.brushType].transform.lossyScale.y, brushes.brushObjects[(int)mode.brushType].transform.lossyScale.z);
            else if (mode.brushType == Mode.BrushType.sphere)
                brushes.DrawInSphere(targetPosition, (brushes.brushObjects[(int)mode.brushType].transform.lossyScale.x / 2) / voxelCanvas.transform.lossyScale.x);
        }
                

        /*
        else if (mode.drawMode == Mode.DrawMode.erase)
        {
            if (isPinching)
                brushTool.draw(targetPositionInt, voxelCanvas);
        }
        */
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
