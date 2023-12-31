﻿using Microsoft.AspNetCore.Mvc;
using WebAppAzureSqlServer.Models;
using WebAppAzureSqlServer.Service;

namespace WebAppAzureSqlServer.Controllers
{
    public class CourseController : Controller
    {
        private readonly CourseService _course_service;

        public CourseController(CourseService _svc)
        {
            _course_service = _svc;
        }

        // The Index method is used to get a list of courses and return it to the view
        public IActionResult Index()
        {
            IEnumerable<Course> _course_list = _course_service.GetCourses();
            return View(_course_list);
            //return View();
        }

    }
}
