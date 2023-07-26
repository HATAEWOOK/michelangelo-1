using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Subsystems;
using System;
using System.Net;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

//[AddComponentMenu("MRTK/Input/XR Controller (Articulated Hand)")]
public class InteractionManager : MonoBehaviour
{
    //#region Associated hand select values
    [SerializeField]
    private XRNode handNode;

    public XRNode HandNode => handNode;

    [SerializeField]
    private Tools tools;
    [SerializeField]
    private MRTKRayInteractor rayInteractor;
    private Vector3 rayOriginPoint;

    private enum interactionMode
    {
        Near,
        Far
    }

    [SerializeField]
    private interactionMode mode;

    //#endregion Associated hand select values
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == interactionMode.Near)
        {
            bool gotPinch = XRSubsystemHelpers.HandsAggregator.TryGetPinchingPoint(
            handNode,
            out HandJointPose jointPose);

            bool isPinch = XRSubsystemHelpers.HandsAggregator.TryGetPinchProgress(
                handNode,
                out bool isReady,
                out bool isPinching,
                out float pinchAmount);

            if (isReady)
            {
                Debug.Log("[hatw] send info");
                tools.setTargetValue(jointPose.Pose.position, pinchAmount);
            }
        }
        else if (mode == interactionMode.Far)
        {
            bool gotPinch = XRSubsystemHelpers.HandsAggregator.TryGetPinchingPoint(
            handNode,
            out HandJointPose jointPose);

            bool isPinch = XRSubsystemHelpers.HandsAggregator.TryGetPinchProgress(
                handNode,
                out bool isReady,
                out bool isPinching,
                out float pinchAmount);

            if (isReady)
            {
                rayOriginPoint = rayInteractor.rayOriginTransform.position;
                Vector3 endPoint = rayInteractor.rayOriginTransform.position + rayInteractor.rayOriginTransform.forward * rayInteractor.maxRaycastDistance;
                tools.setTargetValue(endPoint, pinchAmount);
            }   
        }
        
    }
}
