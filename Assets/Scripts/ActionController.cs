using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//최적화 완료
//인벤토리에서 아이템을 받아오는 클래스로 그냥 통채로 생각
public class ActionController : MonoBehaviour
{
    [SerializeField] private Inventory theInventory;
    private bool pickupActivated = false;  // 아이템 습득 가능할시 True 
    private int count;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Item")){
            Debug.Log(other.name);
            if(other.name=="Branch(Clone)"){
                count=6;
            } 
            else if(other.name=="Berries(Clone)"){
                count=3;
            } 
            else{
                count=1;
            }
            
            
            Debug.Log(count);
            theInventory.AcquireItem(other.GetComponent<ItemPickUp>().item,count);
            other.transform.gameObject.SetActive(false);
        }
    }
}
