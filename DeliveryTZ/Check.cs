using System.Globalization;
using static System.Console;
using static System.Environment;
using static System.String;
using static DeliveryTZ.logValue;

namespace DeliveryTZ
{
    class Check
    {
        internal static void CheckFile(string _storageOrder)
        {
            if (!File.Exists(_storageOrder))
            {
                Log.Warn("Файл отсутствует, создан новый");
                File.Create(_storageOrder).Close();
            }
            else
            {
                Log.Info("Файл найден");
            }
        }
        internal static void CheckMenu_AndNext()
        {
            while (true)
            {
                Log.Info("Ввод цифры меню...");
                ConsoleKey solution = ReadKey(true).Key;
                if (solution == ConsoleKey.D1)
                {
                    Log.Info("Открыто меню нового заказа");
                    Program.ChoiceNewOrder();
                    break;
                }
                else if (solution == ConsoleKey.D2)
                {
                    Log.Info("Открыто меню фильтрации");
                    Program.ChoiceFilteringOrder();
                    break;
                }
                else
                {
                    Log.Warn("Ошибка ввода");
                    Write("Не верный ввод. Нажмите на клавишу предложенную выше" + NewLine);
                }
            }
        }

        internal static double CheckDouble(int RangeMin = 0, int RangeMax = 500)
        {
            while (true)
            {
                Log.Info("Ввод веса...");
                Write("Вес в кг: ");
                if (double.TryParse(ReadLine(), out double doubletmp))
                {
                    if (double.Round(doubletmp,3) == doubletmp)
                    {
                        if (doubletmp > RangeMin && doubletmp <= RangeMax)
                        {
                            Log.Info($"Введен вес: {doubletmp}");
                            return doubletmp;
                        }
                        else
                        {
                            Log.Warn("Ошибка ввода");
                            WriteLine("Превышен доспустимый минимум или максимум");
                        }
                    }
                    else
                    {
                        Log.Warn("Ошибка ввода");
                        WriteLine("Введите число, не больше 3 цифр после запятой");
                    }
                }
                else
                {
                    Log.Warn("Ошибка ввода");
                    WriteLine("Введите число с плавающей запятой");
                }
            }
        }

        internal static string CheckDistrict()
        {
            string? street;
            while (true)
            {
                Log.Info("Ввод улицы...");
                Write("Улица: ");
                street = ReadLine();
                if (!IsNullOrWhiteSpace(street))
                {
                    if (!street.Contains(','))
                    {
                        Log.Info($"Введен успешно");
                        break;
                    }
                    else
                    {
                        Log.Warn("Ошибка ввода");
                        WriteLine("Запятая запрещена");
                    }
                }
                else
                {
                    Log.Warn("Ошибка ввода");
                    WriteLine("Введено пустое значение. Попробуйте ещё раз");
                }
            }

            string? house;
            while (true)
            {
                Log.Info("Ввод улицы...");
                Write("Дом,Корпус(необязательно),Строение(необязательно): ");
                house = ReadLine();
                if (!IsNullOrWhiteSpace(house))
                {
                    if (!house.Contains(','))
                    {
                        Log.Info("Введен успешно");
                        break;
                    }
                    else
                    {
                        Log.Warn("Ошибка ввода");
                        WriteLine("Запятая запрещена");
                    }
                }
                else
                {
                    Log.Warn("Ошибка ввода");
                    WriteLine("Введено пустое значение. Попробуйте ещё раз");
                }
            }

            string district = Join(",", street.Replace(" ", Empty), house.Replace(" ", Empty));
            Log.Info($"Передан район: {district}");
            return district;
        }

        internal static DateTime CheckDateTime()
        {
            while (true)
            {
                Log.Info("Ввод даты и времени заказа...");
                Write("Время в формате (yyyy-MM-dd HH:mm:ss): ");
                if (DateTime.TryParseExact(ReadLine(), "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces, out DateTime timetmp))
                {
                    if (timetmp > DateTime.Now)
                    {
                        Log.Info($"Введена дата и время заказа: {timetmp}");
                        return timetmp;
                    }
                    else
                    {
                        Log.Warn("Ошибка ввода");
                        WriteLine("Нельзя указать прошедшую дату, попробуйте ещё раз");
                    }
                }
                else
                {
                    Log.Warn("Ошибка ввода");
                    WriteLine("Не верный ввод (см. формат выше)");
                }
            }
        }

        internal static string CheckStreet()
        {
            while (true)
            {
                Log.Info("Ввод улицы доставки...");
                Write("Введите улицу доставки: ");
                string? _cityDistrict = ReadLine();
                if (!IsNullOrWhiteSpace(_cityDistrict))
                {
                    if (!_cityDistrict.Contains(','))
                    {
                        Log.Info($"Введена улица доставки: {_cityDistrict}");
                        return _cityDistrict;
                    }
                    else
                    {
                        Log.Warn("Ошибка ввода");
                        WriteLine("Запятая запрещена");
                    }
                }
                else
                {
                    Log.Warn("Ошибка ввода");
                    WriteLine("Введено пустое значение. Попробуйте ещё раз");
                }
            }
        }
    }
}
