using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kurs
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;

        public Form1()
        {
            InitializeComponent();

        }


        private async void Form1_Load(object sender, EventArgs e)
        {
            Search();
        }

        private System.Collections.Generic.List<SqlParameter> SqlParams ;


        ///<summary> conditions and sql parameters</summary>
        private string GetQuery()
        {
            string q = null;
            SqlParams = new System.Collections.Generic.List<SqlParameter>();

            if (txtAuthor.Text.Length > 0)
            {
                q += " And Author like @a +'%'";
                SqlParams.Add(new SqlParameter("a", txtAuthor.Text));
            }
            if (txtSearchName.Text.Length > 0)
            {
                q += " And Name like @name +'%'";
                SqlParams.Add(new SqlParameter("name", txtSearchName.Text));
            }
            if (datSearchStart.Checked)
            {
                q += " And Date>@d";
                SqlParams.Add(new SqlParameter("d", datSearchStart.Value));
            }
            if (q != null) q =" Where "+ q.Substring(4);
            return q;
        }

        private async Task Search()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Dropbox\Курсовая по С#\Kurs\Kurs\Database.mdf;Integrated Security=True";
            //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Dropbox\Курсовая по С#\Kurs\Kurs\Database.mdf;Integrated Security=True";
            using (var sqlConnection = new SqlConnection(connectionString))
            {

                
                await sqlConnection.OpenAsync();
                var sql = "SELECT * FROM [SchoolLibary] " +GetQuery();
                
                using (SqlCommand command = new SqlCommand(sql, sqlConnection)) //Вызываем всё из базы данных
                {
                    command.Parameters.AddRange(SqlParams.ToArray());

                    try
                    {
                        using(var da=new SqlDataAdapter(command))
                        {
                            
                            DataTable dt=new DataTable();
                            da.Fill(dt);

                            dg.DataSource = dt;
                        }
                       
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text)
                && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) && !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text)
                && !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text) && !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text)
                && !string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [SchoolLibary] (Name, Author, Publication, Year, Price, Date, Condition)VALUES(@Name, @Author, @Publication, @Year, @Price, @Date, @Condition)", sqlConnection);

                command.Parameters.AddWithValue("Name", textBox1.Text);
                textBox1.Clear();
                command.Parameters.AddWithValue("Author", textBox2.Text);
                textBox2.Clear();
                command.Parameters.AddWithValue("Publication", textBox4.Text);
                textBox4.Clear();
                command.Parameters.AddWithValue("Year", textBox3.Text);
                textBox3.Clear();
                command.Parameters.AddWithValue("Price", textBox5.Text);
                textBox5.Clear();
                command.Parameters.AddWithValue("Date", textBox7.Text);
                textBox7.Clear();
                command.Parameters.AddWithValue("Condition", textBox6.Text);
                textBox6.Clear();
                await command.ExecuteNonQueryAsync();
                MessageBox.Show("Данные внесены", "Данные внесены", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await command.ExecuteNonQueryAsync();

            }
            else
            {
                MessageBox.Show("Заполните все данные", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async void button2_Click(object sender, EventArgs e)
        {

            /*------*/
            if (!string.IsNullOrEmpty(textBox16.Text) && !string.IsNullOrWhiteSpace(textBox16.Text) && !string.IsNullOrEmpty(textBox14.Text) && !string.IsNullOrWhiteSpace(textBox14.Text) && !string.IsNullOrEmpty(textBox13.Text) && !string.IsNullOrWhiteSpace(textBox13.Text)
                && !string.IsNullOrEmpty(textBox12.Text) && !string.IsNullOrWhiteSpace(textBox12.Text) && !string.IsNullOrEmpty(textBox11.Text) && !string.IsNullOrWhiteSpace(textBox11.Text)
                && !string.IsNullOrEmpty(textBox10.Text) && !string.IsNullOrWhiteSpace(textBox10.Text) && !string.IsNullOrEmpty(textBox9.Text) && !string.IsNullOrWhiteSpace(textBox9.Text)
                && !string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [SchoolLibary] SET [Name] = @Name, [Author] = @Author, [Publication] = @Publication, [Year] = @Year, [Price] = @Price, [Date] = @Date, [Condition] = @Condition WHERE  [Id] = @Id", sqlConnection);
                command.Parameters.AddWithValue("Id", textBox16.Text);
                textBox16.Clear();
                command.Parameters.AddWithValue("Name", textBox14.Text);
                textBox14.Clear();
                command.Parameters.AddWithValue("Author", textBox13.Text);
                textBox13.Clear();
                command.Parameters.AddWithValue("Publication", textBox11.Text);
                textBox11.Clear();
                try
                {
                    command.Parameters.AddWithValue("Year", textBox12.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка ввода ГОДА", "ERORR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                textBox12.Clear();
                command.Parameters.AddWithValue("Price", textBox10.Text);
                textBox10.Clear();
                try
                {
                    command.Parameters.AddWithValue("Date", textBox8.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка ввода ДАТЫ", "ERORR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                textBox8.Clear();
                command.Parameters.AddWithValue("Condition", textBox9.Text);
                textBox9.Clear();
                await command.ExecuteNonQueryAsync();

                MessageBox.Show("Данные изменены", "Данные изменены", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            else
            {
                MessageBox.Show("Заполните все данные", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private async void f5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }


        private async void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox17.Text) && !string.IsNullOrWhiteSpace(textBox17.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [SchoolLibary] WHERE [Id] = @Id", sqlConnection);

                MessageBox.Show("Данные были удалены", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                command.Parameters.AddWithValue("Id", textBox17.Text);
                textBox17.Clear();
                await command.ExecuteNonQueryAsync();
            }

            else
            {
                MessageBox.Show("Заполните все данные", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAuthor.Text) && !string.IsNullOrWhiteSpace(txtAuthor.Text))
            {

            }

            else
            {
                MessageBox.Show("Заполните все данные", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearchName.Text) && !string.IsNullOrWhiteSpace(txtSearchName.Text))
            {

            }

            else
            {
                MessageBox.Show("Заполните все данные", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        /*---------------------//////////////////////////////////////////////////*/


        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void listBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

      
        private async void textBox18_TextChanged(object sender, EventArgs e)
        {
            //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Dropbox\Курсовая по С#\Kurs\Kurs\Database.mdf;Integrated Security=True";
            ////string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Dropbox\Курсовая по С#\Kurs\Kurs\Database.mdf;Integrated Security=True";
            //sqlConnection = new SqlConnection(connectionString);
            //await sqlConnection.OpenAsync();
            await Search();
            // Entity framework - метонит.

        }

        private void datSearchStart_ValueChanged(object sender, EventArgs e)
        {
            Search();
        }
    }
}


