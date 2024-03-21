using RestSharp;
using NUnit.Framework;

namespace ApiTests
{
    [TestFixture]
    public class BookDetailsTests
    {
        [Test]
        public void GetBookDetails_ShouldReturnCorrectDetails()
        {
            // Setup RestSharp client and request
            var client = new RestClient("https://simple-books-api.glitch.me");
            var request = new RestRequest("/books", Method.Get);

            // Execute request
            var response = client.Execute(request);
#pragma warning disable CS8604 // Possible null reference argument.
            var books = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Book>>(response.Content);
#pragma warning restore CS8604 // Possible null reference argument.

            // Assert status code
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            // Assert specific book details (adjust indices and property names based on actual response)
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Assert.That(actual: books[0].Name,
                        Is.EqualTo("The Russian"));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            Assert.That(books[0].Type, Is.EqualTo("fiction"));
            Assert.That(books[0].Available, Is.True);

            Assert.That(books[1].Name, Is.EqualTo("Just as I Am"));
            Assert.That(books[1].Type, Is.EqualTo("non-fiction"));
            Assert.That(books[1].Available, Is.False);
        }
    }

    // Define a book class based on the expected response structure
    public class Book
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public bool Available { get; set; }
    }
}
