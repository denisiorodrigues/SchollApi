
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SchollApi;

public class StudentService : IStudenService
{
    private readonly AppDbContext _context;

    public StudentService(AppDbContext context)
    {
        this._context = context;
    }

    public async Task<IEnumerable<Student>> GetStudents()
    {
        return await _context
                .Students
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ToListAsync();
    }

    public async Task<IEnumerable<Student>> GetStudentByName(string name)
    {
        if(string.IsNullOrEmpty(name))
        {
            return await GetStudents();
        }
        else 
        {
            return await _context
                        .Students
                        .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                        .ToListAsync();
        }
    }

    public async Task<Student> GetStudent(int id)
    {
        return await _context.Students.FindAsync(id);
    }

    public async Task<Student> Create(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<Student> Update(Student student)
    {
        _context.Students.Entry(student).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<Student> Delete(Student student)
    {
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return student;
    }
}
