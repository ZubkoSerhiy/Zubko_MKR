using LabLibrary;

namespace LabConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                PrintHelp();
                return;
            }

            var command = args[0];
            switch (command)
            {
                case "run":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Specify lab name: lab1, lab2, or lab3.");
                        return;
                    }
                    RunLab(args);
                    break;

                case "set-path":
                    SetPath(args);
                    break;

                default:
                    PrintHelp();
                    break;
            }
        }

        private static void RunLab(string[] args)
        {
            string labName = args[1];
            string inputPath = GetArgument(args, "-i", "--input") ?? GetDefaultPath("LAB_PATH", "input.txt");
            string outputPath = GetArgument(args, "-o", "--output") ?? GetDefaultPath("LAB_PATH", "output.txt");

            if (inputPath == null)
            {
                Console.WriteLine("Input file not found.");
                return;
            }

            LabRunner.RunLab(labName, inputPath, outputPath);
            Console.WriteLine($"Lab {labName} completed. Output written to {outputPath}.");
        }

        private static void SetPath(string[] args)
        {
            string path = GetArgument(args, "-p", "--path");
            if (path == null)
            {
                Console.WriteLine("Path not specified.");
                return;
            }

            Environment.SetEnvironmentVariable("LAB_PATH", path, EnvironmentVariableTarget.User);
            Console.WriteLine($"LAB_PATH set to {path}");
        }

        private static string? GetArgument(string[] args, string shortName, string longName)
        {
            int index = Array.FindIndex(args, x => x == shortName || x == longName);
            return (index >= 0 && index < args.Length - 1) ? args[index + 1] : null;
        }

        private static string? GetDefaultPath(string envVar, string fileName)
        {
            string? envPath = Environment.GetEnvironmentVariable(envVar);
            if (!string.IsNullOrEmpty(envPath))
            {
                return Path.Combine(envPath, fileName);
            }

            string defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), fileName);
            return File.Exists(defaultPath) ? defaultPath : null;
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  version                      - Show version information.");
            Console.WriteLine("  run <lab1|lab2|lab3> [args]  - Run the specified lab.");
            Console.WriteLine("    -i, --input <path>         - Input file path.");
            Console.WriteLine("    -o, --output <path>        - Output file path.");
            Console.WriteLine("  set-path -p, --path <path>   - Set LAB_PATH environment variable.");
        }
    }
}
