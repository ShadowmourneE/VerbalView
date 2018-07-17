using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskGlobal
{
    public class UkrainianVerbalView : GlobalVerbalView
    {
        private NumberFormatInfo currencyFormater;
        private bool isExtensive = false;

        public UkrainianVerbalView()
        {
            currencyFormater = new NumberFormatInfo
            {
                CurrencyDecimalSeparator = ",",// for Ukrainian
                NumberDecimalSeparator = "," // deny dot (task)
            };
        }
        public override string ToWords(string number)
        {
            decimal currencyValue;

            bool isOk = decimal.TryParse(number, NumberStyles.Currency, currencyFormater, out currencyValue);
            if (isOk)
            {
                decimal wholePart = Math.Truncate(currencyValue);
                decimal fraction = (currencyValue - wholePart) * 100;
                if (wholePart > int.MaxValue || currencyValue < 0)
                {
                    return $"Число повинно бути від 0 до {int.MaxValue}";
                }

                string buildingResult = "";
                int beforeSeparator = Convert.ToInt32(wholePart);
                int afterSeparator = Convert.ToInt32(fraction);
                if (beforeSeparator == 0 && afterSeparator == 0)
                    return "Нема грошей";

                if (beforeSeparator == 0)
                {
                    isExtensive = IsExtensive(afterSeparator);
                    return
                        $"{ConvertToVerbal(afterSeparator)} {BuildEndsForSmallCurrency(afterSeparator)}";
                }
                if (afterSeparator == 0)
                {
                    isExtensive = IsExtensive(beforeSeparator);
                    return
                        $"{ConvertToVerbal(beforeSeparator)} {BuildEndsForBigCurrency(beforeSeparator)}";
                }
                if (beforeSeparator < 100)
                {
                    isExtensive = IsExtensive(afterSeparator);
                    buildingResult += $"{ConvertToVerbal(beforeSeparator)} {BuildEndsForBigCurrency(beforeSeparator)}";
                    isExtensive = false;
                }
                else
                {
                    buildingResult +=
                        $"{ConvertToVerbal(beforeSeparator)} {BuildEndsForBigCurrency(beforeSeparator)}";
                }
                if (afterSeparator <= 100)
                {
                    isExtensive = IsExtensive(afterSeparator);
                    buildingResult +=
                        $"{ConvertToVerbal(afterSeparator)} {BuildEndsForSmallCurrency(afterSeparator)}";
                    isExtensive = false;
                }
                return $"{buildingResult}";
            }
            return "Некоректний ввод";
        }

        protected override string GetConfigForBillion(int digit)
        {
            return BuildTheLanguageEnds(digit, "мiльярдів", "мiльярд", "мiльйярди", "мiльярдiв");
        }
        protected override string GetConfigForMillion(int digit)
        {
            return BuildTheLanguageEnds(digit, "мiльйонів", "мiльйон", "мiльйони", "мiльйонiв");
        }
        protected override string GetConfigForThousand(int digit)
        {
            return BuildTheLanguageEnds(digit, "тисяч", "тисяча", "тисячi", "тисяч");
        }
        protected override string GetConfigForHundred(int digit)
        {
            return
                digit == 1 ? " сто " :
                digit == 2 ? " двiстi " :
                digit == 3 ? " триста " :
                digit == 4 ? " чотириста " :
                digit == 5 ? " пятсот " :
                digit == 6 ? " шiстсот " :
                digit == 7 ? " сiмсот " :
                digit == 8 ? " вiсiмсот " :
                digit == 9 ? " девятсот " : "";
        }

        protected override string BuildEndsForBigCurrency(int digit)
        {
            return BuildTheLanguageEnds(digit, "гривень", "гривня", "гривнi", "гривень");
        }
        protected override string BuildEndsForSmallCurrency(int digit)
        {
            return BuildTheLanguageEnds(digit, "копiйок", "копiйка", "копiйки", "копiйок");
        }
        protected override string ConvertToVerbal(int number)
        {
            string words = String.Empty;

            if (number / SpanConstants.billion > 0)
            {
                words += ConvertToVerbal(number / SpanConstants.billion) + GetConfigForBillion(number / SpanConstants.billion);
                number %= SpanConstants.billion;
                isExtensive = true;
            }
            if (number / SpanConstants.million > 0)
            {
                words += ConvertToVerbal(number / SpanConstants.million) + GetConfigForMillion(number / SpanConstants.million);
                number %= SpanConstants.million;
                isExtensive = true;
            }
            if (number / SpanConstants.thousand > 0)
            {
                isExtensive = true;
                words += ConvertToVerbal(number / SpanConstants.thousand) + GetConfigForThousand(number / SpanConstants.thousand);
                number %= SpanConstants.thousand;
            }
            if (number / SpanConstants.hundred > 0)
            {
                words += GetConfigForHundred(number / SpanConstants.hundred);
                number %= SpanConstants.hundred;
                isExtensive = true;
            }

            if (number > 0)
            {
                if (isExtensive)
                {
                    UkrainianNumbers.simpleMap = UkrainianNumbers.extension; //for extensive ukrainian mode
                }

                if (number < 20)
                    words += UkrainianNumbers.simpleMap[number];
                else
                {
                    words += UkrainianNumbers.tens[number / 10];
                    if ((number % 10) > 0)
                        words += " " + UkrainianNumbers.simpleMap[number % 10];
                }
            }

            return words;
        }

        private int GetLastNumber(int number) => number % 10;
        private int GetTowLastNumber(int number) => number % 100;
        private string BuildTheLanguageEnds(int digit, string first, string second, string third, string byDefault)
        {
            int restLast = GetLastNumber(digit);
            int restTwoLast = GetTowLastNumber(digit);
            if (digit == 0) return "";
            if (restTwoLast >= 11 && restTwoLast <= 14)
                return $" {first} ";
            if (digit == 1 || restLast == 1)
                return $" {second} ";
            if (digit >= 2 && digit <= 4 || restLast >= 2 && restLast <= 4)
                return $" {third} ";
            return $" {byDefault} ";
        }
        private bool IsExtensive(int digit)
        {
            if (digit < 100)
                return true;
            return false;
        }

    }
}
