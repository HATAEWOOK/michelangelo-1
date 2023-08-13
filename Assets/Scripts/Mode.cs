using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using Microsoft.MixedReality.Toolkit.UX;

public class Mode : MonoBehaviour
{
    [SerializeField]
    VoxelCanvas voxelCanvas;

    [SerializeField]
    FontIconSelector fontIconSelector;
    public enum BrushType
    {
        cube,
        sphere
    }
    public enum DrawMode
    {
        none,
        draw,
        erase
    }

    public enum SelectMode
    {
        none,
        select,
        deselect
    }
    public enum MenuMode
    {
        draw,
        erase,
        select,
        canvas,
        none
    }

    private bool canvasEditMode = true;
    public DrawMode drawMode = DrawMode.none;
    public SelectMode selectMode = SelectMode.none;
    public BrushType brushType = BrushType.cube;
    public MenuMode menuMode = MenuMode.none;

    public void DrawBtnClicked()
    {
        drawMode = DrawMode.draw;
        selectMode = SelectMode.none;
        menuMode = MenuMode.draw;

        canvasEditMode = false;
        voxelCanvas.gameObject.GetComponent<ObjectManipulator>().enabled = false;
        fontIconSelector.CurrentIconName = "Icon 17";
    }

    public void EraseBtnClicked()
    {
        drawMode = DrawMode.erase;
        selectMode = SelectMode.none;
        menuMode = MenuMode.erase;


        canvasEditMode = false;
        voxelCanvas.gameObject.GetComponent<ObjectManipulator>().enabled = false;
        fontIconSelector.CurrentIconName = "Icon 17";
    }

    public void SelectBtnClicked()
    {
        drawMode = DrawMode.none;
        selectMode = SelectMode.select;
        menuMode = MenuMode.select;


        canvasEditMode = false;
        voxelCanvas.gameObject.GetComponent<ObjectManipulator>().enabled = false;
        fontIconSelector.CurrentIconName = "Icon 17";
    }

    public void DeselectBtnClicked()
    {
        drawMode = DrawMode.none;
        selectMode = SelectMode.deselect;
    }

    public void CanvasBtnClicked()
    {
        drawMode = DrawMode.none;
        selectMode = SelectMode.none;
        menuMode = MenuMode.none;

        canvasEditMode = !canvasEditMode;
        voxelCanvas.gameObject.GetComponent<ObjectManipulator>().enabled = canvasEditMode;

        if(canvasEditMode)
            fontIconSelector.CurrentIconName = "Icon 18";
        else
            fontIconSelector.CurrentIconName = "Icon 17";
    }
}
