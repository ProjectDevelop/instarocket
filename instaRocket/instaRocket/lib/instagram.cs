using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Chrome;

namespace instaRocket
{
    class instagram : Iintagram
    {
       
       private string UserName;
       private string Password;
       private string URL = "https://www.instagram.com/";
       private string likeText = "Beğen", followText = "Takip Et", commentButton = "Yorum Yap", commentText = "Yorum ekle...";
       private ChromeDriver browser;
       private IJavaScriptExecutor js;
       

       public instagram(string UserName, string Password)
       {
           this.UserName = UserName;
           this.Password = Password;
           
           ////////////browser and options////////////////
           ChromeDriverService service = ChromeDriverService.CreateDefaultService();
           service.HideCommandPromptWindow = true;
           ChromeOptions options = new ChromeOptions();
            /* options.AddArgument("--headless"); */ //hide chrome
            options.AddArguments("--lang=tr");
            browser = new ChromeDriver(service,options);
           js = (IJavaScriptExecutor)browser;
           /////////////////////

           
       }

       public void Login()
       {
           browser.Navigate().GoToUrl(URL + "accounts/login/");
           delay(1);
           browser.FindElementByName("username").SendKeys(UserName);
           browser.FindElementByName("password").SendKeys(Password);
           browser.FindElementByTagName("button").Click();
           delay(2);

       }

       public void Logout()
       {
           browser.Navigate().GoToUrl(URL + "accounts/logout/");
           delay(1);
       }

       ///////////////////  Like  ///////////////////

       public void LikeLocation(string location,int count=0)
       {
            IWebElement searchbox;

            browser.Navigate().GoToUrl(URL);
            delay(2);
            searchbox = browser.FindElement(By.XPath("//*[@id=\"react-root\"]/section/nav/div[2]/div/div/div[2]/input"));//*[@id="react-root"]/section/nav/div[2]/div/div/div[2]/div[2]/div[2]/div/a[1]
            searchbox.SendKeys(location);
            delay(2);
           IWebElement location1 =  browser.FindElement(By.XPath("//*[@id=\"react-root\"]/section/nav/div[2]/div/div/div[2]/div[2]/div[2]/div/a[contains(@href,'explore/locations/')]"));
            location1.Click();
            delay(2);
            popUpContentLike(count);
        }

       public void LikeHomePage(int count=0)
       {
           browser.Navigate().GoToUrl(URL);

           IWebElement[] articles;
           IWebElement likespan;
           int i = 1;
           delay(3);
           int hight=0;
           js.ExecuteScript("window.scrollBy(0,200);");
           while(true)
           {
               delay(3);
               try
               {
                        
                   articles = browser.FindElements(By.XPath("//*[@id=\"react-root\"]/section/main/section/div/div[1]/div/article")).ToArray();
                   int j=1;
                   foreach(IWebElement article in articles)
                   {
                       likespan = browser.FindElement(By.XPath("//*[@id=\"react-root\"]/section/main/section/div[1]/div[1]/div/article[" + j.ToString() + "]/div[2]/section[1]/span[1]/button/span"));
                       hight += article.Size.Height + 60;
                       if (likespan.GetAttribute("aria-label").ToLower() == likeText.ToLower())
                       {
                           browser.FindElement(By.XPath("//*[@id=\"react-root\"]/section/main/section/div[1]/div[1]/div/article[" + j.ToString() + "]/div[2]/section[1]/span[1]/button")).Click();
                           i++;
                           break;
                       }
                       j++;
                   }
                   j = 1;

                   js.ExecuteScript("window.(0,"+hight+");");
                   hight=0;
                   if (i == count + 1 && i != 0) break;
               }
               catch 
               {

               }   

           }
       }

       public void Likeprofile(string profileName, int count = 0)
       {
           browser.Navigate().GoToUrl(URL +profileName +"/");
           delay(2);
           popUpContentLike(count,false,true);

       }

       public void LikeHashtag(string hashtagName,int count=0)
       {
            browser.Navigate().GoToUrl(URL + "explore/tags/"+hashtagName+"/");
            delay(2);
            popUpContentLike(count, false, true);
        }

       public void LikeExplore(int count)
       {
            browser.Navigate().GoToUrl(URL + "explore/");
            delay(2);
            popUpContentLike(count, false, true);
        }

       //////////////////////////////////////////////////////////

       ///////////////////   Follow   ///////////////////

