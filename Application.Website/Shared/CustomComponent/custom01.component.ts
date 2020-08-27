import { Component, OnInit, Input, ViewChild, ElementRef, NgZone } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: '[data-Custom01]',
  template: `
    <h1>Custom01 Component</h1>
    <p>
      TextHtml=(<span [innerHtml]="json.TextHtml"></span>);
    </p>
    <p>
    OffsetMillisecond={{ offsetMillisecond }}; LastUpdate={{ lastUpdate }};
    </p>
    <canvas #canvas width="300" height="300"></canvas>
  `,
  styles: [
    'canvas { border-style: solid }'
  ]
})
export class Custom01Component implements OnInit {

  constructor(private http: HttpClient, private ngZone: NgZone) { }

  offsetMillisecond: number = 0;
  lastUpdate: string = "";  

  ctx: CanvasRenderingContext2D;

  @ViewChild('canvas', { static: true }) 
  canvas: ElementRef<HTMLCanvasElement>;

  ngOnInit(): void {
    this.ctx = this.canvas.nativeElement.getContext('2d');
    this.ngZone.runOutsideAngular(() => this.tick());
    setInterval(() =>{
      this.http.get("http://localhost:5721/?HostNameOrAddress=185.17.70.106", {responseType: 'text'}).subscribe((data) => {
        let timeResult = <TimeResult>JSON.parse(data);
        this.offsetMillisecond = timeResult.OffsetMillisecond;
        this.lastUpdate = timeResult.LastUpdate;
      });
    }, 10000);
  }

  tick() {
    let timeMillisecond = new Date().getTime();
    timeMillisecond += this.offsetMillisecond;

    let pixel = ((timeMillisecond % 1000) / 1000) * this.ctx.canvas.height;

    this.ctx.clearRect(0, 0, this.ctx.canvas.width, this.ctx.canvas.height);
    this.ctx.fillStyle = 'blue';
    this.ctx.fillRect(0, 0, this.ctx.canvas.width, pixel);

    window.requestAnimationFrame(() => this.tick());
  }

  @Input() json: any;    
}

class TimeResult
{
  OffsetMillisecond: number;

  LastUpdate: string;
}