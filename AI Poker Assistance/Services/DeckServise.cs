using AI_Poker_Assistance.Interfaces.Services;
using AI_Poker_Assistance.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AI_Poker_Assistance.Services
{
    internal class DeckServise : IDeckServise
    {
        public async Task<List<Card>> CreateDeck()
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
                Deck deckForReturn = JsonConvert.DeserializeObject<Deck>(responseBody);
                return deckForReturn.cards;
             //Генерируем колоды в само начале работы
        }

        public Card[] GiveCardsFromDeck(int countCards, ref List<Card> currentDeck)
        {
            Card[] cardArray = new Card[countCards];
            Random random = new Random();
            for (int i = 0; i < countCards; i ++)
            {
                int randomIndex = random.Next(currentDeck.Count);
                cardArray[i] = currentDeck[randomIndex];
                currentDeck.RemoveAt(randomIndex);
            }
            return cardArray;
        }
    }
}
