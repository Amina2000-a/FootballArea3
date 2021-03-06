//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FootballArea.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservation
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int AreaID { get; set; }
        public int RoomID { get; set; }
        public System.DateTime ReserveDateFrom { get; set; }
        public System.DateTime ReserveDateTo { get; set; }
        public decimal Price { get; set; }
    
        public virtual Areas Areas { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Rooms Rooms { get; set; }
        public DateTime Reservation_From { get; internal set; }
        public DateTime Reservation_To { get; internal set; }
        public int Customer_Id { get; internal set; }
        public int Area_Id { get; internal set; }
        public int Rooms_Id { get; internal set; }
    }
}
