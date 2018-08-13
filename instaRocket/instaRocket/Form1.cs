using instaRocket.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace instaRocket
{
    public partial class Form1 : Form
    {
        instagram d;
        public Form1()
        {
            InitializeComponent();
            d = new instagram("denemem122","q1w2e3r4t5"); //new instagram("bebekvecocuk08", "fc010318");
            d.Login();
            writeAndRead.makeCommentList(new string[] { "yorum1", "yorum2", "yorum3", "yorum4" });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //d.likeHomePage();
            //d.likeprofile("prosto_dasha26092004");
            //d.likeExplore(10);
            //d.likeHashtag("istanbul");
            //d.LikeLocation("avcılar");

            //d.Unfollow(12);
            //d.FollowExplore();
            //d.FollowHashtag("canada🇨🇦");
            //d.FollowProfileLastContentLike("cdabreeze._",12);
            //d.FollowLocation("avcılar sahil", 20);
            //d.FollowProfileLastContentComment("thef2",45);

            //d.CommentHashtag("istanbul", writeAndRead.CommentList().ToArray(),0);
            //d.CommentExplore(writeAndRead.CommentList().ToArray(), 0);
            //d.CommentLocation("istanbul", writeAndRead.CommentList().ToArray(),0);
            //d.CommentProfile("cdabreeze._", writeAndRead.CommentList().ToArray(), 0);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            d.closeChrome();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
