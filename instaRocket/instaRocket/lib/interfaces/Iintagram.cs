﻿using System;
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
        void likeHomePage(int count);
        void likeprofile(string profileName);
        void likeHashtag(string hashtagName);
        void likeExplore(int count);
        void LikeLocation(int count);
    
    }
}
