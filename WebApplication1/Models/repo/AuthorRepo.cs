
namespace BookStore.Models.repo
{
    public class AuthorRepo : IBookStoreRepo<Author>
    {

        IList<Author> authors;

        public AuthorRepo()
        {
            authors = new List<Author>()
            {
                new Author { Id = 1, FullName="Medo Moe"},

            };

        }
        public void Add(Author entity)
        {
            //يمكن فعلها بهذا السطر التالي او بالسطر الذي خلفه الذي يقوم بارجاع اعلى قيمة للأيدي
            entity.Id = authors.Count + 1;
            //  entity.Id = authors.Max(i => i.Id) + 1;
            authors.Add(entity);
        }

        public void Delete(Author entity)
        {

            var author = Find(entity.Id);

            authors.Remove(author);
        }

        public Author Find(int id)
        {
            var author = authors.SingleOrDefault(x => x.Id == id);
            return author;
        }

        public IList<Author> List()
        {
            //هذا الكود التالي ليعيد ترتيب الأيدي عند الحذف
            for (var i = 0; i < authors.Count; i++)
            {
                authors[i].Id = i + 1;

            }
            return authors;
        }

        public void Update(int id, Author newAutour)
        {
            var author = authors.SingleOrDefault(x => x.Id == id);
            author.FullName = newAutour.FullName;

        }
    }
}
