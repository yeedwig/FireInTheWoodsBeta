using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Item item;
    public int itemCount;
    public Image itemImage;

    [SerializeField] private Text text_Count;
    [SerializeField] private GameObject go_CountImage;

    public Camera cam;
    

    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        go_CountImage.SetActive(true);
        text_Count.text = itemCount.ToString();

        SetColor(1);

    }

    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        if(itemCount<=99){
            text_Count.text = itemCount.ToString();
        }
        else{
            text_Count.text="99";
        }
        

        if (itemCount <= 0)
            ClearSlot();
    }

    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }



    public void OnBeginDrag(PointerEventData eventData)
    {
        
        if(item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            Vector3 posMouse = cam.ScreenToWorldPoint(new Vector3(eventData.position.x,eventData.position.y,110));
            DragSlot.instance.transform.position = posMouse;
        }
    }

    // 마우스 드래그 중일 때 계속 발생하는 이벤트
    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {   
            Vector3 posMouse = cam.ScreenToWorldPoint(new Vector3(eventData.position.x,eventData.position.y,110));
            DragSlot.instance.transform.position = posMouse;
        }    
    }

    // 마우스 드래그가 끝났을 때 발생하는 이벤트
    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        //GameObject.Find("Main Camera").GetComponent<CameraManager>().movingInventory=false;
        if (DragSlot.instance.dragSlot != null)
        {
            
            LayerMask mask = LayerMask.GetMask("Fire");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hits = Physics2D.GetRayIntersection(ray,Mathf.Infinity,mask);
            if(hits!=null)
            {
                Debug.Log(hits.transform.gameObject.name);
                if(hits.transform.gameObject.name=="fire")
                {
                    GameObject.Find("fire").GetComponent<FireManager>().inputItem += item.itemName;
                    GameObject.Find("fire").GetComponent<FireManager>().count++;
                    SetSlotCount(-1);
                }
                    
            }
        }
        DragSlot.instance.dragSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            ChangeSlot();
        }
            
    }

     private void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if (_tempItem != null)
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        else
            DragSlot.instance.dragSlot.ClearSlot();
    }

    
}
