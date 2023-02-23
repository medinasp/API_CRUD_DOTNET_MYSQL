namespace Data.Interfaces
{
    public interface IGenerics<T> where T : class
    {
        Task Add(T Objeto);
        Task Update(T Objeto);
        Task Delete(T Objeto);
        Task<bool> DeleteRange(List<int> Ids);
        Task<T> GetEntityById(int Id);
        Task<List<T>> List();

    }
}
