import { Component } from '@angular/core';
import { CardService } from '../service/card.service';
import { Word } from './Word';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css'],
  providers: [CardService]
})
export class CardComponent {
  englishText!: string;
  turkishText!: string;
  turkishPronunciations!: string;
  isTextChanged: boolean = false;
  isCardFlipped: boolean = false;
  word!: Word;

  constructor(private cardService: CardService) { }

  GetWord(): void {
    this.cardService.GetRandomWord().subscribe((data: Word) => {
      this.englishText = data.english;
      this.turkishText = data.turkish;
      this.turkishPronunciations = data.turkishPronunciations;
      
    });
  }

  ChangeWord(): void {
    this.isCardFlipped = !this.isCardFlipped;
    this.isTextChanged = !this.isTextChanged;
  }
}