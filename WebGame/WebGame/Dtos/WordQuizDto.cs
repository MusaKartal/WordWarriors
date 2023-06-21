using WebGame.Entities;

namespace WebGame.Dtos
{
    public class WordQuizDto
    {
        public int Id { get; set; }

        public string English { get; set; }

        public string Turkish { get; set; }

        public string TurkishPronunciations { get; set; }

        public Word[] Words { get; set; }        

    }
}
