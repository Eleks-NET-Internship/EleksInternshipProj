import { NgModule } from "@angular/core"
import { CommonModule } from "@angular/common"
import { RouterModule } from "@angular/router"

import { MainLayoutComponent } from "./components/layouts/main-layout/main-layout.component"
import { AuthLayoutComponent } from "./components/layouts/auth-layout/auth-layout.component"

import { SidebarComponent } from "../feature/sidebar/components/sidebar/sidebar.component"
import { MatIcon } from "@angular/material/icon"
import { MatListItem, MatNavList } from "@angular/material/list"

@NgModule({
  declarations: [
    MainLayoutComponent,
    AuthLayoutComponent,
    SidebarComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    MatIcon,
    MatNavList,
    MatListItem,
    MatIcon
  ],
  exports: [
    MainLayoutComponent,
    AuthLayoutComponent
  ]
})
export class SharedModule { }
