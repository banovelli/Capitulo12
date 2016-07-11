using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorDeColmeia
{
    public enum BeeState
    {
        Idle,
        FlyngToFlower,
        GatheringNectar,
        ReturningToHive,
        MakingHoney,
        Retired
    };

    public class Bee
    {
        private const double HoneyConsumed = 0.5;
        private const int MoveRate = 3;
        private const double MinimumFlowerNectar = 1.5;
        private const int CareeSpan = 1000;
        public int Age { get; private set; }
        public bool InsideHive { get; private set; }
        public double NectarCollected { get; private set; }
        private Point location;
        public Point Location { get { return location; } }
        private int ID;
        private Flower destinationFlower;
        public BeeState CurrentState {get; private set;};

        public Bee(int id, Point location)
        {
            this.ID = id;
            this.location = location;
            Age = 0;
            InsideHive = true;
            destinationFlower = null;
            NectarCollected = 0;
            CurrentState = BeeState.Idle;
        }

        public void Go(Random random)
        {
            Age++;
            switch (CurrentState)
            {
                case BeeState.Idle:
                    if(Age > CareeSpan){
                        CurrentState = BeeState.Retired;
                    }
                    else{
                        //o que fazer quando ocioso --> programar ;-)
                    }
                    break;
                case BeeState.FlyngToFlower:
                    //ir para uma flor
                    break;
                case BeeState.GatheringNectar:
                    double nectar = destinationFlower.HarvestNectar();
                    if(nectar > 0)
                        NectarCollected += nectar;
                    else
                        CurrentState = BeeState.ReturningToHive;
                    break;
                case BeeState.ReturningToHive:
                    if(!InsideHive){
                        //ir para colmeia
                    }
                    else{
                        //o que fazer no interior
                    }
                    break;
                case BeeState.MakingHoney:
                    if(NectarCollected < 0.5){
                        NectarCollected = 0;
                        CurrentState = BeeState.Idle;
                    }
                    else{
                        //tonar o nectar em mel
                    }
                    break;
                case BeeState.Retired:
                    //não faz nada, já trabalho d+ :]
                    break;
                default:
                     //não faz nada,
                    break;
            }
        }

        private bool MoveTowardsLocation(Point destination)
        {
            if (destination != null)
            {
                if (Math.Abs(destination.X - location.X) <= MoveRate &&
                    Math.Abs(destination.Y - location.Y) <= MoveRate)
                    return true;
                if (destination.X > location.X)
                    location.X += MoveRate;
                else if (destination.X < location.X)
                    location.X -= MoveRate;
                if (destination.Y > location.Y)
                    location.Y += MoveRate;
                else if (destination.Y < location.Y)
                    location.Y -= MoveRate;
            }
            return false;
        }        
    }
}
