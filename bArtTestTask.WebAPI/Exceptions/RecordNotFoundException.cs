using System.Runtime.Serialization;

namespace bArtTestTask.WebAPI.Exceptions;

[Serializable]
public class RecordNotFoundException : KeyNotFoundException
{
    public RecordNotFoundException(Guid id) : base($"Record with id {id} does not exist")
    {
    }

    public RecordNotFoundException(Guid id, string entityName) : base($"{entityName} with id {id} does not exist")
    {
    }
    
    public RecordNotFoundException(string message) : base(message)
    {
    }

    public RecordNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        
    }
}