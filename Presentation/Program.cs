using ECommerce.Presentation;
using ECommerce.Infrastructure.Data;

namespace ECommerce.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var context = new FakeDbContext();
            context.ResetUsers();

            var menu = new Menu(context);
            menu.Start();
        }
    }
}
