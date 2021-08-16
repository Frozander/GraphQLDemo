import React from 'react';
import ReactDOM from 'react-dom';
import './style.css';
import reportWebVitals from './reportWebVitals';
import { BrowserRouter } from 'react-router-dom';
import {
  ApolloClient,
  InMemoryCache,
  ApolloProvider,
  gql,
  useSubscription,
  useMutation
} from '@apollo/client';
import { WebSocketLink } from '@apollo/client/link/ws';
import { split, HttpLink } from '@apollo/client';
import { getMainDefinition } from '@apollo/client/utilities';

import seedColor from 'seed-color';

const httpLink = new HttpLink({
  uri: 'http://localhost:5000/graphql',
});

const wslink = new WebSocketLink({
  uri: "ws://localhost:5000/graphql",
  options: {
    reconnect: true,
  }
});

const splitLink = split(
  ({ query }) => {
    const { kind, operation } = getMainDefinition(query);
    return kind === 'OperationDefinition' && operation === 'subscription';
  },
  wslink,
  httpLink,
);

const client = new ApolloClient({
  link: splitLink,
  cache: new InMemoryCache()
});


const MESSAGE_SUBSCRIPTION = gql`
  subscription {
    onMessageAdded{
      id
      author
      content
    }
  }
`;

const ADD_MESSAGE = gql`
mutation AddNewMessage (
  $author: String!
  $content: String!
  ) {
  addNewMessage (
    author: $author
    content: $content
  )
  {
    id
  }
}
`;

const messages = []

function LatestMessage() {
  const { data, loading } = useSubscription(
    MESSAGE_SUBSCRIPTION
  );

  if (!loading) {
    messages.push({
      "id": data.onMessageAdded.id,
      "author": data.onMessageAdded.author,
      "content": data.onMessageAdded.content
    })
  }

  const messageslist = messages.slice(-20).map((d) => <p key={d.id}><span style={{ color: seedColor(d.author).toHex() }}>{d.author}</span>: {d.content}</p>)

  return (<div className="chatbox">
    {messageslist}
  </div>);
}

function SendMessage() {
  let user;
  let text;
  const [addMessage, { data }] = useMutation(ADD_MESSAGE);

  return (
    <div>
      <form
        onSubmit={e => {
          e.preventDefault();
          if (user.value === '' || text.value === '') return;
          addMessage({ variables: { author: user.value, content: text.value } });
          text.value = '';
        }}
      >
        <input className="user-field" ref={node => {
          user = node;
        }} placeholder="Username" />
        <input className="input-field" ref={node => {
          text = node;
        }} placeholder="Message" />

        <button className="input-button" type="submit">Send</button>

      </form>
    </div>
  )
}

ReactDOM.render(
  <BrowserRouter>
    <ApolloProvider client={client}>
      <LatestMessage />
      <SendMessage />
    </ApolloProvider>
  </BrowserRouter>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
