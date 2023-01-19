using System.Linq.Expressions;
using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AppDbContext _context; //injentando a instância do contexto

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<T> Get() //retorna uma instância DbSet<T> para o acesso a entidades no contexto
        {
            return _context.Set<T>().AsNoTracking(); //AsNoTracking Desabilita o rastreamento de entidade
        }

        public T GetById(Expression<Func<T, bool>> predicate) //usa o delegate Func e predicate para validar o critério
        {
            return _context.Set<T>().SingleOrDefault(predicate);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity); //usa a instância do contexto para realizar a operação de adicionar
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified; //define o entity state como modificado
            _context.Set<T>().Update(entity); //informa o contexto que foi atualizado.
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
