using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculatorKata
{

    public class StringToIntConverter
    {
        public enum ConvertOptions
        {
            DisallowNegatives
        }

        public List<ConvertOptions> Options { get; set; }

        public StringToIntConverter()
        {
            Options = new List<ConvertOptions>();
        }

        public int Convert(string value)
        {
            int ret;

            if (int.TryParse(value, out ret))
            {
                ValidateByOptions(ret);
                return ret;
            }

            return 0;
        }

        private void ValidateByOptions(int value)
        {

            if (Options.Contains(ConvertOptions.DisallowNegatives))
            {
                if (value<0)
                {
                    throw new ArgumentException("negatives not allowed");
                }
            }
        }

        public List<int> Convert(List<string> values)
        {

            List<int> retVals = new List<int>();

            foreach (var value in values)
            {
                retVals.Add(Convert(value));
            }
            return retVals;

        }
    }
}
