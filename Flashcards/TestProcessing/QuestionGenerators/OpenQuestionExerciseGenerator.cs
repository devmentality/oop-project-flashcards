﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Flashcards.QuestionGenerators
{
    public class OpenQuestionExerciseGenerator : IExerciseGenerator
    {
        public int RequiredAmountOfCards => 1;

        public string GetTypeCaption()
        {
            return "open";
        }

        public Exercise GenerateExerciseFrom(IList<Card> cards)
        {
            if (cards.Count != RequiredAmountOfCards)
                throw new ArgumentException("Invalid amount of cards");

            var card = cards.First();
            return new Exercise(
                new OpenAnswer(card.Term), 
                new OpenAnswerQuestion(card.Definition));
        }
    }
}