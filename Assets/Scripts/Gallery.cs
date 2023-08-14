using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gallery : MonoBehaviour
{
    private SlotMenu[] slotMenus;
    [SerializeField]
    private bool isMultiSelectable = true;
    public static bool _isMultiSelectable;

    void Start()
    {
        _isMultiSelectable = isMultiSelectable;
    }

    public void ToggleSwitchOnSlotMenu(SlotMenu slotMenu)
    {
        slotMenus = GetComponentsInChildren<SlotMenu>();
        foreach (SlotMenu sm in slotMenus)
        {
            if (sm != slotMenu)
            {
                sm.gameObject.SetActive(false);
            }
        }
    }
}
