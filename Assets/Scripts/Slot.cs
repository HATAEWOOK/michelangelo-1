using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Microsoft.MixedReality.Toolkit.UX;

public class Slot : MonoBehaviour
// , IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Item item;
    private static bool isMultiSelectable;
    void Start()
    {
        isMultiSelectable = Gallery._isMultiSelectable;
        DisplayItem();
    }
    private void ClearSlot()
    {
        item = null;
    }

    // create miniatures of the item in the slot
    public void DisplayItem()
    {
        if (item != null)
        {
            GameObject itemPrefab = Instantiate(item.itemPrefab, transform.position, Quaternion.identity);
            itemPrefab.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            itemPrefab.transform.SetParent(transform);
            Destroy(itemPrefab.GetComponent<Collider>());
        }
    }

    public void ToggleSwitchOnSlotMenu()
    {
        SlotMenu[] slotMenus = transform.parent.GetComponentsInChildren<SlotMenu>();
        SlotMenu slotMenu = this.GetComponent<SlotMenu>();
        foreach (SlotMenu sm in slotMenus)
        {
            if (sm != slotMenu)
            {
                sm.gameObject.SetActive(false);
            }
        }
        if (!isMultiSelectable)
        {
            PressableButton[] buttons = transform.parent.GetComponentsInChildren<PressableButton>();
            PressableButton button = this.GetComponent<PressableButton>();
            foreach (PressableButton b in buttons)
            {
                if (b != button)
                {
                    b.ForceSetToggled(false);
                }
            }
        }
    }
    // public void OnBeginDrag(PointerEventData eventData)
    // {
    //     // if(item != null && Inventory.inventoryActivated)
    //     if(item != null)
    //     {
    //         DragSlot.instance.dragSlot = this;
    //         DragSlot.instance.DragSetImage(itemImage);

    //         DragSlot.instance.transform.position = eventData.position;
    //     }
    // }

    // public void OnDrag(PointerEventData eventData)
    // {
    //     if (item != null)
    //     {
    //         DragSlot.instance.transform.position = eventData.position;
    //     }
    // }

    // public void OnEndDrag(PointerEventData eventData)
    // {

    //     if(!(DragSlot.instance.transform.localPosition.x > baseRect.rect.xMin 
    //         && DragSlot.instance.transform.localPosition.x < baseRect.rect.xMax
    //        && DragSlot.instance.transform.localPosition.y > baseRect.rect.yMin 
    //        && DragSlot.instance.transform.localPosition.y < baseRect.rect.yMax)
    //        )
    //     {
    //         if(DragSlot.instance.dragSlot != null)
    //         {
    //             ClearSlot();
    //         }
    //     }
    //     else
    //     {
    //         DragSlot.instance.SetColor(0);
    //         DragSlot.instance.dragSlot = null;
    //     }
    // }

    // public void OnDrop(PointerEventData eventData)
    // {
    //     if (DragSlot.instance.dragSlot != null)
    //     {
    //         ChangeSlot();
    //         if (DragSlot.instance.dragSlot.isQuickSlot)
    //             theItemEffectDatabase.IsActivatedQuickSlot(DragSlot.instance.dragSlot.quickSlotNumber); // 활성화된 퀵슬롯 ? 교체 작업.
    //     }

    // }
}
