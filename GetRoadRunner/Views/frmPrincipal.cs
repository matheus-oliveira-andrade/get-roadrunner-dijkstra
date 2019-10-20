using GetRoadRunner.Models.Graph;
using System;
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
                var adjacencyList = generate.BuildGraph(matrizPosicoes);

                var caminho = solve.Solver(adjacencyList);

                if (caminho == null) { MessageBox.Show("Tente novamente, caminho não gerado!", "Imprevistos!"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ocorreu um ERRO inesperado", MessageBoxButtons.OK);
            }
        }

        private void btnResetar_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            btnGerar.Select();
            btnGerar.Focus();

            richTxtBReport.AppendText("Bem Vindo!!");

        }
    }
}
