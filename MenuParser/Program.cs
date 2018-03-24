using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace MenuParser
{
     public class Program
     {
          private static string _path2xml;
          private static string _targetpath2match;

          public static void Main(string[] args)
          {
               _path2xml = args[0];
               _targetpath2match = args[1];

               List<MenuItem> parsedItems = ParseItems(_path2xml);
               List<MenuItem> markedItems = MarkActives(parsedItems, _targetpath2match);

               foreach (MenuItem mi in markedItems)
               {
                    Console.WriteLine(mi.ToString());
               }
               Console.WriteLine("\nPress ENTER to exit");
               Console.ReadLine();
          }  //Main

          public static List<MenuItem> ParseItems(string path2menu)
          {
               //CONSTRAINT: Within each <item>, <displayName> and <path> may be specified in either order, but must preceed <subMenu>
               List<MenuItem> items = new List<MenuItem>();
               int depth = 0;
               int id = 0;
               Stack<int> parents = new Stack<int>();

               using (XmlReader reader = XmlReader.Create(path2menu))
               {
                    while (reader.Read())
                    {
                         if (reader.NodeType == XmlNodeType.Element && reader.Name == "item")
                         {
                              id++;
                              string name = null;
                              string path = null;
                              while (reader.Read() && (name == null || path == null))
                              {
                                   if (reader.NodeType == XmlNodeType.Element && reader.Name == "displayName")
                                   {
                                        reader.Read();
                                        name = reader.Value.Trim();
                                   }
                                   if (reader.NodeType == XmlNodeType.Element && reader.Name == "path")
                                   {
                                        path = reader["value"];
                                   }
                              }
                              items.Add(new MenuItem(false, id, name, parents, path));
                         }
                         if (reader.NodeType == XmlNodeType.Element && reader.Name == "subMenu")
                         {
                              depth++;
                              parents.Push(id);
                         }
                         if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "subMenu")
                         {
                              depth--;
                              parents.Pop();
                         }
                         if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "menu")
                         {
                              break;
                         }
                    }
               }
               return items;
          }  //ParseItems

          public static List<MenuItem> MarkActives(List<MenuItem> inputItems, string matchpath)
          {
               List<MenuItem> markedItems = new List<MenuItem>(inputItems);
               foreach (MenuItem match in markedItems.Where(x => x.Path == matchpath))
               {
                    match.Activate();
                    foreach (int id in match.Parents)
                    {
                         markedItems.FirstOrDefault(x => x.Id == id).Activate();
                    }
               }
               return markedItems;
          }  //MarkActives

     }  //class
}  //namespace
