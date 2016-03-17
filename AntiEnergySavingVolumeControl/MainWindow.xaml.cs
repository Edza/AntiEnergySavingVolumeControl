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

        bool _isMute;
        bool IsMute
        {
            get
            {
                return this._isMute;
            }
            set
            {
                this._isMute = value;
                if(value == true)
                {
                    Muted.Visibility = Visibility.Visible;
                }
                else
                {
                    Muted.Visibility = Visibility.Collapsed;
                }
            }
        }
        int _mutedVolume;

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

            // I got a calculator key on my keboard that maps to this
            // only for me is this behaviour implemented ;)
            // you are fine
            if (Environment.UserName == "edza")
                this._hook.HookedKeys.Add(System.Windows.Forms.Keys.LaunchApplication2);
            else
                this._hook.HookedKeys.Add(System.Windows.Forms.Keys.VolumeMute);

            this._hook.KeyDown += Hook_KeyDown;
            this._hook.KeyUp += Hook_KeyUp;

            this.Slider.ValueChanged += Slider_ValueChanged;
        }

        private async void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.Slider.Value == 0)
            {
                Muted.Visibility = Visibility.Visible;
            }
            else
            {
                Muted.Visibility = Visibility.Collapsed;
            }

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
            // Special processing again for me personally ;) Everyone else is fine
            if((Environment.UserName == "edza" && e.KeyCode == System.Windows.Forms.Keys.LaunchApplication2) ||
                e.KeyCode == System.Windows.Forms.Keys.VolumeMute)
            {
                e.Handled = true;

                if (this.IsMute)
                {
                    this.Slider.Value = this._mutedVolume;
                    this.IsMute = false;
                }
                else
                {
                    this._mutedVolume = (int)this.Slider.Value;
                    this.Slider.Value = 0;
                    this.IsMute = true;
                }
                
                return;
            }

            this._keyUp = false;

            if (this.IsMute)
            {
                this.Slider.Value = this._mutedVolume;
                this.IsMute = false;
            }

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
