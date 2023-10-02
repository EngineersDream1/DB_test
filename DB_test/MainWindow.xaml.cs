using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Npgsql;

namespace DB_test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PostgreSQL_Manager postgreSQL = new PostgreSQL_Manager();
        SQL_Server_Manager sqlServer = new SQL_Server_Manager();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartSP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(departmentID.Text) && !String.IsNullOrEmpty(percent.Text))
                {
                    int depID = int.Parse(departmentID.Text);
                    decimal percent_t = Convert.ToDecimal(percent.Text);

                    //Получение данных из БД MS SQL Server
                    dataGrid.ItemsSource = sqlServer.CallStoredProcedure(depID, percent_t).DefaultView;

                    //Получение данных из БД PostgreSQL
                    //dataGrid.ItemsSource = postgreSQL.CallStoredProcedure(depID, percent_t).DefaultView;
                }
                else
                {
                    MessageBox.Show("Необходимые для выполнения данные отсутствуют! Проверьте правильность заполнения Department ID и Percent.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }        
    }
}
