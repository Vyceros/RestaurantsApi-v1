namespace Restaurants.Domain.Exceptions;

public class NotFoundException(string resourceType,string resourceIdentifier)
    : Exception($"{resourceType} with the id: {resourceIdentifier} doesn't exist.")
{
}
