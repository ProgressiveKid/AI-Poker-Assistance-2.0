using AI_Poker_Assistance.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Poker_Assistance.Models
{
    public class GameSession
    {

        /// <summary>
        /// Текущий банк
        /// </summary>
        public double Bank { get; set; }


        /// <summary>
        /// Текущая макисмальная ставка по игре
        /// </summary>
        public double curMaxBet { get; set; }

        public double Ante { get; set; }
        public double SmallBlind { get; set; }
        public double BigBlaind { get; set; }
        public StreetsEnum curStreet { get; set; }
        //public Enum текущая улица
        // история раздачи
    }
}
