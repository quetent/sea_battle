namespace Game
{
    internal static class Drawer
    {
        public static void DrawFields(Field attackField, Field defenseField)
        {
            DrawFieldsLettersLine(CreateWhiteSpaceString(IndentBetweenFields),
                                  CreateWhiteSpaceString(IndentFromDigit + AxesIndent));

            DrawRepeatedEmptyLine(AxesIndent + 1);

            if (IsFullDrawing)
                DrawNumbersAndFields(attackField, defenseField, 
                                     CreateWhiteSpaceString(AxesIndent),
                                     CreateWhiteSpaceString(IndentBetweenFields));
            else
                DrawNumbersAndField(attackField, CreateWhiteSpaceString(AxesIndent));

            DrawPlayerCaption(CreateWhiteSpaceString(IndentFromDigit + AxesIndent), OpponentCaption);

            if (IsFullDrawing)
                DrawPlayerCaption(CreateWhiteSpaceString(IndentFromDigit 
                                                       + AxesIndent 
                                                       + IndentBetweenCaptions), 
                                                         SelfCaption);

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

        public static void Draw(object? obj)
        {
            Draw(obj, AutoDetermineColor(obj));
        }

        public static void Draw(object? obj, ConsoleColor color)
        {
            ConsoleForegroundColor = color;
            Write(obj?.ToString());
            ResetConsoleForegroundColor();
        }

        private static void DrawField(Field field, int coordNumber, string axesIndent, bool hideShips = false)
        {
            if (IsFullDrawing)
                Draw(coordNumber + axesIndent);

            for (int y = 0; y < field.GetLength(0); y++)
            {
                if (hideShips && field[y, coordNumber] is FieldMarks.Ship)
                    Draw((char)FieldMarks.Empty);
                else
                    Draw((char)field[y, coordNumber]);
            }
        }

        private static void DrawFieldsLettersLine(string fieldsIndent, string yAxisIndent)
        {
            DrawFieldLettersLine(yAxisIndent);

            if (IsFullDrawing)
            {
                Draw(fieldsIndent);
                DrawFieldLettersLine(yAxisIndent);
            }
        }

        private static void DrawFieldLettersLine(string yAxisIndent)
        {
            Draw(yAxisIndent);

            for (int i = 0; i < LettersCount; i++)
                Draw(Convert.ToChar(i + CharacterOffset), LettersColor);
        }

        private static void DrawNumbersAndField(Field field, string axesIndent)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                Draw(i + axesIndent);
                DrawField(field, i, axesIndent, true);
                DrawLine();
            }
        }

        private static void DrawNumbersAndFields(Field attackField, Field defenseField, 
                                                 string axesIndent, string fieldsIndent)
        {
            for (int i = 0; i < attackField.GetLength(1); i++)
            {
                DrawField(attackField, i, axesIndent, true);

                if (IsFullDrawing)
                {
                    Draw(fieldsIndent);
                    DrawField(defenseField, i, axesIndent);
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

        private static string CreateWhiteSpaceString(int length)
        {
            return new string(' ', length);
        }
    }
}
