using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=SUDIPTO;Initial Catalog=Task;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Product_info VALUES (@id, @name, @design, @color)", con);


                command.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));
                command.Parameters.AddWithValue("@name", textBox2.Text);
                command.Parameters.AddWithValue("@design", textBox3.Text);
                command.Parameters.AddWithValue("@color", comboBox1.Text);

                command.ExecuteNonQuery();
                MessageBox.Show("Successfully Inserted.");
                con.Close();
                BindData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        private void BindData()
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Product_info", con);
                SqlDataAdapter sd = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while fetching data: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Product_info WHERE id = @id", con);
                command.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Successfully Deleted.");
                }
                else
                {
                    MessageBox.Show("No records found to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                BindData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand command = new SqlCommand("UPDATE Product_info SET name = @name, design = @design, color = @color WHERE id = @id", con);
            command.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));
            command.Parameters.AddWithValue("@name", textBox2.Text);
            command.Parameters.AddWithValue("@design", textBox3.Text);
            command.Parameters.AddWithValue("@color", comboBox1.Text);
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Successfully Updated.");
            }
            else
            {
                MessageBox.Show("No records found to update.");
            }
            con.Close();
            BindData();

        }
    }
}
