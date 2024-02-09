namespace BookStore.Models.repo
{
    public class BookRepo : IBookStoreRepo<Book>
    {

        List<Book> books;

        public BookRepo()
        {
            books = new List<Book>()
            {
                new Book
                {
                    Id = 1,
                    Title = "c# programing",
                    Description = "no disc.."
                }


            };

        }


        public Book Find(int id)
        {
            var book = books.SingleOrDefault(x => x.Id == id);
            return book;

        }
        public void Add(Book entity)
        {

            entity.Id = books.Count + 1;

            //    entity.Id = books.Max(i => i.Id) + 1;

            books.Add(entity);
        }

        public void Delete(Book entity)
        {
            //    var book = books.SingleOrDefault(x => x.Id == id);
            //بدلاً من كتابة السطر في الأعلى يمكن كتابة التالي كوننا لدينا بالفعل هذا الكود في وظيفة الفاوند
            var book = Find(entity.Id);
            books.Remove(book);
        }


        public IList<Book> List()
        {
            //هذا الكود التالي ليعيد ترتيب الأيدي عند الحذف
            for (var i = 0; i < books.Count; i++)
            {
                books[i].Id = i + 1;
            }
            return books;
        }

        public void Update(int id, Book newBook)
        {
            var book = books.SingleOrDefault(x => x.Id == id);
            book.Title = newBook.Title;
            book.Description = newBook.Description;
            book.Author = newBook.Author;
            book.ImgUrl = newBook.ImgUrl;

        }
    }
}
