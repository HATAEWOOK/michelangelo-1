// /******************************************************************************
//  * File: ResetOriginToCameraOnLoad.cs
//  * Copyright (c) 2023 Qualcomm Technologies, Inc. and/or its subsidiaries. All rights reserved.
//  *
//  *
//  ******************************************************************************/

#if UNITY_AR_FOUNDATION_LEGACY
using UnityEngine.XR.ARFoundation;
#endif

using Unity.XR.CoreUtils;
using UnityEngine;

namespace QCHT.Samples.Menu
{
    public class ResetOriginToCameraOnLoad : MonoBehaviour
    {
        public bool ResetSessionOriginOnStart = true;

        private bool _isSessionOriginMoved;

#if UNITY_AR_FOUNDATION_LEGACY
        private ARSessionOrigin _arOrigin;
#endif
        private XROrigin _xrOrigin;

        private void Awake()
        {
#if UNITY_AR_FOUNDATION_LEGACY
            _arOrigin = GetComponent<ARSessionOrigin>();
#endif
            _xrOrigin = GetComponent<XROrigin>();
        }

        private void OnEnable() => OffsetSessionOrigin();

        private void Update()
        {
            var cameraInOriginSpaces = Vector3.zero;
            var isSet = false;

#if UNITY_AR_FOUNDATION_LEGACY
            if (_arOrigin != null)
            {
                cameraInOriginSpaces = _arOrigin.transform.InverseTransformPoint(_arOrigin.camera.transform.position);
                isSet = true;
            }
#endif
            if (!isSet && _xrOrigin != null)
                cameraInOriginSpaces = _xrOrigin.CameraInOriginSpacePos;

            if (ResetSessionOriginOnStart && !_isSessionOriginMoved && cameraInOriginSpaces != Vector3.zero)
            {
                OffsetSessionOrigin();
                _isSessionOriginMoved = true;
            }
        }

        public void Recenter()
        {
            _isSessionOriginMoved = false;
        }

        private void OffsetSessionOrigin()
        {
            Transform sessionOrigin = null;
            Transform cameraTransform = null;

#if UNITY_AR_FOUNDATION_LEGACY
            if (_arOrigin != null)
            {
                sessionOrigin = _arOrigin.transform;
                cameraTransform = _arOrigin.camera.transform;
            }
#endif
            if (sessionOrigin == null && _xrOrigin != null)
            {
                sessionOrigin = _xrOrigin.Origin.transform;
                cameraTransform = _xrOrigin.Camera.transform;
            }

            if (sessionOrigin != null && cameraTransform != null)
            {
                var t = sessionOrigin.transform;
                t.Rotate(0.0f, -cameraTransform.rotation.eulerAngles.y, 0.0f, Space.World);
                t.position -= cameraTransform.position;
            }
        }
    }
}