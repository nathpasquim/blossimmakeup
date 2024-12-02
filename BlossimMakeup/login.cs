using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlossimMakeup
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            // 1. Obtenha os valores dos campos
            string email = txtEmail.Text.Trim();
            string senha = txtSenha.Text.Trim();

            // 2. Valide se os campos estão preenchidos
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Por favor, preencha todos os campos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. String de conexão com o banco de dados
            string connectionString = "Server=127.0.0.1;Database=blossommakeup;Uid=root;Pwd=;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // 4. Query para verificar email e senha
                    string query = "SELECT tipoUsuario FROM usuarios WHERE email = @Email AND senha = @Senha";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Senha", senha);

                        // Executa a consulta
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            // 5. Obtém o tipo de usuário (Admin ou Funcionario)
                            string tipoUsuario = result.ToString();

                            // 6. Redireciona para o formulário correspondente
                            if (tipoUsuario == "Admin")
                            {
                                MessageBox.Show("Bem-vindo, Administrador!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Forml adminForm = new Forml();
                                adminForm.Show();
                            }
                            else if (tipoUsuario == "Funcionario")
                            {
                                MessageBox.Show("Bem-vindo, Funcionário!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                menuFunc funcionarioForm = new menuFunc();
                                funcionarioForm.Show();
                            }

                            // Fecha o formulário de login
                            this.Hide();
                        }
                        else
                        {
                            // Credenciais inválidas
                            MessageBox.Show("Email ou senha incorretos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Exibe erros se houver falha na conexão
                MessageBox.Show($"Erro ao conectar ao banco de dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Abre a página de login
            this.Hide();  // Oculta a página de cadastro
            cadastro cadForm = new cadastro();  // Substitua "LoginForm" pelo nome da sua página de login
            cadForm.Show();
        }
    }
}

