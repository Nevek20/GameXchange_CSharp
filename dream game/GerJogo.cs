using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace dream_game
{
    public partial class GerJogo : Form
    {
        public GerJogo()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            buttonPesquisar.Click += buttonPesquisar_Click;
            buttonExcluir.Click += buttonExcluir_Click;
            buttonEditar.Click += buttonEditar_Click;
            buttonSalvar.Click += buttonSalvar_Click;

            dataGridViewJogos.ReadOnly = false;
            dataGridViewJogos.AllowUserToAddRows = false;
            dataGridViewJogos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private string conexaoString = "Server=82.180.153.103; Port=3306; Database=u531683190_bd_gamexchange; Uid=u531683190_ryan; Pwd=RyanGuida123;";

        private void GerJogo_Load(object sender, EventArgs e)
        {
            labelInfo.Text = "";
            CarregarTodosJogos();
        }

        private void CarregarTodosJogos()
        {
            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            {
                try
                {
                    conexao.Open();
                    string query = @"
                        SELECT 
                            id_jogos, nome, foto0, foto1, foto2, foto3, foto4,
                            classificacao_indicativa, nota, descricao,
                            preco, data_lancamento
                        FROM tb_jogos";

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexao))
                    {
                        DataTable tabela = new DataTable();
                        adapter.Fill(tabela);
                        dataGridViewJogos.DataSource = tabela;

                        // libera somente campos editáveis
                        foreach (DataGridViewColumn col in dataGridViewJogos.Columns)
                        {
                            col.ReadOnly = !(col.Name == "nome" || col.Name == "nota" || col.Name == "preco" || col.Name == "descricao");
                        }

                        labelInfo.Text = $"Total: {tabela.Rows.Count} jogo(s).";
                        labelInfo.ForeColor = Color.LightGreen;
                    }
                }
                catch (Exception ex)
                {
                    labelInfo.Text = "Erro ao carregar jogos: " + ex.Message;
                    labelInfo.ForeColor = Color.Salmon;
                }
            }
        }

        private void buttonPesquisar_Click(object sender, EventArgs e)
        {
            string termo = textBoxPesquisar.Text.Trim();

            if (string.IsNullOrEmpty(termo))
            {
                CarregarTodosJogos();
                return;
            }

            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            {
                try
                {
                    conexao.Open();
                    string query = @"
                        SELECT 
                            id_jogos, nome, foto0, foto1, foto2, foto3, foto4,
                            classificacao_indicativa, nota, descricao,
                            preco, data_lancamento
                        FROM tb_jogos
                        WHERE nome LIKE @Termo";

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexao))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@Termo", "%" + termo + "%");

                        DataTable tabela = new DataTable();
                        adapter.Fill(tabela);
                        dataGridViewJogos.DataSource = tabela;

                        labelInfo.Text = tabela.Rows.Count == 0
                            ? "Nenhum jogo encontrado."
                            : $"Encontrado(s): {tabela.Rows.Count} jogo(s).";
                        labelInfo.ForeColor = tabela.Rows.Count == 0 ? Color.OrangeRed : Color.LightGreen;
                    }
                }
                catch (Exception ex)
                {
                    labelInfo.Text = "Erro ao buscar: " + ex.Message;
                    labelInfo.ForeColor = Color.Salmon;
                }
            }
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            if (dataGridViewJogos.SelectedRows.Count == 0)
            {
                labelInfo.Text = "Selecione um jogo para excluir.";
                labelInfo.ForeColor = Color.Salmon;
                return;
            }

            int idJogo = Convert.ToInt32(dataGridViewJogos.SelectedRows[0].Cells["id_jogos"].Value);

            DialogResult confirm = MessageBox.Show("Deseja realmente excluir este jogo?", "Confirmar Exclusão", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                using (MySqlConnection conexao = new MySqlConnection(conexaoString))
                {
                    try
                    {
                        conexao.Open();
                        string deleteQuery = "DELETE FROM tb_jogos WHERE id_jogos = @Id";

                        using (MySqlCommand comando = new MySqlCommand(deleteQuery, conexao))
                        {
                            comando.Parameters.AddWithValue("@Id", idJogo);
                            comando.ExecuteNonQuery();

                            labelInfo.Text = "Jogo excluído com sucesso.";
                            labelInfo.ForeColor = Color.LightGreen;

                            CarregarTodosJogos();
                        }
                    }
                    catch (Exception ex)
                    {
                        labelInfo.Text = "Erro ao excluir: " + ex.Message;
                        labelInfo.ForeColor = Color.Salmon;
                    }
                }
            }
        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewJogos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um jogo para editar.");
                return;
            }

            dataGridViewJogos.BeginEdit(true);
            MessageBox.Show("Você pode editar os campos permitidos: nome, nota, preco e descrição.\nClique em 'Salvar' para aplicar.");
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            if (dataGridViewJogos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um jogo para salvar.");
                return;
            }

            DataGridViewRow row = dataGridViewJogos.SelectedRows[0];

            int idJogo = Convert.ToInt32(row.Cells["id_jogos"].Value);
            string nome = row.Cells["nome"].Value?.ToString();
            string descricao = row.Cells["descricao"].Value?.ToString();
            decimal preco;
            float nota;

            if (!decimal.TryParse(row.Cells["preco"].Value?.ToString(), out preco))
            {
                MessageBox.Show("Preço inválido.");
                return;
            }

            if (!float.TryParse(row.Cells["nota"].Value?.ToString(), out nota))
            {
                MessageBox.Show("Nota inválida.");
                return;
            }

            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            {
                string query = @"
                    UPDATE tb_jogos 
                    SET nome = @Nome, preco = @Preco, nota = @Nota, descricao = @Descricao
                    WHERE id_jogos = @Id";

                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@Nome", nome);
                    comando.Parameters.AddWithValue("@Preco", preco);
                    comando.Parameters.AddWithValue("@Nota", nota);
                    comando.Parameters.AddWithValue("@Descricao", descricao);
                    comando.Parameters.AddWithValue("@Id", idJogo);

                    try
                    {
                        conexao.Open();
                        comando.ExecuteNonQuery();
                        MessageBox.Show("Jogo atualizado com sucesso.");
                        CarregarTodosJogos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao salvar: " + ex.Message);
                    }
                }
            }
        }

        private void buttonSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
