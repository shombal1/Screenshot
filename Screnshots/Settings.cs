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
        private static Key _FirstKeyShow=(Key)GlobalSettings.Default.FirstKeyShow;
        private static Key _SecondKeyShow=(Key)GlobalSettings.Default.SecondKeyShow;
        private static Key _FirstKeyHide = (Key)GlobalSettings.Default.FirstKeyHide;
        private static Key _SecondKeyHide= (Key)GlobalSettings.Default.SecondKeyHide;
        private static Key _CreateScreenshotFirstKey= (Key)GlobalSettings.Default.CreateScreenshotFirstKey;
        private static Key _CreateScreenshotSecondKey= (Key)GlobalSettings.Default.CreateScreenshotSecondKey;
        private static long _NumberSave = GlobalSettings.Default.NumberSave;
        private static String _PathSave = GlobalSettings.Default.PathSave;

        public static Key FirstKeyShow
        {            
            get { return _FirstKeyShow; }
            set
            {
                _FirstKeyShow = value;
                GlobalSettings.Default.FirstKeyShow=(int)value;
            }
        }
        public static Key SecondKeyShow
        {
            get { return _SecondKeyShow; }
            set
            {
                _SecondKeyShow = value;
                GlobalSettings.Default.SecondKeyShow=(int)value;
            }
        }
        public static Key FirstKeyHide
        {
            get { return _FirstKeyHide; }
            set
            {
                _FirstKeyHide = value;
                GlobalSettings.Default.FirstKeyHide=(int)value;
            }
        }
        public static Key SecondKeyHide
        {
            get { return _SecondKeyHide; }
            set
            {
                _SecondKeyHide = value;
                GlobalSettings.Default.SecondKeyHide = (int)value;
            }
        }
        public static Key CreateScreenshotFirstKey
        {
            get { return _CreateScreenshotFirstKey; }
            set
            {
                _CreateScreenshotFirstKey = value;
                GlobalSettings.Default.CreateScreenshotFirstKey = (int)value;
            }
        }
        public static Key CreateScreenshotSecondKey
        {
            get{ return _CreateScreenshotSecondKey; }
            set
            {
                _CreateScreenshotSecondKey = value;
                GlobalSettings.Default.CreateScreenshotSecondKey = (int)value;
            }
        }
        public static long NumberSave
        {
            get { return _NumberSave; }
            set
            {
                _NumberSave = value;
                GlobalSettings.Default.NumberSave = value;
            }
        }
        public static String PathSave
        {
            get { return _PathSave; }
            set
            {
                _PathSave = value;
                GlobalSettings.Default.PathSave = value;
            }
        }

        public static void Save() => GlobalSettings.Default.Save();

    }
}
