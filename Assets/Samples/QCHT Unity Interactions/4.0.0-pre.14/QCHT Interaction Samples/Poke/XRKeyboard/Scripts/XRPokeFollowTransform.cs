// /******************************************************************************
// * File: XRPokeFollowTransform.cs
// * Copyright (c) 2023 Qualcomm Technologies, Inc. and/or its subsidiaries. All rights reserved.
// *
// *
// ******************************************************************************/

using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.State;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using UnityEngine;

namespace QCHT.Samples.XRKeyboard
{
    public class XRPokeFollowTransform : MonoBehaviour
    {
        [SerializeField] private Transform _followTransform;

        [SerializeField] private float _smoothSpeed = 9f;

        [SerializeField] private float _maxDistance = 20f;

        private Vector3 _initPos;
        private Vector3 _targetPos;
        private IPokeStateDataProvider _pokeStateDataProvider;

        private void Awake() => _pokeStateDataProvider = GetComponentInParent<IPokeStateDataProvider>();

        private void Start()
        {
            if (_followTransform == null)
                return;

            _initPos = _followTransform.localPosition;
            _pokeStateDataProvider.pokeStateData.SubscribeAndUpdate(OnPokeDataUpdated);
        }

        private void LateUpdate()
        {
            UpdateTransformPosition();
        }

        private void UpdateTransformPosition()
        {
            _followTransform.localPosition =
                SmoothPosition(_followTransform.localPosition, _targetPos);
        }

        private Vector3 SmoothPosition(Vector3 initPos, Vector3 targetPos)
        {
            var interpolateTime = Time.deltaTime * _smoothSpeed;
            return Vector3.Lerp(initPos, targetPos, interpolateTime);
        }

        private void OnPokeDataUpdated(PokeStateData data)
        {
            var pokeTransform = data.target;
            var hasToFollowPoke = pokeTransform != null && pokeTransform.IsChildOf(transform);

            if (hasToFollowPoke)
            {
                var position = pokeTransform.InverseTransformPoint(data.axisAlignedPokeInteractionPoint);
                var maxDistanceReached = position.sqrMagnitude > Mathf.Sqrt(_maxDistance);
                if (maxDistanceReached)
                    position = Vector3.ClampMagnitude(position, _maxDistance);

                _targetPos = position;
            }
            else
                _targetPos = _initPos;
        }
    }
}
