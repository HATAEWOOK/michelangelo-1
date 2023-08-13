using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush
{
    public void draw(Vector3Int targetPositionInt, VoxelCanvas voxelCanvas)
    {
        voxelCanvas.SetBlock(targetPositionInt.x, targetPositionInt.y, targetPositionInt.z, new BlockFull());
        voxelCanvas.GetBlock(targetPositionInt.x, targetPositionInt.y, targetPositionInt.z).SetTiles(voxelCanvas.DrawWholeColor(targetPositionInt.x, targetPositionInt.y, targetPositionInt.z, 0, 0));
    }
}
