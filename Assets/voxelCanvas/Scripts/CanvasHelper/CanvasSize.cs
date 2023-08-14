using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class CanvasSize : MonoBehaviour
{
    [SerializeField]
    VoxelCanvas voxelCanvas;

    [SerializeField]
    List<CanvasGrid> canvasGrid;

    private int sSize = 16;
    private int mSize = 32;
    private int lSize = 64;

    public ObjectManipulator canvasManipulate;

    public void SsizeBtnClicked()
    {
        voxelCanvas.chunkDimension = sSize;
        voxelCanvas.Generate();
        BoxCollider box = voxelCanvas.gameObject.AddComponent<BoxCollider>();
        box.size = new Vector3(2 * sSize, 2 * sSize, 2 * sSize);
        box.center = new Vector3(sSize, sSize, sSize);
        canvasManipulate = voxelCanvas.gameObject.AddComponent<ObjectManipulator>();
        canvasManipulate.selectMode = InteractableSelectMode.Multiple;
        canvasGrid[0].Generate();
        canvasGrid[1].Generate();
        canvasGrid[2].Generate();
    }

    public void MsizeBtnClicked()
    {
        voxelCanvas.chunkDimension = mSize;
        voxelCanvas.Generate();
        BoxCollider box = voxelCanvas.gameObject.AddComponent<BoxCollider>();
        box.size = new Vector3(2 * mSize, 2 * mSize, 2 * mSize);
        box.center = new Vector3(mSize, mSize, mSize);
        canvasManipulate = voxelCanvas.gameObject.AddComponent<ObjectManipulator>();
        canvasManipulate.selectMode = InteractableSelectMode.Multiple;
        canvasGrid[0].Generate();
        canvasGrid[1].Generate();
        canvasGrid[2].Generate();
    }

    public void LsizeBtnClicked()
    {
        voxelCanvas.chunkDimension = lSize;
        voxelCanvas.Generate();
        BoxCollider box = voxelCanvas.gameObject.AddComponent<BoxCollider>();
        box.size = new Vector3(2 * lSize, 2 * lSize, 2 * lSize);
        box.center = new Vector3(lSize, lSize, lSize);
        canvasManipulate = voxelCanvas.gameObject.AddComponent<ObjectManipulator>();
        canvasManipulate.selectMode = InteractableSelectMode.Multiple;
        canvasGrid[0].Generate();
        canvasGrid[1].Generate();
        canvasGrid[2].Generate();
    }
}
