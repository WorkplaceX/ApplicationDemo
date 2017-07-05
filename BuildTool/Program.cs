using Application;

namespace BuildTool
{
    class Program
    {
        static void Main(string[] args)
        {
            new AppBuildToolDemo(new AppDemo()).Run(args);
        }
    }
}