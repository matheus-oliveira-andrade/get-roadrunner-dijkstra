
using GetRoadRunner.Models.Graph;
using System.Collections.Generic;

namespace GetRoadRunner.Controllers
{
    class Solve
    {
        private Models.Solver.Dijkstra solver;

        public Solve()
        {
            solver = new Models.Solver.Dijkstra();
        }


        public List<Vertice> Solver(List<LinkedList<Vertice>> listAdjacency)
        {            
            solver.Solver(listAdjacency);

            var listaCaminho = new List<Vertice>();

            //solver.GetCaminho(solver.VerticeFinal, listaCaminho);

            return listaCaminho;
        }
    }
}
