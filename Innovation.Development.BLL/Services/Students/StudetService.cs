using Innovation.Development.BLL.Models.Students;
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
            var StudentToCreate = new Student()
            {
                Name = student.Name,
                Address = student.Address,
                DateOfBirth = student.DateOfBirth,
                Subjects = student.Subjects
            };
          

            _unitOfWork.StudentsRepository.Add(StudentToCreate);
            return _unitOfWork.Complete();
        }
        public int UpdateStudent(StudentDto student)
        {
            var StudentToUpdate= new Student()
            {
                Name = student.Name,
                Address = student.Address,
                DateOfBirth = student.DateOfBirth,
                Subjects = student.Subjects
            };
            _unitOfWork.StudentsRepository.Update(StudentToUpdate);
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
