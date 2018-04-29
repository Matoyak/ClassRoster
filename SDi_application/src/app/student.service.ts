import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError, map, tap } from 'rxjs/operators';
import { Student } from './Models/Student';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable()
export class StudentService {

  constructor(private httpClient: HttpClient) { }

  /**
   * Get list of students from database
   */
  getStudents(): Observable<Student[]> {
    console.log('Calling students');
    return this.httpClient.get<Student[]>('api/students')
      .pipe(
        tap(students => console.log('Fetched Students', students)),
        catchError(this.handleError('getStudents', []))
      );
  }

  /**
   * Post call to add student to database
   * @param student The student being added
   */
  addStudent(student: Student): Observable<Student> {
    return this.httpClient.post<Student>('api/students', student, httpOptions)
      .pipe(
        tap((student: Student) => console.log(`Added new student: id=${student.id}`)),
        catchError(this.handleError<Student>('addStudent'))
      );
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      console.error(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
