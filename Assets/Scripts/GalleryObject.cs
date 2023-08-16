using Microsoft.MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GalleryObject : MonoBehaviour
{
    private int idx = 0;
    GameObject[] objTar;

    // Start is called before the first frame update
    void Start()
    {
        objTar = GameObject.FindGameObjectsWithTag("ObjectTarget");
        foreach (GameObject objTarObj in objTar)
        {
            objTarObj.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject cloneChunk = GameObject.Find("CloneChunk");
        if (cloneChunk == null)
            return;
        ChunkToObj cto = cloneChunk.GetComponent<ChunkToObj>();
        if (cto.IsUpdate)
        {
            //Instantiate(cloneChunk, objTar[idx].transform.position, Quaternion.identity);
            MeshFilter mf = objTar[idx].GetComponent<MeshFilter>();
            MeshRenderer mr = objTar[idx].GetComponent<MeshRenderer>();
            MeshCollider mc = objTar[idx].GetComponent<MeshCollider>();

            mf.mesh = cloneChunk.GetComponent<MeshFilter>().mesh;
            mr.material = cloneChunk.GetComponent<MeshRenderer>().material;
            mc.sharedMesh = cloneChunk.GetComponent<MeshCollider>().sharedMesh;
            mr.enabled = true;
            objTar[idx].transform.localPosition = new Vector3(0f, 0.005f, -0.05f);
            cto.IsUpdate = false;
            if (idx < 8)
                idx++;
            else
                idx = 0;
        }
    }
}
