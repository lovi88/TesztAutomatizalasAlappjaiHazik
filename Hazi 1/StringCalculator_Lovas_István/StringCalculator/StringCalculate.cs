using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculatorKata
{
    public class StringCalculate : IStringCalculator
    {

        List<string> PartStrs;
        List<int> nums;
        StringToIntConverter Conv;

        public StringCalculate()
        {
            Conv = new StringToIntConverter();
            //Conv.Options.Add(StringToIntConverter.ConvertOptions.DisallowNegatives);
        }

        public int Add(string values)
        {
            if (String.IsNullOrWhiteSpace(values))
            {
                return 0;
            }

            StringSplitter sp = new StringSplitter(values);

            PartStrs = sp.GetStringParts();

            if (PartStrs.Count == 1)
            {
                var num = Conv.Convert(values);
                CheckNegatives(new List<int>() { num });
                
                return num;
            }
            else
            {
                List<int> nums = Conv.Convert(PartStrs);
                CheckNegatives(nums);

                return Add(nums);
            }

        }


        private void CheckNegatives(List<int> nums)
        {
            var negs = (from n in nums where n < 0 select n).ToList();

            if (negs.Count == 0)
            {
                return;
            }


            String negativesStr = ConvertToComaSeparatedString(negs);
            throw new ArgumentException(String.Format("negatives not allowed - {0}", negativesStr));

        }

        private static String ConvertToComaSeparatedString(List<int> negs)
        {
            StringBuilder negativesSB = new StringBuilder();
            negativesSB.Append(negs[0]);

            if (negs.Count > 1)
            {
                for (int i = 1; i < negs.Count; i++)
                {
                    negativesSB.Append(",");
                    negativesSB.Append(negs[i]);
                }
            }
            return negativesSB.ToString();
        }

        private int Add(int a, int b)
        {
            return a + b;
        }

        private int Add(List<int> values)
        {

            int sum = 0;
            foreach (var v in values)
            {
                sum += v;
            }
            return sum;
        }

        private void InitNumsFromListOfStrs(List<string> numsStr)
        {
            nums = new List<int>();

            foreach (var ns in numsStr)
            {
                nums.Add(int.Parse(ns));
            }

        }

    }
}
