import { Chat, Channel, ChannelList, ChannelHeader, Thread, Window, MessageList, MessageInput, } from 'stream-chat-react';
import React from 'react';
import { useSearchParams } from 'react-router-dom';
import { StreamChat } from 'stream-chat';
import axios from 'axios';
import './app.scss';

const client = new StreamChat('yfea84z3npk4');

function App() {
  const [searchParams] = useSearchParams();
  const user = searchParams.get('user');
  client.connectUser(
    {
      id: user,
      name: user,
    },
    async () => await axios.get('http://localhost:5274/Token?user=' + user).then(x => x.data),
  );

  const filters = { members: { $in: [ user ] } }

  return (
    <Chat client={client}>
    <ChannelList showChannelSearch filters={filters} />
    <Channel>
      <Window>
        <ChannelHeader />
        <MessageList />
        <MessageInput />
      </Window>
      <Thread />
    </Channel>
  </Chat>
  );
}

export default App;
