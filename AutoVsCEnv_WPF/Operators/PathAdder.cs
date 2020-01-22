using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVsCEnv_WPF.Operators
{
    class PathAdder
    {
        public static void AddInUserPath(string newPath)
        {
            string pathVar = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);
            if(!pathVar.Contains(newPath))
            {
                if(!pathVar.EndsWith(";"))
                {
                    pathVar += ";";
                }

                pathVar += newPath;
            }
            Environment.SetEnvironmentVariable("PATH", newPath, EnvironmentVariableTarget.User);
        }
    }
}
