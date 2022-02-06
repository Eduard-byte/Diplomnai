//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UIKitTutorials.Entities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    
    public partial class Room
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Room()
        {
            this.RegisterRooms = new HashSet<RegisterRoom>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public string Image { get; set; }
        public string Location { get; set; }
        public Nullable<double> Rating { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegisterRoom> RegisterRooms { get; set; }

        public string GetPhoto
        {
            get
            {
                if (Image is null)
                    return Directory.GetCurrentDirectory() + @"\Images\Room\Default.jpg";

                return Directory.GetCurrentDirectory() + @"\Images\Room\" + Image.Trim();
            }
        }

        public string GetStatusColor
        {
            get
            {
                if (Status)
                {
                    return "#42E448";
                }

                return "#FF5656";
            }
        }

        public string GetStatusStr
        {
            get
            {
                if (Status)
                {
                    return "Свободен";
                }

                return "Занят";
            }
        }

        public string ShortDesc
        {
            get
            {
                string[] str = Description.Split(' ');

                string[] words = new string[10];

                for (int i = 0; i < 6; i++)
                {
                    words[i] = str[i];
                }

                return String.Join(" ", words);

            }
        }
    }
}
