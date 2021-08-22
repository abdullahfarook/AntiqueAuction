import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DetailComponent } from './routes/detail/detail.component';
import { ItemsComponent } from './routes/items/items.component';
import { LoginComponent } from './routes/login/login.component';
import { NotFoundComponent } from './routes/not-found/not-found.component';

const routes: Routes = [
  {path: '', redirectTo:"items",pathMatch: 'full'},
  { path: 'items', component:ItemsComponent },
  { path: 'items/detail/:id', component:DetailComponent },
  { path: 'login', component:LoginComponent },
  { path: '**', component: NotFoundComponent }  // Wildcard route for a 404 page];
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
