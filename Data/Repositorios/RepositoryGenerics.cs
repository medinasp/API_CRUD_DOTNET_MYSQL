using Data.Entidades;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorios
{
    public class RepositoryGenerics<T> : IGenerics<T>, IDisposable where T : class
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;
        private readonly IConfiguration _configuration;

        public RepositoryGenerics(IConfiguration configuration) 
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
            _configuration = configuration;
        }

        public async Task Add(T Objeto)
        {
            using (var data = new ContextBase(_OptionsBuilder, _configuration))
            {
                await data.Set<T>().AddAsync(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task Update(T Objeto)
        {
            using (var data = new ContextBase(_OptionsBuilder, _configuration))
            {
                var entity = data.Set<T>().Attach(Objeto);
                entity.State = EntityState.Modified;
                await data.SaveChangesAsync();
            }
        }

        public async Task Delete(T Objeto)
        {
            using (var data = new ContextBase(_OptionsBuilder, _configuration))
            {
                data.Set<T>().Remove(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteRange(List<int> Ids)
        {
            using (var data = new ContextBase(_OptionsBuilder, _configuration))
            {

                var produtos = await data.Set<Produto>().Where(p => Ids.Contains(p.Id)).ToListAsync();

                if (produtos == null || !produtos.Any())
                    return false;

                data.Set<Produto>().RemoveRange(produtos);

                await data.SaveChangesAsync();

                return true;

            }
        }

        public async Task<T> GetEntityById(int Id)
        {
            using (var data = new ContextBase(_OptionsBuilder, _configuration))
            {
                return await data.Set<T>().FindAsync(Id);
            }
        }

        public async Task<List<T>> List()
        {
            using (var data = new ContextBase(_OptionsBuilder, _configuration))
            {
                return await data.Set<T>().AsNoTracking().ToListAsync();
            }
        }

        #region Disposed https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);



        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }

        #endregion

    }
}
