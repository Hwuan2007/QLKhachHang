using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLKhachHang.Models;
using System.Diagnostics;
using X.PagedList;

namespace QLKhachHang.Controllers
{
    public class HomeController : Controller
    {

        CustomerManagementContext db = new CustomerManagementContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(int ? page)
        {
            int pageSize = 12;
            int pageNummber = page == null || page < 0 ? 1 : page.Value;

            var lstCustomer = db.Customers.AsNoTracking().OrderBy(x => x.Id).DefaultIfEmpty();
            PagedList<Customer> lst = new PagedList<Customer>(lstCustomer, pageNummber, pageSize);
            return View(lst);
        }
        [HttpGet]
        [Route("addcustomer")]
        public IActionResult AddCustomer()
        {
            return View();
        }
        [Route("addcustomer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCustomer(Customer customer)
        {
            if(ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(customer);
        }

        [HttpGet]
        [Route("editcustomer")]
        public IActionResult editcustomer(int Id)
        {
            var customer = db.Customers.Find(Id);
            return View(customer);
        }
        [Route("editcustomer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult editcustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Update(customer);
                db.SaveChanges();
                return RedirectToAction("index", "Home");
            }
            return View(customer);
        }


        [Route("deletecustomer")]
        public IActionResult deletecustomer(int Id)
        {
            TempData["Message"] = "";
            var customer = db.Customers.Find(Id);
            db.Remove(customer);
            db.SaveChanges();
            TempData["Message"] = "Đã xóa thành công";
            return RedirectToAction("Index", "Home");
        }





        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
