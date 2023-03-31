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
using SkiaSharp;
using System.Net;
using OpenAI;
using OpenAI.GPT3.ObjectModels.RequestModels;
using System.Web.Routing;


namespace AI_Poker_Assistance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<PlayerModel> players = new List<PlayerModel>();


        public enum PokerPosition
        {
            UTG1 = 0,
            UTG2 = 1,
            MP1 = 2,
            MP2 = 3,
            MP3 = 4,
            CO = 5,
            BTN = 6,
            SB = 7,
            BB = 8
        }

        public void CreatePlayersList()
        {

            string[] names = { "John", "Sarah", "Mike", "Emily", "David", "Emma", "Alex", "Olivia", "Daniel" };

            Random random = new Random();

            for (int i = 0; i < 9; i++)
            {
                PlayerModel player = new PlayerModel();

                player.Name = names[i];

                player.Position = Enum.GetValues(typeof(PokerPosition)).GetValue(i).ToString();
             
                string[] notes = { "Tight player", "Aggressive", "Calls too much", "Bluffs often", "Unknown player" };
                player.Notes = notes[random.Next(notes.Length)];

                player.Stack = random.Next(10, 26);

                players.Add(player);
            }


            var a = players;
            string filePath = @"D:\Users\vgol01\Documents\Visual Studio 2022\Project\AI Poker Assistance\history.txt";

            if (File.Exists(filePath))
            {
                // Do something
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write("");
                    writer.Write("Position" + '\t' + "Notes about player" + '\t' + "Stack (in Big blinds)");
                
                    foreach (var item in players)
                    {
                        writer.WriteLine();
                        writer.Write(item.Position + '\t');
                        writer.Write(item.Notes + '\t');
                        writer.Write(item.Stack);
                      //  Console.WriteLine("{0,-24} | {1,-11}", person.Name, person.Age);
                        writer.WriteLine();

                    }
                }
            }
         
        }




        string conversationId = null;
        private async void button1_Click(object sender, EventArgs e)
        {

            var apiKey = "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";

            var api = new OpenAI_API.OpenAIAPI(apiKey);
            var result = await api.Completions.GetCompletion("One Two Three One Two");
            label1.Text = result;
            Console.WriteLine(result);



            while (true)
            {
                // Запрашиваем текст для отправки на сервер OpenAI
                Console.Write("> ");
                string text = Console.ReadLine();

                // Создаем запрос к API OpenAI с использованием контекста разговора
              

                //// Отправляем запрос к API OpenAI
                //var result = api.Completions.Create(request);

                //// Получаем результат запроса
                //var responseText = result.Choices[0].Text;

                //// Сохраняем ID разговора из ответа API OpenAI
                //conversationId = result.GetContext()?.ConversationId;

                //// Выводим результат на экран
                //Console.WriteLine(responseText);








                //HttpClient Http = new HttpClient();
                //Http.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                //// JSON content for the API call


                //var jsonContent = new
                //{
                //    prompt = textBox1.Text,

                //    //context = savedcontext,
                //    //AccessibleRole = "assistant",
                //    model = "text-davinci-003",
                //    max_tokens = 1000
                //};


                //// Make the API call
                //var responseContent = await Http.PostAsync("https://api.openai.com/v1/completions", new StringContent(JsonConvert.SerializeObject(jsonContent), Encoding.UTF8, "application/json"));

                //// Read the response as a string
                //var resContext = await responseContent.Content.ReadAsStringAsync();

                //// Deserialize the response into a dynamic object
                //var apiData = JsonConvert.DeserializeObject<dynamic>(resContext);
                //label1.Text = apiData.choices[0].text;
                // savedcontext = apiData.choices[0].context;


            }
        }
        List<Card> TableCard = new List<Card>(5) { null, null, null, null, null };
        private const int NUM_COLUMNS = 13;
        private const int NUM_ROWS = 4;
        private const int IMAGE_SIZE = 50; // размер изображения
        private const int GAP_SIZE = 10; // промежуток между изображениями
        public void PanelClick(string stageOfGame, object PanelSender)
        {



            Form galleryForm = new Form();
            galleryForm.Size = new Size(750, 350);
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





                    Card card = MainDeck.cards[imageIndex];
                    PictureBox pictureBox = new PictureBox();


                    pictureBox.Size = new Size(imageWidth, imageHeight);
                    pictureBox.Location = new Point(col * (imageWidth + GAP_SIZE), row * (imageHeight + GAP_SIZE));
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox.ImageLocation = $"{MainDeck.cards[imageIndex].images.png}"; // замените на реальный URL-адрес изображения

                    galleryPanel.Controls.Add(pictureBox);

                    switch (stageOfGame)
                    {
                        case "flop":
                            {
                                pictureBox.Click += (sender, e) =>
                                {
                                    if (pictureBox.ImageLocation == "https://images.unsplash.com/photo-1533035353720-f1c6a75cd8ab?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mnx8Z3JheXxlbnwwfHwwfHw%3D&w=1000&q=80")
                                    {
                                        MessageBox.Show("ок?","на ебало себе нажми");
                                        return;
                                    }
                                    Panel panelsender = PanelSender as Panel;
                                    

                                    var controls = panelsender.Controls;
                                    int countOfBoard = 0;
                                    foreach (var a in controls)
                                    { 
                                        if (a is Label)
                                        { 
                                            Label label = a as Label;
                                            countOfBoard = Convert.ToInt32(label.Text.Substring(5, 1));


                                        }

                                    }
                                    using (var webClient = new WebClient())
                                    {
                                        var url = $"{card.images.png}";
                                        var imageStream = new MemoryStream(webClient.DownloadData(url));
                                        var image = Image.FromStream(imageStream);
                                        foreach (var maindeckcard in MainDeck.cards)
                                        {
                                            if (maindeckcard.code == card.code)
                                            {
                                                maindeckcard.images.png = "https://images.unsplash.com/photo-1533035353720-f1c6a75cd8ab?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mnx8Z3JheXxlbnwwfHwwfHw%3D&w=1000&q=80"; 
                                                //изменили картинку на неправильную
                                            }

                                        }
                                        panelsender.Name = card.code;
                                        panelsender.BackgroundImage = image;
                                        panelsender.BackgroundImageLayout = ImageLayout.Stretch;
                                        TableCard[countOfBoard] = card;
                                        galleryForm.Close();

                                    }
                                };


                            }
                            break;

                    }



                    imageIndex++;
                }
            }

            // Открываем галерею
            galleryForm.ShowDialog();


















        }




        private void AddPanels(int countPlayers)
        {
            int PlayerCount = 0;
            int panelWidth = 60;
            int panelHeight = 60; // высота
            int centerX = (this.ClientSize.Width / 2) - 80; // ширина
            int centerY = this.ClientSize.Height / 2; // высота
            double ovalWidth = 300;
            double ovalHeight = 200;

            for (int i = 0; i < countPlayers; i++)
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
                Playerlabel.Name = Playerlabel.Text;
               // panel.Name = Playerlabel.Text;
                Playerlabel.AutoSize = false;

                ButtonComboBox.Items.Add(Playerlabel.Text);
                HeroComboBox.Items.Add(Playerlabel.Text);
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
                    ButtonComboBox.Items.Remove(Playerlabel.Text);
                    HeroComboBox.Items.Remove(Playerlabel.Text);

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
            CreatePlayersList();
            this.Controls.Add(ButtonPictureBox);

            // Открываем файл на перезапись
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                // Перезаписываем содержимое файла пустой строкой
                sw.Write("");
            }
            AddPanels(9);
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

        private void panel2_Click(object sender, EventArgs e) // 0 в массива - 1 на столе
        {
            // Тут при повторному нажатии на возвращаем
            Panel panel = sender as Panel;

            if (panel.BackgroundImage != null)
            {

                foreach (var maindeckcard in MainDeck.cards)
                {
                    if (maindeckcard.code == panel.Name)
                    {
                        maindeckcard.images.png = maindeckcard.image;
                        //изменили картинку в выборе карты
                    }

                }
            }

            if (MainDeck != null)
                PanelClick("flop", sender);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

      


        private void panel3_Click(object sender, EventArgs e) // 1
        {
            Panel panel = sender as Panel;
           
            if (panel.BackgroundImage != null)
            {
                
                foreach (var maindeckcard in MainDeck.cards)
                {
                    if (maindeckcard.code == panel.Name)
                    {
                        maindeckcard.images.png = maindeckcard.image;
                        //изменили картинку в выборе карты
                    }

                }
            }
            if (MainDeck != null)
                PanelClick("flop", sender);
        }

        private void panel4_Click(object sender, EventArgs e) // 2
        {
            Panel panel = sender as Panel;

            if (panel.BackgroundImage != null)
            {

                foreach (var maindeckcard in MainDeck.cards)
                {
                    if (maindeckcard.code == panel.Name)
                    {
                        maindeckcard.images.png = maindeckcard.image;
                        //изменили картинку в выборе карты
                    }

                }
            }
            if (MainDeck != null)
                PanelClick("flop", sender);
        }

    

        private void panel3_DoubleClick(object sender, EventArgs e) // 3
        {
            var a = TableCard;
        }
        PictureBox ButtonPictureBox = new PictureBox()
        {
            SizeMode = PictureBoxSizeMode.StretchImage,
            Visible = true,
            ImageLocation = "https://cdn.imgbin.com/13/23/9/imgbin-texas-hold-em-poker-dealer-button-poker-table-cut-button-8zJLddzeXZ36QirEjPtrFqMUJ.jpg",
            Size = new Size(45, 45),
            Location = new Point(50, 100)
        };
        private void ButtonComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = ButtonComboBox.SelectedItem.ToString();
            int selectedIndex = ButtonComboBox.SelectedIndex;
            for (int i = selectedIndex + 1; i < ButtonComboBox.Items.Count && i <= selectedIndex + 2; i++)
            { // Определение СБ и ББ
                object nextItem = ButtonComboBox.Items[i];
               // int BBPlayer = Convert.ToInt32(nextItem.Split('P')[1]);

                // Делайте что-то с элементом "nextItem"
            }                         //Определение ББ


            // Делайте что-то с элементом "nextItem"

            // тут через ребёнка ищем родителя label->panel
            Control[] tbxs = this.Controls.Find(selectedItem, true);
            foreach (var label in tbxs)
            {
                Control control = label.Parent;
                Panel panel = control as Panel;
                if (tbxs != null && tbxs.Length > 0)
                {
                    textBox1.Text = "founded";
                    Point location = panel.Location;
                    if (location.Y > 200)
                    { // это что ниже
                        ButtonPictureBox.Location = new Point(panel.Location.X + 15, panel.Location.Y - ButtonPictureBox.Height - 2);
                        //Control[] panel = this.Controls.Find(selectedItem, true);

                    }
                    else
                    { // это что выше
                        ButtonPictureBox.Location = new Point(panel.Location.X + 10, panel.Location.Y + ButtonPictureBox.Height + 20);

                    }
                }

            }

            // IList<Xamarin.Forms.View> _list = this.Controls.; // Идем менять элементы формы

        }

        private void CountPlayer_TextChanged(object sender, EventArgs e)
        {
            int count = Convert.ToInt32(CountPlayer.Text);
            AddPanels(count);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LogWrite("---Flop---");
        }

        private void HeroComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
