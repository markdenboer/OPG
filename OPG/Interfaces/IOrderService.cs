using OPG.DTO;
using OPG.Models;
using System.Threading.Tasks;

namespace OPG.Interfaces
{
    public interface IOrderService
    {
        public void AddOrderTask(OrderDTO order);
        public Order GetOrder(int orderNr);
    }
}
