using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace alterar
{
    
    public partial class alterarFunc : Form
    {
        // A variável connectionString deve estar aqui, dentro da classe
        private string connectionString = "server=127.0.0.1;database=blossommakeup;uid=root;pwd=;";
        public alterarFunc()
        {
            InitializeComponent();
            InitializeEventHandlers();
        }
        private void InitializeEventHandlers()
        {
            // Conecta o evento TextChanged ao textBoxID
            txtId.TextChanged += txtId_TextChanged;
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out var id) && id > 0)
            {
                LoadClienteData(id);
            }
            else
            {
                // Limpa os campos caso o ID não seja válido
                txtNome.Clear();
                txtCPF.Clear();
                txtNumCasa.Clear();
                txtEmail.Clear();
            }
        }
        private void LoadClienteData(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new MySqlCommand("SELECT nome, cpf, cep, numeroCasa, email FROM funcionario WHERE id = @id", connection);
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Preencher os campos de texto com os dados do banco
                            txtNome.Text = reader["nome"].ToString();
                            txtCPF.Text = reader["cpf"].ToString();
                            txtNumCasa.Text = reader["numeroCasa"].ToString();
                            txtEmail.Text = reader["email"].ToString();
                            // Supondo que o campo "cep" também seja retornado
                            txtCEP.Text = reader["cep"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Nenhum cliente encontrado com esse ID.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar os dados do cliente: {ex.Message}");
            }
        }

        private void buttonAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validação básica antes de alterar
                if (string.IsNullOrWhiteSpace(txtNome.Text) ||
                    string.IsNullOrWhiteSpace(txtCPF.Text) ||
                    string.IsNullOrWhiteSpace(txtNumCasa.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Por favor, preencha todos os campos.");
                    return;
                }

                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    var command = new MySqlCommand(
                        "UPDATE funcionario SET nome = @nome, cpf = @cpf, cep = @cep, numeroCasa = @numeroCasa, email = @email WHERE id = @id",
                        connection);

                    // Certifique-se de que o ID seja um número válido.
                    if (int.TryParse(txtId.Text, out var id))
                    {
                        command.Parameters.AddWithValue("@id", id);
                    }
                    else
                    {
                        MessageBox.Show("ID inválido.");
                        return;
                    }

                    command.Parameters.AddWithValue("@nome", txtNome.Text);
                    command.Parameters.AddWithValue("@cpf", txtCPF.Text);
                    command.Parameters.AddWithValue("@cep", txtCEP.Text);  // Corrija se necessário.
                    command.Parameters.AddWithValue("@numeroCasa", txtNumCasa.Text);
                    command.Parameters.AddWithValue("@email", txtEmail.Text);

                    var rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cliente atualizado com sucesso.");
                    }
                    else
                    {
                        MessageBox.Show("Nenhum registro foi alterado.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao alterar o cliente: {ex.Message}");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

