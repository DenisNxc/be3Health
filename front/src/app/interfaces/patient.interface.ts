export interface Convenio {
  id: number;
  nome: string;
}

export interface Paciente {
  id: number;
  nome: string;
  sobrenome: string;
  dataNascimento: string;
  genero: string;
  cpf: string;
  rg: string;
  ufRg: string;
  email: string;
  celular: string;
  telefoneFixo: string;
  ativo: boolean;
  convenioNome: string;
  numeroCarteirinha: string;
  validadeCarteirinha: string;
}
