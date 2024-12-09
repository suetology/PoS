import { AsyncPipe, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ItemGroupService } from '../../services/item-group.service';
import { ItemGroup } from '../../types';

@Component({
  selector: 'app-item-group-details',
  standalone: true,
  imports: [AsyncPipe, NgIf],
  templateUrl: './item-group-details.component.html',
  styleUrl: './item-group-details.component.css'
})
export class ItemGroupDetailsComponent implements OnInit {
  itemGroup: ItemGroup | undefined;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private itemGroupService: ItemGroupService
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.itemGroupService.getItemGroup(id).subscribe(
        (itemGroup) => {
          this.itemGroup = itemGroup;
        },
        (error) => {
          console.error('Error fetching item group details:', error);
          this.close();
        }
      );
    } else {
      this.close();
    }
  }

  close() {
    this.router.navigate(['../'], { relativeTo: this.route });
  }
}
