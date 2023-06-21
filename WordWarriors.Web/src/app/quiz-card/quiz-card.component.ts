import { Component, OnInit } from '@angular/core';
import { CardService } from '../service/card.service';
import { QuizWord } from './QuizWord';
import { AnswerResponse } from './AnswerResponse';
import { AnswerRequest } from './AnswerRequest';


@Component({
  selector: 'app-quiz-card',
  templateUrl: './quiz-card.component.html',
  styleUrls: ['./quiz-card.component.css'],
  providers: [CardService]
})
export class QuizCardComponent implements OnInit {
  Quiz!: QuizWord;
  allWords: any[] = [];
  response!:AnswerResponse;
  constructor(private cardService: CardService){}

  ngOnInit() {
    this.cardService.GetRandomQuiz().subscribe((data: QuizWord) => {
      this.Quiz = data;
      this.combineWords();
      this.shuffleWords();
    });
  }
  AnswerQuiz(word: string) {
    const request: AnswerRequest = {
      wordId: this.Quiz.id,
      selectedWord: word
    };
  
    this.cardService.PostAnswerQuiz(request).subscribe(
      (data: string) => {
        
        this.response = { Answer: data };
        alert(this.response.Answer);
        if(this.response.Answer == "TRUE"){
          this.ngOnInit();
        }
      },
      (error: any) => {
        console.error('Error:', error);
        // Hata durumunda yapılacak işlemleri buraya ekleyebilirsiniz
      }
    );
  }

  combineWords(){
    
    this.allWords=[this.Quiz.turkish, ...this.Quiz.words.map(x => x.turkish)]
  }
 
  
  shuffleWords() {
    for (let i = this.allWords.length - 1; i > 0; i--) {
      const j = Math.floor(Math.random() * (i + 1));
      [this.allWords[i], this.allWords[j]] = [this.allWords[j], this.allWords[i]];
    }
  }
}

