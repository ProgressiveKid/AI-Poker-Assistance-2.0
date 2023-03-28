using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Poker_Assistance
{

    // Модель предложенная AI
    public class ApiData
    {
        public List<Choice> Choices { get; set; }
    }

    public class Choice
    {
        public string Text { get; set; }
        public string Index { get; set; }
        public float Logprobs { get; set; }
        public string FinishReason { get; set; }
        public string Context { get; set; }
        
    }
    
    // Модель из документации
    public class MessagesItem
    {

        /// <summary>
        /// 
        /// </summary>
        public string role { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string content { get; set; }
    }

   

    public class Root
    {
        public string model { get; set; }
        public List<Message> messages { get; set; }
        public string context { get; set; }
    }

    public class Message
    {
        public string role { get; set; }
        public string text { get; set; }
    }

    public class Card
    {
        public string code { get; set; }
        public string image { get; set; }
        public Images images { get; set; }
        public string value { get; set; }
        public string suit { get; set; }
    }

    public class Images
    {
        public string svg { get; set; }
        public string png { get; set; }
    }

    public class Deck
    {
        public bool success { get; set; }
        public string deck_id { get; set; }
        public List<Card> cards { get; set; }
        public int remaining { get; set; }
    }

}
