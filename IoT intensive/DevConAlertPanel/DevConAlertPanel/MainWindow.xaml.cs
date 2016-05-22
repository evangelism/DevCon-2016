using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace DevConAlertPanel
{

    public class Data
    {
        public string Id { get; set; }
        public float Temperature { get; set; }
        public int Table { get; set; }
        public int No { get; set; }
        public DateTime Time { get; set; }

    }

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Ellipse[,] Tables = new Ellipse[9,8];
        DispatcherTimer[,] Timers = new DispatcherTimer[9, 8];

        public MainWindow()
        {
            InitializeComponent();
            InitDashboard();
            hub = EventHubClient.CreateFromConnectionString("Endpoint=sb://devconhub-ns.servicebus.windows.net/;SharedAccessKeyName=all;SharedAccessKey=13t4Xa5V2oliBzfZJZnsUUQvAQqMX3y1K7hWH6xq2/o=;EntityPath=devconhub");
            Receive(0);
            Receive(1);
        }

        Ellipse NewEllipse(float x, float y, float dx, float dy)
        {
            var E = new Ellipse();
            E.Height = E.Width = 25;
            E.Stroke = new SolidColorBrush(Colors.Black);
            main.Children.Add(E);
            Canvas.SetTop(E, y + dy);
            Canvas.SetLeft(E, x + dx);
            return E;
        }

        void InitDashboard()
        {
            for (int i=0;i<9;i++)
            {
                var R = new Rectangle();
                R.Stroke = new SolidColorBrush(Colors.Black);
                R.StrokeThickness = 3;
                R.Width = 150; R.Height = 80;
                main.Children.Add(R);
                var y = 50 + 170 * (float)(i / 3);
                var x = 50 + 250 * (float)(i % 3);
                Canvas.SetTop(R,y);
                Canvas.SetLeft(R,x);
                Tables[i,0] = NewEllipse(x, y, -30, 5);
                Tables[i,1] = NewEllipse(x, y, -30, 40);
                Tables[i,2] = NewEllipse(x, y, 20, -30);
                Tables[i,3] = NewEllipse(x, y, 100, -30);
                Tables[i,4] = NewEllipse(x, y, 20, 90);
                Tables[i,5] = NewEllipse(x, y, 100, 90);
                Tables[i,6] = NewEllipse(x, y, 155, 5);
                Tables[i,7] = NewEllipse(x, y, 155, 40);
            }
        }

        EventHubClient hub;

        async Task Receive(int partno)
        {
            var ehcg = hub.GetDefaultConsumerGroup();
            Console.WriteLine("Listening to part={0}", hub.GetRuntimeInformation().PartitionIds[partno]);
            var rec = ehcg.CreateReceiver(hub.GetRuntimeInformation().PartitionIds[partno]);
            DateTime lst = DateTime.Now;
            while (true)
            {
                var msg = await rec.ReceiveAsync();
                var s = Encoding.UTF8.GetString(msg.GetBytes());
                Data D;
                try
                {
                    D = Newtonsoft.Json.JsonConvert.DeserializeObject<Data>(s);
                }
                catch { continue; }
                if (msg.EnqueuedTimeUtc.AddHours(3)>lst)
                //if (DateTime.Now-lst>TimeSpan.FromSeconds(0.9))
                {
                    Console.WriteLine(s);
                    if (D.Table>=0 && D.Table<10 && D.No>=0 && D.No<9)
                    {
                        Tables[D.Table, D.No].Fill = new SolidColorBrush(Colors.Red);
                        var d = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.9) };
                        d.Tick += ((sender, ea) =>
                        {
                            Tables[D.Table, D.No].Fill = new SolidColorBrush(Colors.White);
                            ((DispatcherTimer)sender).Stop();
                        });
                        d.Start();
                    }
                }
                else Console.Write('.');
                // lst = DateTime.Now;
            }
        }
    }
}
