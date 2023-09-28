using System.Reflection.Metadata.Ecma335;

namespace AutoLot.Dal.DataOperations;

public class InventoryDal : IDisposable
{
    private readonly string _connectionString;

    // Default Constructor calls overloaded constructor
    public InventoryDal() : this(
        @"Data Source=192.168.4.33,1433;User Id=sa;Password=Test1234;Initial Catalog=AutoLot;Encrypt=False;")
    {
    }

    public InventoryDal(string connectionString) => _connectionString = connectionString;
    
    private SqlConnection _sqlConnection = null;
    
    private void OpenConnection()
    {
        _sqlConnection = new SqlConnection
        {
            ConnectionString = _connectionString
        };
        _sqlConnection.Open();
    }

    private void CloseConnection()
    {
        if (_sqlConnection?.State != ConnectionState.Closed)
        {
            _sqlConnection?.Close();
        }
    }
    
    bool _disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }
        if (disposing)
        {
            _sqlConnection.Dispose();
        }
        _disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    public List<CarViewModel> GetAllInventory()
    {
        OpenConnection();
        // This will hold the records.
        var inventory = new List<CarViewModel>();
        
        // Prep command object.
        const string sql = """
                           SELECT i.Id, i.Color, i.PetName,m.Name as Make
                                           FROM Inventory i
                                           INNER JOIN Makes m on m.Id = i.MakeId
                           """;
        using var command = new SqlCommand(sql, _sqlConnection);
        command.CommandType = CommandType.Text;
        command.CommandType = CommandType.Text;
        
        var dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
        
        while (dataReader.Read())
        {
            inventory.Add(new CarViewModel
            {
                Id = (int)dataReader["Id"],
                Color = (string)dataReader["Color"],
                Make = (string)dataReader["Make"],
                PetName = (string)dataReader["PetName"]
            });
        }
        dataReader.Close();
        return inventory;
    }
    
    public CarViewModel GetCar(int id)
    {
        OpenConnection();
        CarViewModel car = null;
        
        SqlParameter param = new SqlParameter
        {
            ParameterName = "@carId",
            Value = id,
            SqlDbType = SqlDbType.Int,
            Direction = ParameterDirection.Input
        };
        
        //This should use parameters for security reasons
        const string sql = """
                           SELECT i.Id, i.Color, i.PetName,m.Name as Make
                                               FROM Inventory i INNER JOIN Makes m on m.Id = i.MakeId
                                               WHERE i.Id = @CarId
                           """;
        
        using var command = new SqlCommand(sql, _sqlConnection);
        command.CommandType = CommandType.Text;
        command.Parameters.Add(param);
        
        var dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

        while (dataReader.Read())
        {
            car = new CarViewModel
            {
                Id = (int) dataReader["Id"],
                Color = (string) dataReader["Color"],
                Make = (string) dataReader["Make"],
                PetName = (string) dataReader["PetName"]
            };
        }
        dataReader.Close();
        
        return car;
    }
    
    public void InsertAuto(string color, int makeId, string petName)
    {
        OpenConnection();
        
        // Format and execute SQL statement.
        var sql = $"Insert Into Inventory (MakeId, Color, PetName) Values ('{makeId}', '{color}', '{petName}')";
        
        // Execute using our connection.
        using (var command = new SqlCommand(sql, _sqlConnection))
        {
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
        }
        CloseConnection();
    }
    
    public void InsertAuto(Car car)
    {
        OpenConnection();

        // Note the "placeholders" in the SQL query.
        const string sql = "Insert Into Inventory" +
                           "(MakeId, Color, PetName) Values" +
                           "(@MakeId, @Color, @PetName)";

        // This command will have internal parameters.
        using SqlCommand command = new SqlCommand(sql, _sqlConnection);
        // Fill params collection.
        var parameter = new SqlParameter
        {
            ParameterName = "@MakeId",
            Value = car.MakeId,
            SqlDbType = SqlDbType.Int,
            Direction = ParameterDirection.Input
        };
        command.Parameters.Add(parameter);
            
        parameter = new SqlParameter
        {
            ParameterName = "@Color",
            Value = car.Color,
            SqlDbType = SqlDbType. NVarChar,
            Size = 50,
            Direction = ParameterDirection.Input
        };
        command.Parameters.Add(parameter);
            
        parameter = new SqlParameter
        {
            ParameterName = "@PetName",
            Value = car.PetName,
            SqlDbType = SqlDbType. NVarChar,
            Size = 50,
            Direction = ParameterDirection.Input
        };
        command.Parameters.Add(parameter);
            
        command.ExecuteNonQuery();
            
        CloseConnection();
    }
    
    public void DeleteCar(int id)
    {
        OpenConnection();

        SqlParameter param = new SqlParameter
        {
            ParameterName = "@carId",
            Value = id,
            SqlDbType = SqlDbType.Int,
            Direction = ParameterDirection.Input
        };
        
        // Get ID of car to delete, then do so.
        const string sql = "Delete from Inventory where Id = @carId";

        using (var command = new SqlCommand(sql, _sqlConnection))
        {
            try
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add(param);
                
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                var error = new Exception("Sorry! That car is on order!", ex);
                throw error;
            }
        }
        CloseConnection();
    }
    
    public void UpdateCarPetName(int id, string newPetName)
    {
        OpenConnection();
        
        SqlParameter paramId = new SqlParameter
        {
            ParameterName = "@carId",
            Value = id,
            SqlDbType = SqlDbType.Int,
            Direction = ParameterDirection.Input
        };
        SqlParameter paramName = new SqlParameter
        {
            ParameterName = "@petName",
            Value = newPetName,
            SqlDbType = SqlDbType.NVarChar,
            Size = 50,
            Direction = ParameterDirection.Input
        };

        // Get ID of car to modify the pet name.
        const string sql = $"Update Inventory Set PetName = @petName Where Id = @carId";
        
        using (var command = new SqlCommand(sql, _sqlConnection))
        {
            command.Parameters.Add(paramId);
            command.Parameters.Add(paramName);
            
            command.ExecuteNonQuery();
        }
        CloseConnection();
    }
    
    public string LookUpPetName(int carId)
    {
        OpenConnection();

        // Establish name of stored proc.
        using var command = new SqlCommand("GetPetName", _sqlConnection);
        command.CommandType = CommandType.StoredProcedure;

        // Input param.
        var param = new SqlParameter
        {
            ParameterName = "@carId",
            SqlDbType = SqlDbType.Int,
            Value = carId,
            Direction = ParameterDirection.Input
        };
        command.Parameters.Add(param);

        // Output param.
        param = new SqlParameter
        {
            ParameterName = "@petName",
            SqlDbType = SqlDbType.NVarChar,
            Size = 50,
            Direction = ParameterDirection.Output
        };
        command.Parameters.Add(param);

        // Execute the stored proc.
        command.ExecuteNonQuery();

        // Return output param.
        var carPetName = (string)command.Parameters["@petName"].Value;
        
        CloseConnection();
        
        return carPetName;
    }
}