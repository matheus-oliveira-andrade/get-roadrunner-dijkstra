using GetRoadRunner.Models.Graph;
using System;
using System.Collections.Generic;

namespace GetRoadRunner.Models
{
    public class BuildGraph
    {
        /// <summary>
        /// Matriz de vértices
        /// </summary>
        private Vertice[,] Matrix { get; set; }

        private List<LinkedList<Vertice>> AdjacencyList { get; set; }

        public BuildGraph(Vertice[,] matrix)
        {
            this.Matrix = matrix;
        }

        /// <summary>
        /// Gera a lista de adjacência usando a matriz de vértices criada anteriormente
        /// </summary>
        /// <returns></returns>
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

                    int posicao = Posicao.Calcula(linha, coluna, Matrix.GetLength(1));

                    AdjacencyList.Insert(posicao, lnkdPosicao);
                }
            }

            return AdjacencyList;
        }

        /// <summary>
        /// Verifica se o movimento a ser realizado é possível
        /// </summary>        
        /// <returns>true é possível, false não é possível</returns>
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

        /// <summary>
        /// Verifica se o vértice já existe na lista de adjacência
        /// </summary>
        /// <param name="procurado">Vertice a verificar</param>
        /// <returns>Retorna o vértice se existir</returns>
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

        /// <summary>
        /// Verifica se contain o vertice na lista de adjacência
        /// </summary>
        /// <returns>Retorna o vertice se já ouver na lista</returns>
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

        /// <summary>
        /// Inicializa lista de adjacência
        /// </summary>
        private void InitializeAdjacencyList()
        {
            for (int i = 0; i < AdjacencyList.Capacity; i++)
            {
                AdjacencyList.Insert(i, null);
            }
        }
    }
}
