namespace FitAgenda.Domain.Models
{

    public abstract class Entity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
    }
}
