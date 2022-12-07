namespace SeaBattle
{
    internal class Field
    {
        private const int CountOfLetters = 10;
        private const int CountOfNumbers = 10;

        private static readonly Array _marks = Enum.GetValues(typeof(FieldMarks));

        private readonly FieldMarks[,] _field;

        public Field()
        {
            _field = new FieldMarks[CountOfLetters, CountOfNumbers];
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

        private static bool IsCharacterFieldMark(char character)
        {
            foreach (var value in _marks)
                if (character == (char)(FieldMarks)value)
                    return true;

            return false;
        }

        private static bool IsFieldInputFileCorrupted(string[] fieldAsLines)
        {
            for (int i = 0; i < fieldAsLines.Length; i++)
                if (fieldAsLines[i].Length != CountOfLetters + 1)
                    return true;

            return fieldAsLines.Length != CountOfNumbers + 1;
        }
    }
}