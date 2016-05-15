using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace Lab1.Advanced_Blink
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        GpioPin[] pins = new GpioPin[3];

        public MainPage()
        {
            this.InitializeComponent();

            // Инициализируем пины ввода-вывода
            var gpio = GpioController.GetDefault();
            pins[0] = gpio.OpenPin(17);
            pins[1] = gpio.OpenPin(27);
            pins[2] = gpio.OpenPin(22);
            for (int i = 0; i < 3; i++) pins[i].SetDriveMode(GpioPinDriveMode.Output);

            // Инициализируем и запускем таймер
            DispatcherTimer dt = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.5) };
            dt.Tick += TickHappened;
            dt.Start();
        }

        int count = 0;

        private void TickHappened(object sender, object e)
        {
            for (int i = 0; i < 3; i++) pins[i].Write(GpioPinValue.Low);
            pins[count].Write(GpioPinValue.High);
            count = (count + 1) % 3;
        }
    }
}
