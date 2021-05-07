using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Arview.Models;

using System.Web.Optimization;
using System.Web.Routing;

namespace Arview.Controllers
{
    public class HomeController : Controller
    {
        Logic.Logic logic;
        static Random rnd = new Random();
        string answer;
        GuessModel model = new GuessModel();



        public ActionResult Index()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Send(string guess)
        {
            System.Web.HttpContext.Current.Application.Lock();
            answer = System.Web.HttpContext.Current.Application["Answer"].ToString();
            System.Web.HttpContext.Current.Application.UnLock();
            if (guess == null)
            {
                throw new Exception("Пришла пустая строка");
            }
            string GuessToCompare = guess.Trim().ToLower();
            string AnswerToCompare = answer.Trim().ToLower();
            logic = new Logic.Logic(guess, answer);
            model.Guess.Add(new UserGuess() { Guesses = guess });




            if (GuessToCompare == AnswerToCompare)
            {
                Words.WordsList.Remove(answer);
                if (Words.WordsList.Count == 0 || answer == string.Empty)
                    model.Answers.Add(new ServerAnswer() { Text = "Поздравляю! Ты прошел игру. Иди займись чем нибудь полезным" });
                else
                    model.Answers.Add(new ServerAnswer() { Text = "Поздравляю! Ты угадал это слово, а теперь я придумал новое." });

                System.Web.HttpContext.Current.Application.Lock();
                System.Web.HttpContext.Current.Application["Answer"] = Words.WordsList[rnd.Next(0, Words.WordsList.Count)];
                System.Web.HttpContext.Current.Application.UnLock();
            }
            else
                model.Answers.Add(new ServerAnswer() { Text = logic.AnswerNo[rnd.Next(0, logic.AnswerNo.Count)] });

            

            return PartialView(model);
        }
    }
}