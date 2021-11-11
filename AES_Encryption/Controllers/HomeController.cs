using AES_Encryption.Helpers;
using AES_Encryption.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AES_Encryption.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(EncryptionModel encryptionModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var aseEncryption = new ASEEncryption();

                    // Encrypted Username
                    var username = aseEncryption.EncryptAesManaged(encryptionModel.Username);
                    // Encrypted Email
                    var email = aseEncryption.EncryptAesManaged(encryptionModel.EmailAddress);
                    // Encrypted Password
                    var password = aseEncryption.EncryptAesManaged(encryptionModel.Password);

                    using (var db = new DB.db_JetkinsEntities())
                    {
                        db.UserDatas.Add(new DB.UserData() { UserName = username, Password = password, Email = email });
                        db.SaveChanges();

                        ViewData["Message"] = "Successfully Saved";
                        ViewData["MessageType"] = "success";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewData["Message"] = "Successfully Saved";
                ViewData["MessageType"] = "danger";
                return View();
            }

            return View(encryptionModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}