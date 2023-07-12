using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Ministack.Models;
public class MiniContext : DbContext
{
    public DbSet<Schedule> Schedule { get; set; }

    public string DbPath { get; }

    public MiniContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "ministack.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}
public class Schedule
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}

public class Response
{
    public string Status { get; set; }
    public string Message { get; set; }
}
