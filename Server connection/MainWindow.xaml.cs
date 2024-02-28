using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
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

namespace Server_connection
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SqlConnection sc = new SqlConnection(@"Data Source=(localdb)\.;Integrated Security=True");
            sc.Open();
            string sql = "select Id, title, rating, size, time from songs";
            SqlCommand command = new SqlCommand(sql, sc);
            IDataReader idr = command.ExecuteReader();
            IList<Song> songs = new List<Song>();
            int total_duration = 0, total_size = 0;//суммарные длительность всех песен, вес файлов с песнями
            while (idr.Read())
            {
                int Id = idr.GetInt32(0);
                string title = idr.GetString(1);
                int rating = idr.GetInt32(2), size = idr.GetInt32(3), time = idr.GetInt32(4);
                songs.Add(new Song(Id, title, rating,
                    size, time));
                total_duration += time;
                total_size += size;
            }
            DataContext = songs;
            label.Content = $"суммарная длительность всех песен={total_duration}";
            label_.Content = $"суммарный вес файлов с песнями={total_size}";
        }
    }
}