using GetRoadRunner.Models.Graph;
using System;
using System.Collections.Generic;

namespace GetRoadRunner.Models.Solver
{
    public class Dijkstra
    {
        /// <summary>
        /// Representa o coiote
        /// </summary>
        public Vertice Cacador { get; private set; }

        /// <summary>
        /// Representa o papaléguas
        /// </summary>
        public Vertice Caca { get; private set; }

        /// <summary>
        /// Dada uma lista de adjacência retorna se foi possível resolve-la, se foi possível soluciona o caminho minímo
        /// </summary>        
        /// <returns>Retorna se foi possível ou não solucionar</returns>
        public bool Solver(List<LinkedList<Vertice>> listAdjacency)
        {
            Vertice menorEstimativa;

            SetInitial(listAdjacency);

            // Não foi gerado o caminho
            if (!ValidaPossibilidade(listAdjacency)) { return false; }

            while (ExisteAbertos(listAdjacency))
            {
                menorEstimativa = GetMenorEstimativa(listAdjacency);
                menorEstimativa.Aberto = false;

                AtualizarEstimativa(listAdjacency, menorEstimativa);
            }

            // Foi gerado o caminho
            return true;
        }

        /// <summary>
        /// Verifica se os dois personagem não estão bloqueados
        /// </summary>
        /// <returns>Retorna se esta ou não bloqueado</returns>
        private bool ValidaPossibilidade(List<LinkedList<Vertice>> listaAdjacencia)
        {
            var coiote = listaAdjacencia[Posicao.Calcula(Cacador.Linha, Cacador.Coluna, Cacador.NumeroColunas)];
            var papaleguas = listaAdjacencia[Posicao.Calcula(Caca.Linha, Caca.Coluna, Caca.NumeroColunas)];

            int obstCoiote = 0;
            int obstPapaleguas = 0;

            // Verifica vizinhos do coiote para saber se ele esta completamente fechado
            foreach (var vizinho in coiote)
                if (vizinho.Nome == Pecas.Obstacle) { obstCoiote++; }

            // Verifica vizinhos do papaleguas para saber se ele esta completamente fechado
            foreach (var vizinho in papaleguas)
                if (vizinho.Nome == Pecas.Obstacle) { obstPapaleguas++; }
            
            // Se for maior ou igual a 4 significa que esta fechado
            if (obstPapaleguas >= 4 || obstCoiote >= 4) { return false; }

            return true;
        }

        /// <summary>
        /// Pega o caminho gerado pelo método SOLVER
        /// </summary>
        /// <returns>Retorna uma lista com cada vertice caminho</returns>
        public List<Vertice> GetCaminho()
        {
            var lista = new List<Vertice>();

            var verticeCaminho = Caca;

            while (verticeCaminho != null)
            {
                lista.Add(verticeCaminho);

                verticeCaminho = verticeCaminho.Predecessor;
            }

            return lista;
        }

        /// <summary>
        /// Prepara a lista de adjacência para o método SOLVER
        /// </summary>
        /// <param name="listAdjacency">lista de adjacência</param>
        private void SetInitial(List<LinkedList<Vertice>> listAdjacency)
        {
            foreach (var lnkdList in listAdjacency)
            {
                if (lnkdList == null) { continue; }
                foreach (var vertice in lnkdList)
                {
                    vertice.Predecessor = null;

                    if (vertice.Nome == Pecas.Coyote)
                    {
                        vertice.Estimativa = 0;

                        Cacador = vertice;
                    }
                    else { vertice.Estimativa = int.MaxValue / 2; }

                    if (vertice.Nome == Pecas.Papaleguas)
                    {
                        Caca = vertice;
                    }
                }
            }
        }

        /// <summary>
        /// Verifica a existencia de vertices abertos na lista de adjacência
        /// </summary>
        /// <param name="listAdjacency">lista de adjacência</param>
        /// <returns></returns>
        private bool ExisteAbertos(List<LinkedList<Vertice>> listAdjacency)
        {
            foreach (var lnkdList in listAdjacency)
            {
                if (lnkdList == null) { continue; }

                foreach (var vertice in lnkdList)
                {
                    if (vertice.Aberto == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Pega dentre todos os vertices aberto na lista de adjacência o com a menor estimativa
        /// </summary>
        /// <param name="listAdjacency">lista de adjacência</param>
        /// <returns></returns>
        private Vertice GetMenorEstimativa(List<LinkedList<Vertice>> listAdjacency)
        {
            Vertice verticeMenorEstimativa = new Vertice();
            verticeMenorEstimativa.Estimativa = int.MaxValue;

            foreach (var lnkdList in listAdjacency)
            {
                if (lnkdList == null) { continue; }

                foreach (var vertice in lnkdList)
                {
                    if (vertice.Estimativa <= verticeMenorEstimativa.Estimativa && vertice.Aberto)
                    {
                        verticeMenorEstimativa = vertice;
                    }
                }
            }

            return verticeMenorEstimativa;
        }

        /// <summary>
        /// Atualiza a estimativa dos vizinhos aberto do vertice 
        /// </summary>
        /// <param name="listAdjacency">lista de adjacência</param>
        /// <param name="verticeAtual">Vertice a ser olhado os vizinhos e calculado a estimativa</param>
        private void AtualizarEstimativa(List<LinkedList<Vertice>> listAdjacency, Vertice verticeAtual)
        {
            int linha = verticeAtual.Linha;
            int coluna = verticeAtual.Coluna;
            int numeroColunas = verticeAtual.NumeroColunas;

            var listaAdjacentes = listAdjacency[Posicao.Calcula(linha, coluna, numeroColunas)];

            foreach (var vertice in listaAdjacentes)
            {
                int soma = verticeAtual.Estimativa + vertice.Peso;

                //* Se o vertice não estiver aberto, não atualiza a estimativa
                if (!vertice.Aberto) { continue; }


                // Se a soma da estimativa for a mesma que já existe la, significa que por outro caminho já foi
                // chegado ao mesmo resultado
                if (vertice.Estimativa == soma) { continue; }

                // Se soma for menor que a estimativa no vertice a estimativa está alta, atualize...
                if (soma < vertice.Estimativa)
                {
                    vertice.Estimativa = soma;

                    // Guarda o predecessor!
                    vertice.Predecessor = verticeAtual;
                }

            }
        }
    }
}
