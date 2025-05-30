// patient-list.component.ts
import { Component, OnInit, ViewChild, Input, OnChanges, Output, EventEmitter } from '@angular/core';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { PatientService } from '../../services/patient.service';
import { Paciente } from '../../interfaces/patient.interface';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-patient-table',
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.scss'],
  standalone: true,
  imports: [
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatInputModule,
    MatFormFieldModule,
    MatIconModule,
    MatButtonModule,
    MatDialogModule,
    MatProgressSpinnerModule,
    CommonModule
  ]
})
export class PatientListComponent implements OnInit, OnChanges {
  displayedColumns: string[] = [
    'nome',
    'dataNascimento',
    'cpf',
    'celular',
    'convenioNome',
    'status',
    'actions'
  ];

  dataSource = new MatTableDataSource<Paciente>();
  isLoading = true;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @Input() patients: Paciente[] = [];
  @Output() edit = new EventEmitter<Paciente>();

  constructor(
    private patientService: PatientService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    if (this.patients && this.patients.length) {
      this.dataSource = new MatTableDataSource(this.patients);
      setTimeout(() => {
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      });
      this.isLoading = false;
    } else {
      this.loadPatients();
    }
  }

  ngOnChanges() {
    // Sempre atualiza a tabela, mesmo se a lista vier vazia
    this.dataSource = new MatTableDataSource(this.patients);
    setTimeout(() => {
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
    this.isLoading = false;
  }

  loadPatients(): void {
    this.isLoading = true;
    this.patientService.getAll().subscribe({
      next: (patients: Paciente[]) => {
        this.dataSource = new MatTableDataSource(patients);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading patients', err);
        this.isLoading = false;
      }
    });
  }

  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  onEdit(patient: Paciente): void {
    this.edit.emit(patient);
  }

  onDelete(id: number): void {

  }

  formatPhoneForDisplay(phone: string): string {
  if (!phone) return '';

  phone = phone.replace(/\D/g, '');
  if (phone.length === 10) {
    return phone.replace(/(\d{2})(\d{4})(\d{4})/, '($1) $2-$3');
  } else if (phone.length === 11) {
    return phone.replace(/(\d{2})(\d{5})(\d{4})/, '($1) $2-$3');
  }
  return phone;
}
}
