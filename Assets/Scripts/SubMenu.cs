using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    private RadialView rv;
    private SolverHandler sv;
    private bool isPinned = false;

    public bool IsPinned { get => isPinned; set => isPinned = value; }

    private void Awake()
    {
        rv = GetComponent<RadialView>();
        sv = GetComponent<SolverHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mainMenu.activeSelf)
        {
            rv.enabled = true;
            sv.TrackedTargetType = TrackedObjectType.CustomOverride;
            sv.TransformOverride = mainMenu.transform;
            rv.MaxDistance = 0.0f;
        }
        else
        {
            rv.enabled = !isPinned;
            sv.TrackedTargetType = TrackedObjectType.Head;
            rv.MaxDistance = 0.4f;
            rv.MinDistance = 0.1f;
        }
    }
}
