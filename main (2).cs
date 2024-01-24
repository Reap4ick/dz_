using System;

public abstract class Car
{
    public string Name { get; set; }
    public int Speed { get; set; }

    public Car(string name)
    {
        Name = name;
    }

    public event EventHandler<string> Finish;

    public void Drive()
    {
        Random random = new Random();

        for (int i = 0; i <= 100; i += Speed)
        {
            Console.WriteLine($"{Name} is at a distance of {i} km. Speed: {Speed} km/h.");

            if (i == 100)
            {
                OnFinish($"{Name} finished!");
            }

            System.Threading.Thread.Sleep(random.Next(100, 500));
            Speed = random.Next(1, 20); // Change speed randomly
        }
    }

    protected virtual void OnFinish(string message)
    {
        Finish?.Invoke(this, message);
    }
}

public class SportsCar : Car
{
    public SportsCar(string name) : base(name)
    {
    }
}

public class Sedan : Car
{
    public Sedan(string name) : base(name)
    {
    }
}

public class Truck : Car
{
    public Truck(string name) : base(name)
    {
    }
}

public class Bus : Car
{
    public Bus(string name) : base(name)
    {
    }
}

public class RacingGame
{
    public delegate void StartRaceHandler();

    public event StartRaceHandler StartRace;

    public void Start()
    {
        OnStartRace();
    }

    protected virtual void OnStartRace()
    {
        StartRace?.Invoke();
    }
}

class Program
{
    static void Main()
    {
        RacingGame racingGame = new RacingGame();

        SportsCar sportsCar = new SportsCar("Sports Car");
        Sedan sedan = new Sedan("Sedan Car");
        Truck truck = new Truck("Truck");
        Bus bus = new Bus("Bus");

        sportsCar.Finish += (sender, message) => Console.WriteLine(message);
        sedan.Finish += (sender, message) => Console.WriteLine(message);
        truck.Finish += (sender, message) => Console.WriteLine(message);
        bus.Finish += (sender, message) => Console.WriteLine(message);

        racingGame.StartRace += sportsCar.Drive;
        racingGame.StartRace += sedan.Drive;
        racingGame.StartRace += truck.Drive;
        racingGame.StartRace += bus.Drive;

        racingGame.Start();

        Console.ReadLine();
    }
}
