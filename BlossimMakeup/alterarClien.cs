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

namespace alterar
{
    public partial class alterarClien : Form
    {
        private string connectionString = "server=127.0.0.1;database=blossommakeup;uid=root;pwd=;";
        public alterarClien()
        {
            InitializeComponent();
            InitializeEventHandlers();
        }

       
 private void InitializeEventHandlers()
        {
            // Conecta o evento TextChanged ao textBoxID
            textBoxID.TextChanged += TextBoxID_TextChanged;
        }

        private void TextBoxID_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxID.Text, out var id) && id > 0)
            {
                LoadClienteData(id);
            }
            else
            {
                // Limpa os campos caso o ID não seja válido
                textBoxNome.Clear();
                textBoxCPF.Clear();
                textBoxTelefone.Clear();
                textBoxEmail.Clear();
            }
        }
        private void LoadClienteData(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    var command = new MySqlCommand(
                        "SELECT id, nome, cpf, telefone, email FROM cliente WHERE id = @id",
                        connection);
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBoxNome.Text = reader["nome"].ToString();
                            textBoxCPF.Text = reader["cpf"].ToString();
                            textBoxTelefone.Text = reader["telefone"].ToString();
                            textBoxEmail.Text = reader["email"].ToString();
                        }
                        else
                        {
                            // Limpa os campos caso o cliente não seja encontrado
                            textBoxNome.Clear();
                            textBoxCPF.Clear();
                            textBoxTelefone.Clear();
                            textBoxEmail.Clear();
                            MessageBox.Show("Cliente não encontrado.");
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
                if (string.IsNullOrWhiteSpace(textBoxNome.Text) ||
                    string.IsNullOrWhiteSpace(textBoxCPF.Text) ||
                    string.IsNullOrWhiteSpace(textBoxTelefone.Text) ||
                    string.IsNullOrWhiteSpace(textBoxEmail.Text))
                {
                    MessageBox.Show("Por favor, preencha todos os campos.");
                    return;
                }

                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    var command = new MySqlCommand(
                        "UPDATE cliente SET nome = @nome, cpf = @cpf, telefone = @telefone, email = @email WHERE id = @id",
                        connection);

                    command.Parameters.AddWithValue("@id", textBoxID.Text);
                    command.Parameters.AddWithValue("@nome", textBoxNome.Text);
                    command.Parameters.AddWithValue("@cpf", textBoxCPF.Text);
                    command.Parameters.AddWithValue("@telefone", textBoxTelefone.Text);
                    command.Parameters.AddWithValue("@email", textBoxEmail.Text);

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
