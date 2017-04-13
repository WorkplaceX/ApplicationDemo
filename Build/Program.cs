using Framework.Build;
using System;

namespace Build
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                do
                {
                    Util.Log("");
                    Util.Log("Build Command");
                    Util.MethodExecute(new Script(args));
                }
                while (args.Length == 0); // Loop only if started without command line arguments.
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                Environment.Exit(-1);
            }
        }
    }
}
