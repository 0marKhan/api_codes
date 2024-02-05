namespace HelloWorld.Models{
    public class Computer{
        public string MotherBoard {get; set;} = ""; // for setting the string to null
        public int CPUcores {get; set; }
        public bool HasWifi { get; set; }
        public bool HasLTE { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string VideoCard { get; set; } = "";


    }
}

