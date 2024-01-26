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
        initem.EndDrag(true);
        Debug.Log(string.Format("Trigger£¡Id is : {0}", name));
    }


}
