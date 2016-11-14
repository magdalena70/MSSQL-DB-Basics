namespace HotelDB
{
    class Program
    {
        static void Main()
        {
            HotelContext context = new HotelContext();
            //No relationships between tables are required.
            context.Database.Initialize(true);
        }
    }
}
