#if UNITY_EDITOR

using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 框架配套工具
/// </summary>
namespace  Tokum
{
    public static class LubanGenTools
    {
        //[MenuItem("Tokum/LubanGenTools/GenAll")]
        private static void GenProtoTools()
        {
            /* 未完成
    #if UNITY_EDITOR_WIN
            Application.OpenURL(Path.Combine(Application.dataPath, "../LubanTools/Proto/Deer_Gen_Proto.bat"));
    #else
            string shellPath = Path.Combine(Application.dataPath, "../LubanTools/Proto/Deer_Gen_Proto.sh");

            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
            psi.FileName = shellPath;
            psi.UseShellExecute = false;
            psi.StandardOutputEncoding = System.Text.Encoding.UTF8;
            psi.RedirectStandardOutput = true;
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(psi);
            string strOutput = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Close();
            p.Dispose();
            UnityEngine.Debug.Log(strOutput);
    #endif
            */
        }

        [MenuItem("Tokum扩展/Luban配置工具/重新生成")]
        private static void GenAllLubanConfig()
        {
#if UNITY_EDITOR_WIN 
            Application.OpenURL(Path.Combine(Application.dataPath, "../LubanTools/AutoLoopJson.bat"));
#else
        string shellPath = Path.Combine(Application.dataPath, "../LubanTools/DesignerConfigs/Deer_Build_Config.sh");
        //string shellPath = Application.dataPath + "/../LubanTools/DesignerConfigs/Deer_Build_Config.sh";
        System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
        psi.FileName = shellPath;
        psi.UseShellExecute = false;
        psi.StandardOutputEncoding = System.Text.Encoding.UTF8;
        psi.RedirectStandardOutput = true;
        try
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(psi);
            string strOutput = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Close();
            p.Dispose();
            UnityEngine.Debug.Log(strOutput);
        }
        catch (System.Exception e)
        {
            if (e.ToString().Contains("0x80004005"))
            {
                throw new System.Exception($"请先修改文件权限，终端定位到[Deer_Build_Config.sh]目录，执行[chmod 777 Deer_Build_Config.sh]命令。 error: {e}");
            }
            else
            {
                throw new System.Exception($"可能文件格式编码不对，请核查！ error: {e}");
            }
        }

#endif

        }

        [MenuItem("Tokum扩展/Luban配置工具/打包生成")]
        private static void GenAllLubanConfigBin()
        {
            Application.OpenURL(Path.Combine(Application.dataPath, "../LubanTools/AutoLoopBin.bat"));
        }
        
        [MenuItem("Tokum扩展/Luban配置工具/打开Excel文件夹")]
        private static void OpenExcelFolder()
        {
            Application.OpenURL(Path.Combine(Application.dataPath, "../LubanTools/Datas"));
        }
    }
}


#endif
