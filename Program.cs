namespace MathEquation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Equation equation = new Equation();
            while (true)
            {
                string infix = "";
                Console.Write("Enter The Experation or (exit) : ");
                infix = Console.ReadLine();
                if (infix == "exit") break;
                Console.WriteLine($"infix To postfix : {equation.infixToPostfix(infix)}");
                Console.WriteLine($"Result : {equation.postfixEvaluation(equation.infixToPostfix(infix))}");
            }
        }
    }
}
