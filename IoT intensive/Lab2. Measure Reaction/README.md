#DevCon 2016: IoT-интенсив 

# Лабораторная 2: Измеряем время реакции

В этой работе нам предстоит собрать схему, которая будет измерять время реакции пользователя на зажигание светодиода.
Для этого мы подключим к контроллеру кнопку, как показано на схеме:

![LED Layout](../images/LEDLayout.PNG)

Необходимо зажигать светодиод через случайный интервал времени, после чего засекать время (в ticks), прошедшее до нажатия на кнопку.

Для измерения времени можно использовать объект `Stopwatch`. Для ввода данных с кнопки необходимо сконфигурировать один из портов на ввод,
с так называемым pullup (чтобы в разомкнутом состоянии кнопки вход находился в высоком уровне):

```
var gpio = GpioController.GetDefault();
pin = gpio.OpenPin(26);        
pin.SetDriveMode(GpioPinDriveMode.InputPullUp);
```

Затем можно работать со входом двумя способами: считывая сигнал с помощью `pin.Read()`, или определив реакцию на прерывание
(т.е. чтобы при нажатии кнопки вызывалась некоторая функция):
```
var gpio = GpioController.GetDefault();
pin = gpio.OpenPin(26);        
pin.SetDriveMode(GpioPinDriveMode.InputPullUp);
pin.DebounceTimeout = TimeSpan.FromMilliseconds(50);
pin.ValueChanged += ButtonPressed;
...
private void ButtonPressed(GpioPin sender,
                           GpioPinValueChangedEventArgs args)
{
   if (args.Edge==GpioPinEdge.RisingEdge) { ... }
}
```
