using Core.Entity;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class EFRepository<T> : IRepository<T> where T : Contato
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> _dbSet;

        public EFRepository(ApplicationDbContext contexto)
        {
            _context = contexto;
            _dbSet = _context.Set<T>();
        }

        public void Alterar(T entidade)
        {
            _dbSet.Update(entidade);
            _context.SaveChanges();
        }

        public void Cadastrar(T entidade)
        {
            _dbSet.Add(entidade);
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            _dbSet.Remove(ObterPorId(id));
            _context.SaveChanges();
        }

        public IList<T> ObterPorDDD(int DDD) => _dbSet
                .Where(entity => entity.DDD == DDD).ToList();

        public T ObterPorId(int id)
            => _dbSet.FirstOrDefault(entity => entity.Id == id);


        public IList<T> ObterTodos()
            => _dbSet.ToList();
    }
}
