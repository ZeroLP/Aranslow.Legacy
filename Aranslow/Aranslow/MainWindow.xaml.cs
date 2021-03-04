using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Aranslow.Objects;

namespace Aranslow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private class Timers
        {
            static Timers()
            {
                RenderTimer.Tick += RenderTimer_Tick;
                LocalMovementTimer.Tick += LocalMovementTimer_Tick;
            }

            internal static DispatcherTimer RenderTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 10) };
            internal static DispatcherTimer LocalMovementTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 50) };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LocalPlayer_Placeholder.Visibility = Visibility.Hidden;

            Timers.RenderTimer.Start();

            ObjectManager.PopulateGameObjects();

            foreach (var gObj in ObjectManager.GameObjects)
            {
                this.MainGrid.Children.Add(gObj.Model);
                this.MainGrid.Children.Add(gObj.BoundingBox);
            }
        }

        private static void RenderTimer_Tick(object sender, EventArgs e)
        {
            if (ObjectManager.IsPopulated)
            {
                lock (ObjectManager.GameObjects)
                {
                    foreach (var gObj in ObjectManager.GameObjects)
                        gObj.Draw();
                }
            }
        }

        private static void LocalMovementTimer_Tick(object sender, EventArgs e)
        {
            ObjectManager.LocalPlayer.Move();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.IsRepeat) return;

            switch (e.Key)
            {
                case Key.Right:
                    ObjectManager.LocalPlayer.Act(GameObject.ActionState.WalkRight);
                    break;
                case Key.Left:
                    ObjectManager.LocalPlayer.Act(GameObject.ActionState.WalkLeft);
                    break;
                case Key.X:
                    ObjectManager.LocalPlayer.Act(GameObject.ActionState.BasicAttack);
                    break;
                default:
                    break;
            }

            ObjectManager.LocalPlayer.Move();
            Timers.LocalMovementTimer.Start();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            ObjectManager.LocalPlayer.Act(GameObject.ActionState.Idle);
            Timers.LocalMovementTimer.Stop();
        }
    }
}
