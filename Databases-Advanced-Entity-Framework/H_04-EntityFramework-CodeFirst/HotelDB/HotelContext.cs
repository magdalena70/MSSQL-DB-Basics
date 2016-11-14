namespace HotelDB
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class HotelContext : DbContext
    {
        public HotelContext()
            : base("name=HotelContext")
        {
        }


        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomStatus> RoomStatuses { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<BedType> BedTypes { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Occupancies> Occupancieses { get; set; }
    }
}