using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using Svg;
using SkiaSharp;

namespace AI_Poker_Assistance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }





        private async Task<string> SendAPIRequest(string prompt, string apiKey, string context)
        {
            // Create API request object
            var root = new Root
            {
                model = "text-davinci-002",
                messages = new List<Message>
                 {
                    new Message
                    {
                        role = "assistant",
                        text = prompt
                    }
                  },
                context = context
            };

            // Serialize request object to JSON
            var json = JsonConvert.SerializeObject(root);

            // Send POST request to API
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://api.openai.com/v1/engines/davinci-codex/completions", content);

                // Get response body
                var responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize response object
                var responseObject = JsonConvert.DeserializeObject<Root>(responseBody);

                // Save context for next API request
                context = responseObject.context;

                // Return response text
                return responseObject.messages.Last().text;
            }
        }


        private async void button1_Click(object sender, EventArgs e)
        {

            HttpClient Http = new HttpClient();
            var apiKey = "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";
            Http.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            // JSON content for the API call


            var jsonContent = new
            {
                prompt = textBox1.Text,
                //context = savedcontext,
                //AccessibleRole = "assistant",
                model = "text-davinci-003",
                max_tokens = 1000
            };


            // Make the API call
            var responseContent = await Http.PostAsync("https://api.openai.com/v1/completions", new StringContent(JsonConvert.SerializeObject(jsonContent), Encoding.UTF8, "application/json"));

            // Read the response as a string
            var resContext = await responseContent.Content.ReadAsStringAsync();

            // Deserialize the response into a dynamic object
            var apiData = JsonConvert.DeserializeObject<dynamic>(resContext);
            label1.Text = apiData.choices[0].text;
            // savedcontext = apiData.choices[0].context;


        }
        private const int NUM_COLUMNS = 13;
        private const int NUM_ROWS = 4;
        private const int IMAGE_SIZE = 50; // размер изображения
        private const int GAP_SIZE = 10; // промежуток между изображениями
        public void PanelClick(string stageOfGame)
        {
            switch (stageOfGame)
            {
                case "flop":
                    {
                        //    Form galleryForm = new Form()
                        //    {
                        //        Size = new Size(850, 600)
                        //    };
                        //    galleryForm.Text = "Image Gallery";
                        //    galleryForm.StartPosition = FormStartPosition.CenterScreen;

                        //    // Создаем панель для размещения изображений
                        //    Panel galleryPanel = new Panel();
                        //    galleryPanel.AutoScroll = true;
                        //    galleryPanel.Dock = DockStyle.Fill;
                        //    galleryForm.Controls.Add(galleryPanel);

                        //    // Заполняем панель изображениями
                        //    int imageIndex = 0;
                        //    for (int row = 0; row < NUM_ROWS; row++)
                        //    {
                        //        for (int col = 0; col < NUM_COLUMNS; col++)
                        //        {
                        //            if (imageIndex > 52) // Если заполнены все ячейки - выходим из цикла
                        //                break;

                        //            PictureBox pictureBox = new PictureBox();
                        //            pictureBox.Size = new Size(IMAGE_SIZE, IMAGE_SIZE);
                        //            pictureBox.Location = new Point(col * IMAGE_SIZE, row * IMAGE_SIZE);
                        //            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        //            galleryPanel.Controls.Add(pictureBox);

                        //            imageIndex++;
                        //        }
                        //    }

                        //    // Открываем галерею
                        //    galleryForm.ShowDialog();
                        //}

                        Form galleryForm = new Form();
                        galleryForm.Size = new Size(500, 300);
                        galleryForm.Text = "Image Gallery";
                        galleryForm.StartPosition = FormStartPosition.CenterScreen;

                        // Создаем панель для размещения изображений
                        Panel galleryPanel = new Panel();
                        galleryPanel.AutoScroll = true;
                        galleryPanel.Dock = DockStyle.Fill;
                        galleryForm.Controls.Add(galleryPanel);

                        // Вычисляем размеры изображений
                        int imageWidth = (int)Math.Floor((double)(galleryPanel.Width - (NUM_COLUMNS - 1) * GAP_SIZE) / NUM_COLUMNS);
                        int imageHeight = (int)Math.Floor((double)(galleryPanel.Height - (NUM_ROWS - 1) * GAP_SIZE) / NUM_ROWS);

                        // Заполняем панель изображениями
                        int imageIndex = 0;
                        for (int row = 0; row < NUM_ROWS; row++)
                        {
                            for (int col = 0; col < NUM_COLUMNS; col++)
                            {
                                if (imageIndex > 52) // Если заполнены все ячейки - выходим из цикла
                                    break;


                                var svgDocument = SvgDocument.Open("image.svg");

                                // Создание битмапа для отображения растровой версии SVG-изображения
                                var bitmap = new SKBitmap(100, 100);

                                // Создание рендерера для SVG-изображения
                             //   var renderer = new SKSvg();

                                // Рендеринг SVG-изображения на битмапе
                               // renderer.Render(svgDocument, bitmap);







                                PictureBox pictureBox = new PictureBox();
                                
                                pictureBox.Size = new Size(imageWidth, imageHeight);
                                pictureBox.Location = new Point(col * (imageWidth + GAP_SIZE), row * (imageHeight + GAP_SIZE));
                                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                                pictureBox.ImageLocation = $"{MainDeck.cards[imageIndex].images.svg}"; // замените на реальный URL-адрес изображения
                                galleryPanel.Controls.Add(pictureBox);

                                imageIndex++;
                            }
                        }

                        // Открываем галерею
                        galleryForm.ShowDialog();



                    }

                    break;
            
            }
        
        }




        private void AddPanels()
        {
            int PlayerCount = 0;
            int panelWidth = 60;
            int panelHeight = 60; // высота
            int centerX = (this.ClientSize.Width / 2)-45;
            int centerY = this.ClientSize.Height / 2;
            double ovalWidth = 300;
            double ovalHeight = 200;

            for (int i = 0; i < 9; i++)
            {
                PlayerCount++;
                Panel panel = new Panel();
                panel.Size = new Size(panelWidth, panelHeight);
                panel.BackColor = Color.Red;

                double angle = i * 2 * Math.PI / 9;
                double x = ovalWidth / 2 * Math.Cos(angle);
                double y = ovalHeight / 2 * Math.Sin(angle);

                double rx = x / Math.Sqrt(x * x / (ovalWidth * ovalWidth) + y * y / (ovalHeight * ovalHeight));
                double ry = y / Math.Sqrt(x * x / (ovalWidth * ovalWidth) + y * y / (ovalHeight * ovalHeight));

                int panelX = (int)rx + centerX - panelWidth / 2;
                int panelY = (int)ry + centerY - panelHeight / 2;

                Label Playerlabel = new Label();
                Playerlabel.Text = "P" + PlayerCount;
                Playerlabel.AutoSize = false;
                //  label.Location = new Point(panelWidth / 2 - label.Width / 2, panelHeight / 2 - label.Height / 2 - 20);
                panel.Controls.Add(Playerlabel);

                // Добавляем две кнопки на панель
                Button button1 = new Button();
                button1.Text = "Fold";
                // button1.FlatStyle = FlatStyle.Flat;
                button1.Size = new Size(20, 20);
                button1.Location = new Point(0, panelHeight - button1.Height);
                button1.Click += (sender, e) =>
                { //FOLD
                    Panel parentPanel = (Panel)((Button)sender).Parent;
                    this.Controls.Remove(parentPanel);
                    LogWrite("Player " + Playerlabel.Text[1] + " FOLD");

                };
                panel.Controls.Add(button1);

                Button button2 = new Button();
                button2.Text = "Option";
                button2.Size = new Size(20, 20);
                //  button2.FlatStyle = FlatStyle.Flat;

                button2.Location = new Point(panelWidth - button2.Width, panelHeight - button2.Height);
                button2.Click += (sender, e) =>
                {

                    Panel parentPanel = (Panel)((Button)sender).Parent;

                    Panel newPanel = new Panel();
                    newPanel.Size = new Size(100, 100);
                    newPanel.BackColor = Color.Green;

                    Button buttonCall = new Button();
                    buttonCall.Text = "Call";
                    buttonCall.Size = new Size(40, 25);
                    buttonCall.Location = new Point(10, 10);

                    Button buttonRaise = new Button();
                    buttonRaise.Text = "Raise";
                    buttonRaise.Size = new Size(40, 25);
                    buttonRaise.Location = new Point(50, 10);

                    TextBox textBox = new TextBox();
                    textBox.Size = new Size(80, 25);
                    textBox.Location = new Point(10, 45);
                    textBox.Visible = false;
                    textBox.KeyDown += new KeyEventHandler((sender3, args3) =>
                    {
                        if (args3.KeyCode == Keys.Enter)
                        { // RAISE
                            int SumMoney = Convert.ToInt32(textBox.Text);
                            panel.BackColor = Color.DarkRed;
                            Playerlabel.Text += "\n RAISE";
                            this.Controls.Remove(newPanel);
                            // Действия по добавлению в банк денег 
                            // Действия по удаление денег из стэка игрока
                        }
                    });



                    buttonCall.Click += (sender1, args) =>
                    { // CALL
                        buttonCall.Visible = false;
                        textBox.Visible = true;
                        this.Controls.Remove(newPanel);
                        panel.BackColor = Color.Yellow;
                        Playerlabel.Text += "\n CALL";

                        // Действия по добавлению в банк денег 
                        // Действия по удаление денег из стэка игрока
                    };

                    buttonRaise.Click += (sender2, args) =>
                    {
                        buttonRaise.Visible = false;
                        textBox.Visible = true;
                    };

                    newPanel.Controls.Add(buttonCall);
                    newPanel.Controls.Add(buttonRaise);
                    newPanel.Controls.Add(textBox);

                    // Устанавливаем координаты newPanel относительно координат родительской панели
                    newPanel.Location = new Point(parentPanel.Location.X + parentPanel.Width - newPanel.Width, parentPanel.Location.Y + parentPanel.Height);

                    this.Controls.Add(newPanel);

                };
                panel.Controls.Add(button2);

                panel.Location = new Point(panelX, panelY);

                this.Controls.Add(panel);
            }
            //foreach (Panel panel in this.Controls.OfType<Panel>())
            //{
            //    foreach (Button button in panel.Controls.OfType<Button>())
            //    {
            //        button.Click += (sender, args) =>
            //        {
            //            Panel parentPanel = (Panel)((Button)sender).Parent;
            //            this.Controls.Remove(parentPanel);
            //        };
            //    }
            //}
        }
        public static void LogWrite(string text)
        {

            string filePath = @"D:\Users\vgol01\Documents\Visual Studio 2022\Project\AI Poker Assistance\TextFile.txt";

            // Проверяем существование файла
            if (File.Exists(filePath))
            {
                // Файл существует, добавляем строку
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(text);
                }
            }
            else
            {
                // Файла не существует, создаем новый и добавляем строку
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine("Новая строка");
                }
            }

        }
        public Deck MainDeck; 
        public async void MakeApiDeck()
        {
            //генератор колоды
            HttpClient client1 = new HttpClient();
            HttpResponseMessage response1 = await client1.GetAsync("https://deckofcardsapi.com/api/deck/new/");
            string responseBody1 = await response1.Content.ReadAsStringAsync();
            Deck iddeck = JsonConvert.DeserializeObject<Deck>(responseBody1);
            


            // получение карт
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://deckofcardsapi.com/api/deck/{iddeck.deck_id}/draw/?count=52");
            string responseBody = await response.Content.ReadAsStringAsync();
            MainDeck = JsonConvert.DeserializeObject<Deck>(responseBody);

        }



        private void Form1_Load(object sender, EventArgs e)
        {
            string filePath = @"D:\Users\vgol01\Documents\Visual Studio 2022\Project\AI Poker Assistance\TextFile.txt";



            // Открываем файл на перезапись
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                // Перезаписываем содержимое файла пустой строкой
                sw.Write("");
            }
            AddPanels();
            MakeApiDeck();
            LogWrite("---Preflop---");
        }

      

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Click(object sender, EventArgs e)
        {
            if (MainDeck != null)
                PanelClick("flop");
        }
    }
}
