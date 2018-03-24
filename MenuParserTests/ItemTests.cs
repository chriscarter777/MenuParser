using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MenuParser
{
     [TestClass]
     public class ItemTests
     {

          [TestMethod]
          public void ActivateItemWorks()
          {
               MenuItem i = new MenuItem(false, 1, "Test item", new Stack<int>(new int[] { 2 }), "/test/path.aspx");
               Assert.IsFalse(i.Active);
               i.Activate();
               Assert.IsTrue(i.Active);
          }

          [TestMethod]
          public void ItemConstructorWorks()
          {
               MenuItem i = new MenuItem(true, 1, "testitem", new Stack<int>(new int[] { 2 }), "/test/path.aspx");
               Assert.IsTrue(i.Active);
               Assert.AreEqual(1, i.Id);
               Assert.AreEqual("testitem", i.Name);
               Assert.IsInstanceOfType(i.Parents, typeof(Stack<int>));
               Assert.AreEqual(2, i.Parents.Pop());
               Assert.AreEqual("/test/path.aspx", i.Path);
          }

          [TestMethod]
          public void ItemToStringPrintsCorrectly()
          {
               MenuItem i = new MenuItem(true,1, "Test item", new Stack<int>(new int[] { 2 }), "/test/path.aspx");
               string expected = "\tTest item, /test/path.aspx ACTIVE";
               Assert.AreEqual(expected, i.ToString());
          }
     }
}
