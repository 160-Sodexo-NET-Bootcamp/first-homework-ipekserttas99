using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IpekSerttas_Odev1_bookLibrary.Controllers
{
    public class Book
    {
        //Id,KitapSeriNo, KitapAdi,Yazari vs 5 adet property ekleyerek class olusturmak.
        public int Id { get; set; }
        public int KitapSeriNo { get; set; }
        public string KitapAd { get; set; }
        public string Yazar { get; set; }
        public int Fiyat { get; set; }
    }
    [Route("api/[controller]s")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public List<Book> bookList;

        public BookController()
        {
            //Bir Liste olusturup rastgele 6 kayit eklemek.
            bookList = new List<Book>();

            bookList.Add(new Book { Id = 1, KitapSeriNo = 1234, KitapAd = "The Unbearable Lightness of Being", Yazar = "Milan Kundera", Fiyat = 79 });
            bookList.Add(new Book { Id = 2, KitapSeriNo = 45354, KitapAd = "Justice: What's the Right Thing to Do?", Yazar = "Michael J. Sandel", Fiyat = 189 });
            bookList.Add(new Book { Id = 3, KitapSeriNo = 786, KitapAd = "1Q84", Yazar = "Haruki Murakami", Fiyat = 119 });
            bookList.Add(new Book { Id = 4, KitapSeriNo = 8657, KitapAd = "Confessions of a Mask", Yazar = " Yukio Mishima", Fiyat = 99 });
            bookList.Add(new Book { Id = 5, KitapSeriNo = 486, KitapAd = "Almost Transparent Blue", Yazar = "Ryū Murakami ", Fiyat = 139 });
            bookList.Add(new Book { Id = 6, KitapSeriNo = 8676, KitapAd = "The Noonday Demon: An Atlas of Depression", Yazar = "Andrew Solomon", Fiyat = 159 });
        }

        //HttpPost ile tum kayitlari listeleme.
        [HttpPost]
        public List<Book> PostBook()
        {
            return bookList;
        }

        //HttpGet ile FromRoute ve FromQuery kullanarak 2 farkli api ile girilen id nin detaylarini gostermek.
        [HttpGet("{id}")]
        public IActionResult GetBookById([FromRoute] int id)
        {
            if (id == 0)
            {
                return Unauthorized(); //401
            }
            var book = bookList.Where(x => x.Id == id).FirstOrDefault();
            if (book is null)
            {
                return NotFound(); //404
            }
            return Ok(book); //200
        }

        //[HttpGet("{id}")]
        //public IActionResult GetBookWithId([FromQuery] int id)
        //{
        //    if (id == 0)
        //    {
        //        return Unauthorized(); //401
        //    }
        //    var book = bookList.Where(x => x.Id == id).FirstOrDefault();
        //    if (book is null)
        //    {
        //        return NotFound(); //404
        //    }
        //    return Ok(book); //200
        //}

        ////HttpPost ile FromBody kullanarak listeye yeni bir kayit eklemek.
        //[HttpPost]
        //public ActionResult<List<Book>> PostBookAdd([FromBody] Book request)
        //{
        //    if (request == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (string.IsNullOrEmpty(request.KitapAd))
        //    {
        //        return BadRequest();
        //    }
        //    bookList.Add(request);
        //    return bookList;
        //}

        //HttpPut ile mevcut kaydi guncellemek.
        [HttpPut]
        public ActionResult<List<Book>> PutBook(int id, [FromBody] Book request)
        {
            var guncelle = bookList.Where(x => x.Id == id).FirstOrDefault();
            if(guncelle is null)
            {
                return Ok("Not Found");
            }
            guncelle.KitapSeriNo = request.KitapSeriNo;
            guncelle.KitapAd = request.KitapAd;
            guncelle.Yazar = request.Yazar;
            guncelle.Fiyat = request.Fiyat;

            return Ok(bookList);
        }

        //HttpDelete ile listeden kayit silmek
        [HttpDelete("id")]
        public ActionResult DeleteBook(int id)
        {
            var sil = bookList.Where(x => x.Id == id).FirstOrDefault();
            if(sil is null)
            {
                return Ok("Not Found");
            }
            bookList.Remove(sil);
            return Ok(bookList);
        }
    }
}
