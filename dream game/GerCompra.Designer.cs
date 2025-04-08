namespace dream_game
{
    partial class GerCompra
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button buttonExcluir;
        private System.Windows.Forms.DataGridView dataGridViewCompras;
        private System.Windows.Forms.Button buttonPesquisar;
        private System.Windows.Forms.TextBox textBoxPesquisa;
        private System.Windows.Forms.Button buttonSair;
        private System.Windows.Forms.Button buttonEditar;
        private System.Windows.Forms.Button buttonSalvar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.labelInfo = new System.Windows.Forms.Label();
            this.buttonExcluir = new System.Windows.Forms.Button();
            this.dataGridViewCompras = new System.Windows.Forms.DataGridView();
            this.buttonPesquisar = new System.Windows.Forms.Button();
            this.textBoxPesquisa = new System.Windows.Forms.TextBox();
            this.buttonSair = new System.Windows.Forms.Button();
            this.buttonEditar = new System.Windows.Forms.Button();
            this.buttonSalvar = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCompras)).BeginInit();
            this.SuspendLayout();

            // labelInfo
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.labelInfo.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.labelInfo.Location = new System.Drawing.Point(12, 435);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(0, 13);
            this.labelInfo.TabIndex = 14;

            // buttonExcluir
            this.buttonExcluir.BackColor = System.Drawing.Color.White;
            this.buttonExcluir.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.buttonExcluir.Location = new System.Drawing.Point(112, 400);
            this.buttonExcluir.Name = "buttonExcluir";
            this.buttonExcluir.Size = new System.Drawing.Size(82, 25);
            this.buttonExcluir.TabIndex = 1;
            this.buttonExcluir.Text = "Excluir";
            this.buttonExcluir.UseVisualStyleBackColor = false;

            // dataGridViewCompras
            this.dataGridViewCompras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCompras.Location = new System.Drawing.Point(12, 61);
            this.dataGridViewCompras.Name = "dataGridViewCompras";
            this.dataGridViewCompras.Size = new System.Drawing.Size(776, 325);
            this.dataGridViewCompras.TabIndex = 0;

            // buttonPesquisar
            this.buttonPesquisar.BackColor = System.Drawing.Color.White;
            this.buttonPesquisar.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.buttonPesquisar.Location = new System.Drawing.Point(12, 400);
            this.buttonPesquisar.Name = "buttonPesquisar";
            this.buttonPesquisar.Size = new System.Drawing.Size(82, 25);
            this.buttonPesquisar.TabIndex = 2;
            this.buttonPesquisar.Text = "Pesquisar";
            this.buttonPesquisar.UseVisualStyleBackColor = false;

            // textBoxPesquisa
            this.textBoxPesquisa.Location = new System.Drawing.Point(12, 25);
            this.textBoxPesquisa.Name = "textBoxPesquisa";
            this.textBoxPesquisa.Size = new System.Drawing.Size(400, 20);
            this.textBoxPesquisa.TabIndex = 3;

            // buttonSair
            this.buttonSair.BackColor = System.Drawing.Color.Red;
            this.buttonSair.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.buttonSair.ForeColor = System.Drawing.Color.White;
            this.buttonSair.Location = new System.Drawing.Point(706, 400);
            this.buttonSair.Name = "buttonSair";
            this.buttonSair.Size = new System.Drawing.Size(82, 25);
            this.buttonSair.TabIndex = 6;
            this.buttonSair.Text = "Sair";
            this.buttonSair.UseVisualStyleBackColor = false;
            this.buttonSair.Click += new System.EventHandler(this.buttonSair_Click);

            // buttonEditar
            this.buttonEditar.BackColor = System.Drawing.Color.White;
            this.buttonEditar.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.buttonEditar.Location = new System.Drawing.Point(210, 400);
            this.buttonEditar.Name = "buttonEditar";
            this.buttonEditar.Size = new System.Drawing.Size(82, 25);
            this.buttonEditar.TabIndex = 4;
            this.buttonEditar.Text = "Editar";
            this.buttonEditar.UseVisualStyleBackColor = false;

            // buttonSalvar
            this.buttonSalvar.BackColor = System.Drawing.Color.White;
            this.buttonSalvar.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.buttonSalvar.Location = new System.Drawing.Point(310, 400);
            this.buttonSalvar.Name = "buttonSalvar";
            this.buttonSalvar.Size = new System.Drawing.Size(82, 25);
            this.buttonSalvar.TabIndex = 5;
            this.buttonSalvar.Text = "Salvar";
            this.buttonSalvar.UseVisualStyleBackColor = false;

            // GerCompra
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(64, 0, 64);
            this.ClientSize = new System.Drawing.Size(800, 460);
            this.Controls.Add(this.buttonSalvar);
            this.Controls.Add(this.buttonEditar);
            this.Controls.Add(this.buttonSair);
            this.Controls.Add(this.textBoxPesquisa);
            this.Controls.Add(this.buttonPesquisar);
            this.Controls.Add(this.dataGridViewCompras);
            this.Controls.Add(this.buttonExcluir);
            this.Controls.Add(this.labelInfo);
            this.Name = "GerCompra";
            this.Text = "GameXchange - Gerenciamento de Compras";
            this.Load += new System.EventHandler(this.GerCompra_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCompras)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
