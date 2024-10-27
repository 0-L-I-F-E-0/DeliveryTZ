using static DeliveryTZ.logValue;

namespace DeliveryTZ
{
    class LINQrequest
    {
        internal static int AutoincrementID(string _storageOrder)
        {
            Log.Info("Генерация ID");

            List<ModelFile> modelFiles;
            try
            {
                modelFiles = File.ReadAllLines(_storageOrder)
                                 .Select(ModelFile.IDInLINQ)
                                 .ToList();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"Ошибка! Данные ошибки:");
                throw new Exception();
            }
            
            if (modelFiles.Count == 0)
            {
                return 1;
            }
            else
            {
                int maxID = modelFiles.Max(x => x.ID);
                return maxID + 1;
            }
        }

        internal static string[] Filtering(string _cityDistrict, string _storageOrder, int minutes = 30)
        {
            Log.Info("Поиск первого заказа...");

            List<ModelFile> TimeFirst;
            try
            {
                TimeFirst = File.ReadAllLines(_storageOrder)
                                 .Select(ModelFile.StreetAndTimeinLINQ)
                                 .Where(x => x.Street == _cityDistrict)
                                 .OrderBy(x => x.Time)
                                 .ToList();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"Ошибка! Данные ошибки:");
                throw new Exception();
            }

            if (TimeFirst.Count != 0)
            {
                Log.Info("Заказ найден");
                DateTime _firstDeliveryDateTime = TimeFirst.First().Time;
                _firstDeliveryDateTime += TimeSpan.FromMinutes(minutes);

                List<ModelFile> modelFiles;
                try
                {
                    modelFiles = File.ReadAllLines(_storageOrder)
                                     .Select(ModelFile.ValuesInLINQ)
                                     .Where(x => x.Street == _cityDistrict && x.Time <= _firstDeliveryDateTime)
                                     .ToList();
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, $"Ошибка! Данные ошибки:");
                    throw new Exception();
                }

                var strings = new string[modelFiles.Count];
                for (int i = 0; i < strings.Length; i++)
                {
                    strings[i] = modelFiles[i].ToString();
                }
                Log.Info("Отправка списка заказов в файл фильтрации...");
                return strings;
            }
            else
            {
                Log.Info("Заказ не найден, отправка соотвествующего сообщения в файл фильтрации...");
                return ["Заказ с заданой улицей не найден"];
            }
        }
    }
}
