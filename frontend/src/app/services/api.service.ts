import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { WeatherForecastModel } from '../models/WeatherForecastModel';
import { UtilService } from './util.service';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http:HttpClient,private util:UtilService) { }

  getWeatherForecast() {
    return this.http.get<WeatherForecastModel[]>(this.util.apiUrl + '/weatherforecast');
  }
}
