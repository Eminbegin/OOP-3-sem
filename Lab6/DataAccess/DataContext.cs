using DataAccess.Models;
using DataAccess.Models.Messages;
using DataAccess.Models.SendingMethods;
using DataAccess.Models.Workers;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> contextOptions)
        : base(contextOptions)
    {
        Database.EnsureCreated();
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Area> Areas { get; set; }
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<AbstractMessage> Messages { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Statistic> Statistics { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<SendingMethod> SendingMethods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AbstractMessage>()
            .HasDiscriminator<int>("MyType")
            .HasValue<EmailMessage>(1)
            .HasValue<PhoneMessage>(2)
            .HasValue<MessengerMessage>(3);

        modelBuilder.Entity<SendingMethod>()
            .HasDiscriminator<int>("MyType")
            .HasValue<EmailSender>(1)
            .HasValue<PhoneSender>(2)
            .HasValue<MessengerSender>(3);

        modelBuilder.Entity<Worker>()
            .HasDiscriminator<int>("MyType")
            .HasValue<Daddy>(1)
            .HasValue<Employee>(2);

        modelBuilder.Entity<Account>()
            .HasKey(x => x.Login);
        modelBuilder.Entity<Worker>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<Session>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<Area>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<Group>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<AbstractMessage>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<SendingMethod>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<Report>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<SendMethodMessages>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Group>()
            .HasMany(a => a.Employees);
        modelBuilder.Entity<Group>()
            .HasMany(a => a.MiniDaddies);
        modelBuilder.Entity<Group>()
            .HasMany(x => x.SendingMethods);
        modelBuilder.Entity<Employee>()
            .HasOne(a => a.Group);

        modelBuilder.Entity<Worker>()
            .HasOne(x => x.Account);
        modelBuilder.Entity<Worker>()
            .HasMany(x => x.SendingMethods);

        modelBuilder.Entity<Report>()
            .HasOne(a => a.Statistic);
        modelBuilder.Entity<Statistic>()
            .HasMany(x => x.MethodMessages);
        modelBuilder.Entity<Session>()
            .HasOne(x => x.Statistic);
    }
}