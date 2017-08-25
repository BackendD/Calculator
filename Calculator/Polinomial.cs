using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Polinomial
    {
        /// <summary>
        /// Container of polinomial terms
        /// </summary>
        public string Terms
        {
            get
            {
                return Terms;
            }
            set
            {
                Terms = value.Replace(" ", null);
            }
        }

        public Polinomial() { }

        public Polinomial(string terms)
        {
            Terms = terms;
        }

        /// <summary>
        /// Calculating the answer of terms base on value of variables
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        public string Answer(string[] variables)
        {
            if (!IsValidTerms())
                return "Error: Invalid terms.";

            string t = Terms;

            foreach(string variable in variables)
            {
                string[] v = variable.Split('=');
                t = t.Replace(v[0], "(" + v[1] + ")");
            }
            return null;
        }

        /// <summary>
        /// Return string of terms of innermost parentheses
        /// Errors are:
        /// "No parentheses exist", "Wrong terms, pair parentheses", "Empty parentheses"
        /// </summary>
        /// <param name="terms"></param>
        /// <returns></returns>
        string GetIPT(string terms)
        {
            List<int> opi = new List<int>();                                    //Close Parentheses Index 
            for (int i = 0; i < terms.Length; i++) ;
            return null;
        }

        /// <summary>
        /// Check terms and if that is valid terms return true otherwise, false
        /// </summary>
        /// <returns></returns>
        public bool IsValidTerms()
        {
            if (string.IsNullOrEmpty(Terms))                                    
                return false;                                                   //Null string

            List<int> opi = new List<int>();                                    //Stack of open parentheses index
            for (int i = 0; i < Terms.Length; i++)
                if (Terms.Substring(i, 1) == "(")
                    opi.Add(i);
                else if (Terms.Substring(i, 1) == ")")
                    if (opi.Count > 0)
                        if (opi[opi.Count] > i - 1)
                            return false;                                       //Invalid term. Empty parentheses
                        else
                            opi.RemoveAt(opi.Count - 1);
                    else
                        return false;                                           //Invalid term. Pair parentheses

            Terms.IndexOf('(');
            char[] validChar = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'G', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                '(', ')', '^', '*', '/', '+', '-', '.'};
            string check = Terms;
            foreach (char c in validChar)
                check = check.Replace(c.ToString(), null);
            if (!string.IsNullOrEmpty(check))
                return false;                                                   //Invalid term. Contain Invalid char

            return true;
        }
    }
}
