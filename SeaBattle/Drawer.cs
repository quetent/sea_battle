namespace SeaBattle
{
    internal static class Drawer
    {
        private static readonly string _indent = new(' ', 1);
        private static readonly string _number_intend = new (' ', Field.CountOfLetters.Length());

        public static void DrawFields(Field attackField, Field defenseField)
        {
            WriteLine(attackField);
        }

        public static void DrawField(Field field)
        {
            Write(_number_intend + _indent);

            for (int i = 0; i < Field.CountOfLetters; i++)
            {
                Write($"{Convert.ToChar(i + 65)}");
            }

            DrawDoubleLine();

            for (int i = 0; i < field.GetLength(0); i++)
            {
                Write(i + _indent);

                for (int j = 0; j < field.GetLength(1); j++)
                    Write((char)field[i, j]);

                WriteLine();
            }
        }

        private static void DrawDoubleLine()
        {
            for (int i = 0; i < 2; i++)
                WriteLine();
        }
    }
}
