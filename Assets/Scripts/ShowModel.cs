using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UX;

public class ShowModel : MonoBehaviour
{
    [SerializeField]
    public VoxelCanvas voxelCanvas;

    [SerializeField]
    public List<GameObject> models;

    [SerializeField]
    public List<PressableButton> btns;

    private GameObject model;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("btn count: " + btns.Count);
        for(int i = 0; i<btns.Count;i++)
        {
            int buttonIndex = i;
            btns[i].OnClicked.AddListener(() => Btn0Clicked(buttonIndex));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Btn0Clicked(int i)
    {
        Debug.Log("btn index: " + i);
        Vector3 originPosition = voxelCanvas.gameObject.transform.localPosition;
        Debug.Log("origin pos: "+originPosition);
        Vector3 centerPosition = new Vector3(originPosition.x + voxelCanvas.chunkDimension, 0, originPosition.y + voxelCanvas.chunkDimension);

        Vector3 globalPosition = voxelCanvas.gameObject.transform.TransformPoint(centerPosition);
        //models[0].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        float scale = voxelCanvas.chunkDimension / 32;
        if (model != null)
            Destroy(model);
        model = Instantiate(models[i], globalPosition, voxelCanvas.gameObject.transform.rotation);
        model.transform.localScale = scale * model.transform.localScale;
        model.transform.parent = voxelCanvas.transform;
    }
    /*
    public void Btn1Clicked()
    {
        Vector3 originPosition = voxelCanvas.gameObject.transform.localPosition;
        Debug.Log("origin pos: " + originPosition);
        Vector3 centerPosition = new Vector3(originPosition.x + voxelCanvas.chunkDimension, 0, originPosition.y + voxelCanvas.chunkDimension);

        Vector3 globalPosition = voxelCanvas.gameObject.transform.TransformPoint(centerPosition);
        //models[0].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        GameObject model = Instantiate(models[0], globalPosition, Quaternion.identity);
        model.transform.parent = voxelCanvas.transform;
    }
    public void Btn2Clicked()
    {
        Vector3 originPosition = voxelCanvas.gameObject.transform.localPosition;
        Debug.Log("origin pos: " + originPosition);
        Vector3 centerPosition = new Vector3(originPosition.x + voxelCanvas.chunkDimension, 0, originPosition.y + voxelCanvas.chunkDimension);

        Vector3 globalPosition = voxelCanvas.gameObject.transform.TransformPoint(centerPosition);
        //models[0].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        GameObject model = Instantiate(models[0], globalPosition, Quaternion.identity);
        model.transform.parent = voxelCanvas.transform;
    }
    public void Btn3Clicked()
    {
        Vector3 originPosition = voxelCanvas.gameObject.transform.localPosition;
        Debug.Log("origin pos: " + originPosition);
        Vector3 centerPosition = new Vector3(originPosition.x + voxelCanvas.chunkDimension, 0, originPosition.y + voxelCanvas.chunkDimension);

        Vector3 globalPosition = voxelCanvas.gameObject.transform.TransformPoint(centerPosition);
        //models[0].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        GameObject model = Instantiate(models[0], globalPosition, Quaternion.identity);
        model.transform.parent = voxelCanvas.transform;
    }
    public void Btn4Clicked()
    {
        Vector3 originPosition = voxelCanvas.gameObject.transform.localPosition;
        Debug.Log("origin pos: " + originPosition);
        Vector3 centerPosition = new Vector3(originPosition.x + voxelCanvas.chunkDimension, 0, originPosition.y + voxelCanvas.chunkDimension);

        Vector3 globalPosition = voxelCanvas.gameObject.transform.TransformPoint(centerPosition);
        //models[0].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        GameObject model = Instantiate(models[0], globalPosition, Quaternion.identity);
        model.transform.parent = voxelCanvas.transform;
    }
    public void Btn5Clicked()
    {
        Vector3 originPosition = voxelCanvas.gameObject.transform.localPosition;
        Debug.Log("origin pos: " + originPosition);
        Vector3 centerPosition = new Vector3(originPosition.x + voxelCanvas.chunkDimension, 0, originPosition.y + voxelCanvas.chunkDimension);

        Vector3 globalPosition = voxelCanvas.gameObject.transform.TransformPoint(centerPosition);
        //models[0].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        GameObject model = Instantiate(models[0], globalPosition, Quaternion.identity);
        model.transform.parent = voxelCanvas.transform;
    }
    public void Btn6Clicked()
    {
        Vector3 originPosition = voxelCanvas.gameObject.transform.localPosition;
        Debug.Log("origin pos: " + originPosition);
        Vector3 centerPosition = new Vector3(originPosition.x + voxelCanvas.chunkDimension, 0, originPosition.y + voxelCanvas.chunkDimension);

        Vector3 globalPosition = voxelCanvas.gameObject.transform.TransformPoint(centerPosition);
        //models[0].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        GameObject model = Instantiate(models[0], globalPosition, Quaternion.identity);
        model.transform.parent = voxelCanvas.transform;
       

    }
    public void Btn7Clicked()
    {
        Vector3 originPosition = voxelCanvas.gameObject.transform.localPosition;
        Debug.Log("origin pos: " + originPosition);
        Vector3 centerPosition = new Vector3(originPosition.x + voxelCanvas.chunkDimension, 0, originPosition.y + voxelCanvas.chunkDimension);

        Vector3 globalPosition = voxelCanvas.gameObject.transform.TransformPoint(centerPosition);
        //models[0].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        GameObject model = Instantiate(models[0], globalPosition, Quaternion.identity);
        model.transform.parent = voxelCanvas.transform;
    }
    public void Btn8Clicked()
    {
        Vector3 originPosition = voxelCanvas.gameObject.transform.localPosition;
        Debug.Log("origin pos: " + originPosition);
        Vector3 centerPosition = new Vector3(originPosition.x + voxelCanvas.chunkDimension, 0, originPosition.y + voxelCanvas.chunkDimension);

        Vector3 globalPosition = voxelCanvas.gameObject.transform.TransformPoint(centerPosition);
        //models[0].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        GameObject model = Instantiate(models[0], globalPosition, Quaternion.identity);
        model.transform.parent = voxelCanvas.transform;
    }
    */
}
