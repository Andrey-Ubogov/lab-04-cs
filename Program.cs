using System;
using System.Text;
using static System.Math; // Пример using static (п. 10.5)
using MyDevice = SmartHome.SmartLamp; // Пример псевдонима типа

namespace SmartHome
{
    // Вспомогательный класс для демонстрации using var
    public class IssueLog : IDisposable
    {
        public IssueLog(string title) => Console.WriteLine($"[+] Журнал открыт: {title}");
        public void Write(string text) => Console.WriteLine($"  {text}");
        public void Dispose() => Console.WriteLine($"[-] Журнал закрыт.");
    }

    public static class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("========== ЛР 4. Часть 2: C# ==========\n");

            // 1. Создание наследников (порядок вызовов)
            Console.WriteLine("--- 1. Создание наследников ---");
            MyDevice lamp = new MyDevice("Умная лампа", 10, "Гостиная", 80);
            var thermostat = new SmartThermostat("Термостат 1", 15, "Гостиная", 22.5);

            // 2. Полиморфизм (работа через базовый класс)
            Console.WriteLine("\n--- 2. Виртуальные методы ---");
            BaseDevice[] devices = { lamp, thermostat };
            foreach (var dev in devices)
            {
                Console.WriteLine(dev.Describe());
            }

            // 3. Поверхностное и глубокое копирование
            Console.WriteLine("\n--- 3. Копирование (Shallow vs Deep) ---");

            // MemberwiseClone (поверхностное)
            var shallowCopy = lamp.ShallowCopy();
            shallowCopy.RenameRoom("Спальня");
            Console.WriteLine($"Оригинал: {lamp.Describe()}");
            Console.WriteLine($"Копия:    {shallowCopy.Describe()} <-- (Оригинал изменился! Т.к. MemberwiseClone копирует ссылки)");

            // Глубокое (через кастомный метод)
            var deepCopy = lamp.DeepCopy();
            deepCopy.RenameRoom("Кухня");
            Console.WriteLine($"\nОригинал: {lamp.Describe()}");
            Console.WriteLine($"Копия:    {deepCopy.Describe()} <-- (Оригинал НЕ изменился!)");

            // 4. Equals и GetHashCode (сравнение по содержимому)
            Console.WriteLine("\n--- 4. Сравнение Equals ---");
            var lampClone = new MyDevice("Умная лампа", 10, "Гостиная", 80);
            Console.WriteLine($"lamp.Equals(lampClone) = {lamp.Equals(lampClone)}"); // True
            Console.WriteLine($"HashCode совпадают: {lamp.GetHashCode() == lampClone.GetHashCode()}"); // True
            Console.WriteLine($"Ссылки (ReferenceEquals): {ReferenceEquals(lamp, lampClone)}"); // False

            // 5. Использование using (using var, using static)
            Console.WriteLine("\n--- 5. Использование using ---");
            using var log = new IssueLog("Абонемент умного дома");
            log.Write("Сценарий 'Утро' активирован.");
            // Dispose вызовется автоматически в конце метода Main

            // using static (вызов Sqrt без Math.)
            double result = Round(Sqrt(25), 2);
            Console.WriteLine($"\nUsing static: sqrt(25) = {result}");

            Console.WriteLine("\n========== КОНЕЦ ==========");
        }
    }
}