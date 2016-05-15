using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lab2.Measure_Reaction
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        GpioPin[] pins = new GpioPin[3];
        GpioPin pin;

        Stopwatch sw = new Stopwatch();

        public MainPage()
        {
            this.InitializeComponent();
            // Инициализируем пины вывода
            var gpio = GpioController.GetDefault();
            pins[0] = gpio.OpenPin(17);
            pins[1] = gpio.OpenPin(27);
            pins[2] = gpio.OpenPin(22);
            for (int i = 0; i < 3; i++) pins[i].SetDriveMode(GpioPinDriveMode.Output);

            // Инициализируем пин ввода
            pin = gpio.OpenPin(26);
            pin.SetDriveMode(GpioPinDriveMode.InputPullUp);
            pin.DebounceTimeout = TimeSpan.FromMilliseconds(50);
            pin.ValueChanged += ButtonPressed;

            DoWork();

        }

        Random Rnd = new Random();

        private async Task DoWork()
        {
            while (true)
            {
                SetAllPins(GpioPinValue.Low);
                await Task.Delay(Rnd.Next(1000, 5000));
                for (int i = 0; i < 3; i++)
                {
                    pins[i].Write(GpioPinValue.High);
                    await Task.Delay(500);
                }
                SetAllPins(GpioPinValue.Low);
                await Task.Delay(Rnd.Next(1000, 3000));
                SetAllPins(GpioPinValue.High);
                sw.Restart();
                while (sw.IsRunning) await Task.Delay(100);
                txt.Text = sw.ElapsedTicks.ToString();
            }
        }

        private void SetAllPins(GpioPinValue val)
        {
            for (int i = 0; i < 3; i++) pins[i].Write(val);
        }

        private void ButtonPressed(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            if (args.Edge==GpioPinEdge.RisingEdge)
            {
                sw.Stop();
            }
        }
    }
}
