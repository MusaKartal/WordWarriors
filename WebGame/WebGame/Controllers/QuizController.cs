using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebGame.Data;
using WebGame.Dtos;
using WebGame.Entities;
using WebGame.Request;

namespace WebGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {

        private readonly EnglishWordDictionaryDbContext englishWordDictionaryDbContext;
        private readonly IMemoryCache cache;

        public QuizController(EnglishWordDictionaryDbContext englishWordDictionaryDbContext, IMemoryCache cache)
        {
            this.englishWordDictionaryDbContext = englishWordDictionaryDbContext;
            this.cache = cache;
        }

        [HttpGet("GetQuiz")]
        public ActionResult<WordQuizDto> GetQuiz()
        {
            var randomWord = RandomWord();

            Word[] words = this.englishWordDictionaryDbContext.Words
                .Where(x => x.Turkish != randomWord.Turkish)
                .OrderBy(x => Guid.NewGuid())
                .Take(3)
                .ToArray();

            WordQuizDto wordQuiz = new WordQuizDto()
            {
                Id = randomWord.Id,
                Turkish = randomWord.Turkish,
                English = randomWord.English,
                TurkishPronunciations = randomWord.TurkishPronunciations,
                Words = words
            };

            return Ok(wordQuiz);
        }

        [HttpPost("AnswerQuiz")]
        public ActionResult<string> Post([FromBody] QuizAnswerRequest answerRequest)
        {
            Word word = this.englishWordDictionaryDbContext.Words.Where(x => x.Id == answerRequest.WordId && x.Turkish == answerRequest.SelectedWord).FirstOrDefault();

            if (word == null)
            {
                return Ok("WRONG");
            }

            return Ok("TRUE");
        }

        private Word RandomWord()
        {
            string cacheKey = "RandomWords";

            if (!cache.TryGetValue(cacheKey, out List<Word> randomWords))
            {
                randomWords = this.englishWordDictionaryDbContext.Words.ToList();

                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                };

                cache.Set(cacheKey, randomWords, cacheOptions);
            }

            Random random = new Random();
            int randomIndex = random.Next(0, randomWords.Count);
            var randomWord = randomWords[randomIndex];

            return randomWord;
        }


    }
}
