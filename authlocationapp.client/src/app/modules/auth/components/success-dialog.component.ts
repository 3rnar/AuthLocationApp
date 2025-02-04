import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-success-dialog',
  templateUrl: './success-dialog.component.html',
  styleUrls: ['./success-dialog.component.css'],
})
export class SuccessDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<SuccessDialogComponent>,
    @Inject(MAT_DIALOG_DATA)
    public data: { title: string; message: string; status: string }
  ) {}

  closeDialog(): void {
    this.dialogRef.close();
  }

  getButtonColor(): string {
    return this.data.status === 'success' ? 'primary' : 'warn';
  }
}
