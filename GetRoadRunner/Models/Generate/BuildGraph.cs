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

        public BuildGraph(Vertice[,] matrix)
        {

            this.Matrix = matrix;
            this.MatrizControl = new int[matrix.GetLength(0), matrix.GetLength(1)];
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
            AdjacencyList = new List<LinkedList<Vertice>>(Matrix.GetLength(0) * Matrix.GetLength(1));
            InitializeAdjacencyList();

            for (int linha = 0; linha < Matrix.GetLength(0); linha++)
            {
                for (int coluna = 0; coluna < Matrix.GetLength(1); coluna++)
                {
                    var lnkdPosicao = new LinkedList<Vertice>();
                    // Nome, Peso

                    //Acima
                    if (IsPossible(linha - 1, coluna))
                    {
                        var vertice = ContainsVertice(linha - 1, coluna, Matrix.GetLength(1), Matrix[linha - 1, coluna]);

                        if (vertice != null)
                        {
                            lnkdPosicao.AddLast(vertice);
                        }
                        else
                        {
                            lnkdPosicao.AddLast(Matrix[linha - 1, coluna]);
                        }
                    }
                    //Direita
                    if (IsPossible(linha, coluna + 1))
                    {
                        var vertice = ContainsVertice(linha, coluna + 1, Matrix.GetLength(1), Matrix[linha, coluna + 1]);


                        if (vertice != null)
                        {
                            lnkdPosicao.AddLast(vertice);
                        }
                        else
                        {
                            lnkdPosicao.AddLast(Matrix[linha, coluna + 1]);
                        }
                    }

                    //Abaixo
                    if (IsPossible(linha + 1, coluna))
                    {
                        var vertice = ContainsVertice(linha + 1, coluna, Matrix.GetLength(1), Matrix[linha + 1, coluna]);

                        if (vertice != null)
                        {
                            lnkdPosicao.AddLast(vertice);
                        }
                        else
                        {
                            lnkdPosicao.AddLast(Matrix[linha + 1, coluna]);
                        }
                    }

                    //Esquerda
                    if (IsPossible(linha, coluna - 1))
                    {
                        var vertice = ContainsVertice(linha, coluna - 1, Matrix.GetLength(1), Matrix[linha, coluna - 1]);

                        if (vertice != null)
                        {
                            lnkdPosicao.AddLast(vertice);
                        }
                        else
                        {
                            lnkdPosicao.AddLast(Matrix[linha, coluna - 1]);
                        }
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

        private Vertice ContainsVertice(int linha, int coluna, int totalColunas, Vertice procurado)
        {
            if (AdjacencyList[linha * totalColunas + coluna] == null)
            {
                return null;
            }

            foreach (var vertice in AdjacencyList[linha * totalColunas + coluna])
            {
                if (vertice.Linha == procurado.Linha && vertice.Coluna == procurado.Coluna && vertice.Nome == procurado.Nome)
                {
                    return vertice;
                }
            }

            return null;
        }
        private void InitializeAdjacencyList()
        {
            for (int i = 0; i < AdjacencyList.Capacity; i++)
            {
                AdjacencyList.Insert(i, null);
            }
        }
    }
}
