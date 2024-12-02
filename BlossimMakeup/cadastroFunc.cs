using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace BlossimMakeup
{
    public partial class cadastroFunc : Form
    {
        // String de conexão (modifique conforme necessário)
        string connectionString = "server=127.0.0.1;database=blossommakeup;uid=root;pwd=;";
        public cadastroFunc()
        {
            InitializeComponent();
           
        }
    
        private void buttonCad_Click(object sender, EventArgs e)
        {

            // Validação dos campos
            if (string.IsNullOrWhiteSpace(txtBoxNome.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox2.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox4.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox5.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox6.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox1.Text))
            {
                MessageBox.Show("Todos os campos devem ser preenchidos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validações específicas
            if (!Regex.IsMatch(guna2TextBox2.Text, @"^\d{11}$"))

            {
                MessageBox.Show("CPF inválido! Certifique-se de digitar exatamente 11 números.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Regex.IsMatch(guna2TextBox5.Text, @"^\d{8}$"))
            {
                MessageBox.Show("CEP inválido! Certifique-se de digitar exatamente 8 números.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Regex.IsMatch(guna2TextBox1.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("E-mail inválido! Certifique-se de digitar um e-mail válido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Cadastro no banco de dados
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO funcionario 
                        (nome, cpf, salario, cep, numeroCasa, email) 
                        VALUES (@nome, @cpf, @salario, @cep, @numeroCasa, @email)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nome", txtBoxNome.Text);
                    cmd.Parameters.AddWithValue("@cpf", guna2TextBox2.Text);
                    cmd.Parameters.AddWithValue("@salario", guna2TextBox4.Text);
                    cmd.Parameters.AddWithValue("@cep", guna2TextBox5.Text);
                    cmd.Parameters.AddWithValue("@numeroCasa", guna2TextBox6.Text);
                    cmd.Parameters.AddWithValue("@email", guna2TextBox1.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Funcionário cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpar os campos após o cadastro
                    txtBoxNome.Clear();
                    guna2TextBox2.Clear();
                    guna2TextBox4.Clear();
                    guna2TextBox5.Clear();
                    guna2TextBox6.Clear();
                    guna2TextBox1.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao cadastrar funcionário: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void cadastroFunc_Load(object sender, EventArgs e)
        {

        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtPreco_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
   
}
