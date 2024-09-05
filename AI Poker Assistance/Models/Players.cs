using AI_Poker_Assistance.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Poker_Assistance.Models
{
    public class Players
    {
        public int IdPlayer { get; set; }
        public string NamePlayer { get; set; }

        public double StackPlayer { get; set; }


        /// <summary>
        /// Последня внесённая игроком ставка
        /// </summary>
        public double LastBet { get; set; }

        public Card[] CardsPlayer { get; set; }

        public EnumPostions Position {get;set;}


    }

  

}
