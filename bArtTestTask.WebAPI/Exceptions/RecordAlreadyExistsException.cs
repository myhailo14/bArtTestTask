using System.Runtime.Serialization;

namespace bArtTestTask.WebAPI.Exceptions;
[Serializable]
public class RecordAlreadyExistsException : Exception
{
    public RecordAlreadyExistsException(Guid id) : base($"Record with id {id} already exists.")
    {
        
    }
    
    public RecordAlreadyExistsException(Guid id, string entityName) : base($"{entityName} with id {id} already exists.")
    {
        
    }
    
    public RecordAlreadyExistsException(string message) : base(message)
    {
        
    }
    
    public RecordAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        
    }
}