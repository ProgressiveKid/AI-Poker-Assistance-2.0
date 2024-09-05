using AI_Poker_Assistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Poker_Assistance.Interfaces.Services
{
    internal interface IDeckServise
    {
        //List<Cards> MainDeck;
        Task<List<Card>> CreateDeck();

        Card[] GiveCardsFromDeck(int countCards, ref List<Card> currentDeck);


    }
}
