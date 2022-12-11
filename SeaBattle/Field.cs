﻿namespace SeaBattle
{
    internal class Field
    {
        private static readonly Array _marks = Enum.GetValues(typeof(FieldMarks));

        private readonly FieldMarks[,] _field;

        static Field()
        {
            if (!LettersCount.InRange(1, AlphabetSize))
                throw new Exception($"{nameof(LettersCount)} should be in the range from 1 to 25");

            if (!NumbersCount.InRange(1, NumbersSize))
                throw new Exception($"{nameof(NumbersCount)} should be more than 0");
        }

        public Field()
        {
            _field = new FieldMarks[LettersCount, NumbersCount];
        }

        public FieldMarks this[int index1, int index2] { get { return _field[index1, index2]; } }

        public static bool IsCharacterFieldMark(char character)
        {
            foreach (var value in _marks)
                if (character == (char)(FieldMarks)value)
                    return true;

            return false;
        }

        public static void ProduceAttack(Field field)
        {

        }

        public void ParseFieldFromFile(FileInfo file)
        {
            var fieldAsLines = File.ReadAllLines(file.FullName);

            if (IsFieldInputFileCorrupted(fieldAsLines))
                throw new Exception("Field input file is corrupted");

            for (int i = 1; i < fieldAsLines.Length; i++)
            {
                var line = fieldAsLines[i];
                for (int j = 1; j < line.Length; j++)
                {
                    var character = line[j];
                    if (IsCharacterFieldMark(character))
                        _field[i - 1, j - 1] = (FieldMarks)character;
                    else
                        throw new Exception($"Invalid mark \"{character}\"");
                }
            }
        }

        public int GetLength(int dimension)
        {
            return _field.GetLength(dimension);
        }

        private static bool IsFieldInputFileCorrupted(string[] fieldAsLines)
        {
            for (int i = 0; i < fieldAsLines.Length; i++)
                if (fieldAsLines[i].Length != LettersCount + 1)
                    return true;

            return fieldAsLines.Length != NumbersCount + 1;
        }
    }
}