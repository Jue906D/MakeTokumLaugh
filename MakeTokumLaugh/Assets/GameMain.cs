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
    [SerializeField] public GameObject GO;
    // Start is called before the first frame update
    [SerializeField] public List<Item> ItemList;
    [SerializeField] public List<Pivot> PivotList;
    [SerializeField] public List<GameObject> LevelNode;
    [SerializeField] public List<GameObject> Levels;
    public Dictionary<string, GameObject> ItemDict; 

    [SerializeField] public TextMeshProUGUI title;
    [SerializeField] public TextMeshProUGUI desc;
    [SerializeField] public int CurPuzzle;//
    [SerializeField] public int CurLevel;//�ؿ�
    [SerializeField] public bool Reset = false;
    public static GameMain Main;
    public Coroutine co ;

    void Awake()
    {
        ItemList = new List<Item>(6);
        //PivotList = new List<Pivot>(6);
        Main = this;
        ItemDict = new Dictionary<string, GameObject>();
        Reset = false;
    }

    void OnEnable()
    {
        if (CurPuzzle == 0)
        {
            //CurPuzzle = 1;
            CurLevel = 1;
            //���ر�������
            title.text = LubanLoader.Tables.TbPuzzle[CurLevel].PuzzleName;
            desc.text = LubanLoader.Tables.TbPuzzle[CurLevel].PuzzleName;
            
            //���ɵ���
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
                ItemDict.Remove(pivot.CurItem.Id);
                Debug.Log(string.Format("Delete Item {0}", name));
            }

        }
        ItemDict.Clear();
        CurLevel = 0;
        Levels[1].SetActive(false);
        Levels[2].SetActive(false);
        Levels[3].SetActive(false);
        Levels[4].SetActive(false);
        CurPuzzle = 0;
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

    public void GetItem(string id,string name)
    {
        if (ItemDict.ContainsKey(id))
        {
            Debug.Log(string.Format("�Ѵ��ڣ�δ���� {0}", id));
            return;
        }

        GameObject prefab = Resources.Load<GameObject>(string.Format("Item/{0}",name));
        if (prefab)
        {
            //���ڴ���GameObjectģ���¡һ������,�ü��صõ�����Դ����ʵ������Ϸ����ʵ����Ϸ����Ķ�̬����  
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
                ItemDict.Add(id,obj);
                Debug.Log(string.Format("Add Item {0}",name));

            }
        }
        else
        {
            Debug.Log(string.Format("�Ҳ���Ԥ���� {0}", name));
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
            Debug.Log("In4");
            //���˵��Ĺؾͻ�����
            //���غ�Ļ��������Ʒ
            ShowInternal();
            Levels[CurLevel].SetActive(true);
            //���ù��ز�������Ʒ
            foreach (var pivot in PivotList)
            {
                if (pivot.IsUsing)
                {
                    Debug.Log(string.Format("Destroy item {0}", pivot.CurItem.Name));
                    Destroy(pivot.CurItem.gameObject);
                    pivot.IsUsing = false;
                }

            }
            ItemDict.Clear();
            //��ʾ���沢�ȴ�
            Debug.Log("In4"+Reset);
            yield return new WaitUntil(() => Reset);
            Reset = false;
            Debug.Log("End Wait");
            CurLevel = 1;
            Levels[0].SetActive(true);
            Levels[1].SetActive(false);
            Levels[2].SetActive(false);
            Levels[3].SetActive(false);
            Levels[4].SetActive(false);
            CurPuzzle ++;

            Levels[CurLevel].SetActive(true);
            //���ر�������
            title.text = LubanLoader.Tables.TbPuzzle[CurLevel].PuzzleName;
            desc.text = LubanLoader.Tables.TbPuzzle[CurLevel].PuzzleName;

            //���ɵ���
            string[] itemTemp = LubanLoader.Tables.TbPuzzle[CurLevel].InitItemList;
            foreach (var id in itemTemp)
            {
                if (LubanLoader.Tables.TbItem.DataMap.ContainsKey(id))
                {
                    GetItem(id, LubanLoader.Tables.TbItem[id].Prefab);
                }
                else
                {
                    Debug.Log(string.Format("�Ҳ���Ԥ���� {0}", id));
                }

            }
        }
        else
        {
            foreach (var pivot in PivotList)
            {
                if (pivot.IsUsing && !pivot.CurItem.IsReserved)
                {
                    Debug.Log(string.Format("Destroy item {0}", pivot.CurItem.Name));
                    Destroy(pivot.CurItem.gameObject);
                    pivot.IsUsing = false;
                    ItemDict.Remove(pivot.CurItem.Id);
                    Debug.Log(string.Format("Delete Item {0}", pivot.CurItem.Id));
                }

            }
            //yield return null;
            CurLevel++;
            Levels[CurLevel%4 == 0? 3: CurLevel % 4 -1].SetActive(true);
            //���ر�������
            title.text = LubanLoader.Tables.TbPuzzle[CurLevel].PuzzleName;
            desc.text = LubanLoader.Tables.TbPuzzle[CurLevel].PuzzleName;

            //���ɵ���
            string[] itemTemp = LubanLoader.Tables.TbPuzzle[CurLevel].InitItemList;
            foreach (var id in itemTemp)
            {
                if (LubanLoader.Tables.TbItem.DataMap.ContainsKey(id))
                {
                    GetItem(id, LubanLoader.Tables.TbItem[id].Prefab);
                }
                else
                {
                    Debug.Log(string.Format("�Ҳ���Ԥ���� {0}", id));
                }
                
            }
        }
    }

    public IEnumerator ShowFeedback(string name,string id)
    {
        var lockid = LubanLoader.Tables.TbItem[id].LockId;
        if (!string.IsNullOrEmpty(lockid) && !string.IsNullOrWhiteSpace(lockid))
        {
            GetItem(lockid, LubanLoader.Tables.TbItem[lockid].Prefab);
        }

        GameObject prefab = Resources.Load<GameObject>(string.Format("Feedback/{0}", name));
        if (prefab)
        {
            //���ڴ���GameObjectģ���¡һ������,�ü��صõ�����Դ����ʵ������Ϸ����ʵ����Ϸ����Ķ�̬����  
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
            Debug.Log(string.Format("�Ҳ���Ԥ���� {0}", name));
        }

    }

    public void ShowInternal()
    {
        Internal.SetActive(true);

    }

    public void ShowGameOver()
    {
        GO.SetActive(true);

    }

    public void StartNextLevel()
    {
        co = StartCoroutine(GameMain.Main.NextLevel());
    }
}
