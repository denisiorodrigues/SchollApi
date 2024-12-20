namespace SchollApi;

public interface IStudenService
{
    Task<IEnumerable<Student>> GetStudents();
    Task<Student> GetStudent(int id);
    Task<IEnumerable<Student>> GetStudentByName(string name);
    Task<Student> Create(Student student);
    Task<Student> Update(Student student);
    Task<Student> Delete(Student student);
}
