import { Directive, HostListener } from '@angular/core';

@Directive({
  selector: '[appOnlyNumbers]'
})
export class OnlyNumbersDirective {
  @HostListener('input', ['$event'])
  onInput(event: InputEvent) {
    const input = event.target as HTMLInputElement;
    const filtered = input.value.replace(/[^0-9]/g, '');
    if (input.value !== filtered) {
      input.value = filtered;
      input.dispatchEvent(new Event('input'));
    }
  }
}
