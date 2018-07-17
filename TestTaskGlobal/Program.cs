using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Globalization;
namespace TestTaskGlobal
{
    class Program
    {
        public static void StartMenu()
        {
            Console.WriteLine("Оберіть мову | Choose the language");
            Console.WriteLine("Українська мова - натисніть 1");
            Console.WriteLine("English - press 2");

            GlobalVerbalView languageModel = null;
            string inputNumber = Console.ReadLine();
            string stringInput;
            switch (inputNumber)
            {
                case "1":
                    Console.WriteLine($"Ваша мова - Українська. Введіть сумму у форматі 'число,число(десяткова частина!)' ");
                    languageModel = new UkrainianVerbalView();
                    stringInput = Console.ReadLine();
                    Console.WriteLine(languageModel.ToWords(stringInput));
                    break;
                case "2":
                    Console.WriteLine($"Your language - English. Enter the sum in format 'number.number(decimal part!)' ");
                    languageModel = new EnglishVerbalView();
                    stringInput = Console.ReadLine();
                    Console.WriteLine(languageModel.ToWords(stringInput));
                    break;
                default:
                    Console.WriteLine("Невірний ввод | Bad input");
                    break;
            }                     
        }
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8; // For all computers.Because by default we have Windows - 1251
            StartMenu();
            Console.WriteLine("Press any key for exit...");
            Console.ReadKey(true);
        }
    }
}
