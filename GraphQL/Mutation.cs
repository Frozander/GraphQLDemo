using System.Threading;
using System.Threading.Tasks;
using ChatboxDemo.Data;
using ChatboxDemo.GraphQL.Messages;
using ChatboxDemo.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;

namespace ChatboxDemo.GraphQL
{
  [GraphQLDescription("Represents the mutations available.")]
  public class Mutation
  {
    [UseDbContext(typeof(ChatboxDbContext))]
    [GraphQLDescription("Adds a message.")]
    public async Task<AddMessagePayload> AddMessageAsync(
        AddMessageInput input,
        [ScopedService] ChatboxDbContext context,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken
    )
    {
      var message = new Message
      {
        Author = input.author,
        Content = input.content
      };

      context.Messages.Add(message);
      await context.SaveChangesAsync(cancellationToken);
      await eventSender.SendAsync(nameof(Subscription.OnMessageAdded), message, cancellationToken);
      return new AddMessagePayload(message);
    }

    [UseDbContext(typeof(ChatboxDbContext))]
    [GraphQLDescription("Adds a message.")]
    public async Task<Message> AddNewMessageAsync(
        string author,
        string content,
        [ScopedService] ChatboxDbContext context,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken
    )
    {
      var message = new Message
      {
        Author = author,
        Content = content
      };

      context.Messages.Add(message);
      await context.SaveChangesAsync(cancellationToken);
      await eventSender.SendAsync(nameof(Subscription.OnMessageAdded), message, cancellationToken);
      return message;
    }
  }
}
