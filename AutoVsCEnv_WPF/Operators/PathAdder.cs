using System;

namespace AutoVsCEnv_WPF.Operators
{
    internal class PathAdder
    {
        public static void AddInUserPath(string newPath)
        {
            string pathVar = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);
            if (!pathVar.Contains(newPath))
            {
                if (!pathVar.EndsWith(";") && pathVar != string.Empty)
                {
                    pathVar += ";";
                }

                pathVar += newPath;
            }
            Environment.SetEnvironmentVariable("PATH", pathVar, EnvironmentVariableTarget.User);
        }
    }
}