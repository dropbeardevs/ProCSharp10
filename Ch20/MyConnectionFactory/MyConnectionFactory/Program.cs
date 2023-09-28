using System.Data;
using System.Data.Odbc;
using Microsoft.Data.SqlClient;
using MyConnectionFactory;

Console.WriteLine("**** Very Simple Connection Factory *****\n");

Setup(DataProviderEnum.SqlServer);
Setup(DataProviderEnum.Odbc);
Setup(DataProviderEnum.None);
Console.ReadLine();

void Setup(DataProviderEnum provider)
{
    // Get a specific connection.
    IDbConnection myConnection = GetConnection(provider);
    Console.WriteLine($"Your connection is a {myConnection?.GetType().Name ?? "unrecognized type"}");
    // Open, use and close connection...
}

// This method returns a specific connection object
// based on the value of a DataProvider enum.
IDbConnection GetConnection(DataProviderEnum dataProvider)
    => dataProvider switch
    {
        DataProviderEnum.SqlServer => new SqlConnection(),
        DataProviderEnum.Odbc => new OdbcConnection(),
        _ => null,
    };