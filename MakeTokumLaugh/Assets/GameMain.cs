using cfg;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    [SerializeField] public GameObject GameMainObject;
    [SerializeField] public GameObject GameStart;
    [SerializeField] public GameObject UITop;
    [SerializeField] public GameObject UIBottom;
    [SerializeField] public GameObject Internal;
    // Start is called before the first frame update
    [SerializeField] public List<Item> ItemList;
    [SerializeField] public List<Pivot> PivotList;
    [SerializeField] public List<GameObject> LevelNode;

    [SerializeField] public TextMeshProUGUI title;
    [SerializeField] public TextMeshProUGUI desc;
    [SerializeField] public int CurPuzzle;//
    [SerializeField] public int CurLevel;//关卡
    public static GameMain Main;

    void Awake()
    {
        ItemList = new List<Item>(6);
        //PivotList = new List<Pivot>(6);
        Main = this;
    }

    void OnEnable()
    {
        if (CurPuzzle == 0)
        {
            //CurPuzzle = 1;
            CurLevel = 1;
            //加载标题描述
            title.text = LubanLoader.Tables.TbPuzzle[CurLevel].PuzzleName;
            desc.text = LubanLoader.Tables.TbPuzzle[CurLevel].PuzzleName;
            
            //生成道具
            string[] itemTemp = LubanLoader.Tables.TbPuzzle[CurLevel].InitItemList;
            foreach (var id in itemTemp)
            {
                GetItem(id,LubanLoader.Tables.TbItem[id].Prefab);
            }


        }
        //GetItem("test");
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

    void GetItem(string id,string name)
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
                    tmpItem.Init(pivot, id,name);
                    tmpItem.gameObject.transform.position = pivot.transform.position;
                }
                obj.transform.SetParent(GameMainObject.transform, false);
                ItemList.Add(tmpItem);

            }
        }
        else
        {
            Debug.Log(string.Format("找不到预制体 {0}", name));
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

    public IEnumerator NextLevel()
    {
        if (CurLevel == 4)
        {
            //过了第四关就换谜题
            //过关黑幕，重置物品
            ShowInternal();
            //重置过关不保留物品
            foreach (var pivot in PivotList)
            {
                if (pivot.IsUsing && !pivot.CurItem.IsReserved)
                {
                    Debug.Log(string.Format("Destroy item {0}", pivot.CurItem.Name));
                    Destroy(pivot.CurItem.gameObject);
                    pivot.IsUsing = false;
                }

            }

            CurLevel = 1;
            CurPuzzle ++;
        }
        else
        {
            ShowInternal();
            yield return null;
            CurLevel++;
        }
    }

    public IEnumerator ShowFeedback(string name)
    {
        GameObject prefab = Resources.Load<GameObject>(string.Format("Feedback/{0}", name));
        if (prefab)
        {
            //用内存中GameObject模板克隆一个出来,用加载得到的资源对象，实例化游戏对象，实现游戏物体的动态加载  
            GameObject obj = GameObject.Instantiate(prefab) as GameObject;
            if (obj)
            {
                obj.transform.SetParent(UITop.transform, false);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = new Vector3(1f, 1f, 1f);
                yield return 0; //new WaitForSeconds(3f);
                //Destroy(obj);
            }
        }
        else
        {
            Debug.Log(string.Format("找不到预制体 {0}", name));
        }

    }

    public void ShowInternal()
    {
        Internal.SetActive(true);

    }
}
