namespace Powerplant;

public class Payload
{

    public class Fuel
    {

        public double Gas { get; set; }

        public double Kerosine { get; set; }

        public int Co2 { get; set; }

        public int Wind { get; set; }
    }

    public class Powerplant
    {

        public string? Name { get; set; }

        public string? Type { get; set; }

        public double Efficiency { get; set; }

        public int Pmin { get; set; }

        public int Pmax { get; set; }
    }

    public int Load { get; set; }

    public Fuel? Fuels { get; set; }

    public Powerplant[]? Powerplants { get; set; }
}
