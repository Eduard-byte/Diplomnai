using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using UIKitTutorials.Entities;

namespace UIKitTutorials.Helpers
{
    public class Auth
    {
        public static User User { get; set; }

        public static ImageBrush ImageUser { get; set; }
        public static Label NameUser { get; set; }
        public static Label EmailUser { get; set; }

        public static bool IsAdmin { get; set; } = false;
    }
}
