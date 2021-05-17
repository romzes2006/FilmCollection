using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

namespace FilmCollection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string connection = "Server=mysql60.hostland.ru;Database=host1323541_irkutsk5;Uid=host1323541_itstep;Pwd=269f43dc;";
        private MySqlConnection db;
        private DataSet dataSet; 
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowSuccess(string message)
        {
            Label_Info.Foreground = Brushes.Green;
            Label_Info.Content = message;
        }
        private void ShowInfo(string message)
        {
            Label_Info.Foreground = Brushes.Blue;
            Label_Info.Content = message;
        }
        private void ShowError(string message)
        {
            Label_Info.Foreground = Brushes.Red;
            Label_Info.Content = message;
        }
        private void Btn_Connect_OnClick(object sender, RoutedEventArgs e)
        {
            db = new MySqlConnection(connection);
            
            db.Open();

            if (db.Ping())
            {
                ShowSuccess("Подключились успешно");
            }
            else
            {
                ShowError("Ошибка подключения");
            }
        }

        private void Btn_GetData_OnClick(object sender, RoutedEventArgs e)
        {
            var sql = "SELECT * FROM tbl_FilmCollection";

            var adapter = new MySqlDataAdapter(sql, db);
            dataSet = new DataSet();
            adapter.Fill(dataSet);

            tbl_Films.ItemsSource = dataSet.Tables[0].DefaultView;
        }

        private void Tbl_Films_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = tbl_Films.SelectedIndex;
            var table = dataSet.Tables[0];

            var row = table.Rows[index];
            var cells = row.ItemArray;
          
            var filmName = (string) cells[1].ToString();
            var age = cells[2].ToString();
            var izdat = (string) cells[3].ToString();
            var jenre = (string) cells[4].ToString();

            Box_FilmName.Text = filmName;
            Box_Year.Text = age;
            Box_Izdat.Text = izdat;
            Box_Jenre.Text = jenre;
        }
    }
}