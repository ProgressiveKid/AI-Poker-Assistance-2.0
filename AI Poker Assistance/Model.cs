﻿using AI_Poker_Assistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsControlLibrary;

namespace AI_Poker_Assistance
{

    // Модель предложенная AI





    public class PlayerModel
    {
        public int IDPlayer { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }

        public string Notes { get; set; }

        public double Stack { get; set; }

        public string PanelName { get; set; }

        public double LastBet { get; set; }

        public UserControl1 userControl { get; set; }
    }

    public class CardAA
    {
        public string code { get; set; }
        public string image { get; set; }
        public Images images { get; set; }
        public string value { get; set; }
        public string suit { get; set; }
    }

    public class ImagesAA
    {
        public string svg { get; set; }
        public string png { get; set; }
    }

    public class DeckAAA
    {
        public bool success { get; set; }
        public string deck_id { get; set; }
        public List<Card> cards { get; set; }
        public int remaining { get; set; }
    }

}
