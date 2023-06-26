using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    

    [SerializeField]
    private GameObject go_InventoryBase; // Inventory_Base 이미지
    [SerializeField] 
    private GameObject go_SlotsParent;  // Slot들의 부모인 Grid Setting 
    
    

    public Slot[] slots;  // 슬롯들 배열

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)  // null 이라면 slots[i].item.itemName 할 때 런타임 에러 나서
            {
                if (slots[i].item.itemName == _item.itemName)
                {
                    slots[i].SetSlotCount(_count);
                    return;
                }
            }
        }
        

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }

    

    
}
