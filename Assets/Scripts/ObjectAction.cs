using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAction : MonoBehaviour
{
    [SerializeField]
    private Transform userPos;
    [SerializeField]
    private GameObject targetObject;

    public void OnActionButton()
    {
        if (userPos != null)
        {
            userPos = GameObject.Find("Main Camera").transform;
        }
        GameObject tmp = Instantiate(targetObject, userPos.position + new Vector3(0, 0, 0.3f), Quaternion.identity);
        tmp.AddComponent<ObjectManipulator>();
        tmp.transform.localScale *= 10f;
    }
}
