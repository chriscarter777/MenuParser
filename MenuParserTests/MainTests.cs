using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MenuParser
{
     [TestClass]
     public class MainTests
     {
          [TestMethod]
          public void SetParametersAccepts2Parameters()
          {
               string _p = null;
               string _t = null;
               Program.SetParameters(new string[] { "one", "two" }, out  _p, out  _t);
               Assert.IsNotNull(_p);
               Assert.IsNotNull(_t);
          }  //SetParametersAccepts2Parameters

          [TestMethod]
          public void ParseItemsIdentifiesFirstItem()
          {
               string expectedName = "First";
               string expectedPath = "/Premier.aspx";
               string path = "../../TestMenu.xml";
               List<MenuItem> result = Program.ParseItems(path);
               Assert.IsInstanceOfType(result[0], typeof(MenuItem));
               Assert.AreEqual(expectedName, result[0].Name);
               Assert.AreEqual(expectedPath, result[0].Path);
          }  //ParseItemsIdentifiesFirstItem

          [TestMethod]
          public void ParseItemsHasCorrectItemCount()
          {
               string path = "../../TestMenu.xml";
               List<MenuItem> result = Program.ParseItems(path);
               Assert.AreEqual(3, result.Count);
          }  //ParseItemsHasCorrectItemCount

          [TestMethod]
          public void MarkActiveMarksCorrectly()
          {
               string targetmatch = "/test/path.aspx";
               List<MenuItem> testitems = new List<MenuItem> {
                    new MenuItem(false, 1, "Test item 1", new Stack<int>(), "/not/test/path.aspx"),
                    new MenuItem(false, 2, "Test item 2", new Stack<int>(), "/not/test/path.aspx"),
                    new MenuItem(false, 3, "Test item 3", new Stack<int>(new int[]{2}), "/test/path.aspx")
               };
               List<MenuItem> result = Program.MarkActives(testitems, targetmatch);
               foreach(MenuItem i in result)
               {
                    Console.WriteLine(i.ToString());
               }
               Assert.AreEqual(3, result.Count);
               Assert.IsFalse(result[0].Active);
               Assert.IsTrue(result[1].Active);
               Assert.IsTrue(result[2].Active);
          }  //MarkActiveMarksCorrectly
     }  //class
}  //namespace
