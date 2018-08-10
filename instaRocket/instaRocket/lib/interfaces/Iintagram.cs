using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instaRocket
{
    interface Iintagram
    {
        //account control
        void login();
        void logout();

        //Like
        void likeHomePage(int count=0);
        void likeprofile(string profileName,int count=0);
        void likeHashtag(string hashtagName,int count=0);
        void likeExplore(int count=0);
        void LikeLocation(string location,int count=0);
    
    }
}
