namespace Build
{
    public class Script : Framework.Build.ScriptBase
    {
        public Script(string[] args) 
            : base(args)
        {

        }

        public override void RunSql()
        {
            if (Framework.Util.IsLinux == false)
            {
                Airport.Script.Run();
            }
            base.RunSql();
        }
    }
}