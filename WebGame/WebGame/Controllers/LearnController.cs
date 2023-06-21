using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebGame.Data;
using WebGame.Dtos;
using WebGame.Entities;


namespace WebGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearnController : ControllerBase
    {
        private readonly EnglishWordDictionaryDbContext englishWordDictionaryDbContext;
        private readonly IMemoryCache cache;

        public LearnController(EnglishWordDictionaryDbContext englishWordDictionaryDbContext, IMemoryCache cache)
        {
            this.englishWordDictionaryDbContext = englishWordDictionaryDbContext;
            this.cache = cache;
        }

        [HttpGet("GetRandomWord")]
        public ActionResult<Word> GetRandomWord()
        {
            var randomWord = RandomWord();
            if (randomWord == null)
            {
                return NotFound();
            }        

            return Ok(randomWord);
        }

        private Word RandomWord()
        {
            string cacheKey = "RandomWords";

            if (!cache.TryGetValue(cacheKey, out List<Word> randomWords))
            {
                randomWords = this.englishWordDictionaryDbContext.Words.ToList();

                if (randomWords.Count == 0)
                {
                    return null;
                }

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


