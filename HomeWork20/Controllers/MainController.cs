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
        /// Генерация новых записей в ДБ
        /// </summary>
        /// <param name="count">Требуемое количество новых записей</param>
        private void CreateRecords(int count)
        {
            Repository r = new Repository();
            for (int i = 1; i < count+1; i++)
            {
                AddNewUser(r.NewUser());
            }
        }

        #region Визуализация представлений
        /// <summary>
        /// Визуализация представления Index.cshtml
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewBag.Table = new DataContext().Table;

            using (var db = new DataContext())
            {
                if (db.Table.Count() < 1)
                    CreateRecords(10);
            }
            return View();
        }

        /// <summary>
        /// Визуализация представления UsersInfo.cshtml
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult UsersInfo(int id)
        {
            return View(new DataContext().Table
                .Where(e => e.Id == id)
                .First());
        }
        #endregion

        #region Изменение данных в БД
        /// <summary>
        /// Добавление новой записи
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

        public IActionResult RandomRecord()
        {
            AddNewUser(new Repository().NewUser());
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
        /// Изменение существующей записи без перезаписи скрытых полей
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<IActionResult> UpdateNoDataLoss(User user)
        {
            string temp;
            using (var db = new DataContext())
            {
                temp = db.Table.Where(e => e.Id == user.Id).First().Description;
            }

            using (var db = new DataContext())
            {
                db.Table.Update(new User
                {
                    Id = user.Id,
                    Surname = user.Surname,
                    FirstName = user.FirstName,
                    Patronymic = user.Patronymic,
                    PhoneNumber = user.PhoneNumber,
                    Adress = user.Adress,
                    Description = temp
                });

                await db.SaveChangesAsync();
            }
            return NoContent();
        }

        /// <summary>
        /// Удаление существующей записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("[controller]")]
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
        [Route("Main")]
        [HttpDelete]
        public async Task<IActionResult> DeleteMethod(int Id)
        {
            using (var db = new DataContext())
            {
                db.Table.Remove(await db.Table
                    .Where(e => e.Id == Id)
                    .FirstAsync());
                await db.SaveChangesAsync();
            }
            return NoContent();
        }
        #endregion
    }
}