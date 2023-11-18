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
        int num = 5;
        int a = 1;
        private List<Thread> CreatedThreadsList;
        private List<Thread> WaitingThreadsList;
        private List<Thread> WorkingThreadsList;
        private Semaphore semaphore = new Semaphore(5, 5);

        public MainWindow()
        {
            InitializeComponent();
            numberTextBlock.Text = $"{a}";
            DataContext = this;
            CreatedThreadsList = new List<Thread>();
            WorkingThreadsList = new List<Thread>();
            WaitingThreadsList=new List<Thread>();  

            for (int i = 0; i < num; i++)
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
            
            Console.WriteLine(Thread.CurrentThread.Name + "");
            Thread.Sleep(1000);
        }
        private void IncreaseButtonClick(object sender, RoutedEventArgs e)
        {
            
            semaphore.WaitOne();

            numberTextBlock.Text = $"{++a}";

            
            Thread thread = new Thread(new ThreadStart(ThreadMethod));
            thread.Name = "Thread " + a;

            workingListView.Items.Add(new { ThreadName = thread.Name });

            thread.Start();
        }

        private void DecreaseButtonClick(object sender, RoutedEventArgs e)
        {
 
            semaphore.WaitOne();

        
            numberTextBlock.Text = $"{--a}";

            var selectedItem = workingListView.Items[0];
            workingListView.Items.RemoveAt(0);

            waitingListView.Items.Add(selectedItem);

            semaphore.Release();
        }

        private void newthr_Click(object sender, RoutedEventArgs e)
        {
            int a = num + 1;
            Thread thread = new Thread(new ThreadStart(ThreadMethod));
            thread.Name = "Thread " + a;
            CreatedThreadsList.Add(thread);
            num++;
            CreatedListView.Items.Add(new { ThreadName = thread.Name });
        }

        private void CreatedListView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (CreatedListView.SelectedItem != null)
            {
                waitingListView.Items.Add(CreatedListView.SelectedItem);
            }
        }
    }
}
