using GetRoadRunner.Models.Graph;
using System.Collections.Generic;

namespace GetRoadRunner.Controllers
{
    class Generate
    {
        private Models.GeneratorBoard generatorBoard;
        private Models.BuildGraph buildGraph;

        public Generate()
        {
            generatorBoard = new Models.GeneratorBoard();                        
        }

        public Vertice[,] GenerateBoard()
        {
            return generatorBoard.Generate();
        }

        public List<LinkedList<Vertice>> BuildGraph(Vertice[,] matriz)
        {
            buildGraph = new Models.BuildGraph(matriz);

            return buildGraph.ToListAdjacency();
        }

        public List<LinkedList<Vertice>> ToListAdjacency()
        {
            return buildGraph.ToListAdjacency();
        }
    }
}
