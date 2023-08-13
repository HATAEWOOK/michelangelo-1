// /******************************************************************************
//  * File: HandTrackingTester.cs
//  * Copyright (c) 2023 Qualcomm Technologies, Inc. and/or its subsidiaries. All rights reserved.
//  *
//  *
//  ******************************************************************************/

using UnityEngine;
using UnityEngine.Assertions;
using QCHT.Interactions.Core;

namespace QCHT.Samples.Menu
{
    public class HandTrackingTester : MonoBehaviour
    {
        [SerializeField] private GameObject leftPrefab;
        [SerializeField] private GameObject rightPrefab;
        
        private XRHandTrackingManager _manager;
        
        private void OnEnable() {
            _manager = XRHandTrackingManager.GetOrCreate(leftPrefab, rightPrefab);
            Assert.IsNotNull(_manager);
        }
        
        [ContextMenu("Change Prefabs")]
        private void ChangePrefabs() {
            if (!_manager) return;
            _manager.LeftHandPrefab = leftPrefab;
            _manager.RightHandPrefab = rightPrefab;
            _manager.RefreshLeftHand();
            _manager.RefreshRightHand();
        }

        [ContextMenu("Start Devices")]
        private void StartDevices() {
            if (!_manager) return;
            _manager.StartDevices();
        }
        
        [ContextMenu("Stop Devices")]
        private void StopDevices() {
            if (!_manager) return;
            _manager.StopDevices();
        }

        [ContextMenu("Toggle Manager")]
        private void ToggleManager() {
            if (!_manager) return;
            _manager.enabled = !_manager.enabled;
        }

        private void OnDisable() { 
           if (!_manager) return;
           Destroy(_manager);
        }
    }
}
