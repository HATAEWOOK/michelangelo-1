/******************************************************************************
 * File: InteractionManager.cs
 * Copyright (c) 2023 Qualcomm Technologies, Inc. and/or its subsidiaries. All rights reserved.
 *
 ******************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.OpenXR;
using InputDevice = UnityEngine.XR.InputDevice;
#if QCHT_UNITY_CORE
using QCHT.Interactions.Core;
#endif

#if AR_FOUNDATION_5_0_OR_NEWER
using Unity.XR.CoreUtils;
#endif

namespace Qualcomm.Snapdragon.Spaces.Samples
{
    public enum InputType
    {
        HandTracking = 0,
        GazePointer = 1,
        ControllerPointer = 2
    }

    public enum XRControllerProfile
    {
        HostController = 0,
        XRControllers = 1
    }

    public class InteractionManager : MonoBehaviour
    {
        public delegate void OnInputTypeSwitch(InputType inputType);

        public static OnInputTypeSwitch onInputTypeSwitch;
        public GameObject HandTrackingPointer;
        public GameObject GazePointer;
        public GameObject DevicePointer;
        public InputActionReference SwitchInputAction;
        private const string _controllerTypePrefsKey = "Qualcomm.Snapdragon.Spaces.Samples.Prefs.ControllerType";
#if QCHT_UNITY_CORE
        private static XRHandTrackingManager _handTrackingManager;
#endif
        private XRControllerProfile _xrControllerProfile;
        private XRControllerManager _xrControllerManager;
        private bool _isSessionOriginMoved;
        private bool _isHandTrackingCompatible;
        public InputType InputType { get; private set; }
        public Transform ArCameraTransform { get; private set; }
        protected virtual bool ResetSessionOriginOnStart => true;

        public void OnEnable()
        {
            SwitchInputAction.action.performed += OnSwitchInput;
            InputDevices.deviceConnected += RegisterConnectedDevice;
            RegisterXRProfiles();
            _isHandTrackingCompatible = IsHandTrackingCompatible();
        }

        public void OnDisable()
        {
            SwitchInputAction.action.performed -= OnSwitchInput;
            InputDevices.deviceDisconnected -= RegisterConnectedDevice;
        }

        public void Start()
        {
            ArCameraTransform = OriginLocationUtility.GetOriginCamera().transform;
            _xrControllerManager ??= FindObjectOfType<XRControllerManager>(true);
            SendControllerProfileToManager(_xrControllerManager);
            int controllerType = PlayerPrefs.GetInt(_controllerTypePrefsKey, 0);
            SetControllerProfileType((InputType)controllerType);
        }

        public void Update()
        {
            if (ResetSessionOriginOnStart && !_isSessionOriginMoved && ArCameraTransform.position != Vector3.zero)
            {
                OffsetSessionOrigin();
                _isSessionOriginMoved = true;
            }
        }

        public void SwitchInput()
        {
            var newInputType = InputType + 1;
            var inputTypeCount = Enum.GetNames(typeof(InputType)).Length;
            if (newInputType < 0 || (int)newInputType >= inputTypeCount)
            {
                newInputType = 0;
            }

            SetControllerProfileType(newInputType);
        }

        public void Quit()
        {
            SendHapticImpulse();
            Application.Quit();
        }

        public void SendHapticImpulse(float amplitude = 0.5f, float frequency = 60f, float duration = 0.1f)
        {
            if (InputType == InputType.ControllerPointer)
            {
                _xrControllerManager.SendHapticImpulse(amplitude, frequency, duration);
            }
        }

        private void OnSwitchInput(InputAction.CallbackContext ctx)
        {
            if (ctx.interaction is TapInteraction)
            {
                SwitchInput();
            }

            if (ctx.interaction is HoldInteraction)
            {
                Quit();
            }
        }

        private void SetControllerProfileType(InputType inputType)
        {
            // Checks if QCHT package is installed. If not, the Gaze Pointer will be the fallback.
            if (!_isHandTrackingCompatible)
            {
                InputType = inputType != InputType.HandTracking ? inputType : InputType.GazePointer;
            }
            else
            {
                InputType = inputType;
            }

            // Activates the Pointer used for interaction.
            switch (InputType)
            {
                case InputType.HandTracking:
                {
                    HandTrackingPointer.SetActive(true);
                    HandleHandTrackingDevices(true);
                    GazePointer.SetActive(false);
                    DevicePointer.SetActive(false);
                    break;
                }
                case InputType.GazePointer:
                {
                    ResetPointerPose();
                    HandTrackingPointer.SetActive(false);
                    HandleHandTrackingDevices(false);
                    GazePointer.SetActive(true);
                    DevicePointer.SetActive(false);
                    break;
                }
                case InputType.ControllerPointer:
                {
                    ResetPointerPose();
                    HandTrackingPointer.SetActive(false);
                    HandleHandTrackingDevices(false);
                    GazePointer.SetActive(false);
                    DevicePointer.SetActive(true);
                    break;
                }
            }

            // Sets the pointer type and saves it in the PlayerPrefs.
            int pointerType = GazePointer.activeSelf ? (int)InputType.GazePointer :
                DevicePointer.activeSelf ? (int)InputType.ControllerPointer :
                HandTrackingPointer.activeSelf ? (int)InputType.HandTracking : 0;
            PlayerPrefs.SetInt(_controllerTypePrefsKey, pointerType);
            onInputTypeSwitch?.Invoke(InputType);
        }

        private void ResetPointerPose()
        {
            var baseRuntimeFeature = OpenXRSettings.Instance.GetFeature<BaseRuntimeFeature>();
            if (baseRuntimeFeature)
            {
                baseRuntimeFeature.TryResetPose();
            }

        }

        private void HandleHandTrackingDevices(bool enable)
        {
#if QCHT_UNITY_CORE
            if (!_handTrackingManager && enable)
            {
                _handTrackingManager = XRHandTrackingManager.GetOrCreate(XRHandTrackingManager.DefaultLeftHandPrefab, XRHandTrackingManager.DefaultRightHandPrefab);
            }

            if (_handTrackingManager)
            {
                _handTrackingManager.enabled = enable;
            }
#endif
        }

        protected void OffsetSessionOrigin()
        {
#if AR_FOUNDATION_5_0_OR_NEWER
            XROrigin sessionOrigin = OriginLocationUtility.FindXROrigin();
#else
            ARSessionOrigin sessionOrigin = OriginLocationUtility.FindARSessionOrigin();
#endif
            sessionOrigin.transform.Rotate(0.0f, -ArCameraTransform.rotation.eulerAngles.y, 0.0f, Space.World);
            sessionOrigin.transform.position = -ArCameraTransform.position;
        }

        private void RegisterXRProfiles()
        {
            List<InputDevice> inputDevices = new List<InputDevice>();
            InputDevices.GetDevices(inputDevices);
            foreach (var inputDevice in inputDevices)
            {
                RegisterConnectedDevice(inputDevice);
            }
        }

        private void RegisterConnectedDevice(InputDevice inputDevice)
        {
            _xrControllerProfile = inputDevice.name.Contains("Oculus") ? XRControllerProfile.XRControllers : XRControllerProfile.HostController;
        }

        private void SendControllerProfileToManager(XRControllerManager xrControllerManager)
        {
            if (xrControllerManager != null)
            {
                xrControllerManager.ActivateController(_xrControllerProfile);
            }
        }

        private bool IsHandTrackingCompatible()
        {
#if QCHT_UNITY_CORE
            // Returns ture only if QCHT package is in the project.
            return true;
#endif
            return false;
        }
    }
}
