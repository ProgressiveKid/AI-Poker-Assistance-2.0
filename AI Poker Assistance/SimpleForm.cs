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
using AI_Poker_Assistance.Models;
using Svg;
using AI_Poker_Assistance.Models.Enums;
using AI_Poker_Assistance.Services;

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
     //   private DeckServise deckServise;
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
                    Card card = null;//MainDeck.cards[imageIndex];
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
        private List<Players> PlayerModelGenerator(DeckServise deckServise, int countPlayers) // Генерация игроков
        {
            List<Players> players = new List<Players>();
            Random random = new Random();
            string[] names = { "John", "Sarah", "Mike", "Emily", "David", "Emma", "Alex", "Olivia", "Daniel" };
            string[] notes = { "Tight player", "Aggressive", "Calls too much", "Bluffs often", "Unknown player" };
            // Можно прикрутить Entity Fraemwork

            for (int i = 0; i < countPlayers; i++)
            {
                Players player = new Players();                
                player.IdPlayer = i;
                player.NamePlayer = names[i];
                player.CardsPlayer = deckServise.GiveCardsFromDeck(2, ref MainDeckM);
                player.StackPlayer = random.Next(10, 26);
                player.Position = (EnumPostions)i;
                players.Add(player);
            }
            return players;
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

             foreach (Control control in this.Controls)
            {
                // Если нашли контрол с нужным TabIndex, возвращаем его
                if (control.TabIndex == indexPlayer && control is UserControl1)
                {
                    UserControl1 userC = (UserControl1)control as UserControl1;
                    userC.UserActions.Visible = true;
                    userC.indicatorOfTurn.Visible = true;
                    break;
                }
            }
        }


        private IEnumerable<Control> GetAllControls(Control parent)
        {
            // Собираем все текущие контролы и рекурсивно добавляем дочерние
            var controls = parent.Controls.Cast<Control>();

            // Используем SelectMany для объединения всех контролов с их дочерними
            return controls.SelectMany(ctrl => GetAllControls(ctrl)).Concat(controls);
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
            int nextIndexPlayer = MainPlayers.Select(u => u.IdPlayer).FirstOrDefault(u => u > idCurrentPlayer);

            if (MainGameSession.curStreet == StreetsEnum.PrefLop)
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
                NewAction(nextIndexPlayer);
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



        public SimpleForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Главная колода - она на всю игру
        /// TODO - может быть вынести получение значения колоды в отдельный класс, но пока для удобства это просто глоб переменная
        /// </summary>
        public static List<Card> MainDeckM; // колода
        public static List<Players> MainPlayers;// игроки
        public static GameSession MainGameSession;
        public static int CurIndexPlayer { get; set; } // указатель на игроков

        private async void SimpleForm_Load(object sender, EventArgs e)
        {
            // MakeApiDeck();
            MainGameSession = new GameSession() { Bank = 0, Ante = 0.5 , SmallBlind = 1, BigBlaind = 2, curMaxBet = 2 };
            DeckServise deckServise = new DeckServise();
            MainDeckM = await deckServise.CreateDeck();
            MainPlayers = PlayerModelGenerator(deckServise, 9);
            LoadPlayerPanel(ref MainPlayers);


            //LoadPlayerPanel(9);
            // playersInGame = players; // это буфер
            // TakeFromAllStartMoney();
        }

        public void LoadPlayerPanel(ref List<Players> listPlayers)
        {
            int panelHeight = this.ClientSize.Height / 9;
            int panelWidth = 300;
            // Создаем 9 панелей
            for (int i = 0; i < listPlayers.Count; i++)
            {
                UserControl1 userControl1 = new UserControl1();

                userControl1.Name = listPlayers[i].NamePlayer + " " + listPlayers[i].IdPlayer;
                userControl1.TabIndex = listPlayers[i].IdPlayer; // так не правильно но всё же
                userControl1.Location = new Point(0, i * panelHeight);
                userControl1.positionlabel.Text = listPlayers[i].Position.ToString();
                userControl1.stacklabel.Text = listPlayers[i].StackPlayer.ToString();
                userControl1.nametexbox.Text = listPlayers[i].NamePlayer;

                //userControl1.nametexbox.TextChanged += (sender, e) =>
                //{
                //    int idCurrentPlayer1 = Convert.ToInt32(userControl1.Name.Split(' ')[1]);
                //    int indexCurrentPlayer1 = 0;
                //    foreach (var a in playersInGame)
                //    {
                //        if (a.IDPlayer == idCurrentPlayer1)
                //        {
                //            indexCurrentPlayer1 = playersInGame.IndexOf(a);
                //        }
                //    }
                //    players[indexCurrentPlayer1].Name = userControl1.nametexbox.Text;
                //}; // Смена имени игрока
                //players[i].userControl = userControl1;

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
                    int indexCurrentPlayer = userControl1.TabIndex;

                    if (MainGameSession.curMaxBet > MainPlayers[indexCurrentPlayer].LastBet)
                    { // Были действия от нас раньше
                        double MoneyToCall = MainGameSession.curMaxBet - MainPlayers[indexCurrentPlayer].LastBet;
                        MainGameSession.Bank += MoneyToCall;
                        MainGameSession.curMaxBet = MoneyToCall;
                        MainPlayers[indexCurrentPlayer].StackPlayer -= MoneyToCall;
                        MainPlayers[indexCurrentPlayer].LastBet = MainGameSession.curMaxBet;
                        userControl1.currectbet.Text = MoneyToCall.ToString();
                        //MakeCall(indexCurrentPlayer, MoneyToCall);
                    }
                    else // Колим впервые
                    {
                        MainGameSession.Bank += MainGameSession.curMaxBet;
                        MainGameSession.curMaxBet = MainGameSession.curMaxBet;
                        MainPlayers[indexCurrentPlayer].StackPlayer -= MainGameSession.curMaxBet;
                        MainPlayers[indexCurrentPlayer].LastBet = MainGameSession.curMaxBet;
                        userControl1.currectbet.Text = MainGameSession.curMaxBet.ToString();
                        // MakeBet(indexCurrentPlayer, CurrentBet); // 
                    }
                    userControl1.currentactionlabel.Text = "CALL";
                    userControl1.stacklabel.Text = MainPlayers[indexCurrentPlayer].StackPlayer.ToString();
                    userControl1.UserActions.Visible = false;
                    userControl1.indicatorOfTurn.Visible = false;


                    int indexNextPlayer = GetNextPlayerOrNextStreet(indexCurrentPlayer);
                    NewAction(indexNextPlayer);

                };
                userControl1.foldbutton.Click += (sender, e) =>
                {
                    int indexCurrentPlayer = userControl1.TabIndex;
                    //  var playerForDelete = MainPlayers.Select(u => u).Where(u => u.IdPlayer == indexCurrentPlayer).FirstOrDefault();
                    MainPlayers[indexCurrentPlayer] = null;

                    userControl1.currentactionlabel.Text = "FOLD";
                    userControl1.UserActions.Visible = false;
                    userControl1.indicatorOfTurn.Visible = false;
                    userControl1.BackColor = Color.DarkGray;

                    int indexNextPlayer = GetNextPlayerOrNextStreet(indexCurrentPlayer);
                    NewAction(indexNextPlayer);
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
            }
        } // Загрузка интерфейса 


        //private void SimpleForm_Load(object sender, EventArgs e)
        //{
        //    MakeApiDeck();
        //    PlayerModelGenerator();
        //    LoadPlayerPanel(9);
        //    playersInGame = players; // это буфер
        //    TakeFromAllStartMoney();
        //}
        public int GetNextPlayerOrNextStreet(int curIndexPlayer)
        {
            // тут будет метод который вернёт действия следующему игроку или дальше даст улицу
            // метод сравнит все или игроки по очереди уровняли ставку - > если нет, то идём на следующего от актуального человека начиная по кругу
            // иначе даём новую улицу
            //AreUSuperLast(idCurrentPlayer);
            double curLastMaxBet = MainGameSession.curMaxBet;
            bool checkAllCall = MainPlayers.All(u => u.LastBet == curLastMaxBet);
            if (checkAllCall)
            {
                // меняем на новую улицу

                StreetsEnum currentStreet = MainGameSession.curStreet;
                StreetsEnum[] streets = (StreetsEnum[])Enum.GetValues(typeof(StreetsEnum));
                int currentIndex = Array.IndexOf(streets, currentStreet);
                // Проверяем, является ли текущий элемент последним
                if (currentIndex == streets.Length - 1)
                {
                    // всё шоудаун
                    return 1;

                }
                else
                {
                    // Определяем следующий индекс
                    MainGameSession.curStreet = (StreetsEnum)currentIndex+1;
                    MainGameSession.curMaxBet = 0;
                    var nextPlayer = MainPlayers.Select(u => u.IdPlayer).Min();
                    return nextPlayer;
                }
             
                   
            }
            else
            {
                // идём дальше по ходу 
                var nextPlayer = MainPlayers.Select(u => u).Where(u => u != null).FirstOrDefault(u => u.IdPlayer > curIndexPlayer && u.LastBet < curLastMaxBet)
                    ?? MainPlayers.FirstOrDefault(u => u.IdPlayer >= 0 && u.LastBet < curLastMaxBet);
                // получаем следующего пользователя который должен ходить
                return nextPlayer.IdPlayer;
            }
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
