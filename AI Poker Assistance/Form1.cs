using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;

namespace AI_Poker_Assistance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //private static HttpClient Http = new HttpClient();

        private async void button1_Click(object sender, EventArgs e)
        {
            //  var apiKey = "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";
            HttpClient Http = new HttpClient();
            var apiKey = "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";
            Http.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            // JSON content for the API call
            var jsonContent = new
            {
                prompt = textBox1.Text,
                model = "text-davinci-003",
                max_tokens = 1000
            };

            // Make the API call
            var responseContent = await Http.PostAsync("https://api.openai.com/v1/completions", new StringContent(JsonConvert.SerializeObject(jsonContent), Encoding.UTF8, "application/json"));

            // Read the response as a string
            var resContext = await responseContent.Content.ReadAsStringAsync();

            // Deserialize the response into a dynamic object
            var data = JsonConvert.DeserializeObject<dynamic>(resContext);
            label1.Text = data.choices[0].text;


        }
       
        private void AddPanels()
        {
            int PlayerCount = 0;
            int panelWidth = 60;
            int panelHeight = 60; // высота
            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;
            double ovalWidth = 300;
            double ovalHeight = 220;

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
                Playerlabel.Text = "P"+PlayerCount;
                Playerlabel.AutoSize = false;
              //  label.Location = new Point(panelWidth / 2 - label.Width / 2, panelHeight / 2 - label.Height / 2 - 20);
                panel.Controls.Add(Playerlabel);

                // Добавляем две кнопки на панель
                Button button1 = new Button();
                button1.Text = "Fold";
                button1.Size = new Size(20, 20);
                button1.Location = new Point(0, panelHeight - button1.Height);
                button1.Click += (sender, e) =>
                {
                    Panel parentPanel = (Panel)((Button)sender).Parent;
                    this.Controls.Remove(parentPanel);

                };
                panel.Controls.Add(button1);

                Button button2 = new Button();
                button2.Text = "Option";
                button2.Size = new Size(20, 20);
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
                        {
                            int SumMoney = Convert.ToInt32(textBox.Text);
                            panel.BackColor = Color.DarkRed;
                            Playerlabel.Text += "\n RAISE";
                            this.Controls.Remove(newPanel);
                            // Действия по добавлению в банк денег 
                            // Действия по удаление денег из стэка игрока
                        }
                    });

                    //textBox.MouseLeave += (clickedsender, e) =>
                    //{
                    //    if (textBox.Focused)
                    //    {
                    //        int SumMoney = Convert.ToInt32(textBox.Text); 
                    //        // Действия по добавлению в банк денег 
                    //        // Действия по удаление денег из стэка игрока
                    //    }

                    //};

                    buttonCall.Click += (sender1, args) =>
                    {
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

        private void Form1_Load(object sender, EventArgs e)
        {
            AddPanels();
        }
    }
}
