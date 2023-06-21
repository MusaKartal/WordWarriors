import { Word } from "../card/Word";

export class QuizWord {
    id!: number;
    english!: string;
    turkish!: string;
    turkishPronunciations!: string;
    words!: Word[];
  }