import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CardComponent } from './card/card.component';
import { QuizCardComponent } from './quiz-card/quiz-card.component';

const routes: Routes = [
  {path:"Learn" , component: CardComponent},
  {path:"Quiz" , component: QuizCardComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
