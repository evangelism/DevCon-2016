using IoTX.Devices;
using Microsoft.Azure.Devices.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
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

namespace Lab4.TempToCloud
{

    public class TempData
    {
        public string Id { get; set; }
        public double Temperature { get; set; }
        public DateTime Time { get; set; }

        public int Table { get; set; }

        public int No { get; set; }
        public TempData(string id, double Temp, int Table, int No)
        {
            Time = DateTime.Now;
            Temperature = Temp;
            Id = id;
            this.Table = Table;
            this.No = No;
        }
    }

    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const string DeviceConnectionString = "HostName=Hub13.azure-devices.net;DeviceId=RPi;SharedAccessKey=7yn6xx7mZ1IXQvYNZeGIdQOA+mRqaT9xK7FEiwzyu/U=";
        private const int Table = 1;
        private const int No = 13;

        private DeviceClient iothub;

        private BMP180 sensor = new BMP180();
        private MultiLED LED = new MultiLED(new int[] { 17, 27, 22 });

        DispatcherTimer dt = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };

        public MainPage()
        {
            this.InitializeComponent();
            Receive();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await sensor.Init();
            iothub = DeviceClient.CreateFromConnectionString(DeviceConnectionString);
            await iothub.OpenAsync();
            dt.Tick += SendTemperature;
            dt.Start();
        }

        private async void SendTemperature(object sender, object e)
        {
            var t = sensor.Temperature;
            txt.Text = $"{t}C";
            var d = new TempData("RPi", t, Table, No);
            var s = Newtonsoft.Json.JsonConvert.SerializeObject(d);
            var b = Encoding.UTF8.GetBytes(s);
            await iothub.SendEventAsync(new Message(b));
        }

        private async Task Receive()
        {
            await Task.Delay(1000);
            while (true)
            {
                var msg = await iothub.ReceiveAsync();
                if (msg != null)
                {
                    var s = Encoding.ASCII.GetString(msg.GetBytes());
                    LED.SetInt(int.Parse(s));
                    await iothub.CompleteAsync(msg);
                }
            }
        }
    }
}
