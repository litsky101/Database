using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Database
{
    public class DAL
    {
        MySqlConnection conn = null;
        MySqlCommand cmd = null;
        MySqlDataAdapter da = null;
        MySqlDataReader dr = null;
        string connectionString = Properties.Settings.Default.DB;

        public List<Employee> GetEmployees()
        {
            try
            {
                List<Employee> result = new List<Employee>();
                string query = string.Empty;

                conn = new MySqlConnection(connectionString); //initialize connection
                conn.Open(); // request open connection sa database

                query = "SELECT id, lastname, firstname, middlename, datecreated FROM employees ORDER BY ID";

                cmd = new MySqlCommand(query, conn);

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    result.Add(new Employee
                    {
                        ID = Convert.ToInt32(dr["id"]),
                        LastName = dr["LastName"].ToString(),
                        FirstName = dr["FirstName"].ToString(),
                        MiddleName = dr["MiddleName"].ToString(),
                        DateCreated = Convert.ToDateTime(dr["DateCreated"])
                    });
                }

                return result;
            }
            catch
            {

                throw;
            }
            finally
            {
                if(conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public List<Employee> GetEmployees(string searchKey)
        {
            try
            {
                List<Employee> result = new List<Employee>();
                string query = string.Empty;

                conn = new MySqlConnection(connectionString); //initialize connection
                conn.Open(); // request open connection sa database

                query = "SELECT id, lastname, firstname, middlename, datecreated FROM employees WHERE LastName Like @search OR FirstName Like @search ORDER BY ID";

                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@search", $"%{searchKey}%");

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    result.Add(new Employee
                    {
                        ID = Convert.ToInt32(dr["id"]),
                        LastName = dr["LastName"].ToString(),
                        FirstName = dr["FirstName"].ToString(),
                        MiddleName = dr["MiddleName"].ToString(),
                        DateCreated = Convert.ToDateTime(dr["DateCreated"])
                    });
                }

                return result;
            }
            catch
            {

                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public int UpdateEmployee(Employee emp)
        {
            try
            {
                int result = 0;
                string query = string.Empty;

                conn = new MySqlConnection(connectionString); //initialize connection
                conn.Open(); // request open connection sa database

                query = "UPDATE Employees SET LastName = @lname, FirstName = @fname, MiddleName = @mname WHERE ID = @id";

                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", emp.ID);
                cmd.Parameters.AddWithValue("@lname", emp.LastName);
                cmd.Parameters.AddWithValue("@fname", emp.FirstName);
                cmd.Parameters.AddWithValue("@mname", emp.MiddleName);

                result = cmd.ExecuteNonQuery();

                return result;
            }
            catch
            {

                throw;
            }
        }

        public int InsertEmployee(Employee emp)
        {
            try
            {
                int result = 0;
                string query = string.Empty;

                conn = new MySqlConnection(connectionString); //initialize connection
                conn.Open(); // request open connection sa database

                query = "INSERT INTO Employees (LastName, FirstName, MiddleName) VALUES (@lname, @fname, @mname);";

                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@lname", emp.LastName);
                cmd.Parameters.AddWithValue("@fname", emp.FirstName);
                cmd.Parameters.AddWithValue("@mname", emp.MiddleName);

                result = cmd.ExecuteNonQuery();

                return result;
            }
            catch
            {

                throw;
            }
        }
    }
}
