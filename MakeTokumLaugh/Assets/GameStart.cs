using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField] public GameObject GameMain;
    // Start is called before the first frame update
    void Start()
    {
        LubanLoader.AutoTypeInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        GameMain.SetActive(true);
        this.gameObject.SetActive(false);
        
    }
}
