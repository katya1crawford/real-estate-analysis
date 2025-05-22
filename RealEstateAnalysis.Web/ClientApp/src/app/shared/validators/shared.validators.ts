import { AbstractControl, ValidatorFn } from '@angular/forms';

export function passwordMatcher(passwordPropNam: string, confirmPasswordPropName: string): ValidatorFn {
    return (control: AbstractControl): any => {
        if (!control.get(passwordPropNam) || !control.get(confirmPasswordPropName)) {
            return null;
        }

        const passwordValue: string = (<AbstractControl>control.get(passwordPropNam)).value;
        const confirmPasswordValue: string = (<AbstractControl>control.get(confirmPasswordPropName)).value;
        const match: boolean = passwordValue === confirmPasswordValue;

        if (!match) {
            return { 'noMatch': true };
        } else {
            return null;
        }
    };
}

export function fileValidator(control: AbstractControl): { [key: string]: any } | null {
    const files: File[] | null = control.value as File[] | null;

    if (files === null || files.length === 0) {
        return null;
    }

    const acceptableFileExtensions: string[] = ['txt', 'pdf', 'jpg', 'jpeg', 'gif', 'png', 'docx', 'doc', 'xlsx', 'xls', 'csv'];

    for (let i = 0; i < files.length; i++) {
        if (files[i].size > 15728640) {
            return { 'fileSize': true };
        }

        const fileExtension: string = <string>files[i].name.split('.').pop();
        const validExtension: string | undefined = acceptableFileExtensions.find(x => x === fileExtension.toLowerCase());

        if (!validExtension) {
            return { 'fileType': true };
        }
    }

    return null;
}

export function csvFileValidator(control: AbstractControl): { [key: string]: any } | null {
    const file: File | null = control.value as File | null;

    if (file === null) {
        return null;
    }

    const acceptableFileExtensions: string[] = ['csv'];

    if (file.size > 15728640) {
        return { 'fileSize': true };
    }

    const fileExtension: string = <string>file.name.split('.').pop();
    const validExtension: string | undefined = acceptableFileExtensions.find(x => x === fileExtension.toLowerCase());

    if (!validExtension) {
        return { 'fileType': true };
    }

    return null;
}

export function imageFileValidator(control: AbstractControl): { [key: string]: any } | null {
    const files: File[] | null = control.value as File[] | null;

    if (files === null || files.length === 0) {
        return null;
    }

    const acceptableFileExtensions: string[] = ['jpg', 'jpeg', 'png'];

    for (let i = 0; i < files.length; i++) {
        if (files[i].size > 15728640) {
            return { 'fileSize': true };
        }

        const fileExtension: string = <string>files[i].name.split('.').pop();
        const validExtension: string | undefined = acceptableFileExtensions.find(x => x === fileExtension.toLowerCase());

        if (!validExtension) {
            return { 'fileType': true };
        }
    }

    return null;
}

export function loanValidator(loanAprCtrlName: string, loanYearsCtrlName: string, purchasePriceCtrlName: string, downPaymentCtrlName: string): ValidatorFn {
    return (control: AbstractControl): any => {
        if (!control.get(loanAprCtrlName)
            || !control.get(loanYearsCtrlName)
            || !control.get(purchasePriceCtrlName)
            || !control.get(downPaymentCtrlName)) {
            return null;
        }

        const loanAprValue: number = +(<AbstractControl>control.get(loanAprCtrlName)).value;
        const loanYearsValue: number = +(<AbstractControl>control.get(loanYearsCtrlName)).value;
        const purchasePriceValue: number = +(<AbstractControl>control.get(purchasePriceCtrlName)).value;
        const downPaymentValue: number = +(<AbstractControl>control.get(downPaymentCtrlName)).value;
        const loanAmount = purchasePriceValue - downPaymentValue;

        if (downPaymentValue > purchasePriceValue) {
            return { 'invalidDownPayment': true };
        }

        if (loanAmount > 0 && loanYearsValue === 0) {
            return { 'invalidLoanYears': true };
        } else if (loanYearsValue > 0 && loanAprValue === 0) {
            return { 'invalidLoanApr': true };
        } else {
            return null;
        }
    };
}

export function thumbnailImageValidator(control: AbstractControl): { [key: string]: any } | null {
    const file: File | null = control.value as File | null;

    if (file === null) {
        return null;
    }

    const acceptableFileExtention: string[] = ['jpg', 'jpeg', 'png'];

    if (file.size > 5242880) {
        return { 'fileSize': true };
    }

    const fileExtention: string = <string>file.name.split('.').pop();
    const validExtention: string | undefined = acceptableFileExtention.find(x => x === fileExtention.toLowerCase());

    if (!validExtention) {
        return { 'fileType': true };
    }

    return null;
}
