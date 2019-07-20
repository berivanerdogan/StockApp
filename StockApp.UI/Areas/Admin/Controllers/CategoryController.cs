using StockApp.Model.Option;
using StockApp.Service.Option;
using StockApp.UI.Areas.Admin.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockApp.UI.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        CategoryService _categoryService;
        public CategoryController()
        {
            _categoryService = new CategoryService();
        }
        public ActionResult CategoryAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CategoryAdd(Category data)
        {
            _categoryService.Add(data);
            return Redirect("/Admin/Category/CategoryList");
        }
        public ActionResult CategoryList()
        {
            List<Category> model = _categoryService.GetActive();
            return View(model);
        }

        public ActionResult CategoryUpdate(Guid id)
        {
            Category category = _categoryService.GetByID(id);
            CategoryDTO model = new CategoryDTO();
            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;
            return View(model);
        }

        [HttpPost]
        public ActionResult CategoryUpdate(Category category)
        {
            _categoryService.Update(category);
            return Redirect("/Admin/Category/CategoryList");
        }

        public ActionResult Delete(Guid id)
        {
            _categoryService.Remove(id);
            return Redirect("/Admin/Category/CategoryList");
        }
    }
}