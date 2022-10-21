import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { UserService } from 'src/services/user.service';
import { CountryService } from 'src/services/country.service';
import { ProvinceService } from 'src/services/province.service';
import { matchingInputsValidator } from 'src/services/custom.validators';
import { Coutry } from 'src/services/coutry';
import { Province } from 'src/services/province';
import { User } from 'src/services/user';

@Component({
  selector: 'app-root',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'SimpleWebClient';

  passRegexp = /^((?=.*?[A-Z])|(?=.*?[a-z]))(?=.*?[0-9])/i;

  isUserAdded = false;
  resultText="";

  userForm: FormGroup;
  coutryForm: FormGroup;

  countries: Coutry[] = [];
  provinces: Province[] = [];

  constructor(private formBuilder: FormBuilder, 
    private userService: UserService, 
    private provinceService: ProvinceService, 
    private countryService: CountryService,
    private changeDetector: ChangeDetectorRef
    ) {
      this.userForm = this.formBuilder.group({
        "userLogin": ["", [Validators.required, Validators.email]],
        "userPassword": ["", [Validators.required, Validators.pattern(this.passRegexp)]],
        "userPasswordConfirm": ["", [Validators.required, matchingInputsValidator]],
        "userAgryCheckBox": [true, [Validators.requiredTrue]]
      }, { updateOn: 'blur' });
  
      this.coutryForm = this.formBuilder.group({
        "userCountry": [, [Validators.required]],
        "userProvince": [, [Validators.required]]
      }, { updateOn: 'blur' });
  }

  ngOnInit(): void {
    this.getCountries();    
  }

  get userLogin() { return this.userForm.get('userLogin'); }
  get userPassword() { return this.userForm.get('userPassword'); }
  get userProvince() { return this.coutryForm.get('userProvince'); }

  getCountries(): void {
    this.countryService.getAllCountries()
      .subscribe(
        countries => {
          this.countries = countries;
          this.countries.sort((a, b) => a.countryName > b.countryName ? 1 : -1);
        }
      );
  }

  onChangeCountry(id: string) {
    if (id.length > 0) {
      this.provinceService.getProvinceByCountry(id).subscribe(
        provinces => {
          this.provinces = provinces;
          this.provinces.sort((a, b) => a.provinceName > b.provinceName ? 1 : -1);
          this.changeDetector.markForCheck();
        }
      );
    }
  }

  onSaveClick() {
    if (this.userLogin?.value && this.userProvince?.value && this.userPassword?.value) {

      this.userService.add({
        login: this.userLogin?.value,
        provinceId: this.userProvince?.value,
        password: this.userPassword?.value
      }).subscribe(
        resp => {
          this.isUserAdded = true;
          this.resultText="User add succesful";   
          this.changeDetector.markForCheck();       
        },
        error => {
          this.isUserAdded = true;
          this.resultText="Server error";
          this.changeDetector.markForCheck();
        }

      );
    }
  }

  checkLogin() {
    let form = this.userLogin;
    let login = form?.value;
    if (login)
      this.userService.findUserByLogin(login).subscribe(
        resp => {
          if (resp?.login == login) {
            this.userForm?.get('userLogin')?.setErrors({ loginAlreadyExists: { value: null } });
            this.changeDetector.markForCheck();
          }
        }
      );
  }  

}

