using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AnchorControl : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    public void GrabEnter()
    {
        ARAnchor anchor = GetComponent<ARAnchor>();
        anchor.enabled = false;
    }
    public void GrabExit()
    {
        ARAnchor anchor = GetComponent<ARAnchor>();
        anchor.enabled = true;
    }
}
