using cfg;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    [SerializeField] public GameObject GameMainObject;
    [SerializeField] public GameObject GameStart;
    // Start is called before the first frame update
    [SerializeField] public List<Item> ItemList;
    [SerializeField] public List<Pivot> PivotList;

    [SerializeField] public TextMeshProUGUI title;
    [SerializeField] public int CurChapter;


    void Awake()
    {
        ItemList = new List<Item>(6);
        //PivotList = new List<Pivot>(6);

    }

    void OnEnable()
    {
        if (CurChapter == 0)
        {
            CurChapter = 1;
            string[] itemTemp = LubanLoader.Tables.TbPuzzle[1].InitItemList;

        }
        GetItem("test");
    }

    void OnDisable()
    {
        foreach (var pivot in PivotList)
        {
            if (pivot.IsUsing)
            {
                Debug.Log(string.Format("Destroy item {0}", pivot.CurItem.Name));
                Destroy(pivot.CurItem.gameObject);
                pivot.IsUsing = false;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit()
    {
        GameStart.SetActive(true);
        this.gameObject.SetActive(false);
    }

    void GetItem(string name)
    {
        GameObject prefab = Resources.Load<GameObject>(string.Format("Item/{0}",name));
        if (prefab)
        {
            //用内存中GameObject模板克隆一个出来,用加载得到的资源对象，实例化游戏对象，实现游戏物体的动态加载  
            GameObject obj = GameObject.Instantiate(prefab) as GameObject;
            if (obj)
            {
                Item tmpItem= obj.GetComponent<Item>();
                GameObject pivot = GetPivot(tmpItem);
                if (pivot != null)
                {
                    tmpItem.Init(pivot, name);
                    tmpItem.gameObject.transform.position = pivot.transform.position;
                }
                obj.transform.SetParent(GameMainObject.transform, false);
                ItemList.Add(tmpItem);

            }
        }  

    }

    GameObject GetPivot(Item item)
    {
        foreach (var pivot in PivotList)
        {
            if (!pivot.IsUsing)
            {
                pivot.IsUsing = true;
                pivot.CurItem = item;
                return pivot.gameObject;
            }
        }
        return null;
    }
}
