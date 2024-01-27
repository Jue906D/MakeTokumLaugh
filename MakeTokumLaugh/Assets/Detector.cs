using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var initem = collision.gameObject.GetComponent<Item>();
        string name = initem.Name;
        var sucname = LubanLoader.Tables.TbPuzzle[GameMain.Main.CurLevel].SucItem;
        if (initem.Name == LubanLoader.Tables.TbItem[sucname].Prefab)
        {
            initem.EndDrag(true);
            Debug.Log(string.Format("Next Level£¡Id is : {0}", name));
        }
        else
        {
            initem.EndDrag(false);
            Debug.Log(string.Format("Wrong Answer£¡Id is : {0}", name));
        }

    }


}
