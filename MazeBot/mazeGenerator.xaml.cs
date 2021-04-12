using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MazeBot
{
    /// <summary>
    /// Interaction logic for mazeGenerator.xaml
    /// </summary>
    public partial class mazeGenerator : Page
    {
        List<Node[]> grid;
        private int rows;
        private int columns;
        private int nodeSize;

        public mazeGenerator()
        {
            InitializeComponent();
        }

        /*      GENERATE MAZE BUTTON
         *      When button is clicked
         *      catch from the text:
         *          rows
         *          columns
         *          nodesize
         *      if failed default val:
         *          rws = 20
         *          col = 20
         *          ndsz = 10
         */
        private void GenrateMazeButton_Click(object sender, RoutedEventArgs e)
        {
            this.rows = catchRows();
            this.columns = catchColumns();
            this.nodeSize = catchNodeSize();
            output_text();
            //Draw a grid & set up the maze
            this.gridDrawer();
        }

        /*
         *      OUT PUT DATA ABOUT MAZE
         *      this function simply outputs data in the databox x:Name = output
         */
        private void output_text()
        {
            output.Text = "Rows: " + this.rows +
                "\nColumns: " + this.columns +
                "\nNode Size: " + this.nodeSize;
        }

        /*
         *      CATCHING ROWS NUMBER
         */
        private int catchRows()
        {
            int num;

            try
            {
                num = Int32.Parse(rows_textBox.Text);
            }
            catch
            {
                num = 20;
            }

            return num;
        }

        /*
         *      CATCHING # OF COLUMNS
         */
        private int catchColumns()
        {
            int num;

            try
            {
                num = Int32.Parse(columns_textBox.Text);
            }
            catch
            {
                num = 20;
            }

            return num;
        }

        /*
         *      CATCHING NODE SIZE
         */
        private int catchNodeSize()
        {
            int num;

            try
            {
                num = Int32.Parse(nodeSize_textBox.Text);
            }
            catch
            {
                num = 20;
            }

            return num;
        }

        /*
         *      GRID DRAWER
         *      The funciton draws a maze by getting input from the used.
         *      and set the nodes in a double array
         */
        private void gridDrawer()
        {
            canvas.Children.Clear();

            for(int i = 0; i < this.rows; i++)
            {
                this.grid.Add(new Node[this.columns]);

                for(int j = 0; j < this.columns; j++)
                {
                    Rectangle node = new Rectangle();
                    node.Width = this.nodeSize;
                    node.Height = this.nodeSize;
                    node.Fill = Brushes.Transparent;
                    node.Stroke = Brushes.Black;
                    node.StrokeThickness = 2;

                    canvas.Children.Add(node);

                    Canvas.SetLeft(node, j * (this.nodeSize - 2));
                    Canvas.SetTop(node, i * (this.nodeSize - 2));

                    this.grid[i][j].setNode(i, j);
                }
            }
        }

        /*
         *      DFS
         */
        private void DFS()
        {
            Node current = this.grid[0][0];

            Stack<Node> stack = new Stack<Node>();

            stack.Push(current);
            current.setVisited();

            while(stack.Count != 0)
            {
                Node next = current.getRandomNeighbor(this.grid);

                if(next != null)
                {
                    next.setParent(current);
                    current = next;
                    current.setVisited();
                    stack.Push(current);
                }
                else
                {
                    current = current.getParent();
                    stack.Pop();
                }
            }
        }
    }
}
