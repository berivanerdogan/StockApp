using StockApp.Model.Option;
using StockApp.Service.Option;
using StockApp.UI.Areas.Admin.Models.DTO;
using StockApp.UI.Areas.Admin.Models.VM;
using StockApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockApp.UI.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        ProductService _productService;
        CategoryService _categoryService;
        public ProductController()
        {
            _productService = new ProductService();
            _categoryService = new CategoryService();
        }
        // GET: Admin/Product
        public ActionResult ProductAdd()
        {
            ProductVM model = new ProductVM()
            {
                Categories = _categoryService.GetDefault(x => x.Status == Core.Enum.Status.Active || x.Status == Core.Enum.Status.Updated),
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult ProductAdd(Product data,HttpPostedFileBase Image)
        {
            List<string> UploadedImages = new List<string>();
            UploadedImages = ImageUploader.UploadSingleImage(ImageUploader.OriginalProfileImagePath, Image, 1);
            data.ImagePath = UploadedImages[0];
            if (data.ImagePath=="0"||data.ImagePath=="1"||data.ImagePath=="2")
            {
                data.ImagePath = ImageUploader.DefaultProfileImagePath;
                data.ImagePath = ImageUploader.DefaultCruptedProfileImage;
                data.ImagePath = ImageUploader.DefaultXSmallProfileImage;
            }
            else
            {
                data.ImagePath = UploadedImages[1];
                data.ImagePath = UploadedImages[2];
            }

            data.Status = Core.Enum.Status.Active;
            _productService.Add(data);
            return Redirect("/Admin/Product/ProductAdd");
        }

        public ActionResult ProductList()
        {
            List<Product> model = _productService.GetActive();
            return View(model);
        }

        public ActionResult ProductUpdate(Guid id)
        {
            Product product = _productService.GetByID(id);
            ProductVM model = new ProductVM();
            model.Products.ID = product.ID;
            model.Products.ProductName = product.ProductName;
            model.Products.Quantity = product.Quantity;
            model.Products.FirstPrice = product.FirstPrice;
            model.Products.SalePrice = product.SalePrice;
            model.Products.CriticalStock = product.CriticalStock;
            model.Products.AddDate = DateTime.Now;
            model.Products.ImagePath = product.ImagePath;

            List<Category> categories = _categoryService.GetDefault(x => x.Status == Core.Enum.Status.Active || x.Status == Core.Enum.Status.Updated);
            model.Categories = categories;

            return View(model);
        }
        [HttpPost]
        public ActionResult ProductUpdate(ProductDTO data,HttpPostedFileBase Image)
        {
            List<string> UploadedImagePaths = new List<string>();
            UploadedImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalProfileImagePath, Image, 1);
            data.ImagePath = UploadedImagePaths[0];

            Product update = _productService.GetByID(data.ID);

            if (data.ImagePath=="0"||data.ImagePath=="1"||data.ImagePath=="2")
            {
                if (update.ImagePath==null||update.ImagePath==ImageUploader.DefaultProfileImagePath)
                {
                    update.ImagePath = ImageUploader.DefaultCruptedProfileImage;
                    update.ImagePath = ImageUploader.DefaultProfileImagePath;
                    update.ImagePath = ImageUploader.DefaultXSmallProfileImage;
                }
                else
                {
                    update.ImagePath = data.ImagePath;
                }
            }
            else
            {
                update.ImagePath = UploadedImagePaths[0];
                update.ImagePath = UploadedImagePaths[1];
                update.ImagePath = UploadedImagePaths[2];
            }

            update.ProductName = data.ProductName;
            update.Quantity = data.Quantity;
            update.CriticalStock = data.CriticalStock;
            update.FirstPrice = data.FirstPrice;
            update.SalePrice = data.SalePrice;
            update.AddDate = data.AddDate;
            update.ImagePath = data.ImagePath;
            update.CategoryID = data.CategoryID;

            _productService.Update(update);

            return Redirect("/Admin/Product/ProductList");
        }

        public ActionResult Delete(Guid id)
        {
            _productService.Remove(id);
            return Redirect("/Admin/Product/ProductList");
        }

        public ActionResult SaleAdd(Guid id)
        {
            Product product = _productService.GetByID(id);
            ProductVM model = new ProductVM();
            model.Products.ID = product.ID;
            model.Products.ProductName = product.ProductName;
            model.Products.Quantity = product.Quantity;
            model.Products.FirstPrice = product.FirstPrice;
            model.Products.SalePrice = product.SalePrice;
            model.Products.CriticalStock = product.CriticalStock;
            model.Products.AddDate = DateTime.Now;
            model.Products.ImagePath = product.ImagePath;
            model.Products.RemainingQuantity = product.Quantity-product.SoldQuantity;
            model.Products.SoldQuantity = product.SoldQuantity;
            model.Products.LastProcessingDate = DateTime.Now;

            List<Category> categories = _categoryService.GetDefault(x => x.Status == Core.Enum.Status.Active || x.Status == Core.Enum.Status.Updated);
            model.Categories = categories;

            return View(model);
        }
        [HttpPost]
        public ActionResult SaleAdd(ProductDTO data, HttpPostedFileBase Image)
        {
            List<string> UploadedImagePaths = new List<string>();
            UploadedImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalProfileImagePath, Image, 1);
            data.ImagePath = UploadedImagePaths[0];

            Product update = _productService.GetByID(data.ID);

            if (data.ImagePath == "0" || data.ImagePath == "1" || data.ImagePath == "2")
            {
                if (update.ImagePath == null || update.ImagePath == ImageUploader.DefaultProfileImagePath)
                {
                    update.ImagePath = ImageUploader.DefaultCruptedProfileImage;
                    update.ImagePath = ImageUploader.DefaultProfileImagePath;
                    update.ImagePath = ImageUploader.DefaultXSmallProfileImage;
                }
                else
                {
                    update.ImagePath = data.ImagePath;
                }
            }
            else
            {
                update.ImagePath = UploadedImagePaths[0];
                update.ImagePath = UploadedImagePaths[1];
                update.ImagePath = UploadedImagePaths[2];
            }

            update.ProductName = data.ProductName;
            update.Quantity = data.Quantity;
            update.CriticalStock = data.CriticalStock;
            update.FirstPrice = data.FirstPrice;
            update.SalePrice = data.SalePrice;
            update.AddDate = DateTime.Now;
            update.ImagePath = data.ImagePath;
            update.CategoryID = data.CategoryID;
            update.LastProcessingDate = DateTime.Now;
            update.SoldQuantity = data.SoldQuantity;
            update.RemainingQuantity = data.Quantity-data.SoldQuantity;

            _productService.Update(update);

            return Redirect("/Admin/Product/SaleList");
        }

        public ActionResult SaleList()
        {     
            List <Product> model = _productService.GetActive();
            return View(model);
        }


        public ActionResult SaleUpdate(Guid id)
        {
            Product product = _productService.GetByID(id);
            ProductVM model = new ProductVM();
            model.Products.ID = product.ID;
            model.Products.ProductName = product.ProductName;
            model.Products.Quantity = product.Quantity;
            model.Products.FirstPrice = product.FirstPrice;
            model.Products.SalePrice = product.SalePrice;
            model.Products.CriticalStock = product.CriticalStock;
            model.Products.AddDate = DateTime.Now;
            model.Products.ImagePath = product.ImagePath;
            model.Products.RemainingQuantity = product.RemainingQuantity - product.SoldQuantity;
            model.Products.SoldQuantity = product.SoldQuantity;
            model.Products.LastProcessingDate = DateTime.Now;

            List<Category> categories = _categoryService.GetDefault(x => x.Status == Core.Enum.Status.Active || x.Status == Core.Enum.Status.Updated);
            model.Categories = categories;

            return View(model);
        }
        [HttpPost]
        public ActionResult SaleUpdate(ProductDTO data, HttpPostedFileBase Image)
        {
            List<string> UploadedImagePaths = new List<string>();
            UploadedImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalProfileImagePath, Image, 1);
            data.ImagePath = UploadedImagePaths[0];

            Product update = _productService.GetByID(data.ID);

            if (data.ImagePath == "0" || data.ImagePath == "1" || data.ImagePath == "2")
            {
                if (update.ImagePath == null || update.ImagePath == ImageUploader.DefaultProfileImagePath)
                {
                    update.ImagePath = ImageUploader.DefaultCruptedProfileImage;
                    update.ImagePath = ImageUploader.DefaultProfileImagePath;
                    update.ImagePath = ImageUploader.DefaultXSmallProfileImage;
                }
                else
                {
                    update.ImagePath = data.ImagePath;
                }
            }
            else
            {
                update.ImagePath = UploadedImagePaths[0];
                update.ImagePath = UploadedImagePaths[1];
                update.ImagePath = UploadedImagePaths[2];
            }

            update.ProductName = data.ProductName;
            update.Quantity = data.Quantity;
            update.CriticalStock = data.CriticalStock;
            update.FirstPrice = data.FirstPrice;
            update.SalePrice = data.SalePrice;
            update.AddDate = DateTime.Now;
            update.ImagePath = data.ImagePath;
            update.CategoryID = data.CategoryID;
            update.LastProcessingDate = DateTime.Now;
            update.RemainingQuantity = data.RemainingQuantity - data.SoldQuantity;
            _productService.Update(update);

            return Redirect("/Admin/Product/SaleList");
        }
    }
}