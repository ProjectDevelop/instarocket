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

        //Follow
        void FollowProfileLastContentLike(string profileName, int count = 0);
        void FollowProfileLastContentComment(string profileName, int count = 0);
        void FollowHashtag(string hashtagName, int count = 0);
        void FollowExplore(int count = 0);
        void FollowLocation(string location, int count = 0);
        void Unfollow(int count = 0);

        //Comment
        void CommentHomePage(string comment,int count = 0);
        void CommentHashtag(string comment, int count = 0);
        void CommentLocation(string comment, int count = 0);
        void CommentExplore(string comment, int count = 0);
        void CommentProfile(string comment, string profileName, int count = 0);

        
    
    }
}
