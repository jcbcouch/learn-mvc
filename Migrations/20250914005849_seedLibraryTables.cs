using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace learn_mvc.Migrations
{
    /// <inheritdoc />
    public partial class seedLibraryTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Bio", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "British author best known for the Harry Potter series.", "J.K.", "Rowling" },
                    { 2, "English novelist, essayist, journalist, and critic.", "George", "Orwell" },
                    { 3, "American novelist widely known for To Kill a Mockingbird.", "Harper", "Lee" },
                    { 4, "English writer, poet, philologist, and academic.", "J.R.R.", "Tolkien" },
                    { 5, "English writer known for her detective novels.", "Agatha", "Christie" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Imaginary stories and characters.", "Fiction" },
                    { 2, "Fiction with magical or supernatural elements.", "Fantasy" },
                    { 3, "Fiction dealing with futuristic concepts.", "Science Fiction" },
                    { 4, "Fiction dealing with the solution of a crime.", "Mystery" },
                    { 5, "Works of literature that are considered to be of the highest quality.", "Classic" },
                    { 6, "Fiction set in a society that is undesirable or frightening.", "Dystopian" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Email", "EnrollmentDate", "Name" },
                values: new object[,]
                {
                    { 1, "john.smith@example.com", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "John Smith" },
                    { 2, "emily.j@example.com", new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emily Johnson" },
                    { 3, "michael.b@example.com", new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Michael Brown" },
                    { 4, "sarah.d@example.com", new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sarah Davis" },
                    { 5, "david.w@example.com", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "David Wilson" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "ISBN", "PageCount", "PublishedDate", "Title" },
                values: new object[,]
                {
                    { 1, 1, "9780747532743", 223, new DateTime(1997, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Philosopher's Stone" },
                    { 2, 2, "9780451524935", 328, new DateTime(1949, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "1984" },
                    { 3, 3, "9780061120084", 281, new DateTime(1960, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "To Kill a Mockingbird" },
                    { 4, 4, "9780547928227", 310, new DateTime(1937, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Hobbit" },
                    { 5, 5, "9780007119318", 256, new DateTime(1934, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Murder on the Orient Express" },
                    { 6, 2, "9780451526342", 112, new DateTime(1945, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Animal Farm" },
                    { 7, 4, "9780544003415", 1178, new DateTime(1954, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Lord of the Rings" }
                });

            migrationBuilder.InsertData(
                table: "LibraryCards",
                columns: new[] { "CardNumber", "ExpiryDate", "IsActive", "IssueDate", "StudentId" },
                values: new object[,]
                {
                    { "LC1001", new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { "LC1002", new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2023, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { "LC1003", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { "LC1004", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 }
                });

            migrationBuilder.InsertData(
                table: "BookCategories",
                columns: new[] { "BookId", "CategoryId", "AddedBy", "AddedDate" },
                values: new object[,]
                {
                    { 1, 1, "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 1, 2, "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 1, "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 6, "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 1, "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 5, "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 2, "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 5, "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 4, "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 1, "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 6, "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 2, "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 5, "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumns: new[] { "BookId", "CategoryId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumns: new[] { "BookId", "CategoryId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumns: new[] { "BookId", "CategoryId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumns: new[] { "BookId", "CategoryId" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumns: new[] { "BookId", "CategoryId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumns: new[] { "BookId", "CategoryId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumns: new[] { "BookId", "CategoryId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumns: new[] { "BookId", "CategoryId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumns: new[] { "BookId", "CategoryId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumns: new[] { "BookId", "CategoryId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumns: new[] { "BookId", "CategoryId" },
                keyValues: new object[] { 6, 6 });

            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumns: new[] { "BookId", "CategoryId" },
                keyValues: new object[] { 7, 2 });

            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumns: new[] { "BookId", "CategoryId" },
                keyValues: new object[] { 7, 5 });

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LibraryCards",
                keyColumn: "CardNumber",
                keyValue: "LC1001");

            migrationBuilder.DeleteData(
                table: "LibraryCards",
                keyColumn: "CardNumber",
                keyValue: "LC1002");

            migrationBuilder.DeleteData(
                table: "LibraryCards",
                keyColumn: "CardNumber",
                keyValue: "LC1003");

            migrationBuilder.DeleteData(
                table: "LibraryCards",
                keyColumn: "CardNumber",
                keyValue: "LC1004");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
