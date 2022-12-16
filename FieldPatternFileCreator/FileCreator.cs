namespace FieldPatternFileCreator
{
    internal static class FileCreator
    {
        public static void CreateFile(FileInfo fileInfo, 
                                      int lettersCount, int numbersCount, int charOffset)
        {
            var field = CreateField(lettersCount, numbersCount, charOffset);

            DumpToFile(fileInfo, field);
        }

        private static void DumpToFile(FileInfo fileInfo, IEnumerable<string> lines)
        {
            File.WriteAllLines(fileInfo.FullName, lines);
        }
        
        private static List<string> CreateField(int lettersCount, int numbersCount, int charOffset)
        {
            var lines = new List<string>();

            var lettersLine = new string(' ', 1);
            for (int i = 0; i < lettersCount; i++)
                lettersLine += Convert.ToChar(i + charOffset);

            lines.Add(lettersLine);

            for (int i = 0; i < numbersCount; i++)
                lines.Add(i + new string(' ', lettersCount));

            return lines;
        }
    }
}
