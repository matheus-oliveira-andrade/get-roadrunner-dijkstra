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
            // Se retornou false é porque não foi possível resolver o caminho
            if (!solver.Solver(listAdjacency))
            {
                return null;
            }

            return solver.GetCaminho();
        }
    }
}
