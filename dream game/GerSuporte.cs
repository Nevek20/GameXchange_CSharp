using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace dream_game
{
    public partial class GerSuporte : Form
    {
        public GerSuporte()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Load += GerSuporte_Load;

            buttonVisualizar.Click += buttonVisualizar_Click;
        }

        private void GerSuporte_Load(object sender, EventArgs e)
        {
            CarregarChamados();
            AjustarCores();
        }

        private void CarregarChamados()
        {
            string conexaoString = "Server=localhost; Port=3306; Database=bd_gamexchange; Uid=root; Pwd=;";
            string query = "SELECT id_suporte, titulo, gravidade FROM tb_suporte ORDER BY id_suporte DESC";

            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                DataTable tabela = new DataTable();
                adapter.Fill(tabela);
                dataGridViewDescricao.DataSource = tabela;
            }
        }

        private void buttonVisualizar_Click(object sender, EventArgs e)
        {
            if (dataGridViewDescricao.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma reclamação para visualizar.");
                return;
            }

            int idSuporte = Convert.ToInt32(dataGridViewDescricao.SelectedRows[0].Cells["id_suporte"].Value);
            string conexaoString = "Server=localhost; Port=3306; Database=bd_gamexchange; Uid=root; Pwd=;";
            string query = "SELECT descricao FROM tb_suporte WHERE id_suporte = @Id";

            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                comando.Parameters.AddWithValue("@Id", idSuporte);
                conexao.Open();

                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.Read())
                        richTextBox1.Text = reader["descricao"].ToString();
                    else
                        richTextBox1.Text = "[Reclamação não encontrada]";
                }
            }
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
                else if (ctrl is RichTextBox rtb)
                {
                    rtb.BackColor = Color.White;
                    rtb.ForeColor = Color.Black;
                }
            }
        }

        private void buttonSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
