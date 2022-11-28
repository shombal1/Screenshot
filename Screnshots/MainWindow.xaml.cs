using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        Rectangle SelectRectangle;
        double X = 0;
        double Y = 0;
        bool DoOnce = false;
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            BaseWindow.Topmost = true;
            BaseWindow.WindowState = WindowState.Maximized;
            BaseWindow.Closing+=BaseWindow_Closing;

            timer.Interval = TimeSpan.FromMilliseconds(3);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void BaseWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            timer.Tick -=Timer_Tick;
            Settings.Save();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
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
                if (SelectRectangle!=null && SelectRectangle.Height > 0 && SelectRectangle.Width > 0)
                {
                    if (System.IO.Directory.Exists(Settings.PathSave))
                    {    
                        BaseWindow.WindowState = WindowState.Minimized;
                        using (System.Drawing.Bitmap p = new System.Drawing.Bitmap((int)SelectRectangle.Width, (int)SelectRectangle.Height))
                        {
                            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(p);
                            graphics.CopyFromScreen((int)SelectRectangle.Margin.Left - 7, (int)SelectRectangle.Margin.Top - 7, 0, 0, p.Size);
                            p.Save($"{Settings.PathSave}\\image{Settings.NumberSave}-{System.IO.Path.GetRandomFileName().Replace(".",String.Empty)}.jpg");
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
            timer.Start();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            grid.Children.Remove(SelectRectangle);
            SelectRectangle = new Rectangle();
            SelectRectangle.Fill = Brushes.Black;
            SelectRectangle.HorizontalAlignment = HorizontalAlignment.Left;
            SelectRectangle.VerticalAlignment = VerticalAlignment.Top;
            SelectRectangle.Margin = new Thickness(Mouse.GetPosition(BaseWindow).X, Mouse.GetPosition(BaseWindow).Y, 0, 0);
            Y = SelectRectangle.Margin.Top;
            X = SelectRectangle.Margin.Left;
            BaseWindow.MouseMove += BaseWindow_MouseMove;
            grid.Children.Add(SelectRectangle);
        }


        private void BaseWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.GetPosition(BaseWindow).X < X)
            {
                SelectRectangle.Margin = new Thickness(Mouse.GetPosition(BaseWindow).X, SelectRectangle.Margin.Top, 0, 0);
                SelectRectangle.Width = Math.Abs(Mouse.GetPosition(BaseWindow).X - X);
            }
            else
            {
                SelectRectangle.Width = Math.Abs(Mouse.GetPosition(BaseWindow).X - SelectRectangle.Margin.Left);
            }
            if (Mouse.GetPosition(BaseWindow).Y < Y)
            {
                SelectRectangle.Margin = new Thickness(SelectRectangle.Margin.Left, Mouse.GetPosition(BaseWindow).Y, 0, 0);
                SelectRectangle.Height = Math.Abs(Mouse.GetPosition(BaseWindow).Y - Y);
            }
            else
            {
                SelectRectangle.Height = Math.Abs(Mouse.GetPosition(BaseWindow).Y - SelectRectangle.Margin.Top);
            }
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            BaseWindow.MouseMove -= BaseWindow_MouseMove;
        }
    }
}
