using ChatboxDemo.Models;
using HotChocolate;
using HotChocolate.Types;

namespace ChatboxDemo.GraphQL
{
  [GraphQLDescription("Represents the subscriptions available.")]
  public class Subscription
  {
    [Subscribe]
    [Topic]
    [GraphQLDescription("The subscription for addition of a message.")]
    public Message OnMessageAdded([EventMessage] Message message)
    {
      return message;
    }
  }
}
