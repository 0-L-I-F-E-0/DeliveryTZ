using NLog;
using static System.Console;
using static System.Environment;
using static DeliveryTZ.logValue;


namespace DeliveryTZ
{
    class Program
    {
        private const string _storageOrder = "FileOrder.csv";
        private static string _deliveryOrder { get; set; } = "FileFiltering.csv";
        private static string _deliveryLog { get; set; } = "logfile.log";

        static void Main(string[] args)
        {
            // Передача параметров
            if (args.Length == 2)
            {
                _deliveryOrder = args[0];
                _deliveryLog = args[1];
            }
            else if (args.Length == 1)
            {
                _deliveryOrder = args[0];
            }
            GlobalDiagnosticsContext.Set("pathlog", _deliveryLog);

            Log.Info("Запуск приложения");
            ForegroundColor = ConsoleColor.Green;
            Log.Info("Проверка наличия файла с заказами...");
            Check.CheckFile(_storageOrder);
            for (int i = 1; true; i++)
            {
                Log.Info($"Стартовал {i} цикл приложения");
                Clear();
                WriteLine("Выберите цифру из предложенного ниже меню:");
                WriteLine("1) Ввод нового заказа");
                WriteLine("2) Отфильтровать файл фильтрации" + NewLine);
                Check.CheckMenu_AndNext();
            }
        }

        internal static void ChoiceNewOrder()
        {
            Clear();
            WriteLine("Введите данные нового заказа (Вес,Район,Время)");

            int id = LINQrequest.AutoincrementID(_storageOrder);

            double weight = Check.CheckDouble(RangeMin: 0,RangeMax: 300);
            WriteLine(new string('-',20));

            string? district = Check.CheckDistrict();
            WriteLine(new string('-', 20));

            DateTime time = Check.CheckDateTime();

            Log.Info($"Отправка данных в файл {_storageOrder}...");
            try
            {
                File.AppendAllText(_storageOrder, string.Join(';', id, weight, district, time.ToString("yyyy-MM-dd HH:mm:ss") + NewLine));
                Log.Info($"Успешно");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"Ошибка! Данные ошибки:");
            }
        }

        internal static void ChoiceFilteringOrder()
        {
            Clear();
            string _cityDistrict = Check.CheckStreet();
            string[] stringsOrder = LINQrequest.Filtering(_cityDistrict, _storageOrder);
            try
            {
                File.WriteAllLines(_deliveryOrder, stringsOrder);
                Log.Info($"Успешно");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"Ошибка! Данные ошибки:");
            }
        }
    }
}