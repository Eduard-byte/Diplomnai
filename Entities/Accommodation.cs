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
    
    public partial class Accommodation
    {
        public int Id { get; set; }
        public int Days { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Id_resgister { get; set; }
    
        public virtual RegisterRoom RegisterRoom { get; set; }
    }
}