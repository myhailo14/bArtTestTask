using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using bArtTestTask.WebAPI.Exceptions;

namespace bArtTestTask.WebAPI.Helpers;

public static class ExceptionHelper
{
    public static void ThrowRecordNotFoundIfNull([NotNull] object? o, Guid id)
    {
        if (o is null)
        {
            throw new RecordNotFoundException(id);
        }
    }

    public static void ThrowRecordNotFoundIfNull([NotNull] object? o, Guid id, string entityName)
    {
        if (o is null)
        {
            throw new RecordNotFoundException(id, entityName);
        }
    }

    public static void ThrowRecordNotFoundIfNull([NotNull] object? o, string exceptionMessage)
    {
        if (o is null)
        {
            throw new RecordNotFoundException(exceptionMessage);
        }
    }

    public static void ThrowRecordAlreadyExistsIfNotNull(object? o, Guid id)
    {
        if (o is not null)
        {
            throw new RecordAlreadyExistsException(id);
        }
    }

    public static void ThrowRecordAlreadyExistsIfNotNull(object? o, Guid id, string entityName)
    {
        if (o is not null)
        {
            throw new RecordAlreadyExistsException(id, entityName);
        }
    }

    public static void ThrowRecordAlreadyExistsIfNotNull(object? o, string message)
    {
        if (o is not null)
        {
            throw new RecordAlreadyExistsException(message);
        }
    }
}