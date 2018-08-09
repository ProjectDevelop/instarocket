using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instaRocket
{
    interface Iintagram
    {
        
        void login();
        void logout();
        //
        void likeHomePage(int count);
        void likeprofile(string profileName);
        void likeHashtag(string hashtagName);
        void likeExplore(int count);
    
    }
}
