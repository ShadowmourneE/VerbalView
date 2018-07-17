using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskGlobal
{
    //Resources
    public static class SpanConstants
    {
        public static readonly int billion = 1000000000;
        public static readonly int million = 1000000;
        public static readonly int thousand = 1000;
        public static readonly int hundred = 100;
    }

    public static class UkrainianNumbers
    {
        public static readonly string[] extension = { "", "одна", "двi", "три", "чотири", "п'ять", "шiсть", "сiм", "вiсiм", "девять", "десять", "одинадцять", "дванадцять", "тринадцять", "чотирнадцять", "пятнадцять", "шiстнадцять", "сiмнадцять", "вiсiмнадцять", "девятнадцять" };
        public static string[] simpleMap = { "", "один", "два", "три", "чотири", "п'ять", "шiсть", "сiм", "вiсiм", "девять", "десять", "одинадцять", "дванадцять", "тринадцять", "чотирнадцять", "пятнадцять", "шiстнадцять", "сiмнадцять", "вiсiмнадцять", "девятнадцять" };
        public static readonly string[] tens = { "", "десять", "двадцять", "тридцять", "сорок", "п'ятдесят", "шiстдесят", "сiмдесят", "вiсiмдесят", "девяносто" };
    }

    public static class EnglishNumbers
    {
        public static readonly string[] simpleMap = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        public static readonly string[] tens = { "", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
    }

}
