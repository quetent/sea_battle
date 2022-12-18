namespace Game
{
    internal static class Drawer
    {
        public static void DrawFields(Field attackField, Field defenseField) //
        {
            DrawFieldsLettersLine();
            DrawRepeatedEmptyLine(AxesIndent.Length);

            if (IsFullDrawing)
                DrawNumbersAndFields(attackField, defenseField);
            else
                DrawNumbersAndField(attackField);

            DrawPlayerCaption(IndentFromDigit + AxesIndent, OpponentCaption);

            if (IsFullDrawing)
                DrawPlayerCaption(IndentFromDigit + AxesIndent + IndentBetweenFields + " ", SelfCaption);

            DrawLine();
        }

        public static void DrawField(Field field) //
        {
            DrawFieldLettersLine();
            DrawRepeatedEmptyLine(AxesIndent.Length);
            
            DrawPlayerCaption(IndentFromDigit + AxesIndent, OpponentCaption);
            DrawLine();
        }

        public static void DrawRepeatedEmptyLine(int repeats)
        {
            for (int i = 0; i < repeats; i++)
                DrawLine();
        }

        public static void DrawLine()
        {
            WriteLine();
        }

        public static void Erase()
        {
            Clear();
        }

        private static void Draw(object? obj)
        {
            Draw(obj, AutoDetermineColor(obj));
        }

        private static void Draw(object? obj, ConsoleColor color)
        {
            ConsoleForegroundColor = color;
            Write(obj?.ToString());
            ResetConsoleForegroundColor();
        }

        private static void DrawField(Field field, int coordNumber, bool hideShips = false)
        {
            if (IsFullDrawing)
                Draw(coordNumber + AxesIndent);

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

            if (IsFullDrawing)
            {
                Draw(IndentBetweenFields);
                DrawFieldLettersLine();
            }
        }

        private static void DrawFieldLettersLine()
        {
            Draw(IndentFromDigit + AxesIndent);

            for (int i = 0; i < LettersCount; i++)
                Draw(Convert.ToChar(i + CharacterOffset), LettersColor);
        }

        private static void DrawNumbersAndField(Field field)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                Draw(i + AxesIndent);
                DrawField(field, i, true);
                DrawLine();
            }
        }

        private static void DrawNumbersAndFields(Field attackField, Field defenseField)
        {
            for (int i = 0; i < attackField.GetLength(0); i++)
            {
                DrawField(attackField, i, true);

                if (IsFullDrawing)
                {
                    Draw(IndentBetweenFields);
                    DrawField(defenseField, i, false);
                }

                DrawLine();
            }
        }

        private static void DrawPlayerCaption(string indent, string caption)
        {
            Draw(indent + caption, PlayerCaptionColor);
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
                color = DigitsColor;
            else if (str.ConsistsOfLetters())
                color = LettersColor;
            else
                color = ConsoleColor.White;

            return color;
        }
    }
}
