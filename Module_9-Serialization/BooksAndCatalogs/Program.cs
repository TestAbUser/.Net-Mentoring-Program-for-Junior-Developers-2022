using System;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;

namespace HomeTask1
{
    public class Program
    {
        public static void Main()
        {
            Catalog catalog = new() { Date = new DateTime(2000, 1, 1) };
            Book[] books = { new Book() {
                Id = "bk113",
                Author = "Richter, Jeffry",
                Title = "CLR via C#",
                Genre = Genre.Computer,
                Publisher = "Microsoft Press",
                PublishDate = new DateTime(2013, 12, 1),
                Description = "Book on coding",
                RegistrationDate = new DateTime(2019, 3, 5) }
            };
            catalog.Book = books;

            // Serialize the new catalog object into xml and save it in a file.
            XmlSerializer writer = new(typeof(Catalog));
            using (FileStream wfile = File.Create(Path.Combine(Environment.CurrentDirectory, "newBooks.xml")))
            {
                writer.Serialize(wfile, catalog);
                wfile.Close();
            }

            // Get the data from books.xml, deserialize it and display some of it in Console.
            XmlSerializer reader = new(typeof(Catalog));
            var assembly = Assembly.GetExecutingAssembly();
            using Stream stream = assembly.GetManifestResourceStream("BooksAndCatalogs.Resources.books.xml");
            catalog = (Catalog)reader.Deserialize(stream);
            foreach (var book in catalog.Book)
            {
                Console.WriteLine(book.Title);
            }
        }
    }
}
