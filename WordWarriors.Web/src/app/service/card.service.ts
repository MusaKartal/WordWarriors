import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { Word } from '../card/Word';
import { HttpClient } from '@angular/common/http';
import { QuizWord } from '../quiz-card/QuizWord';
import { AnswerResponse } from '../quiz-card/AnswerResponse';
import { AnswerRequest } from '../quiz-card/AnswerRequest';

@Injectable({
  providedIn: 'root'
})
export class CardService {
  baseUrl = "https://localhost:7029/api";

  constructor(private http: HttpClient) { }


  GetRandomWord(): Observable<Word> {
    return this.http.get<Word>(this.baseUrl + "/Learn/GetRandomWord");
  }

  GetRandomQuiz(): Observable<QuizWord> {
    return this.http.get<QuizWord>(this.baseUrl + "/Quiz/GetQuiz");
  }

  PostAnswerQuiz(answer: AnswerRequest): Observable<string> {
    return this.http.post(this.baseUrl + "/Quiz/AnswerQuiz", answer, { responseType: 'text' });
  }
}

