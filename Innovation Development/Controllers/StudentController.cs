﻿﻿﻿using Innovation.Development.BLL.Models.Students;
using Innovation.Development.BLL.Services.Students;
using Innovation.Development.BLL.Services.Subjects;
using Innovation.Development.DAL.Entities.Students;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Innovation_Development.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ISubjectService _subjectService;

        public StudentController(IStudentService studentService, ISubjectService subjectService)
        {
            _studentService = studentService;
            _subjectService = subjectService;
        }

        public IActionResult Index()
        {
            var students = _studentService.GetStudents();
            ViewBag.Subjects = _subjectService.GetAllSubjects();
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Subjects = _subjectService.GetAllSubjects();
            return View();
        }

        [HttpPost]
        public IActionResult Create(string Name, DateOnly DateOfBirth, string Address, int[] selectedSubjects)
        {
            if (ModelState.IsValid)
            {
                var subjectsList = new List<Subject>();
                
                if (selectedSubjects != null && selectedSubjects.Length > 0)
                {
                    var allSubjects = _subjectService.GetAllSubjects().ToList();
                    subjectsList = allSubjects
                        .Where(s => selectedSubjects.Contains(s.Id))
                        .ToList();
                }

                var studentDto = new CreateStudentDto(Name, DateOfBirth, Address, subjectsList);
                _studentService.CreateStudent(studentDto);
                
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.Subjects = _subjectService.GetAllSubjects();
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _studentService.GetStudent(id);
            if (student == null)
            {
                return NotFound();
            }
            
            ViewBag.Subjects = _subjectService.GetAllSubjects();
            
            // Check if student.Subjects is not null before accessing it
            if (student.Subjects != null)
            {
                ViewBag.SelectedSubjects = student.Subjects.Select(s => s.Id).ToArray();
            }
            else
            {
                ViewBag.SelectedSubjects = new int[0]; // Empty array if no subjects
            }
            
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(int Id, string Name, DateOnly DateOfBirth, string Address, int[] selectedSubjects)
        {
            if (ModelState.IsValid)
            {
                var subjectsList = new List<Subject>();
                
                if (selectedSubjects != null && selectedSubjects.Length > 0)
                {
                    var allSubjects = _subjectService.GetAllSubjects().ToList();
                    subjectsList = allSubjects
                        .Where(s => selectedSubjects.Contains(s.Id))
                        .ToList();
                }

                var studentDto = new StudentDto(Id, Name, DateOfBirth, Address, subjectsList);
                _studentService.UpdateStudent(studentDto);
                
                return RedirectToAction(nameof(Index));
            }
            
            // If we get here, something went wrong, so we need to repopulate the form
            var student = _studentService.GetStudent(Id);
            ViewBag.Subjects = _subjectService.GetAllSubjects();
            
            if (selectedSubjects != null)
            {
                ViewBag.SelectedSubjects = selectedSubjects;
            }
            else if (student?.Subjects != null)
            {
                ViewBag.SelectedSubjects = student.Subjects.Select(s => s.Id).ToArray();
            }
            else
            {
                ViewBag.SelectedSubjects = new int[0];
            }
            
            // Create a new StudentDto to return to the view
            var model = student ?? new StudentDto(Id, Name, DateOfBirth, Address, new List<Subject>());
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _studentService.DeleteStudent(id);
            return Json(new { success = result });
        }
    }
}
