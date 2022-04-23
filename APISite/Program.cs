using APISite.DAL;
using APISite.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EducationContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DbString");
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<UnitOfWork>();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


//GET
app.MapGet("/courses", ([FromServices] UnitOfWork unitOfWork) => Results.Ok(unitOfWork.CourseRepository.GetAllCourses()));

app.MapGet("/students", ([FromServices] UnitOfWork unitOfWork) => Results.Ok(unitOfWork.StudentRepository.GetAllStudents()));


app.MapGet("/courses/{id}", ([FromServices] UnitOfWork unitOfWork, int id) =>
{
    var course = unitOfWork.CourseRepository.GetOneCourse(id);
    return course is not null ? Results.Ok(course) : Results.NotFound();
});

app.MapGet("/students/{email}", ([FromServices] UnitOfWork unitOfWork, string email) =>
{
    var student = unitOfWork.StudentRepository.GetOneStudent(email);
    return student is not null ? Results.Ok(student) : Results.NotFound();
});

app.MapGet("/{email}/studentCourses", ([FromServices] UnitOfWork unitOfWork, string email) =>
{
    var student = unitOfWork.StudentRepository.GetStudentCourses(email);
    return student is not null ? Results.Ok(student) : Results.NotFound();
});


//POST
app.MapPost("/courses", ([FromServices] UnitOfWork unitOfWork, Course? course) =>
{
    if (course is null)
        return Results.BadRequest();
    unitOfWork.CourseRepository.CreateCourse(course);
    unitOfWork.Save();

    return Results.Ok();
});

app.MapPost("/students", ([FromServices] UnitOfWork unitOfWork, Student? student) =>
{
    if (student is null)
        return Results.BadRequest();
    unitOfWork.StudentRepository.CreateStudent(student);
    unitOfWork.Save();

    return Results.Ok();
});


//PUT
app.MapPut("/courses/{id}", ([FromServices] UnitOfWork unitOfWork, int id, Course course) =>
{
    var existing = unitOfWork.CourseRepository.GetOneCourse(id);
    if (existing is null)
    {
        return Results.NotFound();
    }

    unitOfWork.CourseRepository.UpdateCourse(existing, course);
    return Results.Ok(existing);
});
app.MapPut("/students/{email}", ([FromServices] UnitOfWork unitOfWork, string email, Student student) =>
{
    var existing = unitOfWork.StudentRepository.GetOneStudent(email);
    if (existing is null)
    {
        return Results.NotFound();
    }

    unitOfWork.StudentRepository.UpdateStudent(existing, student);
    return Results.Ok(existing);
});

app.MapPut("/studentCourses/{email}", ([FromServices] UnitOfWork unitOfWork, string email, int id) =>
{
    unitOfWork.CourseRepository.SignUpToCourse(email, id);
    //unitOfWork.Save();
    return Results.Ok();
});

//PATCH (dosent work atm)
//app.MapMethods("/courses/status/{id}", new[] { "PATCH" },
//    ([FromServices] UnitOfWork unitOfWork, int id, string status) =>
//    {
//        unitOfWork.CourseRepository.UpdateCourseStatus(id, status);
//        return Results.Ok();
//    });


//DELETE
app.MapDelete("/courses/{id}", ([FromServices] UnitOfWork unitOfWork, int id) =>
{
    unitOfWork.CourseRepository.DeleteCourse(id);
    unitOfWork.Save();
    return Results.Ok();
});

app.MapDelete("/students/{email}", ([FromServices] UnitOfWork unitOfWork, string email) =>
{
    unitOfWork.StudentRepository.DeleteStudent(email);
    unitOfWork.Save();
    return Results.Ok();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();