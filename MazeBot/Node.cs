using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeBot
{
    class Node
    {
        private struct Data {
            Node node;
            char dir;

            public Data(Node nd, char dir)
            {
                this.node = nd;
                this.dir = dir;
            }

            public Data getData()
            {
                return this;
            }

            public Node getNodeData()
            {
                return this.node;
            }

            public char getDirData()
            {
                return this.dir;
            }
        }
        private int column;
        private int row;
        private bool visited = false;
        private Node parent = null;
                                //left, top, right, bottom
        private bool[] walls = { true, true, true, true };
        private bool begining = false;
        private bool end = false;

        Node()
        {

        }

        Node(int row, int col)
        {
            this.row = row;
            this.column = col;
        }

        public void setNode(int r, int c)
        {
            this.row = r;
            this.column = c;
        }

        public void setNodeAsBegin()
        {
            this.begining = true;
        }

        public void setNodeAsEnd()
        {
            this.end = true;
        }

        public bool isVisited()
        {
            return this.visited;
        }

        public void setVisited()
        {
            this.visited = true;
        }

        public Node getParent()
        {
            return this.parent;
        }

        public void setParent(Node current)
        {
            this.parent = current;
        }

        private List<Data> getNeighbors(List<Node[]> grid)
        {
            List<Data> neighbor = new List<Data>();
            //left
            if (!grid[this.row][this.column - 1].isVisited())
            {
                try { neighbor.Add(new Data(grid[this.row][this.column - 1], '<')); } catch { }
            }
            //top
            if (!grid[this.row - 1][this.column].isVisited())
            {
                try { neighbor.Add(new Data(grid[this.row - 1][this.column], '^')); } catch { }
            }
            //right
            if (!grid[this.row][this.column + 1].isVisited())
            {
                try { neighbor.Add(new Data(grid[this.row][this.column + 1], '>')); } catch { }
            }

            //bottom
            if (!grid[this.row + 1][this.column].isVisited())  
            {
                try { neighbor.Add(new Data(grid[this.row + 1][this.column], 'v')); } catch { }
            }

            return neighbor;
        }

        public Node getRandomNeighbor(List<Node[]> grid)
        {
            List<Data> neighbors = this.getNeighbors(grid);
            

            if(neighbors.Count != 0)
            {
                Random rnd = new Random();
                int index = rnd.Next(neighbors.Count);
                Data selected = neighbors[index];
                Node next = selected.getNodeData();
                char dir = selected.getDirData();

                if (dir == '<')
                {
                    this.walls[0] = false; //left
                    next.walls[2] = false;
                }
                else if (dir == '^')
                {
                    this.walls[1] = false; //top
                    next.walls[3] = false;
                }
                else if (dir == '>')
                {
                    this.walls[2] = false; //right
                    next.walls[0] = false;

                }
                else if (dir == 'v')
                {
                    this.walls[3] = false; //bottom
                    next.walls[1] = false;
                }

                //next.parent = this;

                return next;

            }
            else
            {
                Node next = null;
                return next;
            }
        }
    }
}
