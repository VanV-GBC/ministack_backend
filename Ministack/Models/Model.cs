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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Schedule>()
            .HasMany(c => c.Events)
            .WithOne(e => e.Schedule)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Event>()
            .HasOne(a => a.Location)
            .WithOne(b => b.Event)
            .HasForeignKey<EventLocation>(b => b.EventRef)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
public class Schedule
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }

    public ICollection<Event>? Events { get; set; }
}

public class Response
{
    public string Status { get; set; }
    public string Message { get; set; }
}

public class Event 
{ 
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime startDateTime { get; set; }

    public DateTime endDateTime { get; set; }

    public EventLocation? Location { get; set; }

    public Schedule Schedule { get; set; }

}

public class EventLocation
{
    public int Id { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set;}

    public string Country { get; set; }
    public string? CountryCode { get; set; }

    public string? State { get; set; }
    public string? Province { get; set; }
    public string? Region { get; set; }

    public string PostalCode { get; set; }

    public string City { get; set;}

    public string Phone { get; set;}

    public Event Event { get; set; }

    public int EventRef { get; set; }

}