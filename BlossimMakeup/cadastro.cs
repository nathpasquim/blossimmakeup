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
    public partial class cadastro : Form
    {
        public cadastro()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCadastrar_Click_1(object sender, EventArgs e)
        {
            // 1. Obtenha os dados do formulário
            string email = txtEmail.Text.Trim();
            string senha = txtSenha.Text.Trim();
            string tipoUsuario = cmbTipoUsuario.SelectedItem?.ToString();

            // 2. Valide os campos obrigatórios
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha) || string.IsNullOrEmpty(tipoUsuario))
            {
                MessageBox.Show("Preencha todos os campos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. String de conexão com o banco de dados
            string connectionString = "Server=127.0.0.1;Database=blossommakeup;Uid=root;Pwd=;";

            try
            {
                // 4. Conexão com o banco de dados
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // 5. Query de INSERT na tabela `usuarios`
                    string query = "INSERT INTO usuarios (email, senha, tipoUsuario) VALUES (@Email, @Senha, @TipoUsuario)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Substitui os parâmetros com os valores do formulário
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Senha", senha); // **Sugestão:** Criptografar em produção.
                        cmd.Parameters.AddWithValue("@TipoUsuario", tipoUsuario);

                        // Executa o comando
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Usuário cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Limpa os campos
                        txtEmail.Clear();
                        txtSenha.Clear();
                        cmbTipoUsuario.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                // Exibe erros, se ocorrerem
                MessageBox.Show($"Erro ao cadastrar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Cria uma instância do formulário AdmLogin
            login login = new login();

            // Exibe o formulário AdmLogin
            login.Show();

            // (Opcional) Esconde o formulário atual, se desejar
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Abre a página de login
            this.Hide();  // Oculta a página de cadastro
            login loginForm = new login();  // Substitua "LoginForm" pelo nome da sua página de login
            loginForm.Show();  // Exibe a página de login
        }
    }
}
