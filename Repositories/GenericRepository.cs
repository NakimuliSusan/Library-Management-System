using Library_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Repositories
{

    // implements IGeneric Repository interface for access to crud operations i.e centralised database handling
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // Returns all instances of an entity in a list
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
     
        //Adding and saving changes to the database
        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);

            await _context.SaveChangesAsync();

            //Console.WriteLine($"{entity}added successfully!");
        }

        // Updating entities
        public async Task Update(T entity)
        {
            _dbSet.Update(entity);

            await _context.SaveChangesAsync();

            //Console.WriteLine($"{entity}updated successfully!");
        }

        // Deleting entities
        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);

            await _context.SaveChangesAsync();

           // Console.WriteLine($"{entity}deleteed successfully!");
        }
    }

}
