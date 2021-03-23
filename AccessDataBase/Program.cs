using System;
using System.Data.OleDb;

namespace AccessDataBase
{
    class Program
    {
        static void createDataTable()
        {
            OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder();
            builder.Provider = "Microsoft.ACE.OLEDB.12.0";
            builder.DataSource = System.IO.Directory.GetCurrentDirectory() + "\\DataBase.accdb";
#if DEBUG
            if (System.IO.File.Exists(builder.DataSource))
            {
                System.IO.File.Delete(builder.DataSource);
            }
#endif
            ADOX.Catalog catlog = new ADOX.Catalog();
            ADOX.Table table = new ADOX.Table();
            table.Name = "Sample1";
            if (!System.IO.File.Exists(builder.DataSource))
            {
                catlog.Create(builder.ConnectionString);
                table.Columns.Append("Index", ADOX.DataTypeEnum.adInteger, 6);
                table.Columns["Index"].ParentCatalog = catlog;
                table.Columns["Index"].Properties["AutoIncrement"].Value = true;
                table.Keys.Append("PrimaryKey", ADOX.KeyTypeEnum.adKeyPrimary, "Index");
                catlog.Tables.Append(table);
            }
            ADODB.Connection connection = catlog.ActiveConnection;
            if(connection != null)
                connection.Close();
        }
        static void Main(string[] args)
        {
            createDataTable();
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
