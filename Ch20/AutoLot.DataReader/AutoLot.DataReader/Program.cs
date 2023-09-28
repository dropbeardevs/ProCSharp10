using Microsoft.Data.SqlClient;

Console.WriteLine("***** Fun with Data Readers *****\n");

// Create and open a connection.
using var connection = new SqlConnection();
connection.ConnectionString =
    @"Data Source=.,1433;User Id=sa;Password=Test1234;Initial Catalog=AutoLot;Encrypt=False;";
connection.Open();

// Create a SQL command object.
const string sql = @"Select i.id, m.Name as Make, i.Color, i.Petname FROM Inventory i INNER JOIN Makes m on m.Id = i.MakeId";
var myCommand = new SqlCommand(sql, connection);

// Obtain a data reader a la ExecuteReader().
using var myDataReader = myCommand.ExecuteReader();
// Loop over the results.
while (myDataReader.Read())
{
    Console.WriteLine($"-> Make: {myDataReader["Make"]}, PetName: {myDataReader ["PetName"]}, Color: {myDataReader["Color"]}.");
}