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
        private WebPageHref webPage;
        private developersPage devPage;

        public MainWindow()
        {
            InitializeComponent();
            this.mazePage = new mazeGenerator();
            this.webPage = new WebPageHref();
            this.devPage = new developersPage();
        }


        /*  The function is called when clicking on Maze button
         *  Will display the mazeGenerator page created after InitializateComponents
         */
        private void MazeButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Content = this.mazePage;
        }

        private void WebButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Content = this.webPage;
        }

        private void DevelopersButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Content = this.devPage;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
