import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Paciente, Convenio } from '../../interfaces/patient.interface';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PatientService } from '../../services/patient.service';
import { OnlyNumbersDirective } from '../../only-numbers.directive';
import { isValidCPF } from '../../utils-cpf';

@Component({
  selector: 'app-client-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './patient-form.component.html',
  styleUrls: ['./patient-form.component.scss'],
})
export class PatientFormComponent implements OnInit {
  @Input() client: Paciente | null = null;
  @Input() showModal = false;
  @Output() close = new EventEmitter<void>();
  @Output() save = new EventEmitter<Paciente>();

  formData: Paciente = this.getDefaultClient();
  convenios: Convenio[] = [];
  allPatients: Paciente[] = [];

  constructor(private patientService: PatientService) {}

  ngOnInit() {
    this.patientService.getConvenios().subscribe((data) => {
      this.convenios = data;
    });
    this.patientService.getAll().subscribe((patients) => {
      this.allPatients = patients;
    });
  }

ngOnChanges() {
  if (this.client) {
    this.formData = { ...this.client };
    // Formata os campos ao editar
    if (this.formData.cpf) {
      this.formData.cpf = this.formatCPF(this.formData.cpf);
    }
    if (this.formData.rg) {
      this.formData.rg = this.formatRG(this.formData.rg);
    }
    if (this.formData.celular) {
      // Remove formatação antes de aplicar novamente para garantir consistência
      const celularNumerico = this.formData.celular.replace(/\D/g, '');
      this.formData.celular = this.formatPhone(celularNumerico);
    }
    if (this.formData.telefoneFixo) {
      // Remove formatação antes de aplicar novamente para garantir consistência
      const fixoNumerico = this.formData.telefoneFixo.replace(/\D/g, '');
      this.formData.telefoneFixo = this.formatPhone(fixoNumerico);
    }
  } else {
    this.formData = this.getDefaultClient();
  }
}

  private getDefaultClient(): Paciente {
    return {
      id: 0,
      nome: '',
      sobrenome: '',
      dataNascimento: '',
      genero: '',
      cpf: '',
      rg: '',
      ufRg: '',
      email: '',
      celular: '',
      telefoneFixo: '',
      ativo: true,
      convenioNome: '',
      numeroCarteirinha: '',
      validadeCarteirinha: ''
    };
  }

  // Função para formatar CPF
  formatCPF(cpf: string): string {
    cpf = cpf.replace(/\D/g, '');
    if (cpf.length > 3) {
      cpf = cpf.replace(/^(\d{3})/, '$1.');
    }
    if (cpf.length > 7) {
      cpf = cpf.replace(/^(\d{3})\.(\d{3})/, '$1.$2.');
    }
    if (cpf.length > 11) {
      cpf = cpf.replace(/^(\d{3})\.(\d{3})\.(\d{3})/, '$1.$2.$3-');
    }
    return cpf.substring(0, 14);
  }

  // Função para formatar RG
  formatRG(rg: string): string {
    rg = rg.replace(/\D/g, '');
    if (rg.length > 2) {
      rg = rg.replace(/^(\d{2})/, '$1.');
    }
    if (rg.length > 6) {
      rg = rg.replace(/^(\d{2})\.(\d{3})/, '$1.$2.');
    }
    if (rg.length > 10) {
      rg = rg.replace(/^(\d{2})\.(\d{3})\.(\d{3})/, '$1.$2.$3-');
    }
    return rg.substring(0, 12);
  }

  // Função para formatar telefone
  formatPhone(phone: string): string {
    phone = phone.replace(/\D/g, '');
    if (phone.length > 2) {
      phone = phone.replace(/^(\d{2})/, '($1) ');
    }
    if (phone.length > 8 && phone.length <= 10) {
      phone = phone.replace(/^(\d{2})\s(\d{4})/, '$1 $2-');
    } else if (phone.length > 8) {
      phone = phone.replace(/^(\d{2})\s(\d{5})/, '$1 $2-');
    }
    return phone.substring(0, 15);
  }

  // Função para formatar validade da carteirinha
  formatValidity(validity: string): string {
    validity = validity.replace(/\D/g, '');
    if (validity.length > 2) {
      validity = validity.replace(/^(\d{2})/, '$1/');
    }
    return validity.substring(0, 7);
  }

