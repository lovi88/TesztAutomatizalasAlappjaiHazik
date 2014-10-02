using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StringCalculatorKata
{
    public class StringSplitter
    {
        string ToSplit;
        readonly string OptionalDelimeterSign = "//";


        List<string> separators = new List<string>();

        public List<string> Separators
        {
            get { return separators; }
        }

        public StringSplitter(string toSplit)
        {
            ToSplit = toSplit;

            initSeparators();
        }

        private void initDefaultSeparators()
        {
            separators.Add(",");
            separators.Add("\n");
        }

        private void initOptionalSeparators()
        {
            string sep;
            if (!(ToSplit.StartsWith(OptionalDelimeterSign)))
            {
                return;
            }

            sep = PopOptionalSeparator();
            separators.Add(sep);
        }

        private string PopOptionalSeparator()
        {
            string input = ToSplit;
            string pattern = "^"+OptionalDelimeterSign+".";

            Match m = Regex.Match(input, pattern);

            ToSplit = ToSplit.Replace(m.Value, "");

            string sep = m.Value.Replace(OptionalDelimeterSign, "");

            return sep;

        }

        private void initSeparators()
        {
            initDefaultSeparators();
            initOptionalSeparators();
        }

        public List<string> GetStringParts()
        {
            var ret = new List<string>();

            if (ToSplit == "")
            {
                return ret;
            }

            if (NoSeparatorFound())
            {
                ret.Add(ToSplit);
            }
            else
            {
                ret = SplitStringParts();
            }


            return ret;
        }

        private bool NoSeparatorFound()
        {
            foreach (var sep in separators)
            {
                if (ToSplit.IndexOf(sep) > -1)
                {
                    return false;
                }
            }

            return true;
        }

        private List<string> SplitStringParts()
        {
            return ToSplit.Split(separators.ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
