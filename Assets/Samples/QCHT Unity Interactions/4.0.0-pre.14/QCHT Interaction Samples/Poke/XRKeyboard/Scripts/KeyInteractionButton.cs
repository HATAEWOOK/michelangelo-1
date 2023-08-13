// /******************************************************************************
// * File: KeyInteractionButton.cs
// * Copyright (c) 2023 Qualcomm Technologies, Inc. and/or its subsidiaries. All rights reserved.
// *
// *
// ******************************************************************************/

using UnityEngine;
using UnityEngine.EventSystems;
using QCHT.Interactions.Distal;

namespace QCHT.Samples.XRKeyboard
{
    public class KeyInteractionButton : KeyButton
    {
        [SerializeField] private DistalInteractorType DistalInteractorType = DistalInteractorType.Gaze;

        public DistalInteractorType Interaction => DistalInteractorType;

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            OnPointerUp(eventData);
        }
    }
}