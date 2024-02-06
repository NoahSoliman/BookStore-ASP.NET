namespace BookStore.Models.repo
{

    //أضفنا TEntity 
    //لكي تعمل الانترفيس مع كل الكلاسات
    public interface IBookStoreRepo<TEntity>
    {
        IList<TEntity> List();

        TEntity Find(int id);

        void Add(TEntity entity);

        void Update(int ID, TEntity entity);

        void Delete(TEntity entity);


    }
}
