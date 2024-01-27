using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

public class Item : MonoBehaviour
{
    [SerializeField]
    //记录下自己的父物体.
    public GameObject ItemPivot;
    [SerializeField]
    public bool IsReserved = false;
    [SerializeField]
    //
    public string Name;


    [SerializeField]
    //记录鼠标位置.
    Vector3 MousePosition;
    [SerializeField]
    Vector3 distance;

    [SerializeField]
    public bool isDragging = false;
    [SerializeField]
    public bool InDetect = false;

    void OnEnable()
    {
        isDragging = false;
        InDetect = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }

    public void Init(GameObject pivot,string id,string name)
    {
        ItemPivot = pivot;
        Name = name;
        IsReserved = LubanLoader.Tables.TbItem[id].IsReserved;
    }

    private void OnMouseDown()
    {
        if (IsPointerOverGameObject(Input.mousePosition))
        {
            //Debug.Log("Drag Start");
            isDragging = true;
            //Debug.Log("begin drag");
            //rawPosition = transform.position;
            distance = new Vector3(transform.position.x, transform.position.y, 0) -
                       new Vector3(MousePosition.x, MousePosition.y, 0);
        }

    }

    private void OnMouseDrag()
    {
        /*
        if (MousePosition.x > Board.instance.DragLimitR.x
            || MousePosition.x < Board.instance.DragLimitL.x
            || MousePosition.y > Board.instance.DragLimitR.y
            || MousePosition.y < Board.instance.DragLimitL.y)
        {
            isDragging = false;
            transform.position = rawPosition;
            Board.instance.CurDragPiece = null;
        }
        */

        if (IsPointerOverGameObject(Input.mousePosition) && isDragging)
        {
            //Debug.Log("Drag");
            transform.position = new Vector3(MousePosition.x, MousePosition.y, 0) + distance;
        }

    }

    private void OnMouseUp()
    {
        if (isDragging == true)
        {
            if (!InDetect)
            {
                isDragging = false;
                transform.position = ItemPivot.transform.position;
            }
            else
            {
                //var initem = collision.gameObject.GetComponent<Item>();
                string name = Name;
                var sucname = LubanLoader.Tables.TbPuzzle[GameMain.Main.CurLevel].SucItem;
                if (Name == LubanLoader.Tables.TbItem[sucname].Prefab)
                {
                    EndDrag(true);
                    Debug.Log(string.Format("Next Level！Id is : {0}", name));
                }
                else
                {
                    EndDrag(false);
                    Debug.Log(string.Format("Wrong Answer！Id is : {0}", name));
                }
            }
            //Board.instance.CurDragPiece = null;
        }
        //Debug.Log("end drag");

    }

    public void EndDrag(bool isDeleted)
    {
        //Debug.Log("Drag Over");
        isDragging = false;
        if (isDeleted)
        {
            StartCoroutine(GameMain.Main.ShowFeedback(this.Name));
            ItemPivot.GetComponent<Pivot>().IsUsing = false;
            ItemPivot.GetComponent<Pivot>().CurItem = null;
            Destroy(this.gameObject);
            
        }
        else
        {
            StartCoroutine(GameMain.Main.ShowFeedback(this.Name));
            transform.position = ItemPivot.transform.position;
        }
        //Board.instance.CurDragPiece = null;
    }

    public bool IsPointerOverGameObject(Vector2 screenPosition)
    {
        //实例化点击事件
        PointerEventData eventDataCurrentPosition = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);
        //将点击位置的屏幕坐标赋值给点击事件
        eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        //向点击处发射射线
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);


        return results.Count <= 1;

    }
}
