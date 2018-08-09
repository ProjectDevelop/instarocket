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
       
       private string User;
       private string Password;
       private string URL = "https://www.instagram.com/";
       private string likeText = "beğen";
       private ChromeDriver browser;
       private IJavaScriptExecutor js;
       

       public instagram(string User,string Password)
       {
           this.User = User;
           this.Password = Password;
           
           ////////////browser and options////////////////
           ChromeDriverService service = ChromeDriverService.CreateDefaultService();
           service.HideCommandPromptWindow = true;
           ChromeOptions options = new ChromeOptions();
           /* options.AddArgument("--headless"); */ //hide chrome
           browser = new ChromeDriver(service,options);
           js = (IJavaScriptExecutor)browser;
           /////////////////////

           
       }

       public void login()
       {
           browser.Navigate().GoToUrl(URL + "accounts/login/");
           delay(1);
           browser.FindElementByName("username").SendKeys(User);
           browser.FindElementByName("password").SendKeys(Password);
           browser.FindElementByTagName("button").Click();
           delay(2);
       }

       public void logout()
       {
           browser.Navigate().GoToUrl(URL + "accounts/logout/");
           delay(1);
       }

       public void likeHomePage(int count)
       {
           browser.Navigate().GoToUrl(URL);
           int articleCounter = 0;
           IWebElement likebutton,likespan;
           
           for (int i = 0; i < count; i++)
           {
               delay(4);
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
               likebutton.Click();
               js.ExecuteScript("window.scrollBy(0," + height.ToString()+ ")");

           }
       }

       public void likeprofile(string profileName)
       {
           browser.Navigate().GoToUrl(URL +profileName +"/");
           delay(3);
           int i = 1;
            browser.FindElement(By.XPath("//*[@id=\"react-root\"]/section/main/div/div[3]/article/div[1]/div/div[1]/div[1]")).Click();
           try
           {
               while(true)
               {
                   delay(3);
                   if (browser.FindElement(By.XPath("/html/body/div[3]/div/div[2]/div/article/div[2]/section[1]/span[1]/button/span")).GetAttribute("aria-label").ToLower() == likeText)
                   {
                       browser.FindElement(By.ClassName("coreSpriteHeartOpen")).Click();
                       delay(2);
                   }
                   browser.FindElements(By.XPath("/html/body/div[3]/div/div[1]/div/div/a")).Last().Click();
       
               }
               delay(3);
           }
           catch
           {

           }
       }

       public void likeHashtag(string hashtagName)
       {
           throw new NotImplementedException();
       }

       public void likeExplore(int count)
       {
           throw new NotImplementedException();
       }

       private void delay(int second)
       {
           System.Threading.Thread.Sleep(second * 1000);
       }
       public void CloseChrome()
       {
           browser.Quit();
       }
    }
}
