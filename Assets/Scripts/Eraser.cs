using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser
{
    public void erase(Vector3Int targetPositionInt, VoxelCanvas voxelCanvas)
    {
        voxelCanvas.SetBlock(targetPositionInt.x, targetPositionInt.y, targetPositionInt.z, new BlockEmpty());
    }
}
