using GetRoadRunner.Models.Graph;
using System;

namespace GetRoadRunner.Models
{
    class GeneratorBoard
    {        
        /// <summary>
        /// Valor minímo que pode ser gerado a matriz
        /// </summary>
        private const int ValueMin = 4;

        /// <summary>
        /// Valor maximo que pode ser gerado a matriz
        /// </summary>
        private const int ValueMax = 10;

        /// <summary>
        /// Gera a matriz de vértices
        /// </summary>
        /// <returns></returns>
        public Vertice[,] Generate()
        {
            var r = new Random();

            int lenght = r.Next(ValueMin, ValueMax);
            var matriz = new Vertice[lenght, lenght];

            for (int linha = 0; linha < matriz.GetLength(0); linha++)
            {
                for (int coluna = 0; coluna < matriz.GetLength(1); coluna++)
                {
                    //Peça sorteada
                    int valor = r.Next(3, 8);

                    if (valor == Pecas.Sand)
                    {
                        matriz[linha, coluna] = new Vertice(valor, r.Next(10, 15), linha, coluna, lenght, ".");
                        continue;
                    }
                    if (valor == Pecas.Grass)
                    {
                        matriz[linha, coluna] = new Vertice(valor, r.Next(15, 23), linha, coluna, lenght, ".");
                        continue;
                    }
                    if (valor == Pecas.Water)
                    {
                        matriz[linha, coluna] = new Vertice(valor, r.Next(23, 25), linha, coluna, lenght, ".");
                        continue;
                    }
                    if (valor == Pecas.Asphalt)
                    {
                        matriz[linha, coluna] = new Vertice(valor, r.Next(7, 10), linha, coluna, lenght, ".");
                        continue;
                    }
                    if (valor == Pecas.Obstacle)
                    {
                        matriz[linha, coluna] = new Vertice(valor, 2, linha, coluna, lenght, "*");
                        continue;
                    }
                }
            }

            // PosCoyote
            int PosLinha = r.Next(0, lenght);
            int PosColuna = r.Next(0, lenght / 2 - 1);
            matriz[PosLinha, PosColuna] = new Vertice(Pecas.Coyote, 0, PosLinha, PosColuna, lenght, "C");

            // PosPapaleguas
            PosLinha = r.Next(0, lenght);
            PosColuna = r.Next(lenght / 2 - 1, lenght);
            matriz[PosLinha, PosColuna] = new Vertice(Pecas.Papaleguas, 1, PosLinha, PosColuna, lenght, "P");

            return matriz;

        }
    }
}
