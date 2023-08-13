// /******************************************************************************
//  * File: PointerViewer.cs
//  * Copyright (c) 2023 Qualcomm Technologies, Inc. and/or its subsidiaries. All rights reserved.
//  *
//  *
//  ******************************************************************************/

using UnityEngine;
using UnityEngine.InputSystem;

namespace QCHT.Samples.Menu
{
    public class PointerViewer : MonoBehaviour
    {
        [SerializeField] protected GameObject leftController;
        [SerializeField] protected GameObject rightController;
        
        [SerializeField] protected InputAction leftIsTracked;
        [SerializeField] protected InputAction rightIsTracked;

        [SerializeField] protected InputAction leftFlipRatio;
        [SerializeField] protected InputAction rightFlipRatio;
        
        protected bool updateRays = true;
        
        public bool UpdateRays
        {
            get => updateRays;
            set => updateRays = value;
        }
        
        private void OnEnable()
        {
            leftIsTracked.Enable();
            rightIsTracked.Enable();
            leftFlipRatio.Enable();
            rightFlipRatio.Enable();
        }

        private void OnDisable()
        {
            leftIsTracked.Disable();
            rightIsTracked.Disable();
            leftFlipRatio.Disable();
            rightFlipRatio.Disable();
        }

        protected virtual void Update()
        {
            if (!updateRays) return;
            leftController.SetActive(leftIsTracked.IsInProgress() && leftFlipRatio.ReadValue<float>() <= 0f);
            rightController.SetActive(rightIsTracked.IsInProgress() && rightFlipRatio.ReadValue<float>() <= 0f);
        }
    }
}


