using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NewModelsChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient client = new WebClient();
            string a = "test";
            int old_lock = Int32.Parse(Properties.Settings.Default["old_lock"].ToString());
            int old_unlock = Int32.Parse(Properties.Settings.Default["old_unlock"].ToString());
            try
            {
                a = client.DownloadString("https://webdata.c7x.dev/client/models.json");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Что-то пошло не так. Проверьте подключение к интернету. Ошибка:\n" + ex);
                Console.WriteLine("Нажмите любую кнопку чтобы продолжить...");
                Console.ReadLine();
            }

            var unlocked = Regex.Matches(a, "availableInGui\":true").Count;
            var locked = Regex.Matches(a, "availableInGui\":false").Count;
            if (old_lock != locked || old_unlock != unlocked)
                Console.WriteLine($"Одно из значений не свопадает со старым. Старые значения:{old_unlock} и {old_lock}"); Properties.Settings.Default["old_lock"] = locked.ToString(); Properties.Settings.Default["old_unlock"] = unlocked.ToString(); Properties.Settings.Default.Save();
            Console.WriteLine($"Разблокированная персонализация: {unlocked}\nЗаблокированная: {locked}");
            Console.WriteLine("Нажмите любую кнопку чтобы продолжить...");
            Console.ReadLine();
        }
    }
}
