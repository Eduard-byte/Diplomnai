using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKitTutorials.Entities;

namespace UIKitTutorials.Helpers
{
    public class RoomUser
    {
        public static Room Room { get; set; }

        public static RegisterRoom Reserve { get; set; }

        public static int GetDays
        {
            get
            {
                return GetDaysAccommodation();
            }
        }

        public static decimal GetPrice
        {
            get
            {
                return GetDaysAccommodation() * Room.Price;
            }
        }

        public static object GetDaysToAccommodation
        {
            get
            {
                var result = (Reserve.StartDate - DateTime.Now).Days;

                if (result < 0)
                {
                    return "вы уже заселены";
                }

                return (Reserve.StartDate - DateTime.Now).Days;
            }
        }

        private static int GetDaysAccommodation()
        {
            var room = Reserve;

            if (room is null)
                return 0;

            var StartDate = room.StartDate;
            var EndDate = room.EndDate;

            return (EndDate - StartDate).Days;
        }
    }
}
