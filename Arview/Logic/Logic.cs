using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Arview.Models;

namespace Arview.Logic
{
    public class Logic
    {
        public List<string> AnswerNo;

        public static string SetWord()
        {
            Random rnd = new Random();
            if (Words.WordsList.Count == 0)
            {
                return string.Empty;
            }
            return Words.WordsList[rnd.Next(0, Words.WordsList.Count)];
        }


        public Logic(string wordGuess, string wordResult)
        {
            AnswerNo = new List<string>() { "Попробуй еще раз", "Не в этот раз", "Неправильно", "Мда, может быть хватит?" };

            AnswerNo.Add(LowerOrGreater(wordGuess, wordResult));
            AnswerNo.Add(WordEndLetter(wordResult));
            AnswerNo.Add(WordStartLetter(wordResult));
            AnswerNo.Add(LevensteinResultString(wordGuess, wordResult));

        }


        public string LowerOrGreater(string word1, string word2)
        {
            return word1.Length < word2.Length ? "Загаданногое слово длинее" : "Загаданногое слово короче";
        }

        public string WordStartLetter (string word)
        {
            return $"Слово начинается на {word[0]}";
        }

        public string WordEndLetter (string word)
        {
            return $"Слово заканчивается на {word[word.Length - 1]}";
        }


        static int Minimum(int a, int b, int c) => (a = a < b ? a : b) < c ? a : c;

        public static int Levenstein(string firstWord, string secondWord)
        {
            var n = firstWord.Length + 1;
            var m = secondWord.Length + 1;
            var matrixD = new int[n, m];

            const int deletionCost = 1;
            const int insertionCost = 1;

            for (var i = 0; i < n; i++)
            {
                matrixD[i, 0] = i;
            }

            for (var j = 0; j < m; j++)
            {
                matrixD[0, j] = j;
            }

            for (var i = 1; i < n; i++)
            {
                for (var j = 1; j < m; j++)
                {
                    var substitutionCost = firstWord[i - 1] == secondWord[j - 1] ? 0 : 1;

                    matrixD[i, j] = Minimum(matrixD[i - 1, j] + deletionCost,          // удаление
                                            matrixD[i, j - 1] + insertionCost,         // вставка
                                            matrixD[i - 1, j - 1] + substitutionCost); // замена
                }
            }

            return matrixD[n - 1, m - 1];
        }

        public string LevensteinResultString(string word1, string word2)
        {
            int res = Levenstein(word1, word2);
            return $"Расстояние Левенштейна равно {res}";
        }
    }
}