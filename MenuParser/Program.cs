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
               SetParameters(args, out  _path2xml, out  _targetpath2match);
               List<MenuItem> parsedItems = ParseItems(_path2xml);
               List<MenuItem> markedItems = MarkActives(parsedItems, _targetpath2match);
               PrintResults(markedItems);
          }  //Main

          public static void SetParameters(string[] args, out string path2xml, out string targetpath2match)
          {
               if (args.Length != 2)
               {
                    Console.WriteLine("usage: menuparser.exe <path to xml file> <active target url>");
                    Environment.Exit(1);
               }
               path2xml = args[0];
               targetpath2match = args[1];
          }  //SetParameters

          public static List<MenuItem> ParseItems(string path2menu)
          {
               //CONSTRAINT: Within each <item>, <displayName> and <path> may be specified in either order, but must preceed <subMenu>
               List<MenuItem> items = new List<MenuItem>();
               int depth = 0;
               int id = 0;
               Stack<int> parents = new Stack<int>();

               try
               {
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
               }
               catch
               {
                    Console.WriteLine("***An error occurred while reading the XML file.***");
                    return items;
               }
          }  //ParseItems

          public static List<MenuItem> MarkActives(List<MenuItem> inputItems, string matchpath)
          {
               List<MenuItem> markedItems = new List<MenuItem>(inputItems);
               foreach (MenuItem match in markedItems.Where(x => x.Path.Trim().ToLower() == matchpath.Trim().ToLower()))
               {
                    match.Activate();
                    foreach (int id in match.Parents)
                    {
                         markedItems.FirstOrDefault(x => x.Id == id).Activate();
                    }
               }
               return markedItems;
          }  //MarkActives

          public static void PrintResults(List<MenuItem> items)
          {
               foreach (MenuItem mi in items)
               {
                    Console.WriteLine(mi.ToString());
               }
               //for debugging:
               //Console.WriteLine("\nPress ENTER to exit");
               //Console.ReadLine();
          }  //PrintResults

     }  //class
}  //namespace
