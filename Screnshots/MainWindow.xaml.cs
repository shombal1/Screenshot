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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Screnshots
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Rectangle Select;
        double X = 0;
        double Y = 0;
        bool DoOnce = false;

        public MainWindow()
        {
            InitializeComponent();
            BaseWindow.Topmost = true;
            BaseWindow.WindowState = WindowState.Maximized;
            BaseWindow.Closing += BaseWindow_Closing;

            DispatcherTimer timre = new DispatcherTimer();
            timre.Interval = TimeSpan.FromMilliseconds(1);
            timre.Tick += Timre_Tick;
            timre.Start();
        }

        private void BaseWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Settings.Save();
        }

        private void Timre_Tick(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyUp(Settings.CreateScreenshotSecondKey))
            {
                DoOnce = true;
            }
            if (Keyboard.IsKeyDown(Settings.FirstKeyShow) && Keyboard.IsKeyDown(Settings.SecondKeyShow))
            {
                BaseWindow.WindowState = WindowState.Maximized;
            }
            if (Keyboard.IsKeyDown(Settings.FirstKeyHide) && Keyboard.IsKeyDown(Settings.SecondKeyHide))
            {
                BaseWindow.WindowState = WindowState.Minimized;
            }

            if (Keyboard.IsKeyDown(Settings.CreateScreenshotFirstKey) && Keyboard.IsKeyDown(Settings.CreateScreenshotSecondKey) && DoOnce)
            {
                DoOnce = false;
                if (Select.Height > 0 && Select.Width > 0)
                {
                    if (System.IO.Directory.Exists(Settings.PathSave))
                    {
                        BaseWindow.WindowState = WindowState.Minimized;
                        using (System.Drawing.Bitmap p = new System.Drawing.Bitmap((int)Select.Width, (int)Select.Height))
                        {
                            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(p);
                            graphics.CopyFromScreen((int)Select.Margin.Left - 7, (int)Select.Margin.Top - 7, 0, 0, p.Size);
                            p.Save($"{Settings.PathSave}\\image{Settings.NumberSave}.jpg");
                            Settings.NumberSave++;
                        }
                        BaseWindow.WindowState = WindowState.Maximized;
                    }
                    else
                    {
                        MessageBox.Show("Указанный путь не существует");
                    }
                }
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            grid.Children.Remove(Select);
            Select = new Rectangle();
            Select.Fill = Brushes.Black;
            Select.HorizontalAlignment = HorizontalAlignment.Left;
            Select.VerticalAlignment = VerticalAlignment.Top;
            Select.Margin = new Thickness(Mouse.GetPosition(BaseWindow).X, Mouse.GetPosition(BaseWindow).Y, 0, 0);
            Y = Select.Margin.Top;
            X = Select.Margin.Left;
            BaseWindow.MouseMove += BaseWindow_MouseMove;
            grid.Children.Add(Select);
        }


        private void BaseWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.GetPosition(BaseWindow).X < X)
            {
                Select.Margin = new Thickness(Mouse.GetPosition(BaseWindow).X, Select.Margin.Top, 0, 0);
                Select.Width = Math.Abs(Mouse.GetPosition(BaseWindow).X - X);
            }
            else
            {
                Select.Width = Math.Abs(Mouse.GetPosition(BaseWindow).X - Select.Margin.Left);
            }
            if (Mouse.GetPosition(BaseWindow).Y < Y)
            {
                Select.Margin = new Thickness(Select.Margin.Left, Mouse.GetPosition(BaseWindow).Y, 0, 0);
                Select.Height = Math.Abs(Mouse.GetPosition(BaseWindow).Y - Y);
            }
            else
            {
                Select.Height = Math.Abs(Mouse.GetPosition(BaseWindow).Y - Select.Margin.Top);
            }
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            BaseWindow.MouseMove -= BaseWindow_MouseMove;
        }
    }
}
