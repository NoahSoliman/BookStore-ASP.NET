using BookStore.Models;
using BookStore.Models.repo;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        // GET: BookController

        private readonly IBookStoreRepo<Book> bookRepo;
        private readonly IBookStoreRepo<Author> authorRepo;
        private readonly IHostingEnvironment hosting;

        public BookController(IBookStoreRepo<Book> BookRepo, IBookStoreRepo<Author> AuthorRepo, IHostingEnvironment hosting)
        {

            this.bookRepo = BookRepo;
            this.authorRepo = AuthorRepo;
            this.hosting = hosting;
        }
        public ActionResult Index()
        {
            var books = bookRepo.List();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {

            var book = bookRepo.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {

            return View(GetAllAuthors());



        }




        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            if (ModelState.IsValid)
            //هنا نقوم بالتحقق ان الفورم لا يحتوي اي شروط غير محققة
            {
                try
                {



                    if (model.AuthorId == -1)
                    {
                        var modelForPostCreate = new BookAuthorViewModel
                        {
                            Authors = FillSelectList(),
                        };
                        //ViewBag ميزة تقوم بتمكيننا من ارسال قيمة بين الواجهة والخلفية
                        //Message يمكننا اضافة اي شي بديلاً لها و
                        ViewBag.Message = "Please Select An author!";
                        return View(modelForPostCreate);
                        //هنا نقوم بإعادة قائمة فارغة بالمؤلفين عندما لا يتم اختيار اي مؤلف 
                    }


                    string fileName;
                    string uploads = Path.Combine(hosting.WebRootPath, "uploads");


                    Book book = new Book
                    {
                        Id = model.BookId,
                        Title = model.Title,
                        Description = model.Description,
                        Author = authorRepo.Find(model.AuthorId)
                    };


                    if (model.File != null)
                    {
                        fileName = model.File.FileName;
                        string fullPath = Path.Combine(uploads, fileName);
                        var fileStream = new FileStream(fullPath, FileMode.Create);
                        model.File.CopyTo(fileStream);
                        fileStream.Close();

                        book.ImgUrl = fileName;
                    }


                    bookRepo.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }

            }


            //يتم تنفيذ هذا الكود في حال الشرط بالاعلى لم يتنفذ

            ModelState.AddModelError("", "You have to fill all the required fields");
            //السطر بالأعلى يقوم بأظهار رسالة عامة بالفورم في الواجهة لأخبار العميل ان هناك شيء لم يعبأ
            return View(GetAllAuthors());
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepo.Find(id);

            var model = new BookAuthorViewModel
            {
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                Authors = FillSelectList(),
                ImgUrl = book.ImgUrl

            };

            if (book.Author != null)
            {
                model.AuthorId = book.Author.Id;
            }


            return View(model);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookAuthorViewModel model)
        {
            try
            {
                var bookForPostEdit = bookRepo.Find(id);

                if (model.AuthorId == -1)
                {

                    var modelForPostEdit = new BookAuthorViewModel
                    {
                        BookId = bookForPostEdit.Id,
                        Title = bookForPostEdit.Title,
                        Description = bookForPostEdit.Description,
                        Authors = FillSelectList(),
                        ImgUrl = bookForPostEdit.ImgUrl

                    };
                    ViewBag.Message = "Please Select An author!";
                    return View(modelForPostEdit);
                }




                Book book = new Book
                {
                    Title = model.Title,
                    Description = model.Description,
                    Author = authorRepo.Find(model.AuthorId),
                };



                string fileName;
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                if (model.File != null)
                {
                    string oldFileName = bookRepo.Find(id).ImgUrl;
                    if (oldFileName != null)
                    {
                        string fullOldPath = Path.Combine(uploads, oldFileName);
                        System.IO.File.Delete(fullOldPath);

                    }
                    fileName = model.File.FileName;

                    //fileName = bookForPostEdit.ImgUrl;

                    string fullPath = Path.Combine(uploads, fileName);

                    var fileStream = new FileStream(fullPath, FileMode.Create);
                    model.File.CopyTo(fileStream);
                    fileStream.Close();

                    book.ImgUrl = fileName;


                }

                bookRepo.Update(id, book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {

            var model = bookRepo.Find(id);

            return View(model);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Book collection)
        {
            try
            {


                bookRepo.Delete(collection);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        List<Author> FillSelectList()
        {
            var authorList = authorRepo.List().ToList();
            authorList.Insert(0, new Author { Id = -1, FullName = "Please Select An Author" });

            return authorList;

            //هذه الفانكشن لكي تضيف عنصر فارغ ببداية قائمة المؤلفين 
        }

        BookAuthorViewModel GetAllAuthors()
        {
            var model = new BookAuthorViewModel
            {
                Authors = FillSelectList()

            };
            return model;
        }

    }


}
