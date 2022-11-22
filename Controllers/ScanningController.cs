using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedScan.Models;
using MedScan.Services.Images;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedScan.Controllers
{
    public class ScanningController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SubmitImageToRead()
        {
            return View();
        }

        public IActionResult CheckFilePath(ImageModel image)
        {
            ImageDAO dao = new ImageDAO();
            //creates new instance of DAO to alter properties of image model object
            if (dao.GetFilePath(image) != null)
            {
                //if the image has a file path, the image upload has been successful so this view should be returned
                return View("Success");
            }
            else
            {
                return View("Failure");
            }
        }

        public IActionResult Download(ImageModel image)
        {
            ImageDAO dao = new ImageDAO();
            var data = dao.Read(image);
            //Read() is the method used to grab data from the image
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            //encodes data into UTF8 into an array
            return File(bytes, "text/plain", "file.txt");
            //returns a file made from an array that has been converted into a .txt. file
        }
    }
}

