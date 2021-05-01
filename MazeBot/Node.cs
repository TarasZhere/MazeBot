using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeBot
{
    public class Node
    {
        private static Random rnd = new Random();
        public int column;
        public int row;
        private bool visited = false;
        private Node parent = null;
                                //left, top, right, bottom
        private bool[] walls = { true, true, true, true };


        public Node() { }

        public Node(int row, int col)
        {
            this.row = row;
            this.column = col;
        }

        public void setNode(int r, int c)
        {
            this.row = r;
            this.column = c;
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

        private List<Data> getNeighbors(List<List<Node>> grid)
        {
            List<Data> neighbor = new List<Data>();
            
                //left
            if (this.column - 1 >= 0 && !grid[this.row][this.column - 1].isVisited())
                neighbor.Add(new Data(grid[this.row][this.column - 1], '<'));
            

            
            //top
            if (this.row - 1 >= 0 && !grid[this.row - 1][this.column].isVisited())
                neighbor.Add(new Data(grid[this.row - 1][this.column], '^'));



            //right
            if (this.column + 1 < grid[0].Count && !grid[this.row][this.column + 1].isVisited())
                neighbor.Add(new Data(grid[this.row][this.column + 1], '>'));
          
            
            //bottom
            if (this.row + 1 < grid.Count && !grid[this.row + 1][this.column].isVisited())
                    neighbor.Add(new Data(grid[this.row + 1][this.column], 'v'));
            
           
            return neighbor;
        }

        public Data getRandomNeighbor(List<List<Node>> grid)
        {
            List<Data> neighbors = this.getNeighbors(grid);
            
            

            if(neighbors.Count != 0)
            {            
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

                return selected;
            }
            else
            {
                Data next = new Data(new Node(), 'x');
                return next;
            }
        }
    }
}
