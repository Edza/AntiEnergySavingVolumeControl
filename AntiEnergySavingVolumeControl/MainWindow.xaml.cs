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
using System.Threading;

namespace AntiEnergySavingVolumeControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KeyboardHooks _hook = new KeyboardHooks();
        CancellationTokenSource _cancelToken;
        Task _task;
        int _forcedMinHitCount = 0;
        bool _keyUp = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.Slider.Value = VolumeController.GetVolume();
            this.Left = 80;
            this.Top = 80;

            this._hook.HookedKeys.Add(System.Windows.Forms.Keys.VolumeDown);
            this._hook.HookedKeys.Add(System.Windows.Forms.Keys.VolumeUp);
            this._hook.KeyDown += Hook_KeyDown;
            this._hook.KeyUp += Hook_KeyUp;

            this.Slider.ValueChanged += Slider_ValueChanged;
        }

        private async void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.Slider.Value < 22)
            {
                this.Indicator.Fill = Brushes.Maroon;
            }
            else
            {
                var converter = new BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#FF155D78");
                this.Indicator.Fill = brush;
            }

            try
            {
                if (this._cancelToken != null)
                {
                    var previousTask = this._task;
                    var previousToken = this._cancelToken;

                    this._cancelToken.Cancel();
                }

                this.Show();

                if (this.WindowState == WindowState.Minimized)
                    this.WindowState = WindowState.Normal;

                VolumeController.SetVolume((int)this.Slider.Value);

                if (this._keyUp == false)
                    return;

                this._cancelToken = new CancellationTokenSource();

                this._task = Task.Delay(2300, this._cancelToken.Token);

                await this._task;

                this.Hide();
                this._forcedMinHitCount = 0;

                this._cancelToken = null;
            }
            catch (TaskCanceledException)
            {
                // Lets ignore this for now, further investigation; then fix
            }
        }

        private void Hook_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this._keyUp = false;

            if (e.KeyCode == System.Windows.Forms.Keys.VolumeDown)
            {
                if (this.Slider.Value == 22)
                {
                    if (this._forcedMinHitCount == 50)
                    {
                        this._forcedMinHitCount = 0;
                        this.Slider.Value--;
                    }
                    else
                    {
                        this._forcedMinHitCount++;
                        // so show/hide works
                        Slider_ValueChanged(this.Slider, null);
                    }
                }
                else
                {
                    this.Slider.Value--;
                }
            }
            else
            {
                this.Slider.Value++;
            }

            e.Handled = true;
        }


        private void Hook_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            
            this._keyUp = true;
            e.Handled = true;
            // so show/hide works
            Slider_ValueChanged(this.Slider, null);
        }
    }
}
