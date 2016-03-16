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

namespace AntiEnergySavingVolumeControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static KeyboardHooks _hook = new KeyboardHooks();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.Hide();
            this.Left = 80;
            this.Top = 80;

            _hook.HookedKeys.Add(System.Windows.Forms.Keys.VolumeDown);
            _hook.HookedKeys.Add(System.Windows.Forms.Keys.VolumeUp);
            _hook.KeyDown += Hook_KeyDown;
            _hook.KeyUp += Hook_KeyUp;
        }

        private void Hook_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.VolumeDown)
                this.Slider.Value--;
            else
                this.Slider.Value++;

            e.Handled = true;
        }


        private void Hook_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            e.Handled = true;
        }
    }
}
