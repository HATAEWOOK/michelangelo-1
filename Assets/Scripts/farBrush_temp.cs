using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class farBrush_temp : MonoBehaviour
{

    [SerializeReference]
    [InterfaceSelector(true)]
    [Tooltip("The pose source representing the pose this interactor uses for aiming and positioning. Follows the 'pointer pose'")]
    private IPoseSource aimPoseSource;

    /// <summary>
    /// The pose source representing the ray this interactor uses for aiming and positioning.
    /// </summary>
    protected IPoseSource AimPoseSource { get => aimPoseSource; set => aimPoseSource = value; }

    [SerializeReference]
    [InterfaceSelector(true)]
    [Tooltip("The pose source representing the device this interactor uses for rotation.")]
    private IPoseSource devicePoseSource;

    /// <summary>
    /// The pose source representing the device this interactor uses for rotation.
    /// </summary>
    protected IPoseSource DevicePoseSource { get => devicePoseSource; set => devicePoseSource = value; }

    private static readonly ProfilerMarker ProcessInteractorPerfMarker =
        new ProfilerMarker("[MRTK] MRTKRayInteractor.ProcessInteractor");

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Use Pose Sources to calculate the interactor's pose and the attach transform's position
        // We have to make sure the ray interactor is oriented appropriately before calling
        // lower level raycasts
        if (AimPoseSource != null && AimPoseSource.TryGetPose(out Pose aimPose))
        {
            transform.SetPositionAndRotation(aimPose.position, aimPose.rotation);

            //if (hasSelection)
            //{
            //    float distanceRatio = PoseUtilities.GetDistanceToBody(aimPose) / refDistance;
            //    attachTransform.localPosition = new Vector3(initialLocalAttach.position.x, initialLocalAttach.position.y, initialLocalAttach.position.z * distanceRatio);
            //}
        }

        //// Use the Device Pose Sources to calculate the attach transform's pose
        //if (DevicePoseSource != null && DevicePoseSource.TryGetPose(out Pose devicePose))
        //{
        //    attachTransform.rotation = devicePose.rotation;
        //}
    }
}