  onCPFInput(event: any) {
    let value = event.target.value;
    value = this.formatCPF(value);
    event.target.value = value;
    this.formData.cpf = value.replace(/\D/g, '');
  }

  onRGInput(event: any) {
    let value = event.target.value;
    value = this.formatRG(value);
    event.target.value = value;
    this.formData.rg = value.replace(/\D/g, '');
  }

onPhoneInput(event: any, field: 'celular' | 'telefoneFixo') {
  let value = event.target.value;
  value = this.formatPhone(value);
  event.target.value = value;
  // Armazena apenas os números
  this.formData[field] = value.replace(/\D/g, '');
}

  onValidityInput(event: any) {
    let value = event.target.value;
    value = this.formatValidity(value);
    event.target.value = value;
    this.formData.validadeCarteirinha = value;
  }

  onSubmit() {
    // Reset errors
    this.inputErrors = {};
    let isValid = true;

    // Nome obrigatório
    if (!this.formData.nome) {
      this.setInputError('nome', 'Nome é obrigatório.');
      isValid = false;
    }
    // Sobrenome obrigatório
    if (!this.formData.sobrenome) {
      this.setInputError('sobrenome', 'Sobrenome é obrigatório.');
      isValid = false;
    }
    // Email obrigatório
    const emailRegex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
    if (!this.formData.email) {
      this.setInputError('email', 'E-mail é obrigatório.');
      isValid = false;
    } else if (!emailRegex.test(this.formData.email)) {
      this.setInputError('email', 'Informe um e-mail válido.');
      isValid = false;
    }
    // CPF obrigatório
    if (!this.formData.cpf) {
      this.setInputError('cpf', 'CPF é obrigatório.');
      isValid = false;
    } else if (!isValidCPF(this.formData.cpf)) {
      this.setInputError('cpf', 'Informe um CPF válido.');
      isValid = false;
    } else {
      // Verifica duplicidade de CPF
      const cpfNumerico = this.formData.cpf.replace(/\D/g, '');
      const cpfDuplicado = this.allPatients.some(p => p.cpf.replace(/\D/g, '') === cpfNumerico && p.id !== this.formData.id);
      if (cpfDuplicado) {
        this.setInputError('cpf', 'Já existe um paciente cadastrado com este CPF.');
        isValid = false;
      }
    }
    // Data de nascimento obrigatória
    if (!this.formData.dataNascimento) {
      this.setInputError('dataNascimento', 'Data de nascimento é obrigatória.');
      isValid = false;
    } else if (new Date(this.formData.dataNascimento) > new Date()) {
      this.setInputError('dataNascimento', 'A data de nascimento não pode ser futura.');
      isValid = false;
    }
    // Celular obrigatório
    const celularNumerico = this.formData.celular ? this.formData.celular.replace(/\D/g, '') : '';
    if (!celularNumerico) {
      this.setInputError('celular', 'Celular é obrigatório.');
      isValid = false;
    } else if (!(celularNumerico.length === 10 || celularNumerico.length === 11)) {
      this.setInputError('celular', 'Celular deve ter 10 ou 11 dígitos.');
      isValid = false;
    }
    // Convênio obrigatório
    if (!this.formData.convenioNome) {
      this.setInputError('convenioNome', 'Convênio é obrigatório.');
      isValid = false;
    }
    // Número da carteirinha obrigatório
    if (!this.formData.numeroCarteirinha) {
      this.setInputError('numeroCarteirinha', 'Número da carteirinha é obrigatório.');
      isValid = false;
    }
    // Validade da carteirinha obrigatória
    if (!this.formData.validadeCarteirinha) {
      this.setInputError('validadeCarteirinha', 'Validade da carteirinha é obrigatória.');
      isValid = false;
    }

    // Se tudo estiver válido, salva os dados
    if (!isValid) return;
    if (this.client) {
      this.patientService.update(this.formData).subscribe(() => {
        this.save.emit();
        this.close.emit();
      });
    } else {
      this.patientService.save(this.formData).subscribe(() => {
        this.save.emit();
        this.close.emit();
      });
    }
  }

  inputErrors: { [key: string]: string } = {};

  setInputError(field: string, message: string) {
    this.inputErrors = { ...this.inputErrors, [field]: message };
    setTimeout(() => {
      this.inputErrors = { ...this.inputErrors, [field]: '' };
    }, 4000);
  }

  onClose() {
    this.close.emit();
  }
}
