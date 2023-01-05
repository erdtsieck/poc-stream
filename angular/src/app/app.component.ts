import { Component, OnInit } from '@angular/core';
import { ChatClientService, ChannelService, StreamI18nService } from 'stream-chat-angular';
import axios from 'axios';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  constructor(
    private chatService: ChatClientService,
    private channelService: ChannelService,
    private streamI18nService: StreamI18nService,
  ) {
    const user = '00000000-0000-0000-0000-000000000001';

    this.chatService.init('yfea84z3npk4', user, async () => await axios.get('http://localhost:5274/Token?user=' + user).then(x => x.data));
    this.streamI18nService.setTranslation();
  }

  async ngOnInit() {
    const user = '00000000-0000-0000-0000-000000000001';
    this.channelService.init({
      type: 'messaging',
      members: { $in: [ user ] }
    });
  }
}
