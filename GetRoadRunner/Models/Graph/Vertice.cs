using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetRoadRunner.Models.Graph
{
    public class Vertice
    {
        public int Peso { get; set; }
        public int Nome { get; set; }
        //public int Custo { get; set; }
        public bool Aberto { get; set; } = true;
        public Vertice Predecessor { get; set; }
        public int Estimativa { get; set; }

        public string Visualizacao { get; set; }

        public int Linha { get; set; }
        public int Coluna { get; set; }
        public int NumeroColunas { get; set; }


        public Vertice()
        {

        }
        public Vertice(int nome, int peso, int linha, int coluna, int numeroColunas, string visualizacao)
        {
            Peso = peso;
            Nome = nome;
            Linha = linha;
            Coluna = coluna;
            NumeroColunas = numeroColunas;
            Visualizacao = visualizacao;
        }
    }
}

