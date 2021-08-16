using System.Linq;
using ChatboxDemo.Data;
using ChatboxDemo.Models;
using HotChocolate;
using HotChocolate.Data;

namespace ChatboxDemo.GraphQL
{
  [GraphQLDescription("Represents the queries available.")]
  public class Query
  {
    [UseDbContext(typeof(ChatboxDbContext))]
    [UseFiltering]
    [UseSorting]
    [GraphQLDescription("Gets queryable messages.")]
    public IQueryable<Message> GetMessage([ScopedService] ChatboxDbContext context)
    {
      return context.Messages;
    }
  }
}
