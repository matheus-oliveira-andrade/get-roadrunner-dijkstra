using GetRoadRunner.Models.Graph;
using System;
using System.Collections.Generic;

namespace GetRoadRunner.Models
{
    public class BuildGraph
    {
        private Vertice[,] Matrix { get; set; }
        private int[,] MatrizControl { get; set; }
        private List<LinkedList<Vertice>> AdjacencyList { get; set; }

        public BuildGraph(Vertice[,] matriz)
        {
            AdjacencyList = new List<LinkedList<Vertice>>(matriz.GetLength(0) * matriz.GetLength(1));

            this.Matrix = matriz;
            this.MatrizControl = new int[matriz.GetLength(0), matriz.GetLength(1)];
            construirMatrizValorada();
        }

        private void construirMatrizValorada()
        {
            var rand = new Random();

            for (int linha = 0; linha < Matrix.GetLength(0); linha++)
            {
                for (int coluna = 0; coluna < Matrix.GetLength(1); coluna++)
                {
                    MatrizControl[linha, coluna] = rand.Next(1, 16);
                }
            }
        }

        public List<LinkedList<Vertice>> ToListAdjacency()
        {
            for (int linha = 0; linha < Matrix.GetLength(0); linha++)
            {
                for (int coluna = 0; coluna < Matrix.GetLength(1); coluna++)
                {
                    var lnkdPosicao = new LinkedList<Vertice>();
                    // Nome, Peso

                    //Acima
                    if (IsPossible(linha - 1, coluna))
                    {
                        lnkdPosicao.AddLast(Matrix[linha - 1, coluna]);
                    }
                    //Direita
                    if (IsPossible(linha, coluna + 1))
                    {
                        lnkdPosicao.AddLast(Matrix[linha, coluna + 1]);
                    }

                    //Abaixo
                    if (IsPossible(linha + 1, coluna))
                    {
                        lnkdPosicao.AddLast(Matrix[linha + 1, coluna]);
                    }

                    //Esquerda
                    if (IsPossible(linha, coluna - 1))
                    {
                        lnkdPosicao.AddLast(Matrix[linha, coluna - 1]);
                    }

                    int posicao = CalculaPosicao(linha, coluna, Matrix.GetLength(1));

                    AdjacencyList.Insert(posicao, lnkdPosicao);
                }
            }

            return AdjacencyList;
        }

        private bool IsPossible(int linha, int coluna)
        {
            if (linha < 0 || linha > Matrix.GetLength(1) - 1)
            {
                return false;
            }
            if (coluna < 0 || coluna > Matrix.GetLength(1) - 1)
            {
                return false;
            }

            return true;
        }

        private Vertice ExistInListAdjacency(Vertice procurado)
        {
            foreach (LinkedList<Vertice> lnkdList in this.AdjacencyList)
            {
                foreach (Vertice vertice in lnkdList)
                {
                    if (vertice.Nome == procurado.Nome)
                    {
                        return vertice;
                    }
                }
            }

            return null;
        }

        private int CalculaPosicao(int linha, int coluna, int totalColunas)
        {
            // linha index * numero de colunas + coluna index
            return linha * totalColunas + coluna;
        }

    }
}
