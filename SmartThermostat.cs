using System;

namespace SmartHome
{
    // sealed запрещает наследование (аналог final)
    public sealed class SmartThermostat : BaseDevice
    {
        public double TargetTemp { get; private set; }

        public SmartThermostat(string name, int power, string roomName, double targetTemp)
            : base(name, power, roomName)
        {
            TargetTemp = targetTemp;
            Console.WriteLine($"[SmartThermostat] Создан термостат: {Name}");
        }

        public override string GetDeviceType() => "Термостат";

        // Переопределение БЕЗ вызова base.Describe()
        public override string Describe()
        {
            return $"Термостат '{Name}': целевая температура {TargetTemp} C (комната: {RoomName})";
        }

        public void SetTargetTemp(double temp)
        {
            if (temp < 5.0 || temp > 30.0)
            {
                Console.WriteLine("  Ошибка: Температура должна быть от 5 до 30 C.");
                return;
            }
            TargetTemp = temp;
            Console.WriteLine($"  Термостат '{Name}' настроен на {TargetTemp} C.");
        }
    }
}