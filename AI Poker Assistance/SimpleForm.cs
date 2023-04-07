using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsControlLibrary;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
namespace AI_Poker_Assistance
{
    public partial class SimpleForm : Form
    {
        List<Card> TableCard = new List<Card>(5) { null, null, null, null, null };
        List<Card> PlayerCard = new List<Card>(2) { null, null };
        public enum PokerPosition
        {
            BTN = 8,
            CO = 7,
            MP3 = 6,
            MP2 = 5,
            MP1 = 4,
            UTG2 = 3,
            UTG1 = 2,
            BB = 1,
            SB = 0,
            //UTG1 = 8,
            //UTG2 = 7,
            //MP1 = 6,
            //MP2 = 5,
            //MP3 = 4,
            //CO = 3,
            //BTN = 2,
            //BB = 1,
            //SB = 0,
        }
        public PokerStreet CurrentStreet = PokerStreet.Preflop;
        public enum PokerStreet
        {
            Preflop = 0,
            Flop = 1,
            Turn = 2,
            River = 3
        }
        public Deck MainDeck;
        private const int NUM_COLUMNS = 13;
        private const int NUM_ROWS = 4;
        private const int IMAGE_SIZE = 50; // размер изображения
        private const int GAP_SIZE = 10; // промежуток между изображениями
        List<PlayerModel> players = new List<PlayerModel>();
        List<PlayerModel> playersInGame;
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
        } //Генерируем колоды в само начале работы
        public void PanelPicturesClick(object PanelSender)
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
                    pictureBox.Click += (sender, e) =>
                    {
                        if (pictureBox.ImageLocation == "https://images.unsplash.com/photo-1533035353720-f1c6a75cd8ab?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mnx8Z3JheXxlbnwwfHwwfHw%3D&w=1000&q=80")
                        {
                            MessageBox.Show("ок?", "на ебало себе нажми");
                            return;
                        }
                        PictureBox panelsender = PanelSender as PictureBox;
                        //var controls = panelsender.Controls;
                        //int countOfBoard = 0;
                        //foreach (var a in controls)
                        //{
                        //    if (a is Label)
                        //    {
                        //        Label label = a as Label;
                        //        countOfBoard = Convert.ToInt32(label.Text.Substring(5, 1));
                        //    }
                        //}
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
                            //panelsender.Name = card.code;
                            string name = panelsender.Name;
                            int countOfBoard = Convert.ToInt32(name.Substring(10, 1));
                            if (countOfBoard == 6 || countOfBoard == 7)
                            {
                                if (countOfBoard == 6)
                                PlayerCard[0] = card;
                                else
                                PlayerCard[1] = card;


                            }
                            else
                            {
                                TableCard[countOfBoard - 1] = card;


                            }
                            panelsender.BackgroundImage = image;
                            panelsender.BackgroundImageLayout = ImageLayout.Stretch;
                            galleryForm.Close();
                        }
                    };
                    imageIndex++;
                }
            }
            // Открываем галерею
            galleryForm.ShowDialog();
        } // Метод отрисовки миркоформы для выбора кард
        public void PlayerModelGenerator() // Генерация игроков
        {
            Random random = new Random();
            string[] names = { "John", "Sarah", "Mike", "Emily", "David", "Emma", "Alex", "Olivia", "Daniel" };
            string[] notes = { "Tight player", "Aggressive", "Calls too much", "Bluffs often", "Unknown player" };
            // Можно прикрутить Entity Fraemwork
            for (int i = 0; i < 9; i++)
            {
                PlayerModel player = new PlayerModel();
                player.IDPlayer = i;
                player.Name = names[i];
                // player.Position = Enum.GetValues(typeof(PokerPosition)).GetValue(i).ToString();
                player.Notes = notes[random.Next(notes.Length)];
                player.Stack = random.Next(10, 26);
                players.Add(player);
            }
        }
        public double CurrentBet { get; set; }
        public double CurrentBank { get; set; }
        public void MakeCall(int id, double bet)
        {
            CurrentBank += bet;
            playersInGame[id].LastBet = CurrentBet;
            playersInGame[id].Stack -= bet;
           // int idWhoInGame = playersInGame[id].IDPlayer;
            //players[idWhoInGame].Stack -= bet;
            CurrentBankLabel.Text = "CuurentBank: " + CurrentBank;
        }
        public void MakeBet(int id, double bet)
        { //деньги минусуются из текущей раздачи и всего стэка
          // checkBet(bet); // проверка не повысили ли ставку
            if (bet > CurrentBet)
            {
                CurrentBet = bet;
            }
            CurrentBank += CurrentBet;
            playersInGame[id].LastBet = CurrentBet;
            playersInGame[id].Stack -= CurrentBet;
            int idWhoInGame = playersInGame[id].IDPlayer;
            //players[idWhoInGame].Stack -= bet;
            CurrentBankLabel.Text = "CuurentBank: " + CurrentBank;
        }
        private void GetAllControl(Control.ControlCollection c)
        {
            UserControlList.Clear();
            foreach (UserControl1 control in c)
            {
                if (control.BackColor != Color.DarkGray)
                {
                    UserControlList.Add(control);
                }
                // GetAllControl(control, list);
            }
        }
        List<UserControl1> UserControlList = new List<UserControl1>();
        public void NewAction(int indexPlayer)
        {
            // UserControl1 userControl1 = (UserControl1)(this.Controls.Find("UserControl " + playersInGame[indexPlayer].IDPlayer, true)).First();
            UserControl1 userControl1 = playersInGame[indexPlayer].userControl as UserControl1;
            userControl1.UserActions.Visible = true;
            userControl1.indicatorOfTurn.Visible = true;
        }
        public void ActionPlayer(int idPlayer) // Работает на прфелоп
        {
            if (CurrentStreet == PokerStreet.Preflop)
            {
                int nextPlayer = ++idPlayer;
                foreach (var a in playersInGame)
                {
                    if (a.IDPlayer == idPlayer)
                    {
                        // this.Controls controlCollection = 
                        idPlayer = playersInGame.IndexOf(a);
                    }
                }
                int LastIndexPlayer = playersInGame.Count - 1;
                if (nextPlayer <= playersInGame[LastIndexPlayer].IDPlayer && idPlayer != 1)
                {
                    foreach (var a in playersInGame)
                    {
                        if (a.IDPlayer == nextPlayer)
                        {
                            UserControl1 userControl1 = (UserControl1)(this.Controls.Find("UserControl " + a.IDPlayer, true)).First();
                            userControl1.UserActions.Visible = true;
                            userControl1.indicatorOfTurn.Visible = true;
                            break;
                        }
                    }
                }
                else
                {
                    if (nextPlayer == 1)
                    {
                        UserControl1 userControl1 = (UserControl1)(this.Controls.Find("UserControl " + nextPlayer, true)).First();
                        userControl1.UserActions.Visible = true;
                        userControl1.indicatorOfTurn.Visible = true;
                        CurrentStreet = PokerStreet.Flop;
                        GetAllControl(Controls);
                        //Последний на префлопе
                        // Проверяем ставки 
                        // Даём возможность последний раз отрисовать эту улицу
                    }
                    else
                        ActionPlayer(-1);
                }
            }
            else if (CurrentStreet == PokerStreet.Flop)
            {
            }
        }
        public void TakeFromAllStartMoney()
        {
            CurrentBet = 0;
            CurrentBank = 0;
            for (int i = 0; i < playersInGame.Count; i++) // ТУТ I это колв
            {
                if (playersInGame[i].Position == PokerPosition.SB.ToString())
                {
                    MakeBet(i, 0.5);
                }
                if (playersInGame[i].Position == PokerPosition.BB.ToString())
                {
                    MakeBet(i, 1);
                }
                else
                {
                    //  MakeBet(i,0.1); // АНТЕ
                }
            }
        }
        public int HightId = 9;
        public void AreUSuperLast(int idCurrentPlayer)
        {
            if (CurrentStreet == PokerStreet.Preflop)
            {
                //Это касается префлопа
                int currentIndex = 0;

                int nextCurrentPlayer = ++idCurrentPlayer;
               
                if (nextCurrentPlayer >= HightId)
                {
                    //Это уже пошли блайнды
                    nextCurrentPlayer = 0;
                }
                else
                { // Доходим до последнего
                    for (int i = 0; i < playersInGame.Count; i++)
                    { // тут находим существующий следующйи элемент
                        if (playersInGame[i].IDPlayer == nextCurrentPlayer)
                        {
                            currentIndex = playersInGame.IndexOf(playersInGame[i]);
                            break;
                        }
                    }
                }
                if (nextCurrentPlayer == 2)
                {
                    //Это ББ - он ходит последним на префлопе
                    MessageBox.Show("DAVAI FLOP", "DA");
                    bool AllCall = false;
                    foreach (var a in playersInGame)
                    {
                        if (a.LastBet == CurrentBet)
                        {
                            AllCall = true;
                        }
                        else {

                            AllCall = false;
                        
                        }
                    }
                    CurrentBet = 0;
                    CurrentStreet = PokerStreet.Flop;
                    foreach (var a in playersInGame)
                    {
                        a.LastBet = 0;
                    }
                    currentIndex = 0;


                }
                NewAction(currentIndex);
            }
        }
        public bool AreULast(int id)
        {
            bool AllPlayerBet = false;
            foreach (var a in playersInGame)
            {
                if (a.LastBet == CurrentBet)
                {
                    if (a.Position == PokerPosition.BB.ToString() && CurrentStreet == PokerStreet.Preflop)
                    {
                        AllPlayerBet = false;
                    }
                    AllPlayerBet = true;
                }
                else
                {
                    AllPlayerBet = false;
                    break;
                }
            }
            if (AllPlayerBet)
            {
                MessageBox.Show("DAVAI FLOP", "DA");
                CurrentBet = 0;
                CurrentStreet = PokerStreet.Flop;
                foreach (var a in playersInGame)
                {
                    a.LastBet = 0;
                }
                int idFirstPlayer = 0;
                foreach (var a in playersInGame)
                {
                    idFirstPlayer = a.IDPlayer;
                    break;
                }
                ActionPlayer(idFirstPlayer);
                //Возвращать певрого игрока который будет ходиьт на флопе
            }
            else
            {
                // MessageBox.Show("PLATE ESHE", "NET");
                ActionPlayer(id);
            }
            return AllPlayerBet;
        }
        public void LoadPlayerPanel(int countPlayer)
        {
            int panelHeight = this.ClientSize.Height / 9;
            int panelWidth = 300;
            // Создаем 9 панелей
            for (int i = 0; i < 9; i++)
            {
                UserControl1 userControl1 = new UserControl1();

                userControl1.Name = "UserControl " + players[i].IDPlayer;
                userControl1.Location = new Point(0, i * panelHeight);
                userControl1.positionlabel.Text = Enum.GetValues(typeof(PokerPosition)).GetValue(i).ToString();
                players[i].Position = Enum.GetValues(typeof(PokerPosition)).GetValue(i).ToString();
                userControl1.stacklabel.Text = players[i].Stack.ToString();
                userControl1.nametexbox.Text = players[i].Name;
                
                userControl1.nametexbox.TextChanged += (sender, e) =>
                {
                    int idCurrentPlayer1 = Convert.ToInt32(userControl1.Name.Split(' ')[1]);
                    int indexCurrentPlayer1 = 0;
                    foreach (var a in playersInGame)
                    {
                        if (a.IDPlayer == idCurrentPlayer1)
                        {
                            indexCurrentPlayer1 = playersInGame.IndexOf(a);
                        }
                    }
                    players[indexCurrentPlayer1].Name = userControl1.nametexbox.Text;
                }; // Смена имени игрока
                players[i].userControl = userControl1;
                if (i == 2)
                {
                    userControl1.UserActions.Visible = true;
                    userControl1.indicatorOfTurn.Visible = true;
                } // Видимость кнопок по ходу игры
                else
                {
                    userControl1.UserActions.Visible = false;
                }
                userControl1.callbutton.Click += (sender, e) =>
                {
                    int idCurrentPlayer = Convert.ToInt32(userControl1.Name.Split(' ')[1]);
                    int indexCurrentPlayer = 0;
                    foreach (var a in playersInGame)
                    {
                        if (a.IDPlayer == idCurrentPlayer)
                        {
                            indexCurrentPlayer = playersInGame.IndexOf(a);
                        }
                    }
                    if (CurrentBet > playersInGame[indexCurrentPlayer].LastBet)
                    { // Были действия от нас раньше
                        double MoneyToCall = CurrentBet - playersInGame[indexCurrentPlayer].LastBet;
                        MakeCall(indexCurrentPlayer, MoneyToCall);
                    }
                    else // Колим впервые
                    {
                        MakeBet(indexCurrentPlayer, CurrentBet); // 
                    }
                    userControl1.currentactionlabel.Text = "CALL";
                    userControl1.currectbet.Text = CurrentBet.ToString();
                    userControl1.stacklabel.Text = playersInGame[indexCurrentPlayer].Stack.ToString();
                    userControl1.UserActions.Visible = false;
                    userControl1.indicatorOfTurn.Visible = false;
                    AreUSuperLast(idCurrentPlayer);
                };
                userControl1.foldbutton.Click += (sender, e) =>
                {
                    int ii = Convert.ToInt32(userControl1.Name.Split(' ')[1]);
                    foreach (var a in playersInGame)
                    {
                        if (a.IDPlayer == ii)
                        {
                            playersInGame.Remove(a);
                            break;
                        }
                    }
                    foreach (var a in playersInGame)
                    {
                        if (a.IDPlayer > HightId)
                        {
                            HightId = a.IDPlayer;
                        }
                    }
                    userControl1.currentactionlabel.Text = "FOLD";
                    userControl1.UserActions.Visible = false;
                    userControl1.indicatorOfTurn.Visible = false;
                    userControl1.BackColor = Color.DarkGray;
                    AreUSuperLast(ii);
                };
                userControl1.raisebutton.Click += (sender, e) =>
                {
                    userControl1.raisesumTextBox.Focus();
                    userControl1.raisesumTextBox.Visible = true;
                    userControl1.raisesumTextBox.KeyDown += new KeyEventHandler((sender3, args3) =>
                    {
                        if (args3.KeyCode == Keys.Enter)
                        { // RAISE
                            int idCurrentPlayer = Convert.ToInt32(userControl1.Name.Split(' ')[1]);
                            int indexCurrentPlayer = 0;
                            foreach (var a in playersInGame)
                            {
                                if (a.IDPlayer == idCurrentPlayer)
                                {
                                    indexCurrentPlayer = playersInGame.IndexOf(a);
                                }
                            }
                            double bet = Convert.ToDouble(userControl1.raisesumTextBox.Text);
                            MakeBet(indexCurrentPlayer, bet); // 
                            userControl1.currentactionlabel.Text = "RAISE";
                            userControl1.currectbet.Text = CurrentBet.ToString();
                            userControl1.stacklabel.Text = playersInGame[indexCurrentPlayer].Stack.ToString();
                            AreUSuperLast(indexCurrentPlayer);
                            userControl1.UserActions.Visible = false;
                            userControl1.indicatorOfTurn.Visible = false;
                            // Действия по добавлению в банк денег 
                            // Действия по удаление денег из стэка игрока
                        }
                    });
                    userControl1.callbutton.Visible = false;
                    userControl1.foldbutton.Visible = false;
                };
                Controls.Add(userControl1);
                //Panel panel = new Panel();
                //panel.Size = new Size(panelWidth, panelHeight);
                //panel.Location = new Point(0, i * panelHeight);
                //panel.BorderStyle = BorderStyle.FixedSingle;
                //panel.Name = i.ToString();
                //Controls.Add(panel);
                //// Добавляем label
                //Label PositionLabel = new Label(); 
                //PositionLabel.Text = Enum.GetValues(typeof(PokerPosition)).GetValue(i).ToString();
                //players[i].Position = Enum.GetValues(typeof(PokerPosition)).GetValue(i).ToString();
                //PositionLabel.Location = new Point(10, panelHeight / 2 - PositionLabel.Height / 2);
                //panel.Controls.Add(PositionLabel);
                //// Добавляем кнопку NOTES
                //Button NotesButton = new Button();
                //NotesButton.Text = "NOTES";
                //NotesButton.Location = new Point(PositionLabel.Right, PositionLabel.Top+20 + PositionLabel.Height / 2 - NotesButton.Height / 2);
                //panel.Controls.Add(NotesButton);
                ////Имена игроков
                //TextBox NamePlayerTextBox = new TextBox();
                //NamePlayerTextBox.Location = new Point(PositionLabel.Right, PositionLabel.Top - 10 + PositionLabel.Height / 2 - NotesButton.Height / 2);
                //NamePlayerTextBox.Name = players[i].Name;
                //NamePlayerTextBox.Text = players[i].Name;
                //panel.Controls.Add(NamePlayerTextBox);
                ////Stack
                ////Label StackLabel = new Label();
                ////StackLabel.Text = players[i].Stack.ToString();
                ////NamePlayerTextBox.Location = new Point(PositionLabel.Right+NamePlayerTextBox.Width + 10, PositionLabel.Top + 20 + PositionLabel.Height / 2 - NotesButton.Height / 2);
                ////StackLabel.Name = players[i].Name + " " + players[i].Stack;
                ////panel.Controls.Add(StackLabel);
            }
        } // Загрузка интерфейса 
        public SimpleForm()
        {
            InitializeComponent();
        }
        BindingSource bindingSource = new BindingSource();
        private void SimpleForm_Load(object sender, EventArgs e)
        {
            MakeApiDeck();
            PlayerModelGenerator();
            LoadPlayerPanel(9);
            playersInGame = players; // это буфер
            TakeFromAllStartMoney();
        }
        private void CurrentBankLabel_Click(object sender, EventArgs e)
        {
        }
        private void CurrentBankLabel_TextChanged(object sender, EventArgs e)
        {
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PanelPicturesClick(sender);
        }
        private async void GetInfoButton_Click(object sender, EventArgs e)
        {
            var apiKey = "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";
            HttpClient Http = new HttpClient();
            Http.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            // JSON content for the API call
            var jsonContent = new
            {
                prompt = "почему у меня не прогружается сайт бота ChatGPT?",
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
            // label1.Text = apiData.choices[0].text;
            //savedcontext = apiData.choices[0].context;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            PanelPicturesClick(sender);
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            PanelPicturesClick(sender);
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            PanelPicturesClick(sender);
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            PanelPicturesClick(sender);
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            PanelPicturesClick(sender);
        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            PanelPicturesClick(sender);
        }
    }
}
