using Microsoft.Azure.Devices.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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

namespace Lab5.ReactionToCloud
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private bool send = true; // send data to cloud or just testing

        private const string Id = "rpi3";
        private const string DeviceConnectionString = "HostName=ReactionHub.azure-devices.net;DeviceId=rpi3;SharedAccessKey=uG8tNYl+RfEF312enlGh62HP8RrB8vNGUFEcSQ0m7gQ=";
        private const string Nick = "loriz";
        private const int Age = 41;
        private const string Sex = "F";
        private const int Dizz = 2;
        // 0 - нормальное состояние
        // 1 - 5 поворотов вокруг своей оси
        // 2 - 10 поворотов вокруг своей оси

        private int DeviceType = 0;
        // 0 - Raspberry Pi
        // 1 - PC UWP


        GpioPin[] pins = new GpioPin[3];
        GpioPin pin;

        Stopwatch sw = new Stopwatch();


        private async Task SendData(long ticks,int noexp,int pw)
        {
            var d = new ReactionData(Id,Nick,Age,Sex,Dizz,ticks,DeviceType,noexp,pw);
            var s = Newtonsoft.Json.JsonConvert.SerializeObject(d);
            var b = Encoding.UTF8.GetBytes(s);
            if (send) await iothub.SendEventAsync(new Message(b));
        }

        private DeviceClient iothub;

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
            // pin.DebounceTimeout = TimeSpan.FromMilliseconds(50);
            // pin.ValueChanged += ButtonPressed;

            iothub = DeviceClient.CreateFromConnectionString(DeviceConnectionString);

            DoWork();

        }

        Random Rnd = new Random();

        private async Task DoWork()
        {
            var noexp = 0;
            int PreWait;
            while (true)
            {
                SetAllPins(GpioPinValue.Low);
                for (int i = 0; i < 3; i++)
                {
                    pins[i].Write(GpioPinValue.High);
                    await Task.Delay(500);
                }
                while (pin.Read() == GpioPinValue.High); // wait for keypress
                SetAllPins(GpioPinValue.Low);
                await Task.Delay(PreWait=Rnd.Next(2000, 5000));
                SetAllPins(GpioPinValue.High);
                sw.Restart();
                if (pin.Read() == GpioPinValue.Low) continue; // if the button is pre-pressed, ignore
                while (pin.Read()==GpioPinValue.High);
                sw.Stop();
                SetAllPins(GpioPinValue.Low);
                txt.Text = sw.ElapsedTicks.ToString();
                await SendData(sw.ElapsedTicks,noexp,PreWait);
                await Task.Delay(1000);
                noexp++;
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
