namespace SeaBattle
{
    internal static class Drawer
    {
        private static readonly string _indent = new(' ', 1);
        private static readonly string _field_indent = new(' ', 5);
        private static readonly string _number_intend = new(' ', Field.CountOfLetters.Length() - 1);

        public static void DrawFields(Field attackField, Field defenseField)
        {
            DrawLettersLine();
            Write(_field_indent);
            DrawLettersLine();

            DrawRepeatedEmptyLine(2);

            DrawNumbersAndFields(attackField, defenseField);
        }

        public static void DrawField(Field field)
        {
            DrawLettersLine();
            DrawRepeatedEmptyLine(2);

            DrawNumbersAndField(field);
        }

        private static void DrawRepeatedEmptyLine(int repeats)
        {
            for (int i = 0; i < repeats; i++)
                WriteLine();
        }

        private static void DrawLettersLine()
        {
            Write(_number_intend + _indent);

            for (int i = 0; i < Field.CountOfLetters; i++)
                Write($"{Convert.ToChar(i + 65)}");
        }

        private static void DrawNumbersAndField(Field field)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                Write(i + _indent);

                for (int j = 0; j < field.GetLength(1); j++)
                    Write((char)field[i, j]);

                WriteLine();
            }
        }

        private static void DrawNumbersAndFields(Field field1, Field field2)
        {
            for (int i = 0; i < field1.GetLength(0); i++)
            {
                Write(i + _indent);

                for (int j = 0; j < field1.GetLength(1); j++)
                {
                    Write((char)field1[i, j]);
                }
                    
                Write(_field_indent);

                Write(i + _indent);

                for (int j = 0; j < field2.GetLength(1); j++)
                {
                    Write((char)field1[i, j]);
                }
                    

                WriteLine();
            }
        }
    }
}
