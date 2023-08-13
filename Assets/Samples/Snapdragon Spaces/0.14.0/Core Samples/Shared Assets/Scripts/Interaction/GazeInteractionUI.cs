/******************************************************************************
 * File: GazeInteractionUI.cs
 * Copyright (c) 2023 Qualcomm Technologies, Inc. and/or its subsidiaries. All rights reserved.
 *
 ******************************************************************************/

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

namespace Qualcomm.Snapdragon.Spaces.Samples
{
    [RequireComponent(typeof(XRGazeInteractor))]
    public class GazeInteractionUI : MonoBehaviour
    {
        private IPointerClickHandler _activeClickHandler;
        private float _gazeTimerCurrent;
        private bool _isSelectPressed;
        private float _safeTimerCurrent;

        [SerializeField] [Tooltip("Reference to the XRGazeInteractor component from XR Interaction Toolkit.")]
        private XRGazeInteractor _xrGazeInteractor;

        [Tooltip("Distance of the Gaze reticle from the main camera.")]
        public float DefaultDistance = 1.0f;

        [Tooltip("Delay time for the gaze pointer loading initialization")]
        public float DelayGazeLoading = 0.5f;

        [Tooltip("Reference to the reticle Game Object.")]
        public Transform ReticleGameObject;

        [Tooltip("Reference to the outer ring that will have the fill effect.")]
        public Image ReticleOuterRing;

        public InputActionReference SelectAction;
        public bool IsHovering { get; private set; }

        private void Update()
        {
            UpdateGazeCounter();
        }

        private void OnEnable()
        {
            SelectAction.action.performed += SelectPressed;
        }

        private void OnDisable()
        {
            SelectAction.action.performed -= SelectPressed;
        }

        public void Hover(bool isHovering)
        {
            IsHovering = isHovering;
            if (!isHovering)
            {
                ResetReticle();
            }
        }

        private void UpdateGazeCounter()
        {
            if (_xrGazeInteractor.TryGetCurrentUIRaycastResult(out RaycastResult RaycastResult, out int raycastEndpointIndex))
            {
                SetPointerPosition(RaycastResult.worldPosition, RaycastResult.worldNormal);
                // Check if there is a Selectable component in the Game Object
                var selectable = RaycastResult.gameObject.GetComponent<Selectable>();
                if (selectable != null )
                {
                    if (!selectable.IsInteractable())
                    {
                        IsHovering = false;
                        return;
                    }
                }

                // Check if there is a Selectable component in the parent of the Game ObjectCheck (specific for toggles).
                var selectableParent = RaycastResult.gameObject.GetComponentInParent<Selectable>();
                if (selectableParent != null)
                {
                    if (!selectableParent.IsInteractable())
                    {
                        IsHovering = false;
                        return;
                    }
                }

                if (IsHovering)
                {
                    IPointerClickHandler clickHandler = RaycastResult.gameObject.GetComponentInParent<IPointerClickHandler>();
                    GetPointerEventData(RaycastResult.worldPosition, out PointerEventData pointerEventData);
                    _activeClickHandler = clickHandler;
                    float gazeTimerDuration = _xrGazeInteractor.hoverTimeToSelect;
                    if (_safeTimerCurrent <= DelayGazeLoading)
                    {
                        _safeTimerCurrent += Time.deltaTime;
                        return;
                    }

                    if (_gazeTimerCurrent <= gazeTimerDuration)
                    {
                        _gazeTimerCurrent += Time.deltaTime;
                        // Increase the fill amount by the normalized value (0.0 to 1.0)
                        ReticleOuterRing.fillAmount = _gazeTimerCurrent / gazeTimerDuration;
                    }

                    if (ReticleOuterRing.fillAmount >= 1f || _isSelectPressed)
                    {
                        _activeClickHandler.OnPointerClick(pointerEventData);
                        ResetReticle();
                        IsHovering = false;
                        _isSelectPressed = false;
                    }
                }
            }
            else
            {
                var rayOriginTransform = _xrGazeInteractor.rayOriginTransform;
                SetPointerPosition(rayOriginTransform.position + (rayOriginTransform.forward * DefaultDistance), -rayOriginTransform.forward);
            }

            _isSelectPressed = false;
        }

        private void GetPointerEventData(in Vector2 RaycastPosition, out PointerEventData pointerEventData)
        {
            // Gets the Pointer Event Data from the Gaze Interactor position to test against UI.
            pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = RaycastPosition;
        }

        private void SetPointerPosition(Vector3 position, Vector3 normal)
        {
            // Additionally offset the position on Z to avoid z-fighting/clipping
            ReticleGameObject.position = position + (normal * 0.1f);
            ReticleGameObject.rotation = Quaternion.LookRotation(-normal);
        }

        private void SelectPressed(InputAction.CallbackContext ctx)
        {
            _isSelectPressed = true;
        }

        private void ResetReticle()
        {
            _gazeTimerCurrent = 0f;
            _safeTimerCurrent = 0f;
            ReticleOuterRing.fillAmount = 0f;
        }
    }
}
