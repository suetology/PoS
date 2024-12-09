import { AsyncPipe, CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { filter, Observable, Subscription } from 'rxjs';
import { ItemGroup } from '../../types';
import { ItemGroupService } from '../../services/item-group.service';

@Component({
  selector: 'app-item-group',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterOutlet],
  templateUrl: './item-group.component.html',
  styleUrl: './item-group.component.css'
})
export class ItemGroupComponent {

  itemGroups$: Observable<ItemGroup[]>;
  isModalOpen = false;
  private routeSub: Subscription;
  private updateSub: Subscription;

  constructor(private itemGroupService : ItemGroupService, 
    private router: Router,
    private route: ActivatedRoute) {

    this.itemGroups$ = this.itemGroupService.getAllItemGroups();

    this.updateSub = this.itemGroupService.getItemGroupsUpdated().subscribe(() => {
    this.itemGroups$ = this.itemGroupService.getAllItemGroups();
    });

    this.routeSub = this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe(() => {
        this.isModalOpen = !!this.route.firstChild;
      });
  }

  ngOnDestroy() {
    if (this.routeSub) {
      this.routeSub.unsubscribe();
    }
    if (this.updateSub) {
      this.updateSub.unsubscribe();
    }
  }

  trackById(index: number, itemGroup: ItemGroup): string {
    return itemGroup.id;
  }

  goToItemGroupDetails(id: string) {
    this.router.navigate([id], { relativeTo: this.route });
  }
}
