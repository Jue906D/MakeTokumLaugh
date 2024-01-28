using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close(bool isWin)
    {
        if (isWin)
        {
            GameMain.Main.Reset = true;
            this.gameObject.SetActive(false);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
