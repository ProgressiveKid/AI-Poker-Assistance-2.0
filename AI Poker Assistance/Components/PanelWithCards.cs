using AI_Poker_Assistance.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace WindowsFormsControlLibrary.Components
{
    public partial class PanelWithCards : UserControl
    {
        public PanelWithCards(Card card1, Card card2, string playerName)
        {
            InitializeComponent();
            var url1 = $"{card1.images.png}";
            var url2 = $"{card2.images.png}";
            // Используем асинхронный метод для загрузки изображений
            LoadImagesAsync(url1, url2, playerName);
        }
        private void PanelWithCards_Load(object sender, EventArgs e)
        {

        }
        private async void LoadImagesAsync(string url1, string url2, string playerName)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Параллельная загрузка изображений
                    var imageTasks = new[]
                    {
                        LoadImageAsync(httpClient, url1),
                        LoadImageAsync(httpClient, url2)
                    };

                    var images = await Task.WhenAll(imageTasks);

                    // Обновление UI на основном потоке
                  
                        pictureBox1.BackgroundImage = images[0];
                        pictureBox2.BackgroundImage = images[1];
                        labelPlayer.Text = playerName;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке изображений: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<Image> LoadImageAsync(HttpClient httpClient, string url)
        {
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var imageStream = await response.Content.ReadAsStreamAsync();
            return Image.FromStream(imageStream);
        }
    }


}
