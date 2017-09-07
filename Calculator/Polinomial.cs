using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
	public class Polinomial
	{
		/// <summary>
		/// Container of polinomial terms
		/// </summary>
		public string Terms { get; set; }

		public Polinomial(string terms)
		{
			Terms = terms.Replace(" ", null);
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
			t = ValueReplace(t, variables);
			t = ConvertToStandard(t);
			while (t.IndexOf('(') != -1)
			{
				string ipt = GetIPT(t);                                         //Innermost parentheses term
				t = t.Replace(
					"(" + ipt + ")",
					Calc(ipt));                    //Remove '(' and ')' from ipt and send to calc method
			}
			return Calc(t);
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
			int opi = 0;                                                        //Open parenteses index
			for (int i = 0; i < terms.Length; i++)
				if (terms.Substring(i, 1) == "(")
					opi = i;
				else if (terms.Substring(i, 1) == ")")
					return terms.Substring(opi + 1, i - opi - 1);
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

			List<int> opi = new List<int>();                                    //Stack of open parentheses indexes
			for (int i = 0; i < Terms.Length; i++)
				if (Terms.Substring(i, 1) == "(")
					opi.Add(i);
				else if (Terms.Substring(i, 1) == ")")
					if (opi.Count > 0)
						if (opi[opi.Count - 1] > i - 1)
							return false;                                       //Invalid term. Empty parentheses
						else
							opi.RemoveAt(opi.Count - 1);
					else
						return false;                                           //Invalid term. Pair parentheses


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

		/// <summary>
		/// Replace values in terms. For example replaceing {x=1, y=2} in 4x^2+3y is 4(1)^2+3(2)
		/// </summary>
		/// <param name="terms"></param>
		/// <param name="values"></param>
		/// <returns></returns>
		string ValueReplace(string terms, string[] values)
		{
			foreach (string value in values)
			{
				string[] v = value.Replace(" ", null).Split('=');
				if (false)
					return "Error: Invalid values";                             //Invalid values.
				terms = terms.Replace(v[0], "(" + v[1] + ")");
			}
			return terms;
		}

		/// <summary>
		/// Calculate the terms answer whitout parentheses
		/// </summary>
		/// <param name="terms"></param>
		/// <returns></returns>
		string Calc(string terms)
		{
			List<string> numbers = new List<string>();                          //List of numbers
			List<string> oprs = new List<string>();                              //List of operators

			string[] splits = terms.Split(new char[] { '^', '*', '/', '+', '-' });
			foreach (string split in splits)
				numbers.Add(split);
			for (int i = 0; i < terms.Length; i++)
			{
				if ("^*/+-".IndexOf(terms.Substring(i, 1)) != -1)
					oprs.Add(terms.Substring(i, 1));
			}

			for (int j = 0; j < oprs.Count; j++)
				if (oprs[j] == "-" && string.IsNullOrEmpty(numbers[j]))
				{
					numbers[j] = "-" + numbers[j + 1];
					numbers.RemoveAt(j + 1);
					oprs.RemoveAt(j);
					j--;
				}

			for (int k = 0; k < oprs.Count; k++)
				if (oprs[k] == "^")
				{
					numbers[k] = Math.Pow(double.Parse(numbers[k]), double.Parse(numbers[k + 1])).ToString();
					numbers.RemoveAt(k + 1);
					oprs.RemoveAt(k);
					k--;
				}

			for (int l = 0; l < oprs.Count; l++)
				if ("*/".IndexOf(oprs[l]) != -1)
				{
					numbers[l] = oprs[l] == "*" ? (double.Parse(numbers[l]) * double.Parse(numbers[l + 1])).ToString() :
						(double.Parse(numbers[l]) / double.Parse(numbers[l + 1])).ToString();
					numbers.RemoveAt(l + 1);
					oprs.RemoveAt(l);
					l--;
				}

			for (int m = 0; m < oprs.Count; m++)
				if ("+-".IndexOf(oprs[m]) != -1)
				{
					numbers[m] = oprs[m] == "+" ? (double.Parse(numbers[m]) + double.Parse(numbers[m + 1])).ToString() :
						(double.Parse(numbers[m]) - double.Parse(numbers[m + 1])).ToString();
					numbers.RemoveAt(m + 1);
					oprs.RemoveAt(m);
					m--;
				}

			return numbers[0];
		}

		/// <summary>
		/// Convert terms to standard terms, for examle 4(25-3)+2-1.5(3)7^2 convert to 4*(25-3)+2-1.5*(3)*7^2
		/// </summary>
		/// <param name="terms"></param>
		/// <returns></returns>
		string ConvertToStandard(string terms)
		{
			string number = "0123456789";
			string openParenteses = "(";
			string closeParenteses = ")";
			string opr = "^*/+-";
			string dot = ".";

			for (int i = 0; i < terms.Length - 1; i++)
			{
				if (number.IndexOf(terms.Substring(i, 1)) != -1)
				{
					if (openParenteses.IndexOf(terms.Substring(i + 1, 1)) != -1)
						terms = terms.Substring(0, i + 1) + "*" + terms.Substring(i + 1);
				}
				else if (openParenteses.IndexOf(terms.Substring(i, 1)) != -1)
				{
					if (dot.IndexOf(terms.Substring(i + 1, 1)) != -1)
						terms = terms.Substring(0, i) + "*0" + terms.Substring(i + 1);
				}
				else if (closeParenteses.IndexOf(terms.Substring(i, 1)) != -1)
				{
					if (number.IndexOf(terms.Substring(i + 1, 1)) != -1)
						terms = terms.Substring(0, i) + "*" + terms.Substring(i + 1);
					else if (dot.IndexOf(terms.Substring(i + 1, 1)) != -1)
						terms = terms.Substring(0, i) + "*0" + terms.Substring(i + 1);
				}
				else if (opr.IndexOf(terms.Substring(i, 1)) != -1)
				{
					if (dot.IndexOf(terms.Substring(i + 1, 1)) != -1)
						terms = terms.Substring(0, i) + "*0" + terms.Substring(i + 1);
					if (i == 0)
						terms = "0" + terms;
				}
				else if (dot.IndexOf(terms.Substring(i, 1)) != -1)
				{
					if (i == 0)
						terms = "0" + terms;
				}
			}
			return terms;
		}
	}
}
