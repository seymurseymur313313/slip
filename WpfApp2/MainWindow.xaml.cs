using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int threadnum = 3;
        int a = 1;
        private List<Thread> CreatedThreadsList;
        private List<Thread> WaitingThreadsList;
        private List<Thread> WorkingThreadsList;

        public MainWindow()
        {
            InitializeComponent();
            numberTextBlock.Text = $"{a}";
            DataContext = this;
            CreatedThreadsList = new List<Thread>();

            for (int i = 0; i < threadnum; i++)
            {
                Thread thread = new Thread(new ThreadStart(ThreadMethod));
                thread.Name = "Thread " + (i + 1);
                CreatedThreadsList.Add(thread);
            }

            foreach (Thread thread in CreatedThreadsList)
            {
                CreatedListView.Items.Add(new { ThreadName = thread.Name });
            }
        }

        private void ThreadMethod()
        {
            // Thread görevi
            Console.WriteLine(Thread.CurrentThread.Name + " is running.");
            Thread.Sleep(1000);
        }
        private void IncreaseButtonClick(object sender, RoutedEventArgs e)
        {
            numberTextBlock.Text = $"{a++}";
        }

        private void DecreaseButtonClick(object sender, RoutedEventArgs e)
        {
            numberTextBlock.Text = $"{a--}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(ThreadMethod));
            thread.Name = "Thread " + threadnum;
            CreatedThreadsList.Add(thread);
            threadnum++;
        }
    }
}
