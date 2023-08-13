// /******************************************************************************
//  * File: HandTrackingChecker.cs
//  * Copyright (c) 2023 Qualcomm Technologies, Inc. and/or its subsidiaries. All rights reserved.
//  *
//  *
//  ******************************************************************************/

using System.Collections;
using QCHT.Interactions.Core;
using QCHT.Samples.Menu;
using UnityEngine;

namespace QCHT.Samples.Menu
{
    public class HandTrackingChecker : MonoBehaviour
    {
        [SerializeField] private XRHandTrackingManager manager;
        [SerializeField] private SampleSceneManager sampleSceneManager;
        [SerializeField] private GameObject warningToast;
        [SerializeField] private float idleDuration; 
        private float _handTrackingIdleTime;

        private void Start()
        {
            warningToast.SetActive(false);
        }

        private void Update()
        {
            if (manager.HandTrackingStatus == XRHandTrackingSubsystem.QchtStatus.QchtStatusRunning)
            {
                _handTrackingIdleTime = 0;
                return;
            }

            _handTrackingIdleTime += Time.deltaTime;

            if (_handTrackingIdleTime > idleDuration)
            {
                _handTrackingIdleTime = 0;
                StartCoroutine(CloseHandTrackingScene());
            }
        }

        IEnumerator CloseHandTrackingScene()
        {
            warningToast.SetActive(true);
            yield return new WaitForSeconds(4f);
            sampleSceneManager.SwitchToScene("Main Menu");
        }
    }
}