using eCinema.Data.Services;
using eCinema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eCinema.Controllers
{
    [Authorize]
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;

        public ActorsController(IActorsService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync(); // ✅ تصحيح التسمية لاستخدام GetAllAsync()
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Actor actor) // ✅ جعلها async
        {
            if (string.IsNullOrWhiteSpace(actor.Biography))
            {
                ModelState.AddModelError("Biography", "Biography field is required.");
                return View(actor); // ارجع للـ form عشان المستخدم يدخل البيانات
            }

            await _service.AddAsync(actor); // ✅ إضافة await
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id); // ✅ إضافة await و GetByIdAsync()

            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }
        public async Task <IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id); // ✅ إضافة await و GetByIdAsync()

            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            if (id != actor.Id)
                return BadRequest("ID mismatch"); // ✅ تأكيد أن الـ ID صحيح

            try
            {
                var updatedActor = await _service.UpdateAsync(id, actor); // ✅ الآن يعمل بدون خطأ
                return RedirectToAction(nameof(Index)); // ✅ نجاح التحديث
            }
            catch (KeyNotFoundException)
            {
                return View("NotFound", id); // ✅ توجيه المستخدم إلى صفحة الخطأ
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id); // ✅ إضافة await و GetByIdAsync()

            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
            {
                return NotFound(); // ✅ إذا لم يتم العثور على العنصر، أعد 404
            }

            return RedirectToAction(nameof(Index)); // ✅ إعادة التوجيه إلى الصفحة الرئيسية بعد الحذف
        }

    }
}
