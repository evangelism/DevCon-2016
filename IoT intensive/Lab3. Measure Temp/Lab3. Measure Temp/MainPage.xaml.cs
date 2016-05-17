using IoTX.Devices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace Lab3.Measure_Temp
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        BMP180 Sens;
        DispatcherTimer dt = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };

        MultiLED LED = new MultiLED(new int[] { 17, 27, 22 });

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            Sens = new BMP180();
            await Sens.Init();
            dt.Tick += ReMeasure;
            dt.Start();
        }

        private void ReMeasure(object sender, object e)
        {
            var t = Sens.Temperature;
            txt.Text = $"{t}C";
            if (t < 21) LED.SetInt(0);
            else if (t < 25) LED.SetInt(1);
            else if (t < 28) LED.SetInt(2);
            else if (t < 31) LED.SetInt(4);
            else LED.SetInt(7);
        }
    }
}
