using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Screnshots
{
    public static class Settings
    {
        private static Key firstKeyShow = (Key)GlobalSettings.Default.FirstKeyShow;
        private static Key secondKeyShow = (Key)GlobalSettings.Default.SecondKeyShow;
        private static Key firstKeyHide = (Key)GlobalSettings.Default.FirstKeyHide;
        private static Key secondKeyHide = (Key)GlobalSettings.Default.SecondKeyHide;
        private static Key createScreenshotFirstKey = (Key)GlobalSettings.Default.CreateScreenshotFirstKey;
        private static Key createScreenshotSecondKey = (Key)GlobalSettings.Default.CreateScreenshotSecondKey;

        public static Key FirstKeyShow
        {            
            get { return firstKeyShow; }
            set { firstKeyShow =value; }
        }
        public static Key SecondKeyShow
        {
            get { return secondKeyShow; }
            set { secondKeyShow=value; }
        }
        public static Key FirstKeyHide
        {
            get { return firstKeyHide; }
            set { firstKeyHide=value; }
        }
        public static Key SecondKeyHide
        {
            get { return secondKeyHide; }
            set { secondKeyHide=value; }
        }
        public static Key CreateScreenshotFirstKey
        {
            get { return createScreenshotFirstKey; }
            set { createScreenshotFirstKey=value; }
        }
        public static Key CreateScreenshotSecondKey
        {
            get { return createScreenshotSecondKey; }
            set { createScreenshotSecondKey=value; }
        }
        public static long NumberSave
        {
            get { return GlobalSettings.Default.NumberSave; }
            set { GlobalSettings.Default.NumberSave = value; }
        }
        public static String PathSave
        {
            get { return GlobalSettings.Default.PathSave;}
            set { GlobalSettings.Default.PathSave = value; }
        }

        public static void Save()
        {
            GlobalSettings.Default.FirstKeyShow = (int)firstKeyShow;
            GlobalSettings.Default.SecondKeyShow = (int)secondKeyShow;
            GlobalSettings.Default.FirstKeyHide = (int)firstKeyHide;
            GlobalSettings.Default.SecondKeyHide = (int)secondKeyHide;
            GlobalSettings.Default.CreateScreenshotFirstKey = (int)createScreenshotFirstKey;
            GlobalSettings.Default.CreateScreenshotSecondKey = (int)createScreenshotSecondKey;
            GlobalSettings.Default.Save();
        }
        
    }
}
