﻿using System;
using System.Collections.Generic;
using Flashcards;
using FlashcardsClient.Infrastructure;

namespace FlashcardsClient
{
    public static class ExerciseHandler
    {
        private static readonly Dictionary<Type, Func<ExerciseQuestion, ExerciseAnswer>> exercises =
            new Dictionary<Type, Func<ExerciseQuestion, ExerciseAnswer>>
            {
                {
                    typeof(OpenAnswerQuestion), (e) =>
                    {
                        Console.WriteLine(((OpenAnswerQuestion) e.Question).Definition);
                        Console.WriteLine("Enter the term mathcing to this definition");
                        var answer = Console.ReadLine();
                        return new ExerciseAnswer{Id = e.Id, Answer = new OpenAnswer(answer)};
                    }
                },
                { typeof(ChoiceQuestion), (e) =>
                    {
                        var question = (ChoiceQuestion) e.Question;
                        Console.WriteLine(question.Definition);
                        Console.WriteLine("Choose the answer and enter the number of it");
                        for (var i = 0; i < question.Choices.Length; i++)
                            Console.WriteLine($"{i}) {question.Choices[i]}");
                        var answer = int.Parse(Console.ReadLine());
                        return new ExerciseAnswer{Id = e.Id, Answer = new ChoiceAnswer(question.Choices[answer])};
                    }
                },
                {
                    typeof(MatchingQuestion), (e) =>
                    {
                        var question = (MatchingQuestion) e.Question;
                        Console.WriteLine("");
                        for (var i = 0; i < question.Terms.Length; i++)
                            Console.WriteLine($"{i}) {question.Terms[i]}    {i}) {question.Definitions[i]}");
                        var answer = new Dictionary<string, string>();
                        Console.WriteLine("And here you will be given the numbers of terms. Please, enter the number of definition for each one");
                        for (var i = 0; i < question.Terms.Length; i++)
                        {
                            Console.Write($"{i}) ");
                            var answerNumber = int.Parse(Console.ReadLine());
                            answer[question.Terms[i]] = question.Definitions[answerNumber];
                        }
                        return new ExerciseAnswer{Id = e.Id, Answer = new MatchingAnswer(answer)};
                    }
                }
            };

        public static ExerciseAnswer Handle(ExerciseQuestion question)
        {
            return exercises[question.Question.GetType()](question);
        }
    }
}
