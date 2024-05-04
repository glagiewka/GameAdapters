public class Traces {
   public double Throttle { get; set; }
   public double Brake { get; set; }
   public double Clutch { get; set; }
   public double Steering { get; set; }

    public override string ToString() {
        return $"Throttle: {Throttle}, Brake: {Brake}, Clutch: {Clutch}, Steering: {Steering}";
    }  
}
