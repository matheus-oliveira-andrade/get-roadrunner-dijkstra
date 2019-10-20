namespace GetRoadRunner.Models
{
    class Posicao
    {
        /// <summary>
        /// Calcula a posição de matriz, em posição na lista de adjacência
        /// </summary>
        public static int Calcula(int linha, int coluna, int totalColunas)
        {
            // linha index * numero de colunas + coluna index
            return linha * totalColunas + coluna;
        }

    }
}
