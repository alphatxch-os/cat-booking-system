using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace cat_booking_system
{
    public partial class MainWindow : Window
    {
        private string connStr = "server=ND-COMPSCI;" +
                                  "username=TL_S2201761;" +
                                  "database=TL_S2201761;" +
                                  "port=3306;" +
                                  "password=Notre021205";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string catName = NameBlock.Text;
            string catOwnerID = CatOwnerID.Text;
            string catCode = KittenCode.Text; // New input for catCode

            // Make sure the name, catOwnerID, and catCode are not empty
            if (!string.IsNullOrEmpty(catName) && !string.IsNullOrEmpty(catOwnerID) && !string.IsNullOrEmpty(catCode))
            {
                try
                {
                    if (InsertCatData(catName, catOwnerID, catCode))
                    {
                        MessageBox.Show("Data inserted successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Failed to insert data.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please enter name, cat owner ID, and cat code.");
            }
        }

        private bool InsertCatData(string catName, string catOwnerID, string catCode)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    string sql = "INSERT INTO cat (catOwnerID, catName, catCode) VALUES (@catOwnerID, @catName, @catCode)";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@catOwnerID", catOwnerID);
                        cmd.Parameters.AddWithValue("@catName", catName);
                        cmd.Parameters.AddWithValue("@catCode", catCode);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (MySqlException)
                {
                    return false;
                }
            }
        }
    }
}
