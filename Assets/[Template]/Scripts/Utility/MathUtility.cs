

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Template
{
    public static class MathUtility
    {
        /// <summary>
        /// give a string equation and it calculates the result
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static float EvaluateMathExpression(string expression)
        {
            Queue<string> postfixQueue = ConvertToPostfix(expression);
            Stack<float> evaluationStack = new Stack<float>();

            while (postfixQueue.Count > 0)
            {
                string token = postfixQueue.Dequeue();
                if (IsOperator(token))
                {
                    float operand2 = evaluationStack.Pop();
                    float operand1 = evaluationStack.Pop();
                    float result = PerformOperation(operand1, operand2, token);
                    evaluationStack.Push(result);
                }
                else
                {
                    evaluationStack.Push(float.Parse(token));
                }
            }

            return evaluationStack.Pop();
        }

        // Normalize an angle to be within the range of 0 to 360 degrees
        public static float NormalizeAngle(float angle)
        {
            return (angle % 360 + 360) % 360;
        }

        public static bool IsAbsDifferenceHigherThan(float value1, float value2, float higherThan)
        {
            return (Mathf.Abs(value1 - value2) >= higherThan) ? true : false;
        }

        private static Queue<string> ConvertToPostfix(string expression)
        {
            Queue<string> postfixQueue = new Queue<string>();
            Stack<string> operatorStack = new Stack<string>();

            string[] tokens = expression.Split(' ');

            foreach (string token in tokens)
            {
                if (IsNumeric(token))
                {
                    postfixQueue.Enqueue(token);
                }
                else if (IsOperator(token))
                {
                    while (operatorStack.Count > 0 && 
                        Precedence(operatorStack.Peek()) >= Precedence(token))
                    {
                        postfixQueue.Enqueue(operatorStack.Pop());
                    }
                    operatorStack.Push(token);
                }
            }

            while (operatorStack.Count > 0)
            {
                postfixQueue.Enqueue(operatorStack.Pop());
            }

            return postfixQueue;
        }

        private static bool IsNumeric(string token)
        {
            return float.TryParse(token, out _);
        }

        private static bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "x" || token == "/";
        }

        private static int Precedence(string op)
        {
            if (op == "x" || op == "/")
                return 2;
            else if (op == "+" || op == "-")
                return 1;
            else
                return 0; // Parentheses have the highest precedence
        }

        private static float PerformOperation(float operand1, float operand2, string op)
        {
            switch (op)
            {
                case "+":
                    return operand1 + operand2;
                case "-":
                    return operand1 - operand2;
                case "x":
                    return operand1 * operand2;
                case "/":
                    if (operand2 == 0f)
                        throw new DivideByZeroException("Division by zero");
                    return operand1 / operand2;
                default:
                    throw new ArgumentException("Invalid operator: " + op);
            }
        }
    }
}