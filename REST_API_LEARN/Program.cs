using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_API_LEARN.Controllers;
using REST_API_LEARN.DB;
using REST_API_LEARN.Middlewares;
using REST_API_LEARN.Models;
using System.Configuration;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        //builder.Services.AddDbContext<TodoContext>(opt =>
        //    opt.UseInMemoryDatabase("TodoList"));

        string connection = "Server=(localdb)\\mssqllocaldb;Database=DSBASE;Trusted_Connection=True;";

        builder.Services.AddDbContext<TodoContext>(options => options.UseSqlServer(connection));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //builder.Services.Configure<ApiBehaviorOptions>(options =>
        //{
        //    options.SuppressConsumesConstraintForFormFileParameters = true;
        //    options.SuppressInferBindingSourcesForParameters = true;
        //    options.SuppressModelStateInvalidFilter = true;
        //});

        //builder.Services.AddControllersWithViews(options =>
        //{
        //    options.Filters.Add<SampleAsyncActionFilter>();
        //});

        builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

        var app = builder.Build();
        app.UseExceptionHandler("/Error");
        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}