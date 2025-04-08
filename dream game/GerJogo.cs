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

            this.buttonPesquisar.Click += buttonPesquisar_Click;
            this.buttonExcluir.Click += buttonExcluir_Click;
        }

        private string conexaoString = "Server=localhost; Port=3306; Database=bd_gamexchange; Uid=root; Pwd=;";

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

                        if (tabela.Rows.Count == 0)
                        {
                            labelInfo.Text = "Nenhum jogo encontrado.";
                            labelInfo.ForeColor = Color.OrangeRed;
                        }
                        else
                        {
                            labelInfo.Text = $"Encontrado(s): {tabela.Rows.Count} jogo(s).";
                            labelInfo.ForeColor = Color.LightGreen;
                        }
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

            DataGridViewRow linha = dataGridViewJogos.SelectedRows[0];
            int idJogo = Convert.ToInt32(linha.Cells["id_jogos"].Value);

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

        private void buttonSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
