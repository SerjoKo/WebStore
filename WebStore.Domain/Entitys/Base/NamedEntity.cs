using WebStore.Domain.Entitys.Base.Interfaces;

namespace WebStore.Domain.Entitys.Base
{
    class NamedEntity : Entity, INamedEntity
    {
        public string Name { get; set; }
    }
}
