using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DB_test
{
    public class PostgreSQL_Manager
    {
        private NpgsqlConnection CreateConnection()
        {
            string connectionString = "Host=localhost;Port=5432;Database=WorkSpaceDB;Username=postgres;Password=admin";
            return new NpgsqlConnection(connectionString);
        }

        public DataTable CallStoredProcedure(int departmentId, decimal percent)
        {
            using (var connection = CreateConnection())
            {
                try
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand("UPDATESALARYFORDEPARTMENT", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Добавляем параметры для хранимой процедуры
                        command.Parameters.AddWithValue("p_department_id", departmentId);
                        command.Parameters.AddWithValue("p_percent", percent);

                        // Выполняем хранимую процедуру и получаем результаты
                        command.ExecuteNonQuery();
                        var command1 = new NpgsqlCommand("SELECT * FROM results ORDER BY id", connection);
                        var adapter = new NpgsqlDataAdapter(command1);
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
                
            }
        }
    }
}