        public void FollowProfileLastContentLike(string profileName, int count = 0)
        {
            browser.Navigate().GoToUrl(URL + profileName + "/");
            delay(2);
            browser.FindElement(By.XPath("//a[contains(@href,'/p/')]/parent::div")).Click();
            //popUpContentLike(count, false, true);
            delay(2);
            browser.FindElement(By.XPath("/html/body/div[3]/div/div[2]/div/article/div[2]/section[2]/div/a")).Click();
            delay(2);
            try
            {
                int i = 0;
                while (true)
                {
                    i++;
                    browser.FindElement(By.XPath("/html/body/div[3]/div/div[2]/div/article/div[2]/div[2]/ul/div/li["+i+"]/div/div[2]")).Click();
                    delay(2);
                    if (i == count && count != 0) break;
                    if (i % 5 == 0)
                    {
                        js.ExecuteScript("document.getElementsByClassName(\"wwxN2\")[0].scrollBy(0,document.getElementsByClassName(\"wwxN2\")[0].clientHeight+100)");
                        delay(2);
                    }
                }
            }
            catch
            {

            }
        }

        public void FollowProfileLastContentComment(string profileName, int count = 0)
        {
            browser.Navigate().GoToUrl(URL + profileName + "/");
            delay(2);
            try
            {
                browser.FindElement(By.XPath("//a[contains(@href,'/p/')]/parent::div")).Click();
                delay(2);
                while (true)
                    if (browser.FindElements(By.XPath("//li[@class='lnrre']")).Count != 0)
                    {
                        browser.FindElement(By.XPath("//li[@class='lnrre']/child::button")).Click();
                        delay(1);
                        if (browser.FindElements(By.XPath("/html/body/div[3]/div/div[2]/div/article/div[2]/div[1]/ul/li")).Count >= count && count != 0)
                            break;
                    }
                    else
                    {
                        break;
                    }
                List<string> AccountNames=new List<string>();

                foreach (IWebElement element in browser.FindElements(By.XPath("/html/body/div[3]/div/div[2]/div/article/div[2]/div[1]/ul/li/div/div/div/a")))
                {
                    AccountNames.Add(element.Text.ToLower().Trim());
                }
                
                foreach (string AccountName in AccountNames)
                {
                    browser.Navigate().GoToUrl(URL + AccountName + "/");
                    delay(2);

                    if (browser.FindElements(By.XPath("//button[text() = '"+followText+"']")).Count != 0)
                    {
                        browser.FindElement(By.XPath("//button[text() = '" + followText + "']")).Click();
                    }
                    delay(1);
                    
                }
            }
            catch 
            {
                
                
            }
           
        }

        public void FollowLocation(string location, int count = 0)
        {
            IWebElement searchbox;

            browser.Navigate().GoToUrl(URL);
            delay(2);
            searchbox = browser.FindElement(By.XPath("//*[@id=\"react-root\"]/section/nav/div[2]/div/div/div[2]/input"));
            searchbox.SendKeys(location);
            delay(2);
            IWebElement location1 = browser.FindElement(By.XPath("//*[@id=\"react-root\"]/section/nav/div[2]/div/div/div[2]/div[2]/div[2]/div/a[contains(@href,'explore/locations/')]"));
            location1.Click();
            delay(3);
            popUpContentLike(count,true,false);
        }


        public void FollowHashtag(string hashtagName, int count = 0)
        {
            browser.Navigate().GoToUrl(URL + "explore/tags/" + hashtagName + "/");
            delay(2);
            popUpContentLike(count, true, false);
        }

        public void FollowExplore(int count = 0)
        {
            browser.Navigate().GoToUrl(URL + "explore/");
            delay(2);
            popUpContentLike(count, true, false);
        }

        public void Unfollow(int count = 0)
        {
            browser.Navigate().GoToUrl(URL + UserName);
            delay(2);
            browser.FindElement(By.XPath("//*[@id=\"react-root\"]/section/main/div/header/section/ul/li[3]/a")).Click();
            delay(2);
            try
            {
                int i = 0;
                while (true)
                {
                    i++;
                    browser.FindElement(By.XPath("/html/body/div[3]/div/div[2]/div/div[2]/ul/div/li[" + i + "]/div/div[2]")).Click();
                    delay(2);
                    browser.FindElement(By.XPath("/html/body/div[4]/div/div/div/div[3]/button[1]")).Click();
                    delay(2);
                    if (i == count && count != 0) break;
                    if (i % 5 == 0)
                    {
                        js.ExecuteScript("document.getElementsByClassName(\"j6cq2\")[0].scrollBy(0,document.getElementsByClassName(\"j6cq2\")[0].clientHeight+100)");
                        delay(2);
                    }
                }
            }
            catch
            {
                
            }
        }

