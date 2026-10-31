using System;

namespace SmartHome
{
    public class SmartLamp : BaseDevice
    {
        public int Brightness { get; private set; }

        // Вызов конструктора базы через : base(...)
        public SmartLamp(string name, int power, string roomName, int brightness = 100)
            : base(name, power, roomName)
        {
            Brightness = brightness;
            Console.WriteLine($"[SmartLamp] Создана лампа: {Name}");
        }

        // Переопределение абстрактного метода
        public override string GetDeviceType() => "Умная лампа";

        // Переопределение с вызовом base.Describe()
        public override string Describe()
        {
            return base.Describe() + $", яркость: {Brightness}%";
        }

        public void SetBrightness(int brightness)
        {
            if (brightness < 0 || brightness > 100)
            {
                Console.WriteLine("  Ошибка: Яркость должна быть от 0 до 100.");
                return;
            }
            Brightness = brightness;
            Console.WriteLine($"  Яркость лампы '{Name}' изменена на {Brightness}%.");
        }

        // Поверхностное копирование
        public SmartLamp ShallowCopy()
        {
            // MemberwiseClone копирует все поля "как есть" (для ссылочных типов копирует ссылки)
            return (SmartLamp)MemberwiseClone();
        }

        // Глубокое копирование
        public SmartLamp DeepCopy()
        {
            // Создаем новый объект через конструктор, передавая независимые значения
            return new SmartLamp(this.Name, this.Power, this.RoomName, this.Brightness);
        }
    }
}