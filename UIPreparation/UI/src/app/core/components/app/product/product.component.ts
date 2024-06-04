import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Product } from './models/product';
import { ProductService } from './services/product.service';
import { environment } from 'environments/environment';
import { ColorService } from '../color/services/color.service';
import { Color } from '../color/models/color';
import { LookUpService } from 'app/core/services/lookUp.service';
import { LookUp } from 'app/core/models/lookUp';
import { data } from 'jquery';

declare var jQuery: any;

@Component({
	selector: 'app-product',
	templateUrl: './product.component.html',
	styleUrls: ['./product.component.scss']
})
export class ProductComponent implements AfterViewInit, OnInit {

	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['name', 'size', 'createdDate', 'update', 'delete'];

	productList: Product[];
	colorList: LookUp[];
	wareHouseList: LookUp[];
	product: Product = new Product();
	selectedOption: any;
	productAddForm: FormGroup;
	productId: number;

	constructor(private productService: ProductService, private lookupService: LookUpService, private alertifyService: AlertifyService, private formBuilder: FormBuilder, private authService: AuthService) { }

	ngAfterViewInit(): void {
		this.getProductList();
	}

	ngOnInit() {
		this.createProductAddForm();

		this.lookupService.getColorLookUp().subscribe(data => {
			this.colorList = data;
		})
		this.lookupService.getWareHouseLookUp().subscribe(data => {
			this.wareHouseList = data;
		})
	}

	getProductList() {
		this.productService.getProductList().subscribe(data => {
			this.productList = data;

			this.dataSource = new MatTableDataSource(data);
			this.configDataTable();
		});
	}

	save() {

		if (this.productAddForm.valid) {
			this.product = Object.assign({}, this.productAddForm.value)

			if (this.product.id == 0)
				this.addProduct();
			else
				this.updateProduct();
		}

	}

	addProduct() {

		this.productService.addProduct(this.product).subscribe(data => {
			this.getProductList();
			this.product = new Product();
			jQuery('#product').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.productAddForm);
			this.createProductAddForm();

		})

	}

	updateProduct() {
		this.productService.updateProduct(this.product).subscribe(data => {

			var index = this.productList.findIndex(x => x.id == this.product.id);
			this.productList[index] = this.product;
			this.dataSource = new MatTableDataSource(this.productList);
			this.configDataTable();
			this.product = new Product();
			jQuery('#product').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.productAddForm);

		})

	}

	onOptionSelected(event: Event): void {
		const selectElement = event.target as HTMLSelectElement;
		this.selectedOption = selectElement.value;
		console.log('Seçilen değer:', this.selectedOption);
		this.updateProductAddForm();

	}

	updateProductAddForm(): void {
		this.productAddForm.patchValue({
			size: this.selectedOption
		});
	}

	createProductAddForm() {
		this.productAddForm = this.formBuilder.group({
			id: 0,
			createdUserId: this.authService.getCurrentUserId(),
			createdDate: new Date(),
			lastUpdatedUserId: this.authService.getCurrentUserId(),
			lastUpdatedDate: new Date(),
			status: [false, Validators.required],
			isDeleted: [false],
			name: ["", Validators.required],
			colorId: [0, Validators.required],
			wareHouseId: [0, Validators.required],
			count: [0, Validators.required],
			readyForSale: [false, Validators.required],
			size: this.selectedOption
		})
	}

	deleteProduct(productId: number) {
		this.productService.deleteProduct(productId, this.authService.getCurrentUserId()).subscribe(data => {
			this.alertifyService.success(data.toString());
			this.productList = this.productList.filter(x => x.id != productId);
			this.dataSource = new MatTableDataSource(this.productList);
			this.configDataTable();
		})
	}

	getProductById(productId: number) {
		this.clearFormGroup(this.productAddForm);
		this.productService.getProductById(productId).subscribe(data => {
			this.product = data;
			this.productAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'id')
				group.get(key).setValue(0);
		});
	}

	checkClaim(claim: string): boolean {
		return this.authService.claimGuard(claim)
	}

	configDataTable(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();

		if (this.dataSource.paginator) {
			this.dataSource.paginator.firstPage();
		}
	}

}
