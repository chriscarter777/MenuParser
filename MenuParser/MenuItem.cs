using System.Collections.Generic;
using System.Text;

namespace MenuParser
{
     public class MenuItem
     {
          public bool Active { get; set; }
          public int Id { get; set; }
          public string Name { get; set; }
          public Stack<int> Parents { get; set; }
          public string Path { get; set; }

          public MenuItem() {
               Parents = new Stack<int>();
          }

          public MenuItem(bool active, int id, string name, Stack<int> parents, string path)
          {
               Active = active;
               Id = id;
               Name = name;
               Parents = new Stack<int>();
               Path = path;
               foreach(int i in parents)
               {
                    Parents.Push(i);
               }
          }

          public void Activate()
          {
               Active = true;
          }

          public override string ToString()
          {
               StringBuilder sb = new StringBuilder();
               sb.Append('\t', Parents.Count);
               sb.Append(Name + ", " + Path);
               if (Active)
               {
                    sb.Append(" ACTIVE");
               }
               return sb.ToString();
          }
     }
}
