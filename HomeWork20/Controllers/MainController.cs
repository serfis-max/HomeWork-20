using System.Linq;
using System.Threading.Tasks;
using HW_201.Contexts;
using HW_201.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HW_201.Controllers
{
    public class MainController : Controller
    {
        /// <summary>
        /// Визуализация представления Index.cshtml
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Table = new DataContext().Table;
            return View();
        }

        /// <summary>
        /// Визуализация представления UsersInfo.cshtml
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        public IActionResult UsersInfo(int id)
        {
            return View(new DataContext().Table
                .Where(e => e.Id == id)
                .First());
        }

        /// <summary>
        /// Добавление новой записи в телефонную книгу
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddNewUser(User user)
        {
            using (var db = new DataContext())
            {
                db.Table.AddAsync(user);
                db.SaveChanges();
            }
            return Redirect("~/");
        }

        /// <summary>
        /// Изменение существующей записи
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<IActionResult> UpdateUser(User user)
        {
            using (var db = new DataContext())
            {
                db.Table.Update(user);
                await db.SaveChangesAsync();
            }
            return NoContent();
        }

        /// <summary>
        /// Удаление существующей записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteUser(int id)
        {
            using (var db = new DataContext())
            {
                db.Table.Remove(await db.Table
                    .Where(e => e.Id == id)
                    .FirstAsync());
                await db.SaveChangesAsync();
            }
            return Redirect("~/");
        }

        /// <summary>
        /// Удаление существующей записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("[controller]")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMethod(int id)
        {
            using (var db = new DataContext())
            {
                db.Table.Remove(await db.Table
                    .Where(e => e.Id == id)
                    .FirstAsync());
                await db.SaveChangesAsync();
            }
            return Redirect("~/");
        }
    }
}