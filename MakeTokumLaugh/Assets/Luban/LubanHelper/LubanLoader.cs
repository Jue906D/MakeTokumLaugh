using cfg;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Luban;
using UnityEngine;

public static class LubanLoader
{
    public static string DataPath = Application.dataPath+"/GameMain/Data/ConfigData";
    public static cfg.Tables Tables = null;


    public static void AutoTypeInit()
    {
        var tablesCtor = typeof(cfg.Tables).GetConstructors()[0];
        var loaderReturnType = tablesCtor.GetParameters()[0].ParameterType.GetGenericArguments()[1];
        // ����cfg.Tables�Ĺ��캯����Loader�ķ���ֵ���;���ʹ��json����ByteBuf Loader
        System.Delegate loader = loaderReturnType == typeof(ByteBuf) ?
            new System.Func<string, ByteBuf>(LoadByteBuf)
            : (System.Delegate)new System.Func<string, JSONNode>(LoadJson);
        Tables = (cfg.Tables)tablesCtor.Invoke(new object[] { loader });
       
    }

    /*
    void Start()
    {
         //
        var test = Tables.TbBattleGacha["1"].ExcluId; 
    
        // ����һ��������
        Console.WriteLine(tables.TbGlobal.Name);
        // ������ͨ�� key-value ��
        Console.WriteLine(tables.TbItem.Get(12).Name);
        // ֧�� operator []�÷�
        Console.WriteLine(tables.TbMail[1001].Desc);
    }
    */

    private static JSONNode LoadJson(string file)
    {
        return JSON.Parse(File.ReadAllText($"{DataPath}/json/{file}.json", System.Text.Encoding.UTF8));
    }

    private static ByteBuf LoadByteBuf(string file)
    {
        return new ByteBuf(File.ReadAllBytes($"{DataPath}/bin/{file}.bytes"));
    }
}

