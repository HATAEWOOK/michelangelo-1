// /******************************************************************************
//  * File: DistalInteractorsManager.cs
//  * Copyright (c) 2023 Qualcomm Technologies, Inc. and/or its subsidiaries. All rights reserved.
//  *
//  *
//  ******************************************************************************/

using QCHT.Interactions.Distal;
using UnityEngine;

namespace QCHT.Samples.Menu
{
    public class DistalInteractorsManager : MonoBehaviour
    {
        [SerializeField] private DistalInteractorType StartingDistalInteractor = DistalInteractorType.Pointer;
        [SerializeField] private GameObject _xrPointerLeft, _xrPointerRight;
        [SerializeField] private GameObject _xrGaze;
        [SerializeField] private PointerViewer _pointerViewer;

        private void Start()
        {
            SwitchTo(StartingDistalInteractor);
        }

        public void SwitchTo(DistalInteractorType distalInteractor)
        {
            if (distalInteractor == DistalInteractorType.None) return;
            _xrPointerLeft.SetActive(distalInteractor == DistalInteractorType.Pointer);
            _xrPointerRight.SetActive(distalInteractor == DistalInteractorType.Pointer);
            _pointerViewer.UpdateRays = distalInteractor == DistalInteractorType.Pointer;
            _xrGaze.SetActive(distalInteractor == DistalInteractorType.Gaze);
        }
    }
}