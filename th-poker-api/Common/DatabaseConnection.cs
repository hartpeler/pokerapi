namespace th_poker_api.Common
{
    public class DatabaseConnection
    {
        //SIT
        //public string dataBase = "Data Source=10.10.10.12;Initial Catalog=db_pokerSIT;User ID=sa;Password=Nander81;MultipleActiveResultSets = True;";
        public string dataBase = "Server=tcp:perpusku.database.windows.net,1433;Initial Catalog=db-poker;Persist Security Info=False;User ID=perpusadmin;Password=Nander81;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"; //Database Rebase with New Seeding
        
            
        // public string dataBase = "Data Source=10.10.10.12;Initial Catalog=dummyTest;User ID=sa;Password=Nander81;MultipleActiveResultSets = True;";





        //UAT
        //public string dataBase = "Data Source=10.10.10.12;Initial Catalog=db_pokerUAT;User ID=sa;Password=Nander81;MultipleActiveResultSets = True;";

        //PROD
        //public string dataBase = "Data Source=10.10.10.12;Initial Catalog=db_pokerPROD;User ID=sa;Password=Nander81;MultipleActiveResultSets = True;";

    }
}

