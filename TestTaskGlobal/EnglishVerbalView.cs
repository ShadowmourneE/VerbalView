using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskGlobal
{
    public class EnglishVerbalView : GlobalVerbalView
    {
        private NumberFormatInfo currencyFormater;
        public EnglishVerbalView()
        {
            currencyFormater = new NumberFormatInfo
            {
                CurrencyDecimalSeparator = ".",
                NumberDecimalSeparator = ".",
                CurrencyGroupSeparator = "'",
                NumberGroupSeparator = "'"
            };

        }

        //For overloading. It depends on a language specific. 
        protected override string GetConfigForBillion(int digit)
        {
            return " billion ";
        }
        protected override string GetConfigForMillion(int digit)
        {
            return " million ";
        }
        protected override string GetConfigForThousand(int digit)
        {
            return " thousand ";
        }
        protected override string GetConfigForHundred(int digit)
        {
            return " hundred ";
        }

        //For building currency ends. Should be overloaded for each languages.
        protected override string BuildEndsForBigCurrency(int number)
        {
            return
                number == 1 ? "dollar" :
                number == 0 ? "" : "dollars";
        }
        protected override string BuildEndsForSmallCurrency(int number)
        {
            return
                number == 1 ? "cent" :
                number == 0 ? "" : "cents";
        }

        protected override string ConvertToVerbal(int number)
        {
            string words = String.Empty;

            if (number / SpanConstants.billion > 0)
            {
                words += ConvertToVerbal(number / SpanConstants.billion) + GetConfigForBillion(number / SpanConstants.billion);
                number %= SpanConstants.billion;
            }
            if (number / SpanConstants.million > 0)
            {
                words += ConvertToVerbal(number / SpanConstants.million) + GetConfigForMillion(number / SpanConstants.million);
                number %= SpanConstants.million;
            }
            if (number / SpanConstants.thousand > 0)
            {
                words += ConvertToVerbal(number / SpanConstants.thousand) + GetConfigForThousand(number / SpanConstants.thousand);
                number %= SpanConstants.thousand;

            }
            if (number / SpanConstants.hundred > 0)
            {
                words += ConvertToVerbal(number / SpanConstants.hundred) + GetConfigForHundred(number / SpanConstants.hundred);
                number %= SpanConstants.hundred;
            }

            if (number > 0)
            {
                if (number < 20)
                    words += EnglishNumbers.simpleMap[number];
                else
                {
                    words += EnglishNumbers.tens[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + EnglishNumbers.simpleMap[number % 10];
                }
            }

            return words;
        }
        // the main idea is spliting on categories (rates)        
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
                    return $"Number must be from 0 to {int.MaxValue}";
                }

                string buildingResult = "";
                int beforeSeparator = Convert.ToInt32(wholePart);
                int afterSeparator = Convert.ToInt32(fraction);

                if (beforeSeparator == 0 && afterSeparator == 0)
                    return "No money";

                if (beforeSeparator == 0)
                    return $"{ConvertToVerbal(afterSeparator)} {BuildEndsForSmallCurrency(afterSeparator)}";
                if (afterSeparator == 0)
                    return $"{ConvertToVerbal(beforeSeparator)} {BuildEndsForBigCurrency(beforeSeparator)}";
                return $"{ConvertToVerbal(beforeSeparator)} {BuildEndsForBigCurrency(beforeSeparator)} " +
                       $"and {ConvertToVerbal(afterSeparator)} {BuildEndsForSmallCurrency(afterSeparator)}";
            }
            return "Invalid input";
        }      
    }
}
