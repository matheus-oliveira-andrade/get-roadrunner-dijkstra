using GetRoadRunner.Models.Graph;
using GetRoadRunner.Views;
using System;
using System.Threading;
using System.Windows.Forms;

namespace GetRoadRunner
{
    public partial class FrmPrincipal : Form
    {
        private Controllers.Generate generate;
        private Controllers.Solve solve;
        private Controllers.Utils utils;

        private Vertice[,] matrizPosicoes;


        public FrmPrincipal()
        {
            InitializeComponent();

            generate = new Controllers.Generate();
            solve = new Controllers.Solve();
            utils = new Controllers.Utils();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            try
            {
                richTxtBReport.AppendText("\nGerando o tabuleiro...");

                matrizPosicoes = generate.GenerateBoard();
                utils.MostrarTabuleiro(matrizPosicoes, dataGridView1);

                btnResolver.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ocorreu um ERRO inesperado", MessageBoxButtons.OK);
            }
        }

        private void btnResolver_Click(object sender, EventArgs e)
        {
            try
            {
                richTxtBReport.AppendText("\nProcurando o menor caminho até o papaléguas...");
                var adjacencyList = generate.BuildGraph(matrizPosicoes);

                var caminho = solve.Solver(adjacencyList);

                if (caminho == null) { MessageBox.Show("Tente novamente, caminho não gerado!", "Imprevistos!"); }
                else
                {
                    richTxtBReport.AppendText("\nEncontrei um caminho...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ocorreu um ERRO inesperado", MessageBoxButtons.OK);
            }
        }

        private void btnResetar_Click(object sender, EventArgs e)
        {
            richTxtBReport.AppendText("\nRecomeçando em 3... 2... 1...");

            Thread.Sleep(3000);

            Application.Restart();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            btnGerar.Select();
            btnGerar.Focus();

            richTxtBReport.AppendText("Bem Vindo!!");

        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new FrmSobre();
            frm.StartPosition = FormStartPosition.CenterScreen;            

            frm.ShowDialog();
        }
    }
}
