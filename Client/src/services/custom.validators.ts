import { AbstractControl, ValidatorFn, ValidationErrors } from '@angular/forms';

export const matchingInputsValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
    let pass;
    control.root.get('userPassword')?.value ? pass = control.root.get('userPassword')?.value : pass = null;
    let confirmPass = control.value;
    return pass === confirmPass ? null : { mismatchPass: true };
}