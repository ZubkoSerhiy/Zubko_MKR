using System;

namespace LabLibrary
{
    public static class LabRunner
    {
        public static void RunLab(string labName, string inputFile, string outputFile)
        {
            switch (labName)
            {
                case "lab1":
                    Lab1.Run(inputFile, outputFile);
                    break;
                case "lab2":
                    Lab2.Run(inputFile, outputFile);
                    break;
                case "lab3":
                    Lab3.Run(inputFile, outputFile);
                    break;
                default:
                    throw new ArgumentException("Unknown lab name.");
            }
        }
    }
}