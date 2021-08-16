using System.Linq;
using ChatboxDemo.Data;
using ChatboxDemo.Models;
using HotChocolate;
using HotChocolate.Types;

namespace ChatboxDemo.GraphQL.Messages
{
    public class MessageType : ObjectType<Message>
    {
        protected override void Configure(IObjectTypeDescriptor<Message> descriptor)
        {
            descriptor.Description("Represents a message sent by a user.");

            descriptor
                .Field(p => p.Id)
                .Description("Represents the id of a given message.");

            descriptor
                .Field(p => p.Author)
                .Description("Represents the name of the author of the message.");

            descriptor
                .Field(p => p.Content)
                .Description("Represents the content of the message.");
        }

        private class Resolvers
        {
            public IQueryable<Message> GetMessages(Message message, [ScopedService] ChatboxDbContext context)
            {
                return context.Messages.Where(p => p.Id == message.Id);
            }
        }
    }
}