using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskGlobal
{
    /// <summary>  
    /// Represents the abstraction for each language.  
    /// </summary> 
    public abstract class GlobalVerbalView
    {
        //For overloading, because it depends on a language specific. 
        /// <summary>  
        /// Represents the config for billion.  
        /// </summary>
        protected abstract string GetConfigForBillion(int digit);
        /// <summary>  
        /// Represents the config for million.  
        /// </summary>
        protected abstract string GetConfigForMillion(int digit);
        /// <summary>  
        /// Represents the config for thousand.  
        /// </summary>
        protected abstract string GetConfigForThousand(int digit);
        /// <summary>  
        /// Represents the config for hundred.  
        /// </summary>
        protected abstract string GetConfigForHundred(int digit);
        //Should be overloaded for each languages.
        protected abstract string BuildEndsForBigCurrency(int number);
        protected abstract string BuildEndsForSmallCurrency(int number);
        //Main logic
        protected abstract string ConvertToVerbal(int number);
        //Public interface of object which uses as start point
        /// <summary>  
        /// Represents the verbal view of numbers.  
        /// </summary>
        public abstract string ToWords(string number);
    }
}
