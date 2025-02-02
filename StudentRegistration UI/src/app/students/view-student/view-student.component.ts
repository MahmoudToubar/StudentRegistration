import { Component, OnInit, ViewChild, viewChild } from '@angular/core';
import { StudentService } from '../student.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Student } from 'src/app/models/ui-models/student.model';
import { GenderService } from 'src/app/services/gender.service';
import { Gender } from 'src/app/models/ui-models/gender.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-view-student',
  templateUrl: './view-student.component.html',
  styleUrl: './view-student.component.css'
})
export class ViewStudentComponent implements OnInit {

  studentId: string | null | undefined;
  student: Student = {
    id: '',
    firstName: '',
    lastName: '',
    dateOfBirth: '',
    email: '',
    mobile: 0,
    genderId: '',
    gender: {
      id: '',
      description: '',
    },
    address: {
      id: '',
      physicalAddress: '',
    }
  };

  isNewStudent = false;
  header = '';

  genderList: Gender[] = [];

  @ViewChild('studentDetailsForm') studentDetailsForm?: NgForm;


  constructor(private readonly studentService: StudentService, private readonly route: ActivatedRoute,
    private readonly genderService: GenderService, private snackbar: MatSnackBar, private router: Router){}

  ngOnInit(): void {
    this.route.paramMap.subscribe(
      (params) => {
        this.studentId = params.get('id');

        if (this.studentId) {
          if(this.studentId.toLowerCase() === 'Add' .toLowerCase()){

            this.isNewStudent = true;
            this.header = 'Add New Student';


          } else {

            this.isNewStudent = false;
            this.header = 'Edit Student';
            this.studentService.getStudent(this.studentId)
            .subscribe(
            (successResponse) => {
              this.student = successResponse;
            }
          );


          }


          this.genderService.getGenderList()
          .subscribe(
            (successResponse) => {
              this.genderList = successResponse;
            }
          );
        }
      }
    );
  }

  onUpdate(): void {
    if (this.studentDetailsForm?.form.valid){
      this.studentService.updateStudent(this.student.id, this.student)
      .subscribe(
      (successResponse) => {
        this.snackbar.open('Student updated successfully', undefined, {
          duration: 2000
        });
      },
      (errorResponse) => {
        

      }
    );

    }
    
  }

  onDelete(): void {
    this.studentService.deleteStudent(this.student.id)
    .subscribe(
      (successResponse) => {
        this.snackbar.open('Student deleted successfully', undefined, {
          duration: 2000
        });

        setTimeout(() =>{
          this.router.navigateByUrl('students');

        }, 2000);
      },
      (errorResponse) => {
        console.log(errorResponse);

      }
    );
  }

  onAdd(): void {

    if(this.studentDetailsForm?.form.valid){

      this.studentService.addStudent(this.student)
    .subscribe(
      (successResponse) => {
        this.snackbar.open('Student added successfully', undefined, {
          duration: 2000
        });

        setTimeout(() =>{
          this.router.navigateByUrl('students/${successResponse.id}');

        }, 2000);
      },
      (errorResponse) => {
        console.log(errorResponse);
      }
    );

    }



    
  }
}
