using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Screnshots
{
    /// <summary>
    /// Логика взаимодействия для MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public delegate void ConfirmTest();
        List<MainWindow> ChildWindow = new List<MainWindow>();

        public MenuWindow()
        {
            InitializeComponent();
            ShowFirstKey.Text = Convert.ToString(Settings.FirstKeyShow);
            HideFirstKey.Text=Convert.ToString(Settings.FirstKeyHide);
            CreateScreenshotFirstKey.Text = Convert.ToString(Settings.CreateScreenshotFirstKey);

            ShowSecondKey.Text = Convert.ToString(Settings.SecondKeyShow);
            HideSecondKey.Text = Convert.ToString(Settings.SecondKeyHide);
            CreateScreenshotSecondKey.Text = Convert.ToString(Settings.CreateScreenshotSecondKey);
            Path.Text = Settings.PathSave;

            foreach (var a in grid.Children)
            {
                TextBox m = a as TextBox;
                if (m != null)
                {
                    m.MouseDown += M_MouseDown;
                    m.MouseLeave += M_MouseLeave;
                }
            }
            ShowFirstKey.KeyDown += ShowFirstKey_KeyDown;
            HideFirstKey.KeyDown += HideFirstKey_KeyDown;
            CreateScreenshotFirstKey.KeyDown += CreateScrenshotFirstKey_KeyDown;
            ShowSecondKey.KeyDown += ShowSecondKey_KeyDown;
            HideSecondKey.KeyDown += HideSecondKey_KeyDown;
            CreateScreenshotSecondKey.KeyDown += CreateScreenshotSecondKey_KeyDown;
            Window.Closing += Window_Closing;
            Path.MouseDown -= M_MouseDown;
            Path.MouseLeave -= M_MouseLeave;
            Path.MouseDoubleClick += Path_MouseDoubleClick;
            
        }

        private void M_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((TextBox)sender).Focusable = true;
            ((TextBox)sender).Focus();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (var a in ChildWindow)
            {
                a.Close();
            }
            Settings.Save();
        }

        private void CreateScreenshotSecondKey_KeyDown(object sender, KeyEventArgs e)
        {
            Test(sender, e, delegate ()
            {
                Settings.CreateScreenshotSecondKey = e.Key;
            });
        }

        private void HideSecondKey_KeyDown(object sender, KeyEventArgs e)
        {
            Test(sender, e, delegate ()
            {
                Settings.SecondKeyHide = e.Key;
            });
        }

        private void ShowSecondKey_KeyDown(object sender, KeyEventArgs e)
        {
            Test(sender, e, delegate ()
            {
                Settings.SecondKeyShow = e.Key;
            });
        }

        private void CreateScrenshotFirstKey_KeyDown(object sender, KeyEventArgs e)
        {
            Test(sender, e, delegate ()
            {
                Settings.CreateScreenshotFirstKey = e.Key;
            });
        }

        private void HideFirstKey_KeyDown(object sender, KeyEventArgs e)
        {
            Test(sender, e, delegate ()
            {
                Settings.FirstKeyHide = e.Key;
            });
        }

        private void ShowFirstKey_KeyDown(object sender, KeyEventArgs e)
        {
            Test(sender, e, delegate ()
            {
                Settings.FirstKeyShow = e.Key;
            });
        }

        private void M_MouseLeave(object sender, MouseEventArgs e)
        {
            ((TextBox)sender).Focusable = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Path.Text == "")
            {
                MessageBox.Show("Путь не выбран");
            }
            else
            {
                if (System.IO.Directory.Exists(Path.Text))
                {
                    this.WindowState = WindowState.Minimized;
                    ChildWindow.Add(new MainWindow());
                    ChildWindow[ChildWindow.Count - 1].Show();
                }
                else
                {
                    MessageBox.Show("Указанный путь не существует");
                }
            }
        }

        public void Test(object sender, KeyEventArgs e,ConfirmTest confirmTest)
        {
            String last = ((TextBox)sender).Text;
            ((TextBox)sender).Text = Convert.ToString(e.Key);
            if (ShowFirstKey.Text + ShowSecondKey.Text == HideFirstKey.Text + HideSecondKey.Text
               || ShowFirstKey.Text + ShowSecondKey.Text == CreateScreenshotFirstKey.Text + CreateScreenshotSecondKey.Text
               || HideFirstKey.Text + HideSecondKey.Text == CreateScreenshotFirstKey.Text + CreateScreenshotSecondKey.Text
               || ShowFirstKey.Text == ShowSecondKey.Text
               || HideFirstKey.Text == HideSecondKey.Text
               || CreateScreenshotFirstKey.Text == CreateScreenshotSecondKey.Text)
            {
                ((TextBox)sender).Text = last;
            }
            else
            {
                confirmTest();
            }
        }

        private void Path_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog a = new System.Windows.Forms.FolderBrowserDialog();
            a.ShowDialog();
            if (a.SelectedPath == "")
            {
                MessageBox.Show("Путь не выбран");
            }
            else
            {
                if (System.IO.Directory.Exists(a.SelectedPath))
                {
                    Settings.PathSave = a.SelectedPath;
                    Path.Text = a.SelectedPath;
                }
                else
                {
                    MessageBox.Show("Указанный путь не существует");
                }
            } 
        }
    }
}
