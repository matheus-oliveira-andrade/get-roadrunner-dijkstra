namespace GetRoadRunner.Models.Graph
{
    public class Vertice
    {
        /// <summary>
        /// Peso do vértice
        /// </summary>
        public int Peso { get; set; }

        /// <summary>
        /// Nome da peça no vertice
        /// </summary>
        public int Nome { get; set; }    
        
        /// <summary>
        /// Controle se o vértice esta aberto
        /// </summary>
        public bool Aberto { get; set; } = true;

        /// <summary>
        /// Predecessor do vértice
        /// </summary>
        public Vertice Predecessor { get; set; }

        /// <summary>
        /// Estimativa de custo até o vértice
        /// </summary>
        public int Estimativa { get; set; }

        /// <summary>
        /// Como será mostrado na tela
        /// </summary>
        public string Visualizacao { get; set; }

        /// <summary>
        /// Linha correspondente na matriz de vértice
        /// </summary>
        public int Linha { get; set; }

        /// <summary>
        /// Coluna correspondente na matriz de vértice
        /// </summary>
        public int Coluna { get; set; }

        /// <summary>
        /// Número de colunas na matriz de vértice (GAMBIARRA)
        /// </summary>
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

