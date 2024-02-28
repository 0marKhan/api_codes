using System.Text.Json.Serialization;

namespace HelloWorld.Models
{
    public class Computer
    {
        // lets you map the value to the value below it
        [JsonPropertyName("computer_id") ]
        public int ComputerId { get; set; } 

        [JsonPropertyName("motherboard") ]
        public string MotherBoard { get; set; } = "";

        [JsonPropertyName("cpu_cores") ]
        public int? CPUcores { get; set; }

        [JsonPropertyName("has_wifi") ]
        public bool HasWifi { get; set; }

        [JsonPropertyName("has_lte") ]
        public bool HasLTE { get; set; }

        [JsonPropertyName("release_date") ]
        public DateTime? ReleaseDate { get; set; }

        [JsonPropertyName("price") ]
        public decimal Price { get; set; }

        [JsonPropertyName("video_card") ]
        public string VideoCard { get; set; } = "";

        public Computer()
        {
            if (CPUcores == null)
            {
                CPUcores = 0;
            }
        }
    }
}
