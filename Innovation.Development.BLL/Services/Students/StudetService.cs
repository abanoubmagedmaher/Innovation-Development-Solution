﻿﻿using Innovation.Development.BLL.Models.Students;
using Innovation.Development.DAL.contracts;
using Innovation.Development.DAL.Entities.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovation.Development.BLL.Services.Students
{
    public class StudetService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudetService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<StudentDto> GetStudents()
        {
            var students = _unitOfWork.StudentsRepository.GetAll();
            foreach (var student in students)
            {
                yield return new StudentDto(student.Id, student.Name, student.DateOfBirth, student.Address, student.Subjects);
            }
        }
        public StudentDto? GetStudent(int id)
        {
            var student = _unitOfWork.StudentsRepository.Get(id);
            if (student == null)
                return null;
            return new StudentDto(student.Id, student.Name, student.DateOfBirth,
                student.Address, student.Subjects);
        }
        public int CreateStudent(CreateStudentDto student)
        {
            // Get existing subjects from the database
            var existingSubjects = new List<Subject>();
            if (student.Subjects != null && student.Subjects.Any())
            {
                var subjectIds = student.Subjects.Select(s => s.Id).ToList();
                existingSubjects = _unitOfWork.SubjectsRepository.GetAll()
                    .Where(s => subjectIds.Contains(s.Id))
                    .ToList();
            }

            var StudentToCreate = new Student()
            {
                Name = student.Name,
                Address = student.Address,
                DateOfBirth = student.DateOfBirth,
                Subjects = existingSubjects
            };

            _unitOfWork.StudentsRepository.Add(StudentToCreate);
            return _unitOfWork.Complete();
        }
        
        public int UpdateStudent(StudentDto student)
        {
            // Get the existing student from the database
            var existingStudent = _unitOfWork.StudentsRepository.Get(student.Id);
            if (existingStudent == null)
            {
                return 0; // Student not found
            }

            // Update basic properties
            existingStudent.Name = student.Name;
            existingStudent.Address = student.Address;
            existingStudent.DateOfBirth = student.DateOfBirth;

            // Update subjects
            existingStudent.Subjects.Clear();
            if (student.Subjects != null && student.Subjects.Any())
            {
                var subjectIds = student.Subjects.Select(s => s.Id).ToList();
                var existingSubjects = _unitOfWork.SubjectsRepository.GetAll()
                    .Where(s => subjectIds.Contains(s.Id))
                    .ToList();
                
                foreach (var subject in existingSubjects)
                {
                    existingStudent.Subjects.Add(subject);
                }
            }

            _unitOfWork.StudentsRepository.Update(existingStudent);
            return _unitOfWork.Complete();
        }

        public bool DeleteStudent(int id)
        {
            _unitOfWork.StudentsRepository.Delete(id);
            var deleted = _unitOfWork.Complete() > 0;
            return deleted;
        }

       
      
    }
}
