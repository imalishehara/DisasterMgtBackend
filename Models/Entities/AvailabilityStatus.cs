using System.Text.Json.Serialization;

namespace Disaster_demo.Models.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AvailabilityStatus
    {
        //None,
        Unavailable,
        Available
        
    }
}
