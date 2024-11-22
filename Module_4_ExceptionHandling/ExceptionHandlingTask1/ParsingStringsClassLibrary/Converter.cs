using System;
using System.Text.RegularExpressions;

namespace ParsingStringsClassLibrary
{
    /// <summary>
    /// Converts a string to Int32.
    /// </summary>
    public class Converter
    {
        private readonly string onlyMinusAndDigits = "^-?[0-9]+$";
        private readonly string onlyMinusAndZeros = "^-0+$";
        public int ParseString(string line)
        {
            line = line.Trim();
            int number = default;

            if (Regex.IsMatch(line, onlyMinusAndDigits) && !Regex.IsMatch(line, onlyMinusAndZeros))
            {
                if (line == Int32.MinValue.ToString())
                {
                    number = Int32.MinValue;
                }
                else
                {
                    bool isNegative = line.StartsWith("-");
                    // if the line starts with "minus", reassign the line to itself starting with the second element
                    if (isNegative)
                    {
                        line = line[1..];
                    }
                    // Converting to Int32 using ASCII table
                    foreach (var character in line)
                    {
                        try
                        {
                            checked // throws OverflowException if arithmetic limits of Int32 are exceeded
                            {
                                number *= 10;
                                number += character - '0'; // actually positions of the characters in ASCII table are being subtracted
                            }
                        }
                        catch (OverflowException)
                        {
                            throw new OverflowException("The entered value should be within the following range: [-2,147,483,648 - 2,147,483,647]");
                        }
                    }
                    if (isNegative)
                    {
                        number *= -1;
                    }
                }
            }
            else
            {
                throw new FormatException("The entered value does not consist of an optional minus sign followed by a sequence of digits (0 through 9)");
            }
            return number;
        }
    }
}
