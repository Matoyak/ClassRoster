import { Component } from '@angular/core';
import { StudentService } from '../student.service';
import { Student } from '../Models/Student';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';

@Component({
  selector: 'app-student-form',
  templateUrl: './student-form.component.html',
  styleUrls: ['./student-form.component.scss']
})
export class StudentFormComponent implements OnInit {
  submitted = false;
  doDisplay = false;
  newStudent: Student = new Student();

  constructor(private studentSvc: StudentService) { }

  ngOnInit() {
  }

  onSubmit() { this.submitted = true; }

  display() {
    this.doDisplay = true;
  }

  addStudent(): void {
    this.studentSvc.addStudent(this.newStudent)
      .subscribe(student => {
        console.log('Inside Subscribe of addStudent');
        window.location.reload();
      });
  }
}
