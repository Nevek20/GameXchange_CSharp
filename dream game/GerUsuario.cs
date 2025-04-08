using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace dream_game
{
    public partial class GerUsuario : Form
    {
        public GerUsuario()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Load += GerUsuario_Load;
            buttonPesquisar.Click += buttonPesquisar_Click;
            buttonExcluir.Click += buttonExcluir_Click;
        }

        private void GerUsuario_Load(object sender, EventArgs e)
        {
            CarregarUsuarios();
            AjustarCores();
        }

        private void CarregarUsuarios(string filtro = "")
        {
            string conexaoString = "Server=localhost; Port=3306; Database=bd_gamexchange; Uid=root; Pwd=;";
            string query = "SELECT id_usuario, email, nome_perfil, nome_real, data_nascimento, qtd_jogos, tipo FROM tb_usuario";

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                query += " WHERE nome_perfil LIKE @Filtro OR nome_real LIKE @Filtro";
            }

            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                if (!string.IsNullOrWhiteSpace(filtro))
                    comando.Parameters.AddWithValue("@Filtro", "%" + filtro + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                DataTable tabela = new DataTable();

                adapter.Fill(tabela);
                dataGridViewUsuarios.DataSource = tabela;
            }
        }

        private void buttonPesquisar_Click(object sender, EventArgs e)
        {
            string filtro = textBoxPesquisa.Text.Trim();
            CarregarUsuarios(filtro);
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um usuário para excluir.");
                return;
            }

            int idUsuario = Convert.ToInt32(dataGridViewUsuarios.SelectedRows[0].Cells["id_usuario"].Value);

            DialogResult confirm = MessageBox.Show("Tem certeza que deseja excluir este usuário?", "Confirmação", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            string conexaoString = "Server=localhost; Port=3306; Database=bd_gamexchange; Uid=root; Pwd=;";
            string query = "DELETE FROM tb_usuario WHERE id_usuario = @Id";

            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                comando.Parameters.AddWithValue("@Id", idUsuario);
                conexao.Open();
                comando.ExecuteNonQuery();
            }

            CarregarUsuarios();
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
