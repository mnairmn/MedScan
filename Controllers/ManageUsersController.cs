using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedScan.Models;
using MedScan.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedScan.Controllers
{
    public class ManageUsersController : Controller //inherits from Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateUser(User user)
        {
            UsersDAO dao = new UsersDAO();
            //creates a new instance of a data access object, as described in the Explored: Algorithms section of the Design

            if (dao.CreateUser(user) == 1)
            {
                // if CreateUser method returns a 1, an error has occured so an error message should be displayed
                return View("CrudFailure");
            }
            else
            {
                // if 1 is not returned, i.e., 0 is returned, creation was successful and no error message is needed.
                return View("Index");
            }
        }

        public IActionResult DeleteUser(User user)
        {
            //this function works in a similar way to CreateUser, creating an instance of a DAO and using this to access data
            UsersDAO dao = new UsersDAO();

            if (dao.DeleteUser(user) == 1)
            {
                return View("CrudFailure");
            }
            else
            {
                return View("Index");
            }
        }

        public IActionResult ViewAllUsers()
        {
            UsersDAO dao = new UsersDAO();
            return View("ViewAllUsers", dao.GetUsers());
            //two arguments passed into this view - first is the view we want to return, and second parameter is the list we want to display in that view
        }
    }
}

