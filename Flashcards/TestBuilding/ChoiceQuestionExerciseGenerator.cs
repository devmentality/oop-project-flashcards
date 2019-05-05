﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Flashcards
{
    public class ChoiceQuestionExerciseGenerator : IExerciseGenerator
    {
        private readonly Random random;

        public ChoiceQuestionExerciseGenerator(int amountOfChoices = 4)
        {
            random = new Random();
            RequiredAmountOfCards = amountOfChoices;
        }

        public int RequiredAmountOfCards { get; }

        public Exercise GenerateExerciseFrom(IList<Card> cards)
        {
            if (cards.Count != RequiredAmountOfCards)
                throw new ArgumentException("Invalid amount of cards");

            var targetCard = cards[random.Next(RequiredAmountOfCards)];
            var choices = cards.Select(card => card.Term).ToList();
            choices.Shuffle();

            return new Exercise(
                new ChoiceAnswer(targetCard.Term),
                new ChoiceQuestion(targetCard.Definition, choices.ToArray()));
        }
    }
}
