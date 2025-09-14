using Microsoft.EntityFrameworkCore;
using LearnMvc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LearnMvc.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }
        
        // New DbSets for library models
        public DbSet<Student> Students { get; set; }
        public DbSet<LibraryCard> LibraryCards { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // Configure one-to-one relationship between Student and LibraryCard
            builder.Entity<Student>()
                .HasOne(s => s.LibraryCard)
                .WithOne(lc => lc.Student)
                .HasForeignKey<LibraryCard>(lc => lc.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure one-to-many relationship between Author and Book
            builder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure many-to-many relationship between Book and Category
            builder.Entity<BookCategory>()
                .HasKey(bc => new { bc.BookId, bc.CategoryId });

            builder.Entity<BookCategory>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCategories)
                .HasForeignKey(bc => bc.BookId);

            builder.Entity<BookCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.BookCategories)
                .HasForeignKey(bc => bc.CategoryId);

            // Seed data
            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            // Seed Authors
            var authors = new[]
            {
                new Author { Id = 1, FirstName = "J.K.", LastName = "Rowling", Bio = "British author best known for the Harry Potter series." },
                new Author { Id = 2, FirstName = "George", LastName = "Orwell", Bio = "English novelist, essayist, journalist, and critic." },
                new Author { Id = 3, FirstName = "Harper", LastName = "Lee", Bio = "American novelist widely known for To Kill a Mockingbird." },
                new Author { Id = 4, FirstName = "J.R.R.", LastName = "Tolkien", Bio = "English writer, poet, philologist, and academic." },
                new Author { Id = 5, FirstName = "Agatha", LastName = "Christie", Bio = "English writer known for her detective novels." }
            };
            builder.Entity<Author>().HasData(authors);

            // Seed Categories
            var categories = new[]
            {
                new Category { Id = 1, Name = "Fiction", Description = "Imaginary stories and characters." },
                new Category { Id = 2, Name = "Fantasy", Description = "Fiction with magical or supernatural elements." },
                new Category { Id = 3, Name = "Science Fiction", Description = "Fiction dealing with futuristic concepts." },
                new Category { Id = 4, Name = "Mystery", Description = "Fiction dealing with the solution of a crime." },
                new Category { Id = 5, Name = "Classic", Description = "Works of literature that are considered to be of the highest quality." },
                new Category { Id = 6, Name = "Dystopian", Description = "Fiction set in a society that is undesirable or frightening." }
            };
            builder.Entity<Category>().HasData(categories);

            // Seed Books
            var books = new[]
            {
                new Book { Id = 1, Title = "Harry Potter and the Philosopher's Stone", ISBN = "9780747532743", 
                          PublishedDate = new DateTime(1997, 6, 26), PageCount = 223, AuthorId = 1 },
                new Book { Id = 2, Title = "1984", ISBN = "9780451524935", 
                          PublishedDate = new DateTime(1949, 6, 8), PageCount = 328, AuthorId = 2 },
                new Book { Id = 3, Title = "To Kill a Mockingbird", ISBN = "9780061120084", 
                          PublishedDate = new DateTime(1960, 7, 11), PageCount = 281, AuthorId = 3 },
                new Book { Id = 4, Title = "The Hobbit", ISBN = "9780547928227", 
                          PublishedDate = new DateTime(1937, 9, 21), PageCount = 310, AuthorId = 4 },
                new Book { Id = 5, Title = "Murder on the Orient Express", ISBN = "9780007119318", 
                          PublishedDate = new DateTime(1934, 1, 1), PageCount = 256, AuthorId = 5 },
                new Book { Id = 6, Title = "Animal Farm", ISBN = "9780451526342", 
                          PublishedDate = new DateTime(1945, 8, 17), PageCount = 112, AuthorId = 2 },
                new Book { Id = 7, Title = "The Lord of the Rings", ISBN = "9780544003415", 
                          PublishedDate = new DateTime(1954, 7, 29), PageCount = 1178, AuthorId = 4 }
            };
            builder.Entity<Book>().HasData(books);

            // Create a fixed date for all seed data
            var fixedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Seed BookCategories (many-to-many relationship)
            var bookCategories = new[]
            {
                // Harry Potter
                new { BookId = 1, CategoryId = 1, AddedBy = "System", AddedDate = fixedDate },
                new { BookId = 1, CategoryId = 2, AddedBy = "System", AddedDate = fixedDate },
                
                // 1984
                new { BookId = 2, CategoryId = 1, AddedBy = "System", AddedDate = fixedDate },
                new { BookId = 2, CategoryId = 6, AddedBy = "System", AddedDate = fixedDate },
                
                // To Kill a Mockingbird
                new { BookId = 3, CategoryId = 1, AddedBy = "System", AddedDate = fixedDate },
                new { BookId = 3, CategoryId = 5, AddedBy = "System", AddedDate = fixedDate },
                
                // The Hobbit
                new { BookId = 4, CategoryId = 2, AddedBy = "System", AddedDate = fixedDate },
                new { BookId = 4, CategoryId = 5, AddedBy = "System", AddedDate = fixedDate },
                
                // Murder on the Orient Express
                new { BookId = 5, CategoryId = 4, AddedBy = "System", AddedDate = fixedDate },
                
                // Animal Farm
                new { BookId = 6, CategoryId = 1, AddedBy = "System", AddedDate = fixedDate },
                new { BookId = 6, CategoryId = 6, AddedBy = "System", AddedDate = fixedDate },
                
                // The Lord of the Rings
                new { BookId = 7, CategoryId = 2, AddedBy = "System", AddedDate = fixedDate },
                new { BookId = 7, CategoryId = 5, AddedBy = "System", AddedDate = fixedDate }
            };
            builder.Entity<BookCategory>().HasData(bookCategories);

            // Seed Students
            var students = new[]
            {
                new Student { Id = 1, Name = "John Smith", Email = "john.smith@example.com", 
                            EnrollmentDate = new DateTime(2024, 1, 15) },
                new Student { Id = 2, Name = "Emily Johnson", Email = "emily.j@example.com", 
                            EnrollmentDate = new DateTime(2024, 2, 20) },
                new Student { Id = 3, Name = "Michael Brown", Email = "michael.b@example.com", 
                            EnrollmentDate = new DateTime(2024, 1, 5) },
                new Student { Id = 4, Name = "Sarah Davis", Email = "sarah.d@example.com", 
                            EnrollmentDate = new DateTime(2023, 12, 10) },
                new Student { Id = 5, Name = "David Wilson", Email = "david.w@example.com", 
                            EnrollmentDate = new DateTime(2024, 3, 1) }
            };
            builder.Entity<Student>().HasData(students);

            // Seed Library Cards (one-to-one with Students)
            var libraryCards = new[]
            {
                new LibraryCard { 
                    CardNumber = "LC1001", 
                    StudentId = 1, 
                    IssueDate = new DateTime(2023, 12, 1), 
                    ExpiryDate = new DateTime(2024, 12, 1), 
                    IsActive = true 
                },
                new LibraryCard { 
                    CardNumber = "LC1002", 
                    StudentId = 2, 
                    IssueDate = new DateTime(2023, 11, 1), 
                    ExpiryDate = new DateTime(2024, 11, 1), 
                    IsActive = true 
                },
                new LibraryCard { 
                    CardNumber = "LC1003", 
                    StudentId = 3, 
                    IssueDate = new DateTime(2023, 10, 1), 
                    ExpiryDate = new DateTime(2024, 10, 1), 
                    IsActive = true 
                },
                new LibraryCard { 
                    CardNumber = "LC1004", 
                    StudentId = 4, 
                    IssueDate = new DateTime(2023, 9, 1), 
                    ExpiryDate = new DateTime(2024, 9, 1), 
                    IsActive = false 
                }
                // Student 5 doesn't have a library card yet
            };
            builder.Entity<LibraryCard>().HasData(libraryCards);
        }
    }
}
