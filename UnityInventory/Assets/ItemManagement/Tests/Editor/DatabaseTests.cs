using NUnit.Framework;

namespace ItemManagement.Tests
{
    public class DatabaseTests
    {
        [Test]
        public void Retrieve_Null_Id()
        {
            IItemDatabase database = new ItemDatabase();
            Assert.IsNull(database.GetItem(null));
        }
    }
}
