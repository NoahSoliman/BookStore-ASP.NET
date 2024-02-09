using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModels
{
    public class BookAuthorViewModel
    {

        public int BookId { get; set; }


        [MaxLength(20)]
        [MinLength(5)]
        //ملاحظة أنني هنا لم أضطر لكتابة عبارة ريكوايريت اي مطلوب كما في الفيديو وانما تم تنفيذها بشكل تلقائي
        public string Title { get; set; }


        [MaxLength(120)]
        [MinLength(6)]
        public string Description { get; set; }


        public int AuthorId { get; set; }

        public List<Author>? Authors { get; set; }
        //عن طريق اضافة اشارة التعجب هنا نخبر الموديل ان المؤلف هو ليس شيء اجباري في عملية التحقق من البيانات

        public string? ImgUrl { get; set; }


        public IFormFile? File { get; set; }
    }
}

//قمنا باضافة هذا الفييو موديل
//بدلاً من ان نقوم بالتغييرات ضمن كلاس الكتاب نفسه
//لأننا نريد أن نعيد قائمة من المؤلفين لكي يختار
//المتسخدم منهم مؤلف ومن ثم نريد للكلاس الأساسي
//أن يحفظ فقط مؤلف واحد عند التعامل مع قاعدة البيانات
