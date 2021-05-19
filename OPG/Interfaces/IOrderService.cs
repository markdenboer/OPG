using OPG.Models;

namespace OPG.Interfaces
{
    public interface IOrderService
    {
        public void AddOrderTask(Order order);
        public Order GetOrder(int orderNr);
    }
}
