using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
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

    [SerializeField]
    ChunkToObj cto;

    public void SsizeBtnClicked()
    {
        voxelCanvas.chunkDimension = sSize;
        if (!cto.IsUpdate)
        {
            Debug.Log("isUpdate false");
            voxelCanvas.Generate();
        }
        else
        {
            Debug.Log("isUpdate true");
            voxelCanvas.transform.position = new Vector3(0, 0, 0);
            voxelCanvas.transform.rotation = Quaternion.identity;
            voxelCanvas.transform.localScale = new Vector3(1, 1, 1);
            voxelCanvas.Generate();
        }
            
        //voxelCanvas.Generate();
        BoxCollider box = voxelCanvas.GetComponent<BoxCollider>();
        if (box == null )
        {
            box = voxelCanvas.gameObject.AddComponent<BoxCollider>();
            box.size = new Vector3(2 * sSize, 2 * sSize, 2 * sSize);
            box.center = new Vector3(sSize, sSize, sSize);
        }
        else
        {
            box.size = new Vector3(2 * sSize, 2 * sSize, 2 * sSize);
            box.center = new Vector3(sSize, sSize, sSize);
        }

        canvasManipulate = voxelCanvas.GetComponent<ObjectManipulator>();
        if(canvasManipulate == null)
        {
            canvasManipulate = voxelCanvas.gameObject.AddComponent<ObjectManipulator>();
        }
        
        canvasManipulate.selectMode = InteractableSelectMode.Multiple;
        foreach (CanvasGrid cg in canvasGrid)
        {
            if (!cg.isInitialized)
                cg.Generate();
        }
    }

    public void MsizeBtnClicked()
    {
        voxelCanvas.chunkDimension = mSize;
        if (!cto.IsUpdate)
        {
            Debug.Log("isUpdate false");
            voxelCanvas.Generate();
        }
        else
        {
            Debug.Log("isUpdate true");
            voxelCanvas.transform.position = new Vector3(0, 0, 0);
            voxelCanvas.transform.rotation = Quaternion.identity;
            voxelCanvas.transform.localScale = new Vector3(1, 1, 1);
            voxelCanvas.Generate();
        }
        //voxelCanvas.Generate();
        BoxCollider box = voxelCanvas.GetComponent<BoxCollider>();
        if (box == null)
        {
            box = voxelCanvas.gameObject.AddComponent<BoxCollider>();
            box.size = new Vector3(2 * sSize, 2 * sSize, 2 * sSize);
            box.center = new Vector3(sSize, sSize, sSize);
        }
        else
        {
            box.size = new Vector3(2 * sSize, 2 * sSize, 2 * sSize);
            box.center = new Vector3(sSize, sSize, sSize);
        }
        canvasManipulate = voxelCanvas.GetComponent<ObjectManipulator>();
        if (canvasManipulate == null)
        {
            canvasManipulate = voxelCanvas.gameObject.AddComponent<ObjectManipulator>();
        }
        canvasManipulate.selectMode = InteractableSelectMode.Multiple;
        foreach (CanvasGrid cg in canvasGrid)
        {
            if (!cg.isInitialized)
                cg.Generate();
        }
    }

    public void LsizeBtnClicked()
    {
        voxelCanvas.chunkDimension = lSize;
        if (!cto.IsUpdate)
        {
            Debug.Log("isUpdate false");
            voxelCanvas.Generate();
        }
        else
        {
            Debug.Log("isUpdate true");
            voxelCanvas.transform.position = new Vector3(0, 0, 0);
            voxelCanvas.transform.rotation = Quaternion.identity;
            voxelCanvas.transform.localScale = new Vector3(1, 1, 1);
            voxelCanvas.Generate();
        }
        //voxelCanvas.Generate();
        BoxCollider box = voxelCanvas.GetComponent<BoxCollider>();
        if (box == null)
        {
            box = voxelCanvas.gameObject.AddComponent<BoxCollider>();
            box.size = new Vector3(2 * sSize, 2 * sSize, 2 * sSize);
            box.center = new Vector3(sSize, sSize, sSize);
        }
        else
        {
            box.size = new Vector3(2 * sSize, 2 * sSize, 2 * sSize);
            box.center = new Vector3(sSize, sSize, sSize);
        }
        canvasManipulate = voxelCanvas.GetComponent<ObjectManipulator>();
        if (canvasManipulate == null)
        {
            canvasManipulate = voxelCanvas.gameObject.AddComponent<ObjectManipulator>();
        }
        canvasManipulate.selectMode = InteractableSelectMode.Multiple;
        foreach (CanvasGrid cg in canvasGrid)
        {
            if (!cg.isInitialized)
                cg.Generate();
        }
    }
}
