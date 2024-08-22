using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IRepository<T> where T : Contato
    {
        IList<T> ObterTodos();
        IList<T> ObterPorDDD(int DDD);
        T ObterPorId(int Id);
        void Cadastrar(T entidade);
        void Alterar(T entidade);
        void Deletar(int id);
    }
}
