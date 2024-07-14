namespace ReservationApi.Application.Models
{
    public record ErrorModel(string Message, List<KeyValuePair<string, string>> Metadata);
}
