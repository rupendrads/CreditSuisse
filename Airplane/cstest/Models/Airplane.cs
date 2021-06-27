
using System;
using System.Collections.Generic;
using System.Linq;
public class Airplane: IAirplane {
    
    private IList<Seat> _Seats; 
    private IList<Passenger> _Passengers;

    private bool[] simulations;

    private float probability;

    private Result result;

    public Airplane()
    {
        _Seats = new List<Seat>();
        _Passengers = new List<Passenger>();
        PopulateSeats();
        PopulatePassangers();
    }

    public IList<Passenger> Passengers 
    {
        get
        {
            return _Passengers;
        }
    }
    public IList<Seat> Seats 
    {
        get
        {
            return _Seats;
        }
    }

    public float Probability 
    {
        get
        {
            return probability;
        }
    }

    public bool[] Simulations { 
        get
        {
            return simulations;
        }
    }

    public Result Result { 
        get
        {
            return result;
        }
    }

    private void PopulateSeats() 
    {
        for(int i=1; i<=100; i++) {
            _Seats.Add(new Seat{
                No = i,
                Empty = true,
            });
        }
    }

    private void PopulatePassangers()
    {
        for(int i=1; i<=100; i++) {
            _Passengers.Add(new Passenger{
                No = i,
                AllocatedSeat  = i,
                TakenSeat = -1
            });
        }
    }
    
    void Clear()
    {
        // clear seats objects
        Seats.Clear();
        PopulateSeats();

        // cleat passengers objects
        Passengers.Clear();
        PopulatePassangers();
    }

    public Result CheckProbability(int simulationsCount) {
        if(simulationsCount < 1)
        {
            throw new Exception("Simulation count should be more than zero.");
        }

        simulations = new bool[simulationsCount];
        IList<PassengerMatch> passengerMatchList = new List<PassengerMatch>();

        // for each simulation
        for(int j=0; j<simulationsCount; j++)
        {
            // for every passenger entering airplane
            for(int i=1; i<101; i++) 
            {
                // current passenger
                var passanger = Passengers[i-1];

                // seats available (empty seats)
                var emptyList = _Seats.Where(s => s.Empty).ToList();

                // expected/allocated set (book seat)
                Seat expectedSeat = null;

                // find expected seat for any passenger other than first one
                if(i > 1)
                {
                    expectedSeat = emptyList.FirstOrDefault(s => s.No == passanger.AllocatedSeat); 
                }

                // expected seat taken
                if(expectedSeat!= null) 
                {
                    passanger.TakenSeat = expectedSeat.No;
                    var takenSeat = _Seats.FirstOrDefault(s => s.No == expectedSeat.No);
                    takenSeat.Empty = false;
                }
                else 
                {
                    // random seat taken
                    var randomNo = new Random();
                    var availableSeatsCount = emptyList.Count;
                    var randomSeatIndex = randomNo.Next(1, availableSeatsCount + 1);

                    var takenSeat = _Seats.FirstOrDefault(s => s.No == emptyList[randomSeatIndex - 1].No);
                    takenSeat.Empty = false;

                    passanger.TakenSeat = takenSeat.No;
                }
            }

            //////////// after each simulation //////////////////////////
            // is last passenger getting allocated seat
            var lastPassenger = Passengers[Passengers.Count -1];     
            simulations[j] = (lastPassenger.TakenSeat == Passengers.Count) ? true: false;

            // last passenger and his seat status
            var pm = new PassengerMatch {
             Passenger = lastPassenger,
             Match =  simulations[j]
            };

            // passenger and his seat status list
            passengerMatchList.Add(pm);

            // clear seats and passangers
            Clear();
            //////////// after each simulation //////////////////////////
        }

        ///////////// after all simulations /////////////////////////
        // counts of last passengers who got allocated seat
        var trueCount = simulations.Count(s => s == true);

        // probability of getting allocated seat
        probability = (float) trueCount / simulationsCount;

        // final result
        result = new Result {
            PassengerMatchList = passengerMatchList,
            Probability = probability
        };
        ///////////// after all simulations /////////////////////////

        return result;
    }
} 