import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public clients: Client[] = [];
  private http: HttpClient;
  private baseUrl: string;
  public syncState: boolean = false;
  public syncTemp: number = 0;
  public selectedId: number = -1;
  public clientNewName: string = "";
  public clientNewAddress: string = "";

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    http.get<Client[]>(baseUrl + 'client').subscribe(result => {
      this.clients = result;
    }, error => console.error(error));

    http.get<Sync>(baseUrl + 'sync').subscribe(result => {
      this.syncState = result.state == "true";
      this.syncTemp = result.temp;
    }, error => console.error(error));
  }

  public toggleSync() {
    this.http.get(this.baseUrl + "sync/toggle").subscribe(result => {
      this.syncState = result == "1";
    });
  }
  public edit(client: Client) {
    this.selectedId = client.clientID;
    this.clientNewName = client.clientName;
    this.clientNewAddress = client.clientAddress;
  }
  public save(client: Client) {
    client.clientName = this.clientNewName;
    this.http.get(this.baseUrl + "client/edit/" + this.selectedId + "_" + this.clientNewName+"_"+this.clientNewAddress).subscribe(result => {
    }, error => console.error(error));
    this.selectedId = -1;

  }
  public cancel() {
    this.selectedId = -1;
  }
  public delete(id: number) {
    this.http.get(this.baseUrl + id);
  }
  public warm(c: Client) {
    if (c.targetTemp < 45) {
      c.targetTemp = c.targetTemp + 1;
      this.setTemp(c.clientID, c.targetTemp);
    }
  }
  public warmSync() {
    if (this.syncTemp< 45) {
      this.syncTemp++;
      this.setSyncTemp();
    }
  }
  public cold(c: Client) {
    if (c.targetTemp > 15) {
      c.targetTemp = c.targetTemp - 1;
      this.setTemp(c.clientID, c.targetTemp);
    }
  }
  public coldSync() {
    if (this.syncTemp > 15) {
      this.syncTemp--;
      this.setSyncTemp();
    }
  }
  private setTemp(id: number, temp: number) {
    this.http.get(this.baseUrl + 'client/settemp/' + id + '_' + temp).subscribe(result => {
    }, error => console.error(error));
  }
  private setSyncTemp() {
    this.http.get(this.baseUrl + 'sync/settemp/' + this.syncTemp);
  }
} 

interface Sync {
  state: string;
  temp: number;
}

interface Client {
  clientID: number;
  clientAddress: string;
  currentTemp: number;
  targetTemp: number;
  clientName: string;
}
