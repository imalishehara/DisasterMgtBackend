using System.Text.Json.Serialization;

namespace Disaster_demo.Models.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRole
    {
        
        DS,
        DMC,
        volunteer

    }
}
