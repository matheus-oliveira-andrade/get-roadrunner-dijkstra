using GetRoadRunner.Models.Graph;
using System.Collections.Generic;

namespace GetRoadRunner.Models.Solver
{
    public class Dijkstra
    {
        public Vertice Caminho { get; set; }
        public Vertice VerticeFinal { get; set; }

        public void Solver(List<LinkedList<Vertice>> listAdjacency)
        {
            Vertice menorEstimativa;

            SetInitial(listAdjacency);

            while (ExisteAbertos(listAdjacency))
            {
                menorEstimativa = GetMenorEstimativa(listAdjacency);
                menorEstimativa.Aberto = false;

                AtualizarEstimativa(listAdjacency, menorEstimativa);
            }

        }

        private void SetInitial(List<LinkedList<Vertice>> listAdjacency)
        {
            foreach (var lnkdList in listAdjacency)
            {
                foreach (var vertice in lnkdList)
                {
                    vertice.Predecessor = null;

                    if (vertice.Nome == Pecas.Coyote)
                    {
                        vertice.Estimativa = 0;
                    }else if(vertice.Nome == Pecas.Papaleguas)
                    {
                        VerticeFinal = vertice;
                    }
                    else { vertice.Estimativa = int.MaxValue / 2; }
                }
            }
        }

        private bool ExisteAbertos(List<LinkedList<Vertice>> listAdjacency)
        {
            foreach (var lnkdList in listAdjacency)
            {
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

        public List<Vertice> GetCaminho(Vertice verticeFinal, List<Vertice> listaCaminho)
        {
            listaCaminho.Add(verticeFinal);

            if (verticeFinal.Predecessor == null) { return listaCaminho ; }

            GetCaminho(verticeFinal.Predecessor, listaCaminho);

            return listaCaminho;
        }
    }
}
