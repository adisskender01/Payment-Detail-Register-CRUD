import { Component, OnInit } from '@angular/core';
import { PaymentDetailService } from '../shared/payment-detail.service';
import { PaymentDetail } from '../shared/payment-detail.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-payment-details',
  templateUrl: './payment-details.component.html',
  styleUrls: ['./payment-details.component.css']
})
export class PaymentDetailsComponent implements OnInit{


  constructor(public service: PaymentDetailService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.service.refreshList();
  }

  populateForm(selectedRecord: PaymentDetail){
    this.service.formData = Object.assign({}, selectedRecord);
  }

  onDelete(id: number){
    if(confirm('Are you sure you want to delete this record?'))
    this.service.deletePaymentDetail(id)
    .subscribe({
      next: response => {
        this.service.list = response as PaymentDetail[];
        this.toastr.error('Removed successfully', 'Payment Detail Register')
      },
      error: err => { console.log(err) }
    });
  }
}
