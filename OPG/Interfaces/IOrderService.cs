using OPG.DTO;
using OPG.Models;
using System.Threading.Tasks;

namespace OPG.Interfaces
{
    public interface IOrderService
    {
        public Task AddOrderTaskWithJSON(OrderDTO order);
        public Order GetOrder(int orderNr);
    }
}
