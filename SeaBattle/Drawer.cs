namespace SeaBattle
{
    internal class Drawer
    {
        public static void DrawFields(Field attackField, Field defenseField)
        {
            WriteLine(attackField);
        }

        public static void DrawField(Field field)
        {
            Write("  ");

            for (int i = 0; i < Field.CountOfLetters; i++)
            {
                Write($"{Convert.ToChar(i + 65)}");
            }

            DrawDoubleLine();

            for (int i = 0; i < field.GetLength(0); i++)
            {
                Write(i + " ");

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
