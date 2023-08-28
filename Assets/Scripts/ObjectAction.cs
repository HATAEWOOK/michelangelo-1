using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ObjectAction : MonoBehaviour
{
    private Transform userPos;
    [SerializeField]
    private GameObject targetObject;

    public void OnActionButton()
    {
        if (userPos == null)
        {
            userPos = GameObject.Find("Main Camera").transform;
        }
        GameObject tmp = Instantiate(targetObject, userPos.position + new Vector3(0, 0, 0.1f), Quaternion.identity);
        tmp.AddComponent<ARAnchor>();
        tmp.GetComponent<ARAnchor>().enabled = false;
        tmp.AddComponent<BoxCollider>();
        //tmp.AddComponent<Rigidbody>();
        //tmp.GetComponent<Rigidbody>().useGravity = false;
        tmp.transform.localScale *= 10f;
    }
}
