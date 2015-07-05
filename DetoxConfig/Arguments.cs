using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetoxConfig
{
    static class Arguments
    {
        static Dictionary<string, string> values;

        public static void Init(string[] args)
        {
            values = new Dictionary<string, string>();
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith("-"))
                {
                    if (args[i].Contains('='))
                    {
                        values.Add(args[i].Substring(1, args[i].IndexOf('=') - 1).ToLower(), args[i].Substring(args[i].IndexOf('=') + 1));
                    }
                    else
                    {
                        values.Add(args[i].Substring(1).ToLower(), "");
                    }
                }
            }
        }

        public static bool HasArgument(string argument)
        {
            return values.ContainsKey(argument.ToLower());
        }

        public static bool GetArgument(string argument, out string value)
        {
            value = null;
            if (HasArgument(argument))
            {
                value = values[argument.ToLower()];
                return true;
            }
            return false;
        }

        public static string GetArgument(string argument)
        {
            if (HasArgument(argument))
            {
                return values[argument.ToLower()];
            }
            throw new IndexOutOfRangeException("The argument \"" + argument + "\" was not found!");
        }
    }
}
