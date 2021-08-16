using HotChocolate.Types;

namespace ChatboxDemo.GraphQL.Messages
{
    public class AddMessagePayloadType : ObjectType<AddMessagePayload>
    {
        protected override void Configure(IObjectTypeDescriptor<AddMessagePayload> descriptor)
        {
            descriptor.Description("Represents the payload to return for an added message");

            descriptor
                .Field(p => p.message)
                .Description("Represents the added message");

            base.Configure(descriptor);
        }
    }
}