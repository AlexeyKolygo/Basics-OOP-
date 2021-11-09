using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Buildings
{
    public class Buildings
    {
        private static long Bid;
        private int Floors;
        private double Height;
        private int Entrance;
        private int Appartment;


        public Buildings(int floors, int entrance)
        {
            if (entrance <= 0) entrance = 1;
            this.Entrance = entrance;
            var settings = new AppSettings(1);
            Bid = GenerateBid(settings.BuildingCounter);
            this.Floors = floors;
            this.Height = GetBuldingHeight(floors,settings.FloorsHeight);
            this.Appartment = GetAppartmentsPerFloor(floors,settings.AppPerFloor);
            
        }

        private double GetBuldingHeight(int floors, double height)
        {
            return (floors* height)/this.Entrance;
        }

        private int GetAppartmentsPerFloor( int floor, int AppPerFloor)
        {
            return floor * AppPerFloor;
        }

        private long GenerateBid(int lastBid)
        {
            lastBid++;
            var savesettings = new AppSettings(1);
                savesettings.SaveSettings(lastBid);
                return lastBid;
        }

        public void PrintDetalis()
        {
            Console.WriteLine($"Here's the details of your building! Bid: {Bid},Height: {Height},Appartments per floor:{Appartment}");
        }
    }
}
