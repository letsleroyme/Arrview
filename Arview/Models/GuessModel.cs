using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arview.Models
{

    public class GuessModel
    {
        public List<ServerAnswer> Answers;
        public List<UserGuess> Guess;
        public GuessModel()
        {
            Answers = new List<ServerAnswer>();
            Guess = new List<UserGuess>();
        }

    }

    public class ServerAnswer
    {
        public string Text { get; set; }
    }

    public class UserGuess
    {
        public string Guesses { get; set; }
    }



}