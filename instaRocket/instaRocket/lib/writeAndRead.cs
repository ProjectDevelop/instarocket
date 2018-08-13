using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instaRocket.lib
{
     static class writeAndRead
    {

         public static List<string> CommentList()
         {
            List<string> temp = new List<string>();
            try {
            using (StreamReader sr = new StreamReader("commentlist.dat")) {
               string line;
               while ((line = sr.ReadLine()) != null) {
                   temp.Add(line);
               }
            }
         }
         catch {
             return new List<string>{"Boş yorum"};
         }
            return temp;
        }

         public static void makeCommentList(string[] list)
        {
            using (StreamWriter sw = new StreamWriter("commentlist.dat"))
            {

                foreach (string s in list)
                {
                    sw.WriteLine(s);
                }
            }
        }

    }
}
