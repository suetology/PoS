import { Component } from '@angular/core';
import { filter, Observable, Subscription } from 'rxjs';
import { Item } from '../../types';
import { ItemService } from '../../services/item.service';
import { ActivatedRoute, NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { AsyncPipe, CommonModule } from '@angular/common';

@Component({
  selector: 'app-item',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterOutlet],
  templateUrl: './item.component.html',
  styleUrl: './item.component.css'
})
export class ItemComponent {
  items$: Observable<Item[]>;
  isModalOpen = false;
  private routeSub: Subscription;
  private updateSub: Subscription;

  constructor(private itemService: ItemService, 
    private router: Router,
    private route: ActivatedRoute) {

    this.items$ = this.itemService.getItems();

    this.updateSub = this.itemService.getItemsUpdated().subscribe(() => {
      this.items$ = this.itemService.getItems();
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

  trackById(index: number, item: Item): string {
    return item.id;
  }

  goToItemDetails(id: string) {
    this.router.navigate([id], { relativeTo: this.route });
  }
}
