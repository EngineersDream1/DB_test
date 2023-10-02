using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DB_test
{
    public class SQL_Server_Manager
    {        
        private SqlConnection CreateSQL_Connection()
        {
            string connectionString = @"Data Source=DESKTOP-1120MHP\SQLEXPRESS;Initial Catalog=WorkSpaceDB;Integrated Security=True;";
            return new SqlConnection(connectionString);
        }

        public DataTable CallStoredProcedure(int departmentId, decimal percent)
        {
            using (SqlConnection connection = CreateSQL_Connection())
            {
                try
                {
                    connection.Open();

                    using(var command = new SqlCommand("UPDATESALARYFORDEPARTMENT",connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Добавляем параметры для хранимой процедуры
                        command.Parameters.AddWithValue("p_department_id", departmentId);
                        command.Parameters.AddWithValue("p_percent", percent);

                        // Выполняем хранимую процедуру и получаем результаты
                        command.ExecuteNonQuery();
                        var command1 = new SqlCommand("SELECT * FROM results ORDER BY id", connection);
                        var adapter = new SqlDataAdapter(command1);
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка выполнения хранимой процедуры: " + ex.Message);
                    return null;
                }
            }
        }

    }
}
