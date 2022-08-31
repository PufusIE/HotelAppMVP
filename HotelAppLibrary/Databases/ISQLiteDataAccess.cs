namespace HotelAppLibrary.Databases
{
    //SQLite DB Reading and Writing Interface for Dependency Injection
    public interface ISQLiteDataAccess
    {
        List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionStringName);
        void SaveData<T>(string sqlStatement, T parameters, string connectionStringName, bool isStoredProcedure = false);
    }
}