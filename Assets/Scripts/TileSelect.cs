using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UX;

// the tile selection UI

public class TileSelect : MonoBehaviour
{

    // the tile button and canvas
    private VoxelCanvas voxelCanvas;
    private Brushes brushes;
    private PressableButton button;
    private ColorQuad cq;

    private Vector2 point;
    private int bx;
    private int by;

    //[SerializeField]
    //private GameObject CanvasInfo;

    // Use this for initialization
    void Start()
    {
        voxelCanvas = GameObject.Find("VoxelCanvas").GetComponent<VoxelCanvas>();
        brushes = GameObject.Find("Brushes").GetComponent<Brushes>();
        button = GetComponentInParent<PressableButton>();
        cq = this.GetComponent<ColorQuad>();
        point = cq.curPoint;
        button.OnClicked.AddListener(() => PointUpdate());
        button.OnClicked.AddListener(() => TileOnClick((int)point.x, (int)point.y));
        button.OnClicked.AddListener(() => voxelCanvas.FillSelection());
        button.OnClicked.AddListener(() => voxelCanvas.ClearSelection());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PointUpdate()
    {
        point = cq.curPoint;
    }

    void TileOnClick(int x, int y)
    {
        Debug.Log("You have clicked the button! " + x + ", " + y);
        //voxelCanvas.drawColorX = x;
        //voxelCanvas.drawColorY = y;
        voxelCanvas.DrawColors = new int[2] { x, y };
        brushes.ChangeBrushColor();
        //CanvasInfo.GetComponent<HUD>().ChangeDisplayCube();
    }
}
