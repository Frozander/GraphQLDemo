using HotChocolate.Types;

namespace ChatboxDemo.GraphQL.Messages
{
    public class AddMessageInputType : InputObjectType<AddMessageInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AddMessageInput> descriptor)
        {
            descriptor.Description("Represents the input to add for a message.");

            descriptor
                .Field(p => p.author)
                .Description("Represents the author name for the message.");

            descriptor
                .Field(p => p.content)
                .Description("Represents the message content.");

            base.Configure(descriptor);
        }
    }
}