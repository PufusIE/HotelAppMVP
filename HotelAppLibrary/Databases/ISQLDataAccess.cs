namespace HotelAppLibrary.Databases
{
    //SQL DB Reading and Writing Interface for Dependency Injection
    public interface ISQLDataAccess
    {
        List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionStringName, bool isStoredProcedure = false);
        void SaveData<T>(string sqlStatement, T parameters, string connectionStringName, bool isStoredProcedure = false);
    }
}