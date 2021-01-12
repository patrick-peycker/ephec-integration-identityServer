import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AuthentificationService } from '../services/authentification.service';
import { UtilService } from '../services/util.service';

@Component({
  selector: 'app-call-api',
  templateUrl: './call-api.component.html',
  styleUrls: ['./call-api.component.css']
})
export class CallApiComponent implements OnInit {

  content: string;
  
  constructor(private httpClient: HttpClient, private autheticationService: AuthentificationService, private util: UtilService) { }


  ngOnInit(): void {
    const token = this.autheticationService.getUser().access_token;
    this.httpClient.get(
      this.util.apiUrl + '/api/identity',
      { headers: new HttpHeaders({ "Authorization": "Bearer " + token }) }
    ).subscribe(
      content => { this.content = JSON.stringify(content); }
    );
  }

}
