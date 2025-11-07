namespace ECommerce.Core.Entities.Payment
{
    public interface IPayment
    {
        bool ProcessPayment(decimal amount);
    }
}
