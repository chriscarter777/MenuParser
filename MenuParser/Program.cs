using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace MenuParser
{
     public class Program
     {
          private static string _path2menu;
          private static string _targetpath2match;

          public static void Main(string[] args)
          {
               _path2menu = args[0];
               _targetpath2match = args[1];

               List<MenuItem> parsedItems = ParseItems(_path2menu);
               List<MenuItem> markedItems = MarkActives(parsedItems, _targetpath2match);

               foreach (MenuItem i in markedItems)
               {
                    Console.WriteLine(i.ToString());
               }
               Console.ReadLine();
          }

          public static List<MenuItem> ParseItems(string path2menu)
          {
               List<MenuItem> items = new List<MenuItem>();
               int depth = 0;
               int id = 0;
               Stack<int> parents = new Stack<int>();

               using (XmlReader reader = XmlReader.Create(path2menu))
               {
                    while (reader.Read())
                    {
                         if (reader.IsStartElement() && reader.Name == "item")
                         {
                              id++;
                              string name = "";
                              string path = "";
                              reader.Read();
                              if (reader.IsStartElement() && reader.Name == "displayName")
                              {
                                   reader.Read();
                                   name = reader.Value.Trim();
                                   reader.Read();
                                   reader.Read();
                                   if (reader.IsStartElement() && reader.Name == "path")
                                   {
                                        path = reader["value"];
                                   }

                              }
                              else if (reader.Name == "path")
                              {
                                   path = reader["value"];
                                   reader.Read();
                                   reader.Read();
                                   name = reader.Value.Trim();
                                   reader.Read();
                              }
                              items.Add(new MenuItem(false, id, name, parents, path));
                         }
                         if (reader.IsStartElement() && reader.Name == "subMenu")
                         {
                              depth++;
                              parents.Push(id);
                         }
                         if (!reader.IsStartElement() && reader.Name == "subMenu")
                         {
                              depth--;
                              parents.Pop();
                         }
                         if (!reader.IsStartElement() && reader.Name == "menu")
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
