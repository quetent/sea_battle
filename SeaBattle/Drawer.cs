using Microsoft.VisualBasic;
using System;
using System.Drawing;

namespace SeaBattle
{
    internal static class Drawer
    {
        private static readonly string _axes_indent;
        private static readonly string _indent_between_fields;
        private static readonly string _indent_from_digit;

        private static readonly ConsoleColor _digitsColor;
        private static readonly ConsoleColor _lettersColor;
        private static readonly ConsoleColor _enemyCaptionColor;


        static Drawer()
        {
            _axes_indent = new(' ', 1);
            _indent_between_fields = new(' ', 5);
            _indent_from_digit = new(' ', 1);

            _digitsColor = ConsoleColor.White;
            _lettersColor = ConsoleColor.DarkBlue;
            _enemyCaptionColor = ConsoleColor.DarkYellow;
        }

        public static void DrawFields(Field attackField, Field defenseField)
        {
            DrawFieldsLettersLine();
            DrawRepeatedEmptyLine(2);
            DrawNumbersAndFields(attackField, defenseField);
            DrawPlayerCaption(_indent_from_digit + _axes_indent, "< Enemy >");
            DrawPlayerCaption(_indent_from_digit + _axes_indent + _indent_between_fields + " ", "< You >");
        }

        public static void DrawField(Field field)
        {
            DrawFieldLettersLine();
            DrawRepeatedEmptyLine(2);
            DrawNumbersAndField(field);
            Draw(_indent_from_digit + _axes_indent + "< Enemy >", _enemyCaptionColor);
            DrawLine();
        }

        private static void DrawLine()
        {
            WriteLine();
        }

        private static void Draw(object? obj)
        {
            Draw(obj, AutoDetermineColor(obj));
        }

        private static void Draw(object? obj, ConsoleColor color)
        {
            SetForegroundColor(color);
            Write(obj?.ToString());
        }

        private static void DrawField(Field field, int coordNumber, bool isSingleField = true, bool hideShips = false)
        {
            if (!isSingleField)
                Draw(coordNumber + _axes_indent);

            for (int y = 0; y < field.GetLength(1); y++)
            {
                if (hideShips && field[coordNumber, y] is FieldMarks.Ship)
                {
                    Draw((char)FieldMarks.Empty);
                    continue;
                }
                
                Draw((char)field[coordNumber, y]);
            }
        }

        private static void DrawFieldsLettersLine()
        {
            DrawFieldLettersLine();
            Draw(_indent_between_fields);
            DrawFieldLettersLine();
        }

        private static void DrawFieldLettersLine()
        {
            Draw(_indent_from_digit + _axes_indent);

            for (int i = 0; i < Field.LettersCount; i++)
                Draw($"{Convert.ToChar(i + 65)}");
        }

        private static void DrawNumbersAndField(Field field)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                Draw(i + _axes_indent);
                DrawField(field, i, true, true);
                DrawLine();
            }
        }

        private static void DrawNumbersAndFields(Field attackField, Field defenseField)
        {
            for (int i = 0; i < attackField.GetLength(0); i++)
            {
                DrawField(attackField, i, false, true);
                Draw(_indent_between_fields);
                DrawField(defenseField, i, false, false);

                DrawLine();
            }
        }

        private static void DrawPlayerCaption(string indent, string caption)
        {
            Draw(indent + caption, _enemyCaptionColor);
        }

        private static void DrawRepeatedEmptyLine(int repeats)
        {
            for (int i = 0; i < repeats; i++)
                DrawLine();
        }

        private static void SetForegroundColor(ConsoleColor color)
        {
            ForegroundColor = color;
        }

        private static ConsoleColor AutoDetermineColor(object? obj)
        {
            ConsoleColor color;

            if (obj is char @char && Field.IsCharacterFieldMark(@char))
                color = DetermineColorByFieldMark(@char);
            else if (obj is string @string)
                color = DetermineColorByString(@string);
            else
                color = ConsoleColor.White;

            return color;
        }

        private static ConsoleColor DetermineColorByFieldMark(char mark)
        {
            var color = mark switch
            {
                (char)FieldMarks.Hit => ConsoleColor.DarkRed,
                (char)FieldMarks.Miss => ConsoleColor.DarkGray,
                (char)FieldMarks.Ship => ConsoleColor.DarkGreen,
                _ => ConsoleColor.White,
            };

            return color;
        }

        private static ConsoleColor DetermineColorByString(string str)
        {
            ConsoleColor color;

            if (str.ConsistsOfDigits())
                color = _digitsColor;
            else if (str.ConsistsOfLetters())
                color = _lettersColor;
            else
                color = ConsoleColor.White;

            return color;
        }
    }
}
