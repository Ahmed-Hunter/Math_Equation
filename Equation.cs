using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEquation
{
    public class Equation
    {
        public int Priority(char c)
        {
            return ((c == '+' || c == '-') ? 1 : (c == '*' || c == '/') ? 2 : (c == '^') ? 3 : 0);
        }
        public double operation(double op1, double op2, char operate)
        {
            return ((operate == '+') ? op1 + op2 : (operate == '-') ? op1 - op2 :
            (operate == '*') ? op1 * op2 : (operate == '/') ? op1 / op2 : (operate == '^') ? ((double)Math.Pow(op1, op2)) : 0);
        }
        public void check(Stack<char> s, ref string output, char c)
        {
            while (s.Count > 0 && Priority(c) <= Priority(s.Peek()))
            {
                output += s.Peek();
                output += ' ';
                s.Pop();
            }
            s.Push(c);
        }
        public string RemoveSpace(ref string str)
        {
            string result = "";
            foreach (char c in str)
            {
                if (c != ' ')
                {
                    result += c;
                }
            }
            return result;
        }
        public string infixToPostfix(string exp)
        {
            //stack<char> s;
            //exp = exp.Replace(" ", "");
            exp = RemoveSpace(ref exp);
            var s = new Stack<char>();
            string output = "";
            for (int i = 0; i < exp.Length; i++)
            {
                if (char.IsDigit(exp[i]) || exp[i] == '.') // for  3+2^3*(2+3)2 
                {
                    if (i > 0 && exp[i - 1] == ')')
                    {
                        check(s, ref output, '*');
                    }

                    while (i < exp.Length && (Char.IsDigit(exp[i]) || exp[i] == '.'))
                    {
                        output += exp[i];
                        i++;
                    }

                    output += ' ';
                    i--;
                }
                else if (exp[i] == '(')
                {
                    if ((i > 0 && exp[i - 1] != '*') && (char.IsDigit(exp[i - 1]) || exp[i - 1] == ')')) // for 3+2^3(2+3)*2 || (3+2^3)(2+3)*2
                    {
                        check(s, ref output, '*');
                    }
                    s.Push(exp[i]);
                }
                else if (exp[i] == ')')
                {
                    while (s.Peek() != '(')
                    {
                        output += s.Peek();
                        output += ' ';
                        s.Pop();
                    }
                    s.Pop();
                }
                else
                {
                    check(s, ref output, exp[i]);
                }
            }
            while (s.Count > 0)
            {
                output += s.Peek();
                output += ' ';
                s.Pop();
            }
            return output;
        }

        public double postfixEvaluation(string exp)
        {
            //stack<float> s;
            var s = new Stack<double>();
            var tokens = exp.Split(' ');

            foreach (var token in tokens)
            {
                if (double.TryParse(token, out double number))
                {
                    s.Push(number);
                }
                else if (token.Length == 1) // single character operator
                {
                    double operandTwo = s.Pop();
                    double operandOne = s.Pop();
                    double result = operation(operandOne, operandTwo, token[0]);
                    s.Push(result);
                }
            }

            return s.Pop();
        }

    }
}
