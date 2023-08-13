// /******************************************************************************
// * File: KeyButton.cs
// * Copyright (c) 2023 Qualcomm Technologies, Inc. and/or its subsidiaries. All rights reserved.
// *
// *
// ******************************************************************************/

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace QCHT.Samples.XRKeyboard
{
    [System.Serializable]
    public class KeyEvent : UnityEvent<KeyButton>
    {

    }

    public abstract class KeyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler,
        IPointerUpHandler
    {
        [SerializeField] private GameObject _hoverButton, _selectButton;
        [SerializeField] private AudioSource _audioSource;
        public KeyEvent inputEvent;

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            //_hoverButton.SetActive(true);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            _selectButton.SetActive(false);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            _selectButton.SetActive(true);
            _audioSource.Play();
            inputEvent?.Invoke(this);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            _selectButton.SetActive(false);
        }
    }

    public enum KeySpecial
    {
        None,
        Delete,
        Shift,
        Enter,
        DeleteAll,
        SwitchObject
    }
}
