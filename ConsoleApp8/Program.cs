﻿using System;
using System.Collections.Generic;

class Calculator
{
    delegate double ArithmeticOperation(double a, double b);

    static void Main()
    {
        Console.WriteLine("введите выражение через пробел (3 + 4 * 2):");
        string input = Console.ReadLine();

        string[] tokens = input.Split(' ');

        double result = EvaluateExpression(tokens);
        Console.WriteLine("Result: " + result);
    }

    static double EvaluateExpression(string[] tokens)
    {
        Stack<double> operands = new Stack<double>();
        Stack<char> operators = new Stack<char>();

        for (int i = 0; i < tokens.Length; i++)
        {
            string token = tokens[i];
            if (double.TryParse(token, out double operand))
            {
                operands.Push(operand);
            }
            else
            {
                char op = token[0];
                if (op == '(')
                {
                    operators.Push(op);
                }
                else if (op == ')')
                {
                    while (operators.Count > 0 && operators.Peek() != '(')
                    {
                        ProcessOperator(operands, operators);
                    }
                    operators.Pop(); 
                }
                else
                {
                    while (operators.Count > 0 && HasPrecedence(op, operators.Peek()))
                    {
                        ProcessOperator(operands, operators);
                    }
                    operators.Push(op);
                }
            }
        }

        while (operators.Count > 0)
        {
            ProcessOperator(operands, operators);
        }

        return operands.Pop();
    }

    static void ProcessOperator(Stack<double> operands, Stack<char> operators)
    {
        char op = operators.Pop();
        double operand2 = operands.Pop();
        double operand1 = operands.Pop();
        double result;

        if (op == '/' && operand2 == 0)
        {
            Console.WriteLine("Делитҗ на 0 нелҗзя"); 
            Environment.Exit(0);
        }

        switch (op)
        {
            case '+':
                result = operand1 + operand2;
                break;
            case '-':
                result = operand1 - operand2;
                break;
            case '*':
                result = operand1 * operand2;
                break;
            case '/':
                result = operand1 / operand2;
                break;
            default:
            throw new InvalidOperationException("Недопустимый оператор");
                
        }

        operands.Push(result);
    }

    static bool HasPrecedence(char op1, char op2)
    {
        if (op2 == '(' || op2 == ')')
        {
            return false;
        }
        if ((op1 == '*' || op1 == '/') && (op2 == '+' || op2 == '-'))
        {
            return false;
        }
        return true;
    }
}