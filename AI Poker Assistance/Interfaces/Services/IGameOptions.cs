using AI_Poker_Assistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Poker_Assistance.Interfaces.Services
{
    internal interface IGameOptions
    {
        //List<Cards> MainDeck;


    }

    abstract class AbstractGameOptions
    {

        public abstract void Call(double curCall);

        public abstract void Bet(double curBet);

        public abstract void Fold();

        public abstract void Check();


    }


}
