using GetRoadRunner.Models.Graph;
using System;
using System.Collections.Generic;

namespace GetRoadRunner.Models.Solver
{
    public class Dijkstra
    {
        public Vertice Caminho { get; set; }

        public Vertice Cacador { get; set; }
        public Vertice Caca { get; set; }

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
