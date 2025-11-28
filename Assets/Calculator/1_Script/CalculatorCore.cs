using System;
using System.Collections.Generic;

namespace calculator
{
    public class CalculatorCore : IEventListener
    {
        private string currentInput = "0";
        private bool isEqualButtonClicked = false;
        private UICallback uICallback;

        public CalculatorCore(UICallback uICallback)
        {
            this.uICallback = uICallback;
        }

        public void AddEventListener()
        {
            UICallback.OnClickNumberBtnRequest += AddNumber;
            UICallback.OnClickAllClearBtnRequest += ClearAll;
            UICallback.OnClickSingleClearBtnRequest += ClearSingle;
            UICallback.OnClickEqualsBtnRequest += CalculateFullEquation;

            UICallback.OnClickAdditionBtnRequest += () => AddOperator("+");
            UICallback.OnClickSubtractBtnRequest += () => AddOperator("-");
            UICallback.OnClickMultiplyBtnRequest += () => AddOperator("x");
            UICallback.OnClickDivisionBtnRequest += () => AddOperator("/");
        }

        public void RemoveEventListener()
        {
            UICallback.OnClickNumberBtnRequest -= AddNumber;
            UICallback.OnClickAllClearBtnRequest -= ClearAll;
            UICallback.OnClickSingleClearBtnRequest -= ClearSingle;
            UICallback.OnClickEqualsBtnRequest -= CalculateFullEquation;

            UICallback.OnClickAdditionBtnRequest -= () => AddOperator("+");
            UICallback.OnClickSubtractBtnRequest -= () => AddOperator("-");
            UICallback.OnClickMultiplyBtnRequest -= () => AddOperator("x");
            UICallback.OnClickDivisionBtnRequest -= () => AddOperator("/");
        }

        private void AddNumber(string value)
        {
            if (isEqualButtonClicked) ClearAll();
            isEqualButtonClicked = false;

            if (value == "+" || value == "-" || value == "x" || value == "/")
            {
                AddOperator(value);
                return;
            }

            if (currentInput == "0" && value != ".")
                currentInput = value;
            else
                currentInput += value;

            
            UpdateDisplay();
        }

        private void AddOperator(string operatr)
        {
            if (currentInput == "0") return;

            char lastChar = currentInput[currentInput.Length - 1];

            if (lastChar == '+' || lastChar == '-' || lastChar == 'x' || lastChar == '/')
            {
                currentInput = currentInput.Remove(currentInput.Length - 1);
            }

            currentInput += operatr;
            UpdateDisplay();
        }

        private void CalculateFullEquation()
        {
            try
            {
                List<string> parts = SplitIntoParts(currentInput);

                if (parts.Count < 3)
                    return;

                List<string> pass1List = new List<string>(parts);

                for (int i = 0; i < pass1List.Count; i++)
                {
                    string item = pass1List[i];

                    if (item == "x" || item == "/")
                    {
                        double leftNumber = ConvertToDouble(pass1List[i - 1]);
                        double rightNumber = ConvertToDouble(pass1List[i + 1]);
                        double answer = SimpleCalculate(leftNumber, rightNumber, item);

                        pass1List[i - 1] = answer.ToString();
                        pass1List.RemoveAt(i + 1);
                        pass1List.RemoveAt(i);

                        i--;
                    }
                }

                double finalResult = ConvertToDouble(pass1List[0]);

                int index = 1;
                while (index < pass1List.Count - 1)
                {
                    string operetr = pass1List[index];
                    double nextNumber = ConvertToDouble(pass1List[index + 1]);

                    finalResult = SimpleCalculate(finalResult, nextNumber, operetr);

                    index += 2;
                }

                currentInput = FormatNumber(finalResult);
                UpdateDisplay();
            }
            catch
            {
                currentInput = "Error";
                UpdateDisplay();
            }
        }


        private List<string> SplitIntoParts(string fullValue)
        {
            List<string> parts = new List<string>();
            string number = "";

            foreach (char digit in fullValue)
            {
                if (char.IsDigit(digit) || digit == '.')
                {
                    number += digit;
                }
                else
                {
                    if (number != "")
                    {
                        parts.Add(number);
                        number = "";
                    }
                    parts.Add(digit.ToString());
                }
            }

            if (number != "")
            {
                parts.Add(number);
            }

            return parts;
        }

        private double SimpleCalculate(double firstValue, double secValue, string operatr)
        {
            double result = 0.0;
           
            switch(operatr)
            {
                case "+":
                    result = firstValue + secValue;
                    break;

                case "-":
                    result = firstValue - secValue;
                    break;

                case "x": 
                    result = firstValue * secValue;
                    break;

                case "/":
                    if (secValue == 0) result = Double.NaN;
                    else result = firstValue / secValue;
                    break;
            }

            return result;
        }

        private double ConvertToDouble(string nextNumberText)
        {
            double value = 0;
            double decimalPlace = 0.1;
            bool isDecimal = false;

            foreach (char number in nextNumberText)
            {
                if (number == '.')
                {
                    isDecimal = true;
                    continue;
                }
                int digit = number - '0';
                if (digit < 0 || digit > 9) continue;

                if (!isDecimal)
                    value = value * 10 + digit;
                else
                {
                    value += digit * decimalPlace;
                    decimalPlace *= 0.1;
                }
            }

            return value;
        }

        private string FormatNumber(double value)
        {
            string result = value.ToString();

            if (result.Length > 12)
                result = value.ToString("0.#####E+0");

            return result;
        }

        private void ClearAll()
        {
            currentInput = "0";
            UpdateDisplay();
        }

        private void ClearSingle()
        {
            if (currentInput.Length <= 1)
                currentInput = "0";
            else
                currentInput = currentInput.Substring(0, currentInput.Length - 1);

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            uICallback.OnResultValueUpdateRequested(currentInput);
        }
    }
}
