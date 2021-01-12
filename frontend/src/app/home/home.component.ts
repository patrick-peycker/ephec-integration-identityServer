import { Component, OnInit } from '@angular/core';
import { take} from 'rxjs/operators';
import { WeatherForecastModel } from '../models/WeatherForecastModel';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  tab:WeatherForecastModel[];
  constructor(private api:ApiService) { }

  ngOnInit(): void {
    this.api.getWeatherForecast().pipe(take(1)).subscribe(
      (value) => this.tab = value
    )
  }
}
