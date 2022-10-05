using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Adonet_Nortwind
{
    static class Db
    {
        /*
         1-Kütüphane eklenecek
         using System.Data
         using System.Data.SqlClient

        2-Sınıfları(Class) Belirtmek
        SqlConnection conn;
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        SqlCommand cmd;
        SqlDataReader dr;
         */
        static SqlConnection conn = new SqlConnection("Data Source=202-001;Initial Catalog=Northwind;User ID=sa; Password=1234");
        static SqlDataAdapter da;
        static DataSet ds;
        static DataTable dt;
        static SqlCommand cmd;
        static SqlDataReader dr;
        static string cmdText;

        static Form1 f = (Form1)Application.OpenForms["Form1"];
        public static void GridFill(string param)
        {
            conn.Open();
            cmdText = $"select EmployeeID,FirstName,LastName,City,Country from Employees {param}";
            da = new SqlDataAdapter(cmdText,conn);
            ds = new DataSet();
            //dt = new DataTable();
            da.Fill(ds);
            //da.Fill(ds,"Calısanlar");
            //da.Fill(dt);
            f.dataGridView1.DataSource = ds.Tables[0];
            //f.dataGridView1.DataSource = ds.Tables["Calısanlar"];
            //f.dataGridView1.DataSource = dt;
            conn.Close();
            GridEdit();
        }

        private static void GridEdit()
        {
            DataGridViewColumn column_id,column_name,column_surname,column_city,column_country;
            column_id = f.dataGridView1.Columns[0];
            column_name = f.dataGridView1.Columns[1];
            column_surname = f.dataGridView1.Columns[2];
            column_city = f.dataGridView1.Columns[3];
            column_country = f.dataGridView1.Columns[4];


            column_id.HeaderText = "ID";
            column_name.HeaderText = "AD";
            column_surname.HeaderText = "SOYAD";
            column_city.HeaderText = "ŞEHİR";
            column_country.HeaderText = "ÜLKE";

            column_id.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column_id.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public static void AddEmployees(string name,string surname,string city,string country)
        {
            conn.Open();
            cmdText = "insert into Employees(FirstName,LastName,City,Country) values(@name,@surname,@city,@country)";
            cmd = new SqlCommand(cmdText,conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@surname",surname);
            cmd.Parameters.AddWithValue("@city", city);
            cmd.Parameters.AddWithValue("@country",country);

            cmd.ExecuteNonQuery();
            conn.Close();
            GridFill("");
        }

        public static void UpdateEmployees(int id,string name, string surname, string city, string country)
        {
            conn.Open();
            cmdText = "update Employees set FirstName =  @name,LastName = @surname,City = @city,Country = @country where EmployeeID  = @id";
            cmd = new SqlCommand(cmdText, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@surname", surname);
            cmd.Parameters.AddWithValue("@city", city);
            cmd.Parameters.AddWithValue("@country", country);

            cmd.ExecuteNonQuery();
            conn.Close();
            GridFill("");
        }

        public static void DeleteEmployees(int id)
        {
            conn.Open();
            cmdText = $"delete Employees where EmployeeID = {id}";
            cmd = new SqlCommand(cmdText, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            GridFill("");
        }

        public static string username, pass;
        static FrmLogin fl = (FrmLogin)Application.OpenForms["FrmLogin"];
        public static void Login()
        {
            f = new Form1();
            conn.Open();
            cmdText = "select Username,Pass from Users";
            cmd = new SqlCommand(cmdText, conn);
            dr = cmd.ExecuteReader();
            while(dr.Read())//satır satır okur
            {
                username = dr[0].ToString();
                pass = dr["Pass"].ToString();
                if(username == fl.txtUsername.Text && pass == fl.txtPass.Text)
                {
                    f.Show();
                    fl.Hide();
                    conn.Close();
                    return;
                }
            }
            MessageBox.Show("Hatalı Girdiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            conn.Close();
        }
    }
}
