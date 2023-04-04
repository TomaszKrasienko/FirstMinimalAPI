var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
var books = new List<Book>
{
    new Book {Id = 1, Title="The witcher", Author="Andrzej Sapkowski"},
    new Book {Id=2, Title="Harry Potter", Author="J.K. Rowling"}
};
app.MapGet("/book", () =>
{
    return books;
});
app.MapGet("/book/{id}", (int id) =>
{
    var book = books.FirstOrDefault(x => x.Id == id);
    if (book is null)
        return Results.NotFound("This book doesn't exists");
    return Results.Ok(book);
});
app.MapPost("/book", (Book book) =>
{
    books.Add(book);
    return books;
});
app.MapPut("/book/{id}", (Book updatedBook, int id) =>
{
    var book = books.FirstOrDefault(x => x.Id == id);
    if (book is null)
        return Results.NotFound("This book doesn't exists");
    book.Title = updatedBook.Title;
    book.Author = updatedBook.Author;
    return Results.Ok(book);
});
app.MapDelete("/book/{id}", (int id) =>
{
    var book = books.FirstOrDefault(x => x.Id == id);
    if (book is null)
        return Results.NotFound("This book doesn't exists");
    books.Remove(book);
    return Results.Ok(book);
});
app.Run();

class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
}

