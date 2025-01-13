using System;

namespace LabLibrary
{
    public static class LabRunner
    {
        public static string RunLab(int labName, string[] lines)
        {
            switch (labName)
            {
                case 1:
                    return Lab1.Run(lines);
                case 2:
                    return Lab2.Run(lines);
                case 3:
                    return Lab3.Run(lines);
                default:
                    throw new ArgumentException("Unknown lab name.");
            }
        }
    }
}