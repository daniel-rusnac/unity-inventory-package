using NUnit.Framework;

namespace ItemManagement.Tests
{
    public class DatabaseTests
    {
        [Test]
        public void Retrieve_NullId()
        {
            IItemDatabase database = new ItemDatabase();
            Assert.IsNull(database.GetItem(null));
        }
    }
}
