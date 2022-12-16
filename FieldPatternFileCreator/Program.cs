using FieldFileCreator;

namespace FieldPatternFileCreator
{
    internal class Program
    {
        static void Main()
        {
            var fileName = FieldFilename;
            var filesLocationDir = @"..\..\..\..\";

            var filename1 = $"{fileName}1.txt";
            var filename2 = $"{fileName}2.txt";

            LogAndCreateFiles(filesLocationDir, 
              filename1, filename2);
        }

        private static void LogAndCreateFiles(string filesLocationDir,
                              string filename1, string filename2)
        {
            Log("Creating files...");
            WaitTime(FileCreatingTimeInMs);

            CreateFieldPatternFiles(filesLocationDir,
                                    filename1, filename2);

            Log("Files was successfully created");
            Log("Press any button to exit");
            WaitButtonPress();
        }

        private static void CreateFieldPatternFiles(string filesLocationDir, 
                                                    string filename1, string filename2) 
        {
            filesLocationDir = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), filesLocationDir));

            var filepath1 = Path.Combine(filesLocationDir, filename1);
            var filepath2 = Path.Combine(filesLocationDir, filename2);

            FileCreator.CreateFile(new FileInfo(filepath1),
                                   LettersCount, NumbersCount, CharacterOffset);

            FileCreator.CreateFile(new FileInfo(filepath2),
                       LettersCount, NumbersCount, CharacterOffset);
        }
    }
}