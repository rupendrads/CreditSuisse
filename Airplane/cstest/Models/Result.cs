using System.Collections.Generic;

public class Result
{
    public float Probability { get; set; }
    
    public IList<PassengerMatch> PassengerMatchList { get; set; }
}

public class PassengerMatch
{
    public Passenger Passenger {get; set;}

    public bool Match { get; set; }
}

