using ItemManagement.Database;
using ItemManagement.Items;
using NSubstitute;
using NUnit.Framework;

namespace ItemManagement.Tests
{
    public class DatabaseTests
    {
        [Test]
        public void Retrieve_Null()
        {
            IItemDatabase database = new ItemDatabase();
            
            Assert.IsNull(database.GetItem(null));
        }

        [Test]
        public void Added_Item_IsRetrieved()
        {
            const string id = "id";
            IItemDefinition item = Substitute.For<IItemDefinition>();
            item.Id.Returns(id);
            IItemDatabase database = new ItemDatabase(item);
            
            Assert.IsTrue(database.GetItem(id) == item);
        }

        [Test]
        public void Add_Same_Item()
        {
            const string id = "id";
            IItemDefinition itemA = Substitute.For<IItemDefinition>();
            itemA.Id.Returns(id);
            IItemDefinition itemB = Substitute.For<IItemDefinition>();
            itemA.Id.Returns(id);
            IItemDatabase database = new ItemDatabase(itemA, itemB);
            
            Assert.IsTrue(database.GetItem(id) == itemA);
        }
    }
}
