import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Student } from '../models/api-models/students.model';
import { UpdateStudentRequest } from '../models/api-models/update-student-request.model';
import { AddStudentRequest } from '../models/api-models/add-student-request.model';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private baseApiUrl = 'https://localhost:7214'

  constructor(private httpClient: HttpClient) { }

  getStudents(): Observable<Student[]> {
   return this.httpClient.get<Student[]>(this.baseApiUrl + '/students');
  }

  getStudent(studentId : string): Observable<Student> {
    return this.httpClient.get<Student>(this.baseApiUrl + '/students/' + studentId);
   }

   updateStudent(studentId: string, studentRequest:Student): Observable<Student>{
    const updateStudentRequest: UpdateStudentRequest = {
      firstName : studentRequest.firstName,
      lastName : studentRequest.lastName,
      dateOfBirth : studentRequest.dateOfBirth,
      email : studentRequest.email,
      mobile : studentRequest.mobile,
      genderId : studentRequest.genderId,
      physicalAddress : studentRequest.address.physicalAddress
    }
    return this.httpClient.put<Student>(this.baseApiUrl + '/students/' + studentId,
      updateStudentRequest);
   }

   deleteStudent(studentId: string): Observable<Student> {
    return this.httpClient.delete<Student>(this.baseApiUrl + '/students/' + studentId);
   }

   addStudent(studentRequest: Student): Observable<Student> {
    const addStudentRequest: AddStudentRequest = {
      firstName : studentRequest.firstName,
      lastName : studentRequest.lastName,
      dateOfBirth : studentRequest.dateOfBirth,
      email : studentRequest.email,
      mobile : studentRequest.mobile,
      genderId : studentRequest.genderId,
      physicalAddress : studentRequest.address.physicalAddress
    };
    
    return this.httpClient.post<Student>(this.baseApiUrl + '/students/add' , addStudentRequest );
   }
}
