namespace Application
{
    using Framework;
    using Framework.Application;
    using System;
    using UnitTest.Application;

    /// <summary>
    /// AppSelector has to be in same assembly like App classes.
    /// </summary>
    public class AppSelectorDemo : AppSelector
    {
        public AppSelectorDemo()
        {
            // UtilFramework.UnitTest(typeof(AppDemo)); // Enable InMemory database.
            // UtilFramework.UnitTest(typeof(MyApp)); // Enable InMemory database.
        }
    }

    public class AppDemo : App
    {
        protected override Type TypePageMain()
        {
            return typeof(PageGridDatabaseBrowse);
        }
    }
}

namespace UnitTest.Application
{
    using Framework.Application;
    using Framework.Component;
    using Database.UnitTest.Application;
    using System;

    public class MyApp : App
    {
        protected override Type TypePageMain()
        {
            return typeof(MyPage);
        }
    }

    public class MyPage : Page
    {
        protected override void InitJson(App app)
        {
            new Grid(app.AppJson, new GridName<MyRow>());
        }
    }
}

namespace Database.UnitTest.Application
{
    using Framework.DataAccessLayer;

    [SqlTable("dbo", "MyRow")]
    public class MyRow : Row
    {
        [SqlColumn(null, null, true)]
        public int Id { get; set; }

        [SqlColumn("Text", typeof(MyRow_Text))]
        public string Text { get; set; }

        [SqlColumn(null, typeof(MyRow_Text))]
        public string X { get; set; }

        public bool? Y { get; set; }
    }

    public class MyRow_Text : Cell<MyRow>
    {

    }
}
