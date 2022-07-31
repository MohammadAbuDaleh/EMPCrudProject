import { Injectable } from '@angular/core';
import { HttpClient, HttpClientModule, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee } from '../model/employee';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpClient) {


  }
  GetToken(username: string, password: string): any {
    var user = { UserName: username, Password: "password1" };
    return this.http.post<Employee>(environment.apiURL + "Employee/Authenticate", user);
  }
  addEmployee(emp: Employee, sToken: string): Observable<Employee> {
    var headers_object = new HttpHeaders().set("Authorization", "Bearer " + sToken);
    return this.http.post<Employee>(environment.apiURL + "Employee/addEmployee", emp, { headers: headers_object });
  }

  getAllEmployee(sToken: string): Observable<Employee[]> {
    var headers_object = new HttpHeaders().set("Authorization", "Bearer " + sToken);
    return this.http.get<Employee[]>(environment.apiURL + "Employee/GetAll", { headers: headers_object });
  }

  updateEmployee(emp: Employee, sToken: string): Observable<Employee> {
    var headers_object = new HttpHeaders().set("Authorization", "Bearer " + sToken);
    return this.http.put<Employee>(environment.apiURL + "Employee/updateEmployee", emp, { headers: headers_object });
  }

  deleteEmployee(emp: Employee, sToken: string): Observable<Employee> {
    var headers_object = new HttpHeaders().set("Authorization", "Bearer " + sToken);
    return this.http.post<Employee>(environment.apiURL + "Employee/deleteEmployee", emp, { headers: headers_object });
  }


}
