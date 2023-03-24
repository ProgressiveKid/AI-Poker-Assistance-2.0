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

        private static HttpClient Http = new HttpClient();

        private async void button1_Click(object sender, EventArgs e)
        {
            //  var apiKey = "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";

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
    }
}
