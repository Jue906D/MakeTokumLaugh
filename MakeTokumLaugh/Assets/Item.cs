using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

public class Item : MonoBehaviour
{
    [SerializeField]
    //��¼���Լ��ĸ�����.
    public GameObject ItemPivot;
    [SerializeField]
    //
    public string Name;


    [SerializeField]
    //��¼���λ��.
    Vector3 MousePosition;
    [SerializeField]
    Vector3 distance;

    [SerializeField]
    public bool isDragging = false;

    void OnEnable()
    {
        isDragging = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }

    public void Init(GameObject pivot,string name)
    {
        ItemPivot = pivot;
        Name = name;
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
            EndDrag(false);
        }
        //Debug.Log("end drag");

    }

    public void EndDrag(bool isDeleted)
    {
        //Debug.Log("Drag Over");
        isDragging = false;
        if (isDeleted)
        {
            ItemPivot.GetComponent<Pivot>().IsUsing = false;
            ItemPivot.GetComponent<Pivot>().CurItem = null;
            Destroy(this.gameObject);
        }
        else
        {
            transform.position = ItemPivot.transform.position;
        }
        //Board.instance.CurDragPiece = null;
    }

    public bool IsPointerOverGameObject(Vector2 screenPosition)
    {
        //ʵ��������¼�
        PointerEventData eventDataCurrentPosition = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);
        //�����λ�õ���Ļ���긳ֵ������¼�
        eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        //��������������
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);


        return results.Count <= 1;

    }
}
