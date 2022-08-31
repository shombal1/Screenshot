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
        public static Key FirstKeyShow
        {            
            get { return (Key)GlobalSettings.Default.FirstKeyShow; }
            set
            {
                GlobalSettings.Default.FirstKeyShow=(int)value;
            }
        }
        public static Key SecondKeyShow
        {
            get { return (Key)GlobalSettings.Default.SecondKeyShow; }
            set
            {
                GlobalSettings.Default.SecondKeyShow=(int)value;
            }
        }
        public static Key FirstKeyHide
        {
            get { return (Key)GlobalSettings.Default.FirstKeyHide; }
            set
            {
                GlobalSettings.Default.FirstKeyHide=(int)value;
            }
        }
        public static Key SecondKeyHide
        {
            get { return (Key)GlobalSettings.Default.SecondKeyHide; }
            set
            {
                GlobalSettings.Default.SecondKeyHide = (int)value;
            }
        }
        public static Key CreateScreenshotFirstKey
        {
            get { return (Key)GlobalSettings.Default.CreateScreenshotFirstKey; }
            set
            {
                GlobalSettings.Default.CreateScreenshotFirstKey = (int)value;
            }
        }
        public static Key CreateScreenshotSecondKey
        {
            get{ return (Key)GlobalSettings.Default.CreateScreenshotSecondKey; }
            set
            {
                GlobalSettings.Default.CreateScreenshotSecondKey = (int)value;
            }
        }
        public static long NumberSave
        {
            get { return GlobalSettings.Default.NumberSave; }
            set
            {
                GlobalSettings.Default.NumberSave = value;
            }
        }
        public static String PathSave
        {
            get { return GlobalSettings.Default.PathSave; }
            set
            {
                GlobalSettings.Default.PathSave = value;
            }
        }

        public static void Save() => GlobalSettings.Default.Save();

    }
}
