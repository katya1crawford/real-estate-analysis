import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-post-acceptance',
  templateUrl: './post-acceptance.component.html',
  styleUrls: ['./post-acceptance.component.css']
})
export class PostAcceptanceComponent implements OnInit {

  public panelOpenState = false;

  step = 0;



  ngOnInit(): void {
  }

}
