using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace alterar
{
    public partial class alterarProd : Form
    {
        private string connectionString = "server=127.0.0.1;database=blossommakeup;uid=root;pwd=;";

        public alterarProd()
        {
            InitializeComponent();
            InitializeEventHandlers();
            LoadComboBoxes();
        }

        private void InitializeEventHandlers()
        {
            // Conecta o evento TextChanged ao textBoxCodigo
            textBoxCodigo.TextChanged += textBoxCodigo_TextChanged;
        }

        private void LoadComboBoxes()
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Carregar Tipos
                    var tipoCommand = new MySqlCommand("SELECT id, nome FROM tipo", connection);
                    var tipoTable = new DataTable();
                    tipoTable.Load(tipoCommand.ExecuteReader());

                    comboBoxTipo.DataSource = tipoTable;
                    comboBoxTipo.DisplayMember = "nome"; // Campo exibido
                    comboBoxTipo.ValueMember = "id";    // Valor associado

                    // Carregar Marcas
                    var marcaCommand = new MySqlCommand("SELECT id, nome FROM marca", connection);
                    var marcaTable = new DataTable();
                    marcaTable.Load(marcaCommand.ExecuteReader());

                    comboBoxMarca.DataSource = marcaTable;
                    comboBoxMarca.DisplayMember = "nome"; // Campo exibido
                    comboBoxMarca.ValueMember = "id";    // Valor associado
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar os ComboBoxes: {ex.Message}");
            }
        }
        private void LoadProdutoData(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    var command = new MySqlCommand(
                        "SELECT id, nome, preco, quantidade, FK_tipo_id, FK_marca_id FROM produto WHERE id = @id",
                        connection);
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            TextBoxNome.Text = reader["nome"].ToString();
                            TextBoxPreco.Text = reader["preco"].ToString();
                            TextBoxQuantidade.Text = reader["quantidade"].ToString();

                            // Atualiza os valores dos ComboBoxes
                            comboBoxTipo.SelectedValue = reader["FK_tipo_id"];
                            comboBoxMarca.SelectedValue = reader["FK_marca_id"];
                        }
                        else
                        {
                            // Limpa os campos caso o produto não seja encontrado
                            TextBoxNome.Clear();
                            TextBoxPreco.Clear();
                            TextBoxQuantidade.Clear();
                            comboBoxTipo.SelectedIndex = -1;
                            comboBoxMarca.SelectedIndex = -1;
                            MessageBox.Show("Produto não encontrado.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar os dados do produto: {ex.Message}");
            }
        }









        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validação básica antes de alterar
                if (string.IsNullOrWhiteSpace(TextBoxNome.Text) ||
                    !float.TryParse(TextBoxPreco.Text, out var preco) ||
                    !int.TryParse(TextBoxQuantidade.Text, out var quantidade))
                {
                    MessageBox.Show("Por favor, preencha todos os campos corretamente.");
                    return;
                }

                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    var command = new MySqlCommand(
                        "UPDATE produto SET nome = @nome, preco = @preco, quantidade = @quantidade, " +
                        "FK_tipo_id = @tipo, FK_marca_id = @marca WHERE id = @id",
                        connection);

                    command.Parameters.AddWithValue("@id", textBoxCodigo.Text);
                    command.Parameters.AddWithValue("@nome", TextBoxNome.Text);
                    command.Parameters.AddWithValue("@preco", preco);
                    command.Parameters.AddWithValue("@quantidade", quantidade);
                    command.Parameters.AddWithValue("@tipo", comboBoxTipo.SelectedValue);
                    command.Parameters.AddWithValue("@marca", comboBoxMarca.SelectedValue);

                    var rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Produto atualizado com sucesso.");
                    }
                    else
                    {
                        MessageBox.Show("Nenhum registro foi alterado.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao alterar o produto: {ex.Message}");
            }
        }

        private void textBoxCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
        }
    }
}
