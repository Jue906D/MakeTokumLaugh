#if UNITY_EDITOR

using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ������׹���
/// </summary>
namespace  Tokum
{
    public static class LubanGenTools
    {
        //[MenuItem("Tokum/LubanGenTools/GenAll")]
        private static void GenProtoTools()
        {
            /* δ���
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

        [MenuItem("Tokum��չ/Luban���ù���/��������")]
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
                throw new System.Exception($"�����޸��ļ�Ȩ�ޣ��ն˶�λ��[Deer_Build_Config.sh]Ŀ¼��ִ��[chmod 777 Deer_Build_Config.sh]��� error: {e}");
            }
            else
            {
                throw new System.Exception($"�����ļ���ʽ���벻�ԣ���˲飡 error: {e}");
            }
        }

#endif

        }

        [MenuItem("Tokum��չ/Luban���ù���/�������")]
        private static void GenAllLubanConfigBin()
        {
            Application.OpenURL(Path.Combine(Application.dataPath, "../LubanTools/AutoLoopBin.bat"));
        }
        
        [MenuItem("Tokum��չ/Luban���ù���/��Excel�ļ���")]
        private static void OpenExcelFolder()
        {
            Application.OpenURL(Path.Combine(Application.dataPath, "../LubanTools/Datas"));
        }
    }
}


#endif
