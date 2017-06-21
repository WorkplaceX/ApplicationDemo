namespace BuildTool.Airport
{
    using Framework.BuildTool;

    public class Script
    {
        /// <summary>
        /// Load Airport.xlsx into database.
        /// </summary>
        public static void Run()
        {
            string connectionString = Framework.Server.ConnectionManager.ConnectionString;
            string fileName = Framework.UtilFramework.FolderName + "Submodule/Office/bin/Debug/Office.exe";
            // SqlDrop
            {
                string command = "SqlDrop";
                string arguments = command + " " + "\"" + connectionString + "\"";
                UtilBuildTool.Start(Framework.UtilFramework.FolderName, fileName, arguments);
            }
            // SqlCreate
            {
                string command = "SqlCreate";
                string arguments = command + " " + "\"" + connectionString + "\"";
                UtilBuildTool.Start(Framework.UtilFramework.FolderName, fileName, arguments);
            }
            // Run
            {
                string command = "Run";
                string folderName = Framework.UtilFramework.FolderName + "BuildTool/Airport/";
                string arguments = command + " " + "\"" + connectionString + "\"" + " " + "\"" + folderName + "\"";
                UtilBuildTool.Start(Framework.UtilFramework.FolderName, fileName, arguments);
            }
        }
    }
}
