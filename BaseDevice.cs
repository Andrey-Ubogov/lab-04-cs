using System;

namespace SmartHome
{
    // Абстрактный класс (аналог чисто виртуального в C++)
    public abstract class BaseDevice
    {
        private static int _lastId = 0; // Статическое поле (п.11)

        public int Id { get; }
        public string Name { get; }
        public bool IsOn { get; protected set; }
        public bool IsBroken { get; protected set; }
        public int Power { get; }

        // Поле для демонстрации копирования (как в C++ версии)
        public string RoomName { get; protected set; }

        // Конструктор базового класса
        protected BaseDevice(string name, int power, string roomName)
        {
            Id = ++_lastId;
            Name = name;
            Power = power;
            RoomName = roomName;
            IsOn = false;
            IsBroken = false;
            Console.WriteLine($"[BaseDevice] Создано устройство #{Id}: {Name}");
        }

        // Абстрактные методы (обязательны для переопределения)
        public abstract string GetDeviceType();

        // Виртуальный метод (можно переопределить, но есть реализация по умолчанию)
        public virtual string Describe()
        {
            return $"Устройство #{Id} '{Name}' ({Power} Вт, комната: {RoomName}), тип: {GetDeviceType()}";
        }

        // Содержательные методы
        public void TurnOn()
        {
            if (IsBroken)
            {
                Console.WriteLine($"  Ошибка: Устройство '{Name}' сломано! Нельзя включить.");
                return;
            }
            IsOn = true;
            Console.WriteLine($"  Устройство '{Name}' включено.");
        }

        public void Break()
        {
            IsBroken = true;
            IsOn = false;
            Console.WriteLine($"  Устройство '{Name}' сломалось!");
        }

        public void RenameRoom(string newName)
        {
            RoomName = newName;
            Console.WriteLine($"  Комната устройства #{Id} переименована в '{newName}'.");
        }

        // Переопределение Equals и GetHashCode (п. 10.3 методички)
        // Сравниваем по имени и мощности (или можно добавить ID, если нужно строгое равенство)
        public override bool Equals(object? obj)
        {
            if (obj is not BaseDevice other) return false;
            return Name == other.Name && Power == other.Power;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Power);
        }

        public override string ToString() => Describe();
    }
}