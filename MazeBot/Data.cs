namespace MazeBot
{
    public struct Data
    {
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
}