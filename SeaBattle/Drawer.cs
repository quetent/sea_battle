namespace SeaBattle
{
    internal static class Drawer
    {
        private static readonly string _axes_indent;
        private static readonly string _indent_between_fields;

        private static readonly string _indent_from_number = new(' ', Field.CountOfLetters.Length() - 1);

        static Drawer()
        {
            _axes_indent = new(' ', 1);
            _indent_between_fields = new(' ', 5);
        }

        public static void DrawFields(Field attackField, Field defenseField)
        {
            DrawFieldsLettersLine();
            DrawRepeatedEmptyLine(2);
            DrawNumbersAndFields(attackField, defenseField);
        }

        public static void DrawField(Field field)
        {
            DrawFieldLettersLine();
            DrawRepeatedEmptyLine(2);
            DrawNumbersAndField(field);
            DrawLine();
        }

        private static void DrawLine()
        {
            WriteLine();
        }

        private static void Draw<T>(T arg)
        {
            Write(arg);
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
            Draw(_indent_from_number + _axes_indent);

            for (int i = 0; i < Field.CountOfLetters; i++)
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

        private static void DrawRepeatedEmptyLine(int repeats)
        {
            for (int i = 0; i < repeats; i++)
                DrawLine();
        }

        private static void SetColor()
        {
            ForegroundColor = ConsoleColor.Red;
        }
    }
}