        /////////////////////////////////////////////////////////////////////////

        ///////////////////  Comment  ///////////////////

        public void CommentHomePage(string[] comment, int count = 0)
        {
            browser.Navigate().GoToUrl(URL);

            commentWrite("deneme");
        }

        public void CommentHashtag(string hashtagName,string[] comment, int count = 0)
        {
            browser.Navigate().GoToUrl(URL + "explore/tags/" + hashtagName + "/");
            delay(2);
            popUpCommnet(comment, count);
        }

        public void CommentLocation(string location, string[] comment, int count = 0)
        {
            IWebElement searchbox;

            browser.Navigate().GoToUrl(URL);
            delay(2);
            searchbox = browser.FindElement(By.XPath("//*[@id=\"react-root\"]/section/nav/div[2]/div/div/div[2]/input"));
            searchbox.SendKeys(location);
            delay(2);
            IWebElement location1 = browser.FindElement(By.XPath("//*[@id=\"react-root\"]/section/nav/div[2]/div/div/div[2]/div[2]/div[2]/div/a[contains(@href,'explore/locations/')]"));
            location1.Click();
            delay(3);
            popUpCommnet(comment, count);
        }

        public void CommentExplore(string[] comment, int count = 0)
        {
            browser.Navigate().GoToUrl(URL + "explore/");
            delay(2);
            popUpCommnet(comment, count);
        }

        public void CommentProfile(string profileName,string[] comment, int count = 0)
        {
            browser.Navigate().GoToUrl(URL + profileName + "/");
            delay(2);
            popUpCommnet(comment, count);
        }


        //////////////////////////////////////////////////////////

        /////////////////// Necessary Fuctions ///////////////////

        private void popUpContentLike(int count, bool isFollow = false, bool isLike = false)
        {
            int i = 1;
            browser.FindElement(By.XPath("//a[contains(@href,'/p/')]/parent::div")).Click();
            try
            {

                while (true)
                {
                    delay(2);
                    if (isLike)
                    {
                        if (browser.FindElement(By.XPath("/html/body/div[3]/div/div[2]/div/article/div[2]/section[1]/span[1]/button/span")).GetAttribute("aria-label").ToLower() == likeText.ToLower())
                        {
                            browser.FindElement(By.ClassName("coreSpriteHeartOpen")).Click();
                        }
                    }
                    if (isFollow)
                    {
                        if (browser.FindElement(By.XPath("/html/body/div[3]/div/div[2]/div/article/header/div[2]/div[1]/div[2]/button")).Text.ToLower() == followText.ToLower())
                        {
                            browser.FindElement(By.XPath("/html/body/div[3]/div/div[2]/div/article/header/div[2]/div[1]/div[2]/button")).Click();
                        }
                    }
                    delay(1);
                    i++;
                    browser.FindElements(By.XPath("/html/body/div[3]/div/div[1]/div/div/a")).Last().Click();
                    if (i == count + 1 && i != 0) break;

                }
            }
            catch { }
        }

        private void delay(int second)
        {
            System.Threading.Thread.Sleep(second * 1000);
        }

        public void closeChrome()
        {
            browser.Quit();
        }

        private void commentWrite(string Text,int homePage = 1)
        {
            browser.FindElement(By.XPath("//article["+homePage.ToString()+"]/div[2]/section[1]/span[2]/button/span[@aria-label='" + commentButton + "']")).Click();
            IWebElement textarea = browser.FindElement(By.XPath("//article[" + homePage.ToString() + "]/div[2]/section[3]/div/form/textarea[@aria-label='" + commentText + "']"));
            textarea.SendKeys(Text);
            //textarea.SendKeys(Keys.Enter);
        }

        private void popUpCommnet(string[] commentlist,int count)
        {

            int i = 1;
            browser.FindElement(By.XPath("//a[contains(@href,'/p/')]/parent::div")).Click();
            try
            {

                while (true)
                {
                    delay(2);
                    commentWrite(commentlist[new Random().Next(0,commentlist.Length-1)]);
                    delay(1);
                    i++;
                    browser.FindElements(By.XPath("/html/body/div[3]/div/div[1]/div/div/a")).Last().Click();
                    if (i == count + 1 && i != 0) break;

                }
            }
            catch { }
        }


        //////////////////////////////////////////////////////////
    }
}
