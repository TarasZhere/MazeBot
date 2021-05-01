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
using System.IO;
using System.Windows.Forms;


namespace MazeBot
{
    /// <summary>
    /// Interaction logic for mazeGenerator.xaml
    /// </summary>
    
    public partial class mazeGenerator : Page
    {
        List<List<Node>> grid = new List<List<Node>>();
        private int rows;
        private int columns;
        private int nodeSize;
        private bool createdMaze = false;
        

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
            //Draw a grid & set up the maze
            this.setCanvasSize();
            this.gridDrawer();
            // begin mazing
            this.DFS();
            this.createdMaze = true;
        }

        /*
         *      OUT PUT DATA ABOUT MAZE
         *      this function simply outputs data in the databox x:Name = output
         */
       

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
                if (num < 10) num = 10;
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

        private void setCanvasSize()
        {
            canvas.Height = this.rows * (this.nodeSize-2) + 2;
            canvas.Width = this.columns * (this.nodeSize-2) + 2;
        }

        private void gridDrawer()
        {
            this.grid = null;
            this.grid = new List<List<Node>>();


            canvas.Children.Clear();

            for(int i = 0; i < this.rows; i++)
            {
                List<Node> tempList = new List<Node>();

                for(int j = 0; j < this.columns; j++)
                {
                    Rectangle node = new Rectangle();
                    node.Width = this.nodeSize;
                    node.Height = this.nodeSize;
                    node.Fill = Brushes.Transparent;
                    node.Stroke = Brushes.Black;
                    node.StrokeThickness = 2;

                    canvas.Children.Add(node);

                    
                    Canvas.SetLeft(node, j * (this.nodeSize-2));
                    Canvas.SetTop (node, i * (this.nodeSize-2));

                    Node tempNode = new Node(i, j);
                    tempList.Add(tempNode);
                }

                this.grid.Add(tempList);
            }
        }

        /*
         *      DFS
         *      function define and draws the maze
         */
        private void DFS()
        {
            try
            { // selecting a random node as begi of maze
                Random rnd = new Random();
                int rndRow = rnd.Next(0, this.rows);
                int rndCol = rnd.Next(0, this.columns);

                Node current = this.grid[rndRow][rndCol]; 

                //stack used for depth first search
                Stack<Node> stack = new Stack<Node>();

                stack.Push(current);
                current.setVisited();

                while (stack.Count != 0)
                {
                    /* DRAWING PATH OF THE MAZE */
                    Data temp = current.getRandomNeighbor(this.grid);
                    Node next = temp.getNodeData();
                    char dir = temp.getDirData();
                    Line line = new Line();
                    var converter = new System.Windows.Media.BrushConverter();
                    line.Stroke = (Brush)converter.ConvertFromString("#eeeeee");
                    line.StrokeThickness = this.nodeSize - 4;

                    /* if maze has find a node */
                    if (dir != 'x')
                    {
                        line.X1 = current.column * (this.nodeSize - 2) + this.nodeSize / 2;
                        line.Y1 = current.row * (this.nodeSize - 2) + this.nodeSize / 2;

                        line.X2 = next.column * (this.nodeSize - 2) + this.nodeSize / 2;
                        line.Y2 = next.row * (this.nodeSize - 2) + this.nodeSize / 2;
                        canvas.Children.Add(line);

                        next.setParent(current);
                        current = next;
                        current.setVisited();
                        stack.Push(current);
                    }
                    /* if maze has not find a node to expand */
                    else
                    {
                        current = current.getParent();
                        stack.Pop();
                    }
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("The application has run into some issue while generating your maze\nThe value you used may be to great.\nTry to lower them." , "Error");
                createdMaze = false;
            }
        }

        /*
         *      FUNCTION SAVES CANVAS TO PNG
         *          the following function will save a png from a selected path from the user
         *          used libraries:
         *              -Rect
         *              -RenderTargetBitmap
         *              -DrawingVisual
         *              -VisualBrush
         *              -DialogResult
         */
        private void SaveMazeToPNG(object sender, RoutedEventArgs e)
        {
            if (this.createdMaze)
            {
                //rendering the canvas to png file
                Rect bounds = VisualTreeHelper.GetDescendantBounds(canvas);
                RenderTargetBitmap rtb = new RenderTargetBitmap((Int32)bounds.Width, (Int32)bounds.Height, 96, 96, PixelFormats.Pbgra32);
                DrawingVisual dv = new DrawingVisual();
                using (DrawingContext dc = dv.RenderOpen())
                {
                    VisualBrush vb = new VisualBrush(canvas);
                    dc.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
                }
                rtb.Render(dv);

                // PNG encoder
                PngBitmapEncoder png = new PngBitmapEncoder();
                png.Frames.Add(BitmapFrame.Create(rtb));

                //asking the user for directory path
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();
                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        try
                        {
                            //fdb is the folder selected by the user
                            string directory = fbd.SelectedPath + "\\maze.png";

                            //if file exists change name by adding a number
                            if (File.Exists(directory)) 
                            {
                                int i = 1;
                                do
                                {
                                    directory = fbd.SelectedPath + "\\maze"+ i +".png";
                                    i++;
                                } while (File.Exists(directory));
                            }

                            //finally saving the file
                            using (var save = File.OpenWrite(directory))
                            {
                                png.Save(save);
                                save.Close();
                            }
                            //confirmation of file exported
                            System.Windows.MessageBox.Show("Exported @ " + fbd.SelectedPath, "Done");
                        }
                        catch //file has not been saved?
                        {
                            System.Windows.MessageBox.Show("Not allowed to export\n" + fbd.SelectedPath, "Not Allow");
                        }
                    }
                }
            }
            else //if there is not a maze in the canvas
            {
                System.Windows.MessageBox.Show("You are not allow to store a maze if have not generated one yet." , "Not Allow");
            }
        }

    }
}