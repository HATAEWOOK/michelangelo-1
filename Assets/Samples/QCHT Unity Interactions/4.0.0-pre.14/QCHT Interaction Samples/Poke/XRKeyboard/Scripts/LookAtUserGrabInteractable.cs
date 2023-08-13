// /******************************************************************************
// * File: LookAtUserGrabInteractable.cs
// * Copyright (c) 2023 Qualcomm Technologies, Inc. and/or its subsidiaries. All rights reserved.
// *
// * Confidential and Proprietary - Qualcomm Technologies, Inc.
// *
// ******************************************************************************/

using QCHT.Samples.XRKeyboard;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace QCHT.Samples
{
    public class LookAtUserGrabInteractable : XRGrabInteractable
    {
        private Transform _userTransform;
        private bool _shouldFaceUser = false;
        private float _xStartingAngle;

        private void Start()
        {
            _userTransform = Camera.main.transform;
            selectEntered.AddListener(arg => _shouldFaceUser = true);
            selectExited.AddListener(arg => _shouldFaceUser = false);
            _xStartingAngle = transform.eulerAngles.x;
            var pointerViewerKeyboard = FindObjectOfType<PointerViewerKeyboard>();
            if (pointerViewerKeyboard != null) pointerViewerKeyboard.KeyboardTransform = transform;
        }

        private void Update()
        {
            if (!_shouldFaceUser)
                return;

            Quaternion quaternion = Quaternion.LookRotation(transform.position - _userTransform.position);
            transform.rotation = quaternion;
            transform.eulerAngles = new Vector3(_xStartingAngle, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}