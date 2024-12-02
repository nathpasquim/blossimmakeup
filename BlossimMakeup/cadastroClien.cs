using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlossimMakeup
{
    public partial class cadastroClien : Form
    {
        // String de conexão (modifique conforme necessário)
        string connectionString = "server=127.0.0.1;database=blossommakeup;uid=root;pwd=;";
        public cadastroClien()
        {
            InitializeComponent();
        }

        private void btnCad_Click(object sender, EventArgs e)
        {
            // Capturar os valores dos campos
            string nome = txtNome.Text.Trim();
            string cpf = txtCpf.Text.Trim();
            string telefone = txtTel.Text.Trim();
            string email = txtEmail.Text.Trim();

            // Validar os campos
            if (string.IsNullOrWhiteSpace(nome) ||
                !IsValidCpf(cpf) ||
                !IsValidPhone(telefone) ||
                !IsValidEmail(email))
            {
                MessageBox.Show("Por favor, preencha todos os campos corretamente.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Inserir no banco de dados
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO cliente (nome, cpf, telefone, email) VALUES (@Nome, @Cpf, @Telefone, @Email)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nome", nome);
                        cmd.Parameters.AddWithValue("@Cpf", cpf);
                        cmd.Parameters.AddWithValue("@Telefone", telefone);
                        cmd.Parameters.AddWithValue("@Email", email);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Cliente cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimparCampos();
                        }
                        else
                        {
                            MessageBox.Show("Erro ao cadastrar cliente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        // Método para validar CPF
        private bool IsValidCpf(string cpf)
        {
            return Regex.IsMatch(cpf, @"^\d{11}$"); // Apenas 11 dígitos numéricos
        }

        // Método para validar Telefone
        private bool IsValidPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^\d{10,11}$"); // 10 ou 11 dígitos numéricos
        }

        // Método para validar Email
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Método para limpar os campos após cadastro
        private void LimparCampos()
        {
            txtNome.Clear();
            txtCpf.Clear();
            txtTel.Clear();
            txtEmail.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
