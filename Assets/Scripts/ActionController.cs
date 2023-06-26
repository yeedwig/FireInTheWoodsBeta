using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//최적화 완료
//인벤토리에서 아이템을 받아오는 클래스로 그냥 통채로 생각
public class ActionController : MonoBehaviour
{
    [SerializeField] private Inventory theInventory;
    private bool pickupActivated = false;  // 아이템 습득 가능할시 True 

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Item")){
            theInventory.AcquireItem(other.GetComponent<ItemPickUp>().item);
            other.transform.gameObject.SetActive(false);
        }
    }
}
