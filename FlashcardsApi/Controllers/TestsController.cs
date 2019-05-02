using System.Collections.Generic;
using Flashcards;
using FlashcardsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FlashcardsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : Controller
    {
        private readonly IStorage storage;
        private readonly IAnswersStorage answersStorage;
        private readonly IAuthorizationService authorizationService;

        public TestsController(IStorage storage, IAnswersStorage answersStorage, IAuthorizationService authorizationService)
        {
            this.storage = storage;
            this.answersStorage = answersStorage;
            this.authorizationService = authorizationService;
        }

        [Authorize]
        [HttpPost("generate")]
        public async Task<ActionResult> GenerateTest(TestDto test)
        {
            var collection = storage.FindCollection(test.CollectionId);
            if (collection is null)
            {
                return NotFound();
            }

            var authResult = await authorizationService.AuthorizeAsync(User, collection, Policies.ResourceAccess);
            if (!authResult.Succeeded)
                return Forbid();

            var builder = new TestBuilder(collection.Cards);
            builder.GenerateTasks(test.OpenCnt, typeof(OpenAnswerQuestion));
            builder.GenerateTasks(test.ChoiceCnt, typeof(ChoiceQuestion));
            builder.GenerateTasks(test.MatchCnt, typeof(MatchingQuestion));

            var exercises = builder.Build();
            var testId = answersStorage.AddAnswers(exercises);

            return new JsonResult(new Dictionary<string, object>{{"testId", testId}, {"exercises", exercises}}, 
                new JsonSerializerSettings{TypeNameHandling = TypeNameHandling.Objects});
        }
    }
}