using Core.Entity;
using Core.Repository;

namespace Infrastructure.Repository
{
    public class ContatoRepository : EFRepository<Contato>, IContatoRepository
    {
        public ContatoRepository(ApplicationDbContext contexto) : base(contexto)
        {
        }
    }
}
