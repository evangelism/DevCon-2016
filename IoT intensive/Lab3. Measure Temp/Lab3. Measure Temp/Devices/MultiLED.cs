using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace IoTX.Devices
{
    public class MultiLED
    {

        GpioPin[] pins;

        public MultiLED(int[] pin_no)
        {
            pins = new GpioPin[pin_no.Length];
            var Gpio = GpioController.GetDefault();
            for (int i=0;i<pin_no.Length;i++)
            {
                pins[i] = Gpio.OpenPin(pin_no[i]);
                pins[i].SetDriveMode(GpioPinDriveMode.Output);
                pins[i].Write(GpioPinValue.Low);
            }
        }

        public void SetAll(GpioPinValue v)
        {
            foreach (var p in pins) p.Write(v);
        }

        public void SetInt(int x)
        {
            foreach (var p in pins)
            {
                p.Write(x % 2 > 0 ? GpioPinValue.High : GpioPinValue.Low);
                x /= 2;
            }
        }

    }
}
