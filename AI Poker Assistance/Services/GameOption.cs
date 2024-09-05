using AI_Poker_Assistance.Interfaces.Services;
using AI_Poker_Assistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace AI_Poker_Assistance.Services
{
    class GameOption : AbstractGameOptions
    {
        GameSession _curSession;
        Players _player; 
        public GameOption(ref GameSession curSession, Players player = null)
        {
           _curSession = curSession;
           _player = player;
        }

        public override void Bet(double curBet)
        {
            _curSession.Bank += curBet;
            _curSession.curMaxBet = curBet;
            _player.StackPlayer -= curBet;
        }

        public override void Call(double curBet)
        {
            _curSession.Bank += curBet;
            _curSession.curMaxBet = curBet;
            _player.StackPlayer -= curBet;


            throw new NotImplementedException();
        }

        public override void Check()
        {
        }

        public override void Fold()
        {

        }
    }
}
