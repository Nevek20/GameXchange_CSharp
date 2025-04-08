using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace dream_game
{
    public partial class GerCompra : Form
    {
        public GerCompra()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Load += GerCompra_Load;
            buttonPesquisar.Click += buttonPesquisar_Click;
            buttonExcluir.Click += buttonExcluir_Click;
        }

        private void GerCompra_Load(object sender, EventArgs e)
        {
            CarregarCompras();
            AjustarCores();
        }

        private void CarregarCompras(string filtro = "")
        {
            string conexaoString = "Server=localhost; Port=3306; Database=bd_gamexchange; Uid=root; Pwd=;";
            string query = @"
                SELECT c.id, u.nome_real AS usuario, j.nome AS jogo, c.chave_ativacao, c.data_compra
                FROM tb_compras c
                JOIN tb_usuario u ON u.id_usuario = c.id_usuario
                JOIN tb_jogos j ON j.id_jogos = c.id_jogos";

            if (!string.IsNullOrWhiteSpace(filtro))
                query += " WHERE u.nome_real LIKE @Filtro OR j.nome LIKE @Filtro";

            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                if (!string.IsNullOrWhiteSpace(filtro))
                    comando.Parameters.AddWithValue("@Filtro", "%" + filtro + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                DataTable tabela = new DataTable();
                adapter.Fill(tabela);
                dataGridViewCompras.DataSource = tabela;
            }
        }

        private void buttonPesquisar_Click(object sender, EventArgs e)
        {
            string filtro = textBoxPesquisa.Text.Trim();
            CarregarCompras(filtro);
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            if (dataGridViewCompras.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma compra para excluir.");
                return;
            }

            int idCompra = Convert.ToInt32(dataGridViewCompras.SelectedRows[0].Cells["id"].Value);

            DialogResult confirm = MessageBox.Show("Tem certeza que deseja excluir esta compra?", "Confirmação", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            string conexaoString = "Server=localhost; Port=3306; Database=bd_gamexchange; Uid=root; Pwd=;";
            string query = "DELETE FROM tb_compras WHERE id = @Id";

            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                comando.Parameters.AddWithValue("@Id", idCompra);
                conexao.Open();
                comando.ExecuteNonQuery();
            }

            CarregarCompras();
        }

        private void AjustarCores()
        {
            this.BackColor = Color.FromArgb(64, 0, 64);
            foreach (Control ctrl in this.Controls)
            {
                ctrl.ForeColor = Color.White;

                if (ctrl is Button btn)
                {
                    btn.BackColor = Color.DarkSlateBlue;
                    btn.ForeColor = Color.White;
                }
                else if (ctrl is TextBox tb)
                {
                    tb.BackColor = Color.White;
                    tb.ForeColor = Color.Black;
                }
                else if (ctrl is DataGridView dgv)
                {
                    dgv.BackgroundColor = Color.White;
                    dgv.DefaultCellStyle.BackColor = Color.White;
                    dgv.DefaultCellStyle.ForeColor = Color.Black;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkSlateBlue;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dgv.EnableHeadersVisualStyles = false;
                }
            }
        }

        private void buttonSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
