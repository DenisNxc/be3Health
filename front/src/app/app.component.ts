import { Component, OnInit } from '@angular/core';
import { PatientService } from './services/patient.service';
import { Paciente, Convenio } from './interfaces/patient.interface';
import { HeaderComponent } from './@components/header/header.component';
import { CommonModule } from '@angular/common';
import { PatientFormComponent } from './@components/patient-form/patient-form.component';
import { PatientListComponent } from './@components/patient-list/patient-list.component';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    HeaderComponent,
    PatientFormComponent,
    PatientListComponent
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  clients: Paciente[] = [];
  convenios: Convenio[] = [];
  showForm = false;
  currentClient: Paciente | null = null;
  isLoading = false;
  onInitConvenios: boolean = false;

  constructor(
    private patientService: PatientService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.patientService.getConvenios().subscribe((data: Convenio[]) => {
      this.convenios = data;
      this.onInitConvenios = true;
      this.loadClients();
    });
  }

  loadClients(): void {
    this.isLoading = true;
    this.patientService.getAll().subscribe({
      next: (patients: Paciente[]) => {
        this.clients = patients;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading patients', err);
        this.isLoading = false;
        this.showSnackBar('Erro ao carregar pacientes');
      }
    });
  }

  onAddClient(): void {
    this.currentClient = null;
    this.showForm = true;
  }

  onEditClient(client: Paciente): void {
    this.currentClient = { ...client };
    this.showForm = true;
  }

  onDeleteClient(id: any): void {

  }

  onSaveClient(client: Paciente | undefined): void {
    if (!client) {
      return;
    }
    // Sempre recarrega a lista do backend após salvar/editar
    const operation = client.id
      ? this.patientService.update(client)
      : this.patientService.save(client);

    operation.subscribe({
      next: () => {
        this.loadClients(); // Garante reload da lista
        this.showForm = false;
        this.showSnackBar(`Paciente ${client.id ? 'atualizado' : 'cadastrado'} com sucesso`);
      },
      error: (err) => {
        console.error('Error saving patient', err);
        this.showSnackBar(`Erro ao ${client.id ? 'atualizar' : 'cadastrar'} paciente`);
      }
    });
  }

  onCloseForm(): void {
    this.showForm = false;
  }

  private showSnackBar(message: string): void {
    this.snackBar.open(message, 'Fechar', {
      duration: 3000,
      horizontalPosition: 'right',
      verticalPosition: 'top'
    });
  }
}
