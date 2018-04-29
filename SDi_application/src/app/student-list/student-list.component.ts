import { Component, OnInit } from '@angular/core';
import { DatePipe, DecimalPipe } from '@angular/common'
import { Student } from '../Models/Student';
import { StudentService } from '../student.service';

@Component({
  selector: 'app-student-list',
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.scss']
})
export class StudentListComponent implements OnInit {
  students: Student[] = [];

  constructor(private studentSvc: StudentService) { }

  ngOnInit() {
    this.getStudents();
  }

  getStudents(): void {
    this.studentSvc.getStudents().subscribe(students => this.students = students);
  }

}
