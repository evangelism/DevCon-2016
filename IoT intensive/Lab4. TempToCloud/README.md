#Lab 4: Посылаем погоду в облако

## Создаём IoT Hub в облаке

![Get Access Key](../images/IoTHub_AccessKeys.PNG)

## Получаем код для работы с IoT-хабом

Очень хорошая страничка [Getting Started](https://azure.microsoft.com/ru-ru/develop/iot/get-started/) есть в MSDN. 
Выбираете своё устройство, язык программирования и т.д. - и получаете фрагмент кода.

В нашем случае выбираем Raspberry Pi 2 -> Windows -> C#. При добавлении кода в проект, необходимо также добавить ссылку на NuGet-пакет
`Microsoft.Azure.Devices.Client`.