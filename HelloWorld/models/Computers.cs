namespace HelloWorld.Models
{
    public class Computer
    {
        public int ComputerId { get; set; } 
        public string MotherBoard { get; set; } = "";
        public int? CPUcores { get; set; }
        public bool HasWifi { get; set; }
        public bool HasLTE { get; set; }
        public DateTime? ReleaseDate { get; set; }

        public decimal Price { get; set; }
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
