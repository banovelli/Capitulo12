using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuladorDeColmeia
{
    public class Hive
    {
        public double Honey { get; private set; }
        private Dictionary<string, Point> locations;
        private int beeCount;

        private const int initialBee = 6;
        private const double initialHoney = 3.2;
        private const double maxHoney = 15;
        private const double honeyFromNectar = 0.25;
        private const int maxBee = 8;
        private const int HoneyToNewBee = 4;

        public void Hive()
        {
            Honey = initialHoney;           
            InitializeLocations();
            Random random = new Random();
            for (int i = 0; i < initialBee; i++ )
                AddBee(random);
        }

        private void AddBee(Random random)
        {
            beeCount++;
            int r1 = random.Next(50);
            int r2 = random.Next(50);
            Point startPoint = new Point(locations["Berçario"].X + r1, locations["Berçario"].Y + r2);
            Bee bee = new Bee(beeCount, startPoint); 
        }

        public Point getLocation(string location)
        {
            try{
                return locations[location];
            }
            catch(ArgumentException e){
                MessageBox.Show("Argumento localizacao inválido:" + e,"Atenção");
                return Point.Empty;
            }
            /*
             * if (locations.Keys.Contains(location))
             *   return locations[location];
             * else
             *   trow new ArgumentExcption ("Unknow location: " + location);
             * */
        }

        public void InitializeLocations()
        {
            locations = new Dictionary<string, Point>();
            locations.Add("Entrada", new Point(600, 100));
            locations.Add("Berçario", new Point(95, 174));
            locations.Add("Fábrica de Mel", new Point(157, 98));
            locations.Add("Saída", new Point(194, 213));
        }

        public bool AddHoney(double nectar)
        {
            double honeyToAdd = nectar * honeyFromNectar;
            if (honeyToAdd + Honey > maxHoney)
                return false;
            Honey += honeyToAdd;
            return true;
        }

        public bool ConsumeHoney(double amount)
        {
            if (amount > Honey)
                return false;
            Honey -= amount;
            return true;
        }

        public void Go(Random random)
        {
            if (honeyFromNectar > HoneyToNewBee && random.Next(10) == 1)
                AddBee(random);
        }
    }
}
