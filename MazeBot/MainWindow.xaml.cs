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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        //this is the page that would display the maze
        private mazeGenerator mazePage;
        private developersPage devPage;
        private Page1 homePage;

        public MainWindow()
        {
            InitializeComponent();
            this.mazePage = new mazeGenerator();
            this.devPage = new developersPage();
            this.homePage = new Page1();
            MainPanel.Content = this.homePage;
        }


        /*  The function is called when clicking on Maze button
         *  Will display the mazeGenerator page created after InitializateComponents
         */
        private void MazeButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Content = this.mazePage;
        }

        private void DevelopersButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Content = this.devPage;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Content = this.homePage;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
