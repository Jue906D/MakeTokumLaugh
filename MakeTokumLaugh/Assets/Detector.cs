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

    private void OnTriggerStay2D(Collider2D collision)
    {
        var initem = collision.gameObject.GetComponent<Item>();
        initem.isDetect =true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var initem = collision.gameObject.GetComponent<Item>();
        initem.isDetect = false;

    }
}
