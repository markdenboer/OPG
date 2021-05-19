using OPG.Models;
using System.Threading.Tasks;

namespace OPG.Interfaces
{
    public interface IOrderService
    {
        public void AddOrderTask(Order order);
        public Task AddOrder(Order order);
        public Order GetOrder(int orderNr);
    }
}
