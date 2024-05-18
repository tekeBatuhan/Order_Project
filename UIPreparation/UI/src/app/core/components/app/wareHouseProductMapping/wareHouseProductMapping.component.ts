import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { WareHouseProductMapping } from './models/WareHouseProductMapping';
import { WareHouseProductMappingService } from './services/WareHouseProductMapping.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-wareHouseProductMapping',
	templateUrl: './wareHouseProductMapping.component.html',
	styleUrls: ['./wareHouseProductMapping.component.scss']
})
export class WareHouseProductMappingComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','createdUserId','createdDate','lastUpdatedUserId','lastUpdatedDate','status','isDeleted','productId','wareHouseId', 'update','delete'];

	wareHouseProductMappingList:WareHouseProductMapping[];
	wareHouseProductMapping:WareHouseProductMapping=new WareHouseProductMapping();

	wareHouseProductMappingAddForm: FormGroup;


	wareHouseProductMappingId:number;

	constructor(private wareHouseProductMappingService:WareHouseProductMappingService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getWareHouseProductMappingList();
    }

	ngOnInit() {

		this.createWareHouseProductMappingAddForm();
	}


	getWareHouseProductMappingList() {
		this.wareHouseProductMappingService.getWareHouseProductMappingList().subscribe(data => {
			this.wareHouseProductMappingList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.wareHouseProductMappingAddForm.valid) {
			this.wareHouseProductMapping = Object.assign({}, this.wareHouseProductMappingAddForm.value)

			if (this.wareHouseProductMapping.id == 0)
				this.addWareHouseProductMapping();
			else
				this.updateWareHouseProductMapping();
		}

	}

	addWareHouseProductMapping(){

		this.wareHouseProductMappingService.addWareHouseProductMapping(this.wareHouseProductMapping).subscribe(data => {
			this.getWareHouseProductMappingList();
			this.wareHouseProductMapping = new WareHouseProductMapping();
			jQuery('#warehouseproductmapping').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.wareHouseProductMappingAddForm);

		})

	}

	updateWareHouseProductMapping(){

		this.wareHouseProductMappingService.updateWareHouseProductMapping(this.wareHouseProductMapping).subscribe(data => {

			var index=this.wareHouseProductMappingList.findIndex(x=>x.id==this.wareHouseProductMapping.id);
			this.wareHouseProductMappingList[index]=this.wareHouseProductMapping;
			this.dataSource = new MatTableDataSource(this.wareHouseProductMappingList);
            this.configDataTable();
			this.wareHouseProductMapping = new WareHouseProductMapping();
			jQuery('#warehouseproductmapping').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.wareHouseProductMappingAddForm);

		})

	}

	createWareHouseProductMappingAddForm() {
		this.wareHouseProductMappingAddForm = this.formBuilder.group({		
			id : [0],
createdUserId : [0, Validators.required],
createdDate : [null, Validators.required],
lastUpdatedUserId : [0, Validators.required],
lastUpdatedDate : [null, Validators.required],
status : [false, Validators.required],
isDeleted : [false, Validators.required],
productId : [0, Validators.required],
wareHouseId : [0, Validators.required]
		})
	}

	deleteWareHouseProductMapping(wareHouseProductMappingId:number){
		this.wareHouseProductMappingService.deleteWareHouseProductMapping(wareHouseProductMappingId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.wareHouseProductMappingList=this.wareHouseProductMappingList.filter(x=> x.id!=wareHouseProductMappingId);
			this.dataSource = new MatTableDataSource(this.wareHouseProductMappingList);
			this.configDataTable();
		})
	}

	getWareHouseProductMappingById(wareHouseProductMappingId:number){
		this.clearFormGroup(this.wareHouseProductMappingAddForm);
		this.wareHouseProductMappingService.getWareHouseProductMappingById(wareHouseProductMappingId).subscribe(data=>{
			this.wareHouseProductMapping=data;
			this.wareHouseProductMappingAddForm.patchValue(data);
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

	checkClaim(claim:string):boolean{
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
