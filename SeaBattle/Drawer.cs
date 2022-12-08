namespace SeaBattle
{
    internal static class Drawer
    {
        private static readonly string _axes_indent = new(' ', 1);
        private static readonly string _indent_between_fields = new(' ', 5);
        private static readonly string _indent_from_number = new(' ', Field.CountOfLetters.Length() - 1);

        public static void DrawFields(Field attackField, Field defenseField)
        {
            DrawFieldsLettersLine();
            DrawRepeatedEmptyLine(2);
            DrawNumbersAndFields(attackField, defenseField);
        }

        public static void DrawField(Field field)
        {
            DrawLettersLine();
            DrawRepeatedEmptyLine(2);

            DrawNumbersAndField(field);
        }

        private static void DrawLine()
        {
            WriteLine();
        }

        private static void Draw<T>(T arg)
        {
            Write(arg);
        }

        private static void DrawField(Field field, int coordNumber, bool hideShips = false)
        {
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

        private static void DrawRepeatedEmptyLine(int repeats)
        {
            for (int i = 0; i < repeats; i++)
                DrawLine();
        }

        private static void DrawFieldsLettersLine()
        {
            DrawLettersLine();
            Draw(_indent_between_fields);
            DrawLettersLine();
        }

        private static void DrawLettersLine()
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
                DrawField(field, i);
                DrawLine();
            }
        }

        private static void DrawNumbersAndFields(Field attackField, Field defenseField)
        {
            for (int i = 0; i < attackField.GetLength(0); i++)
            {
                DrawField(attackField, i, true);
                Draw(_indent_between_fields);
                DrawField(defenseField, i);

                DrawLine();
            }
        }
    }
}
