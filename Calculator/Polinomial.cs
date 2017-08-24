using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Polinomial
    {
        string Terms;
        public Polinomial(string terms)
        {
            Terms = terms.Replace(" ", null);
        }

        /// <summary>
        /// Calculating the answer of terms base on value of variables
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        public string Answer(string[] variable)
        {
            switch (GetIPT(Terms))
            {
                case "No parentheses exist":
                    break;
                case "Wrong terms, pair parentheses":
                    break;
                case "Empty parentheses":
                    break;
                default:
                    break;
            }
            return null;
        }

        /// <summary>
        /// Return string of terms of innermost parentheses
        /// Errors is:
        /// "No parentheses exist", "Wrong terms, pair parentheses", "Empty parentheses", and Terms
        /// </summary>
        /// <param name="terms"></param>
        /// <returns></returns>
        string GetIPT(string terms)
        {                     
            int opi;                                                            //Open Parentheses Index      
            int cpi;                                                            //Close Parentheses Index 
            opi = terms.LastIndexOf('(');
            cpi = terms.IndexOf(')');
            if (opi == -1 && cpi == -1)
                return "No parentheses exist";                                  // Return error
            if (cpi == -1 || cpi == -1)
                return "Wrong terms, pair parentheses";                         // Return error
            if (opi > cpi)
                return "Wrong terms, pair parentheses";                         // Return error
            if (opi + 1 == cpi)
                return "Empty parentheses";                                     // Return error
            return terms.Substring(opi + 1, cpi - opi -1);
        }
    }
}
