using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain;
using RestaurantManagement.Domain.DBModel;
using RestaurantManagement.Domain.Enumerations;
using System;

namespace RestaurantManagement.Data.Contexts
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions options) : base(options)
        {
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is Audit);
            entities.ToList().ForEach(entity =>
            {
                var baseEntity = ((Audit)entity.Entity);
                var now = DateTime.UtcNow;
                if (entity.State == EntityState.Added)
                {
                    baseEntity.CreatedOn = now;
                    baseEntity.CreatedBy = string.IsNullOrWhiteSpace(baseEntity.UserId) ? baseEntity.CreatedBy : baseEntity.UserId;
                }

                baseEntity.ModifiedBy = string.IsNullOrWhiteSpace(baseEntity.UserId) ? baseEntity.ModifiedBy : baseEntity.UserId;
                baseEntity.ModifiedOn = now;
            });

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().HasData(GetStatuses());
            modelBuilder.Entity<OrderStatus>().HasData(GetOrderStatuses());
            modelBuilder.Entity<Category>().HasData(CreateTestCategory());
            modelBuilder.Entity<Restaurant>().HasData(CreateTestRestaurnat());
            modelBuilder.Entity<Menu>().HasData(CreateTestMenu());
            modelBuilder.Entity<User>().HasData(CreateAdminUser());
        }
        private static List<Status> GetStatuses()
        {
            List<Status> statuses = new List<Status>();
            foreach (StatusEnum status in Enum.GetValues(typeof(StatusEnum)))
            {
                statuses.Add(new Status { Id = (byte)status, Name = status.ToString() });
            }
            return statuses;
        }

        private static List<OrderStatus> GetOrderStatuses()
        {
            List<OrderStatus> orderStatuses = new List<OrderStatus>();
            foreach (OrderStatusEnum status in Enum.GetValues(typeof(OrderStatusEnum)))
            {
                orderStatuses.Add(new OrderStatus { Id = (byte)status, Name = status.ToString() });
            }
            return orderStatuses;
        }

        private static List<Category> CreateTestCategory()
        {
            return new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    CreatedBy = "System",
                    CreatedOn = DateTime.UtcNow,
                    Description = "This is dummy category created by default by system for demo purporse only.",
                    ModifiedBy = "System",
                    ModifiedOn = DateTime.UtcNow,
                    Name = "Demo Category",
                    StatusId = (byte)StatusEnum.Active
                }
            };
        }

        private static List<Restaurant> CreateTestRestaurnat()
        {
            return new List<Restaurant>()
            {
                new Restaurant()
                {
                    Id = 1,
                    Address = "43a Nehru place, delhi, India - 174890",
                    CreatedBy = "System",
                    CreatedOn = DateTime.UtcNow,
                    Description = "This is dummy restaurant created by default by system for demo purporse only.",
                    ModifiedBy = "System",
                    ModifiedOn = DateTime.UtcNow,
                    Name = "Demo Restaurant",
                    PhoneNumber = "+919123456780",
                    StatusId = (byte)StatusEnum.Active,
                    WebsiteUrl = "www.restaurant.com"

                }
            };
        }

        private static List<Menu> CreateTestMenu()
        {
            return new List<Menu>()
                {
                    new Menu()
                    {
                        Id = 1,
                        CreatedBy = "System",
                        Description = "This is dummy Ist menu created by default by system for demo purporse only.",
                        ModifiedBy = "System",
                        Name = "Demo Ist Menu",
                        Price = 10,
                        RestaurantId =1,
                        ModifiedOn = DateTime.UtcNow,
                        CreatedOn = DateTime.UtcNow,
                        Quantity = 10,
                        CategoryId = 1,
                        StatusId= (byte)StatusEnum.Active

                    },
                    new Menu()
                    {
                        Id = 2,
                        CreatedBy = "System",
                        Description = "This is dummy IInd menu created by default by system for demo purporse only.",
                        ModifiedBy = "System",
                        Name = "Demo IInd Menu",
                        Price = 20,
                        RestaurantId =1,
                        ModifiedOn = DateTime.UtcNow,
                        CreatedOn = DateTime.UtcNow,
                        Quantity = 10,
                        CategoryId = 1,
                        StatusId= (byte)StatusEnum.Active

                    }
                };
        }

        public static List<User> CreateAdminUser()
        {
            return new List<User>()
            {
                new User()
                {
                    Id=1,
                    Name = "Admin User",
                    ModifiedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                    StatusId= (byte)StatusEnum.Active,
                    UserName = "admin",
                    Password = "admin",
                }
            };
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
