using WebStore.ViewModels;

namespace WebStore.Servicess.Interfaces
{
    public interface ICartService
    {

        void Add(int Id);

        void Decrement(int Id);

        void Remove(int Id);

        void Clear();

        CartViewModel GetCartViewModel();
    }
}
