// /******************************************************************************
//  * File: FollowOrigin.cs
//  * Copyright (c) 2023 Qualcomm Technologies, Inc. and/or its subsidiaries. All rights reserved.
//  *
//  *
//  ******************************************************************************/

using QCHT.Interactions.Core;
using UnityEngine;

namespace QCHT.Samples.Drawing
{
    public class FollowOrigin : MonoBehaviour
    {
        private Transform _origin;
        
        private void Awake() {
            _origin = XROriginUtility.GetOriginTransform();
            if (!_origin)
                DestroyImmediate(this);
        }

        private void Update() {
            if (!_origin) return;
            var t = transform;
            var originTransform = _origin.transform;
            t.position = originTransform.position;
            t.rotation = originTransform.rotation;
        }
    }
}