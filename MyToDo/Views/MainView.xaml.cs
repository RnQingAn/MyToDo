
using Dm.util;
using MyToDo.API.Entity;
using MyToDo.API.Service;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace MyToDo.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        
        public MainView()
        {
            InitializeComponent();

            BtnClickEvent();

            menuBar.SelectionChanged += (s, e) => {
                drawrHost.IsLeftDrawerOpen = false;
            };

        }

        private void BtnClickEvent()
        {
            btnMin.Click += (s, e) => { this.WindowState = WindowState.Minimized; };

            btnMax.Click += (s, e) => {
                if (this.WindowState == WindowState.Maximized)
                    this.WindowState = WindowState.Normal;
                else this.WindowState = WindowState.Maximized;
            };

            btnClose.Click += (s, e) => { this.Close(); };

            colorZone.MouseMove += (s, e) => {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };

            colorZone.MouseDoubleClick += (s, e) => {
                if (WindowState == WindowState.Normal)
                    WindowState = WindowState.Maximized;
                else WindowState = WindowState.Normal;
            };
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
