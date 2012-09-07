using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices; 


namespace Kuaff.Tractor
{
    /// <summary>
    /// 提供声音播放的方法
    /// </summary>
    class MciSoundPlayer
    {

        [DllImport("winmm.dll", EntryPoint = "mciSendString", SetLastError = true, CharSet = CharSet.Auto)] 
        private static extern int mciSendString(string lpstrCommand, [MarshalAs(UnmanagedType.LPTStr)]string lpstrReturnString, int uReturnLength, int hwndCallback);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetShortPathName([MarshalAs(UnmanagedType.LPTStr)]string path,[MarshalAs(UnmanagedType.LPTStr)]StringBuilder shortPath,int shortPathLength);
        
        
        public static void Play(string FileName,String alias)
        {
            StringBuilder shortPathTemp = new StringBuilder(255);
            int result = GetShortPathName(FileName, shortPathTemp, shortPathTemp.Capacity);
            string ShortPath = shortPathTemp.ToString();

            mciSendString("open " + ShortPath + " alias " + alias, "", 0, 0);
            mciSendString("play " + alias, "", 0, 0);
        }

        public static  void Stop()
        {
            mciSendString("stop song", "", 0, 0);
        }

        public static void Pause()
        {
            mciSendString("pause song", "", 0, 0);
        }

        public static void Close()
        {
            mciSendString("close song", "", 0, 0);
        }

        public static void CloseAll()
        {
            mciSendString("close all", "", 0, 0);
        }

        public static bool IsPlaying()
        {
                string durLength = "";
                durLength = durLength.PadLeft(128, Convert.ToChar(" "));
                mciSendString("status song mode", durLength, 128, 0);
               
                return durLength.Substring(0, 7).ToLower() == "playing".ToLower();
        }

       

    }
}
