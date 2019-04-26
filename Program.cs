using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;

namespace ThingWriter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string file = @"all_things.json";

            var serializer = new JsonSerializer
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };

            var array = new JArray();
            if (File.Exists(file) && new FileInfo(file).Length > 0)
            {
                array = JArray.Parse(File.ReadAllText(@"all_things.json", Encoding.UTF8));
            }
            int count = array.Count;
            var isContinue = true;

            while (isContinue)
            {
                var thing = new Thing();
                Console.Write("Thing name EN-US: ");
                thing.ThingNameEn = Console.ReadLine();
                Console.Write("Thing name RU-RU: ");
                thing.ThingNameRu = Console.ReadLine();

                Console.WriteLine("Type: ");
                Console.WriteLine("1 - Документы\n2 - Техника\n3 - Бытовые мелочи, комфорт и удобство\n" +
                    "4 - Гигиена и косметика\n5 - Верхняя одежда\n6 - Повседневная одежда\n7 - Нательное белье\n" +
                    "8 - Аксессуары\n9 - Украшения\n10 - Выходная одежда\n11 - Обувь\n12 - Головные уборы\n13 - Детские вещи");
                var type = Convert.ToInt32(Console.ReadLine());
                switch (type)
                {
                    case 1: thing.Type = "documents"; break;
                    case 2: thing.Type = "equipment"; break;
                    case 3: thing.Type = "comfort"; break;
                    case 4: thing.Type = "cosmetics"; break;
                    case 5: thing.Type = "outerwear"; break;
                    case 6: thing.Type = "casual"; break;
                    case 7: thing.Type = "underwear"; break;
                    case 8: thing.Type = "accessiores"; break;
                    case 9: thing.Type = "decorations"; break;
                    case 10: thing.Type = "dresscode"; break;
                    case 11: thing.Type = "shoes"; break;
                    case 12: thing.Type = "headgear"; break;
                    case 13: thing.Type = "kids"; break;
                }
                Console.WriteLine("Category:");
                bool isAddMore;
                thing.Category = string.Empty;
                do
                {
                    Console.WriteLine("1 - Туризм\n2 - Велотуризм\n3 - Зима\n4 - Пляж\n5 - Командировка\n" +
                        "6 - Дайвинг\n7 - Самое необходимое\n8 - Фотография\n9 - Рыбалка\n10 - Поход\n" +
                        "11 - Туалет\n12 - Дети\n13 - Ресторан\n14 - Путешествие");
                    var category = Convert.ToInt32(Console.ReadLine());
                    switch (category)
                    {
                        case 1: thing.Category += "tourism"; break;
                        case 2: thing.Category += "cycle"; break;
                        case 3: thing.Category += "winter"; break;
                        case 4: thing.Category += "beach"; break;
                        case 5: thing.Category += "business"; break;
                        case 6: thing.Category += "swim"; break;
                        case 7: thing.Category += "need"; break;
                        case 8: thing.Category += "photo"; break;
                        case 9: thing.Category += "fishing"; break;
                        case 10: thing.Category += "hike"; break;
                        case 11: thing.Category += "wc"; break;
                        case 12: thing.Category += "kids"; break;
                        case 13: thing.Category += "restaurant"; break;
                        case 14: thing.Category += "travel"; break;
                    }
                    Console.Write("Add one more category? [1-yes/0-no]: ");
                    isAddMore = Convert.ToInt32(Console.ReadLine()) == 1;
                    if (isAddMore)
                    {
                        thing.Category += ",";
                    }
                } while (isAddMore);
                Console.Write("Weight: ");
                thing.Weight = Convert.ToDouble(Console.ReadLine());

                array.Add(JObject.FromObject(thing, serializer));

                Console.Write("Continue? [1-yes/0-no]: ");
                isContinue = Convert.ToInt32(Console.ReadLine()) == 1;
                Console.Clear();
            }

            File.WriteAllText(file, array.ToString());
        }
    }

    public class Thing
    {
        [JsonProperty("thingName_en-US")]
        public string ThingNameEn { get; set; }
        [JsonProperty("thingName_ru-RU")]
        public string ThingNameRu { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public double Weight { get; set; }
    }
}
