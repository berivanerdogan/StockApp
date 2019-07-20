using StockApp.Model.Option;
using StockApp.Service.Option;
using StockApp.UI.Areas.Admin.Models.DTO;
using StockApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockApp.UI.Areas.Admin.Controllers
{
    public class AppUserController : Controller
    {

        AppUserService _appUserService;
        public AppUserController()
        {
            _appUserService = new AppUserService();
        }

        public ActionResult AppUserAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AppUserAdd(AppUser user, HttpPostedFileBase Image)
        {
            List<string> UploadedImagePaths = new List<string>();

            UploadedImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalProfileImagePath, Image, 1);
            user.UserImage = UploadedImagePaths[0];

            if (user.UserImage == "0" || user.UserImage == "1" || user.UserImage == "2")
            {
                user.UserImage = ImageUploader.DefaultProfileImagePath;
                user.XSmallUserImage = ImageUploader.DefaultXSmallProfileImage;
                user.CruptedUserImage = ImageUploader.DefaultCruptedProfileImage;
            }
            else
            {
                user.UserImage = UploadedImagePaths[1];
                user.UserImage = UploadedImagePaths[2];
            }
            user.Status = Core.Enum.Status.Active;
            _appUserService.Add(user);

            return Redirect("/Admin/AppUser/AppUserList");
        }

        public ActionResult AppUserList()
        {
            List<AppUser> model = _appUserService.GetActive();
            return View(model);
        }
        public ActionResult UpdateAppUser(Guid id)
        {
            AppUser user = _appUserService.GetByID(id);
            AppUserDTO model = new AppUserDTO();
            model.ID = user.ID;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.UserName = user.UserName;
            model.Password = user.Password;
            model.Address = user.Address;
            model.PhoneNumber = user.PhoneNumber;
            model.Role = user.Role;
            model.ImagePath = user.ImagePath;
            model.UserImage = user.UserImage;
            model.XSmallUserImage = user.XSmallUserImage;
            model.CruptedUserImage = user.CruptedUserImage;

            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateAppUser(AppUser user, HttpPostedFileBase Image)
        {
            List<string> UploadedImagePaths = new List<string>();
            UploadedImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalProfileImagePath, Image, 1);
            user.UserImage = UploadedImagePaths[0];

            AppUser update = _appUserService.GetByID(user.ID);

            if (user.UserImage == "0" || user.UserImage == "1" || user.UserImage == "2")
            {
                if (update.UserImage == null || update.UserImage == ImageUploader.DefaultProfileImagePath)
                {
                    update.UserImage = ImageUploader.DefaultProfileImagePath;
                    update.XSmallUserImage = ImageUploader.DefaultXSmallProfileImage;
                    update.CruptedUserImage = ImageUploader.DefaultCruptedProfileImage;

                }
                else
                {
                    update.UserImage = update.UserImage;
                    update.XSmallUserImage = update.XSmallUserImage;
                    update.CruptedUserImage = update.CruptedUserImage;

                }

            }
            else
            {
                update.UserImage = UploadedImagePaths[0];
                update.XSmallUserImage = UploadedImagePaths[1];
                update.CruptedUserImage = UploadedImagePaths[2];



            }

            update.FirstName = user.FirstName;
            update.LastName = user.LastName;
            update.ImagePath = user.ImagePath;
            update.UserName = user.UserName;
            update.Password = user.Password;
            update.Address = user.Address;
            update.PhoneNumber = user.PhoneNumber;
            update.Role = user.Role;
            _appUserService.Update(user);
            return Redirect("/Admin/AppUser/AppUserList");
        }
        public ActionResult Delete(Guid id)
        {
            _appUserService.Remove(id);
            return Redirect("/Admin/AppUser/AppUserList");
        }
    }
}