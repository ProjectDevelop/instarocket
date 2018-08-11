﻿using System;
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
       private string likeText = "beğen";
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

       public void login()
       {
           browser.Navigate().GoToUrl(URL + "accounts/login/");
           delay(1);
           browser.FindElementByName("username").SendKeys(UserName);
           browser.FindElementByName("password").SendKeys(Password);
           browser.FindElementByTagName("button").Click();
           delay(2);
       }

       public void logout()
       {
           browser.Navigate().GoToUrl(URL + "accounts/logout/");
           delay(1);
       }

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

       public void likeHomePage(int count=0)
       {
           browser.Navigate().GoToUrl(URL);
           int articleCounter = 0;
           IWebElement likebutton,likespan;
           int i = 1;
           while(true)
           {
               delay(3);
               if (articleCounter < 8) articleCounter++;
               int height;
               try
               {
                   likebutton = browser.FindElement(By.XPath("//*[@id=\"react-root\"]/section/main/section/div[1]/div[1]/div/article[" + articleCounter.ToString() + "]/div[2]/section[1]/span[1]/button"));
                   likespan = browser.FindElement(By.XPath("//*[@id=\"react-root\"]/section/main/section/div[1]/div[1]/div/article[" + articleCounter.ToString() + "]/div[2]/section[1]/span[1]/button/span"));
                   height = browser.FindElement(By.XPath("//*[@id=\"react-root\"]/section/main/section/div[1]/div[1]/div/article[" + articleCounter.ToString() + "]")).Size.Height + 200;
               }
               catch 
               {
                   articleCounter--;
                   likebutton = browser.FindElement(By.XPath("//*[@id=\"react-root\"]/section/main/section/div[1]/div[1]/div/article[" + articleCounter.ToString() + "]/div[2]/section[1]/span[1]/button"));
                   likespan = browser.FindElement(By.XPath("//*[@id=\"react-root\"]/section/main/section/div[1]/div[1]/div/article[" + articleCounter.ToString() + "]/div[2]/section[1]/span[1]/button/span"));
                   height = browser.FindElement(By.XPath("//*[@id=\"react-root\"]/section/main/section/div[1]/div[1]/div/article[" + articleCounter.ToString() + "]")).Size.Height + 200;
               }

               if (likespan.GetAttribute("aria-label").ToLower() == likeText)
               {
                   likebutton.Click();
                   i++;
               }
               if (i == count+1 && i !=0 ) break;
               
               js.ExecuteScript("window.scrollBy(0," + height.ToString()+ ")");

           }
       }

       public void likeprofile(string profileName, int count = 0)
       {
           browser.Navigate().GoToUrl(URL +profileName +"/");
           delay(2);
           popUpContentLike(count);

       }

       public void likeHashtag(string hashtagName,int count=0)
       {
            browser.Navigate().GoToUrl(URL + "explore/tags/"+hashtagName+"/");
            delay(2);
            popUpContentLike(count);
        }

       public void likeExplore(int count)
       {
            browser.Navigate().GoToUrl(URL + "explore/");
            delay(2);
            popUpContentLike(count);
        }

       private void delay(int second)
       {
           System.Threading.Thread.Sleep(second * 1000);
       }

       private void popUpContentLike(int count)
       {
           int i = 1;
           browser.FindElement(By.XPath("//a[contains(@href,'/p/')]/parent::div")).Click();
           try
           {
               
               while (true)
               {
                   delay(2);
                   if (browser.FindElement(By.XPath("/html/body/div[3]/div/div[2]/div/article/div[2]/section[1]/span[1]/button/span")).GetAttribute("aria-label").ToLower() == likeText)
                   {
                       browser.FindElement(By.ClassName("coreSpriteHeartOpen")).Click();
                       delay(1);
                       i++;
                   }
                   browser.FindElements(By.XPath("/html/body/div[3]/div/div[1]/div/div/a")).Last().Click();
                   if (i == count+1 && i !=0 ) break;
                   
               }
           }
           catch { }
       }

       public void CloseChrome()
       {
           browser.Quit();
       }

        public void FollowProfileLastContentLike(string profileName, int count = 0)
        {
            throw new NotImplementedException();
        }

        public void FollowProfileLastContentComment(string profileName, int count = 0)
        {
            throw new NotImplementedException();
        }

        public void FollowHashtag(string hastagName, int count = 0)
        {
            throw new NotImplementedException();
        }

        public void FollowExplore(int count = 0)
        {
            throw new NotImplementedException();
        }

        public void Unfollow(int count = 0)
        {
            browser.Navigate().GoToUrl(URL + UserName);
            delay(2);
            browser.FindElement(By.XPath("//*[@id=\"react-root\"]/section/main/div/header/section/ul/li[3]/a")).Click();
            delay(2);
            js.ExecuteScript("document.getElementById(\"j6cq2\").scrollTop = 200;");
            delay(2);
            int i = 0;
            while (true)
            {
                i++;
                browser.FindElement(By.XPath("/html/body/div[3]/div/div[2]/div/div[2]/ul/div/li["+i+"]/div/div[2]")).Click();
                delay(2);
                browser.FindElement(By.XPath("/html/body/div[4]/div/div/div/div[3]/button[1]")).Click();
                delay(2);
                if (i == count && count != 0) break;
            }
        }
    }
}
