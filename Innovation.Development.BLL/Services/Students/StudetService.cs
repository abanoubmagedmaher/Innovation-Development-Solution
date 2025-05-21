﻿﻿﻿﻿﻿﻿using Innovation.Development.BLL.Models.Students;
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
            // Create a new student without subjects first
            var studentToCreate = new Student()
            {
                Name = student.Name,
                Address = student.Address,
                DateOfBirth = student.DateOfBirth,
                Subjects = new List<Subject>()
            };

            // Add the student to the database
            _unitOfWork.StudentsRepository.Add(studentToCreate);
            _unitOfWork.Complete(); // Save to get the student ID

            // If there are subjects to add, retrieve them from the database and attach them
            if (student.Subjects != null && student.Subjects.Any())
            {
                // Get the student with tracking enabled
                var createdStudent = _unitOfWork.StudentsRepository.Get(studentToCreate.Id);
                if (createdStudent != null)
                {
                    // Get the subject IDs
                    var subjectIds = student.Subjects.Select(s => s.Id).ToList();
                    
                    // For each subject ID, find the subject in the database and add it to the student
                    foreach (var subjectId in subjectIds)
                    {
                        var subject = _unitOfWork.SubjectsRepository.Get(subjectId);
                        if (subject != null)
                        {
                            createdStudent.Subjects.Add(subject);
                        }
                    }
                    
                    // Save the changes
                    return _unitOfWork.Complete();
                }
            }

            return 1; // Return success
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

            // Clear existing subjects
            existingStudent.Subjects.Clear();
            _unitOfWork.Complete(); // Save the changes to clear the subjects

            // Add selected subjects
            if (student.Subjects != null && student.Subjects.Any())
            {
                // Get the subject IDs
                var subjectIds = student.Subjects.Select(s => s.Id).ToList();
                
                // For each subject ID, find the subject in the database and add it to the student
                foreach (var subjectId in subjectIds)
                {
                    var subject = _unitOfWork.SubjectsRepository.Get(subjectId);
                    if (subject != null)
                    {
                        existingStudent.Subjects.Add(subject);
                    }
                }
            }

            // Save the changes
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
