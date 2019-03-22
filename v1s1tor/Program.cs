using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium.Chrome;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;
using System.IO;

namespace v1s1tor
{
    class Program
    {//TODO: new thread that handles when there is a new day and switching the unfollowing and following 
        //
        static ChromeOptions options = new ChromeOptions();
        static string username = "godlyrigs";
        static string username2 = "zen.obi";
        static string targ = "pcgaming";
        static string targ2 = "porsche";
        static string threadnum = "1";
        static int fnum = 0;
        static bool unrestricted = false;
        static bool followday = false;

        #region log
        static string password = "Anime1243";
        static string password2= "anime1234";
        #endregion
        static void Main(string[] args)
        {
            #region Intro
            Console.WriteLine("       o         o");
            Console.WriteLine("        \\       /");
            Console.WriteLine("         \\     /");
             Console.WriteLine("        :-'--'-:");
            Console.WriteLine("      .-'  ____  `-.");
            Console.WriteLine("     ( (  (_()_)  ) )");
            Console.WriteLine("      `-.   ^^   .-'");
            Console.WriteLine("        `._ ==_.'");
            Console.WriteLine("          __)(___");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n   Welcome to v1s1tor bot");
            Console.WriteLine("\n Press any key to continue.");
            Console.ForegroundColor = ConsoleColor.White;
            #endregion
            Menu();
            
            
        }

        

        static void Menu()
        {
            Console.ReadLine();
            Console.Clear();
            
            Console.WriteLine("Menu");
            Console.WriteLine("1> Start Bot");
            Console.WriteLine("2> Set Threads");
            Console.WriteLine("3> Check Daily Stats");
            Console.WriteLine("4> Unrestricted Follow Bot");
            Console.WriteLine("5> Unrestricted Unfollow Bot");
            
            #region Calendar

            Console.WriteLine(" ");
                Console.WriteLine("Today is " + DateTime.Now.DayOfWeek.ToString() + ".");
                Console.WriteLine(" ");
                if (File.Exists("config.txt"))
                {
                    string fdays = File.ReadAllLines("config.txt")[0].Split(':')[1];
                    Console.WriteLine("Your following days consist of: " + fdays + ".");
                    Console.WriteLine(" ");
                    string udays = File.ReadAllLines("config.txt")[1].Split(':')[1];
                    Console.WriteLine("Your unfollowing days consist of: " + udays + ".");
                   // Console.ReadLine();
                   // Console.WriteLine(fdays.Split(',')[1]);
                    foreach (string s in fdays.Split(','))
                        {
                        
                            if(s == DateTime.Now.DayOfWeek.ToString())
                        {
                            followday = true;
                        }
                        }
                    Console.WriteLine(" ");
                    if (followday == true)
                        Console.WriteLine("Today is a follow day.");

                    if (followday == false)
                        Console.WriteLine("Today is an unfollow day.");
                    Console.WriteLine(" ");
                    
                }

                if (!File.Exists("config.txt"))
                {
                    Console.WriteLine("It seems you have not used the calendar before.");
                    Console.WriteLine(" ");
                    Console.WriteLine("I will set up your schedule for you, it can be changed any time in config.txt");
                    Console.WriteLine(" ");
                    Thread.Sleep(3000);
                    Console.WriteLine("Your follow days will be Monday Tuesday Wednesday Thursday and Sunday");
                    Console.WriteLine("Your unfollow days will be Friday and Saturday");
                    //File.Create("config.txt");
                    Thread.Sleep(1000);
                    File.WriteAllText("config.txt","F:Monday,Tuesday,Wednesday,Thursday,Sunday\nU:Friday,Saturday");
                    
                }


            #endregion

            string input = Console.ReadLine();
            Thread thread = new Thread(BsThread);
            #region StartBot
            if (input == "1")
            {
                if (followday == true)
                {
                    if (threadnum == "2")
                    {
                        thread = new Thread(new ParameterizedThreadStart(Follow));
                        thread.Start(targ2);
                    }
                    thread = new Thread(new ParameterizedThreadStart(Follow));
                    thread.Start(targ);
                }
                if(followday == false)
                {
                    if (threadnum == "2")
                    {
                        thread = new Thread(new ParameterizedThreadStart(UnFollow));
                        thread.Start(targ2);
                    }
                    thread = new Thread(new ParameterizedThreadStart(UnFollow));
                    thread.Start(targ);
                }
            }
            if(input == "2")
            {
                
                Console.Clear();
                Console.WriteLine("How many threads?");
                threadnum = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Threads changed to " + threadnum);
                Thread.Sleep(3000);
                Menu();
            }
            #endregion
        }

        #region LoginVoid
        static IWebDriver Login(string username, string password)
        {
            
            IWebDriver driver = new ChromeDriver(@"C:\Users\poseidon\Downloads", options);
            driver.Navigate().GoToUrl("https://www.instagram.com/accounts/login/");
            WaitForPageLoad(driver);
            Thread.Sleep(2000);
            driver.FindElement(By.ClassName("_2hvTZ")).SendKeys(username);
            int i = 0;
            foreach (IWebElement e in driver.FindElements(By.ClassName("_2hvTZ")))
            {
                i++;
                if (i == 2)
                    e.SendKeys(password);
            }
            Thread.Sleep(1000);
            foreach (IWebElement e in driver.FindElements(By.ClassName("_4EzTm")))
            {
                i++;
                if (e.Text == "Log in")
                {
                    e.Click();
                    break;
                }
            }
            Thread.Sleep(1000);
            WaitForPageLoad(driver);
            Console.WriteLine("\nLogged in, awaiting phone auth. Once authed, press any key to continue.");
            string s = Console.ReadLine();
            Console.WriteLine("\nLogged in to " + username);
            return driver;
        }
        #endregion
        #region FollowVoid
        static void Follow(object user)
        {
            string targ1 =(string) user;
            string us = "null";
            string pa = "null";
            if (targ1 == targ2)
            {
                us = username2;
                pa = password2;
            }
            if (targ1 == targ)
            {
                us = username;
                pa = password;
            }
            IWebDriver driver = Login(us,pa);
            driver.Navigate().GoToUrl("https://www.instagram.com/" + targ1 + "/");
            Thread.Sleep(1000);
            WaitForPageLoad(driver);

            //Y8-fY 
            int i = 0;
            foreach (IWebElement e in driver.FindElements(By.ClassName("Y8-fY")))
            {
                i++;
                if (i == 2)
                    e.Click();

            }
            Thread.Sleep(6000);
            i = 0;

            driver.Manage().Window.Size = new System.Drawing.Size(333, 969);
            while (true)
            {
                try
                {
                    Actions builder = new Actions(driver);

                    foreach (IWebElement e in driver.FindElements(By.XPath("//*[contains(@class, 'wo9IH')]")))
                    {    //HoLwm
                        if (e.FindElement(By.ClassName("sqdOP")).Text == "Follow")
                        {
                            Thread.Sleep(30000);
                            Console.WriteLine("Followed " + e.FindElement(By.ClassName("FPmhX")).Text + ".");
                            fnum++;
                            Console.WriteLine(fnum);
                            if (!File.Exists("following.txt"))
                                File.Create("following.txt").Close();
                            File.WriteAllText("following.txt",File.ReadAllText("following.txt") + "\n" + e.FindElement(By.ClassName("FPmhX")).Text + "|" + DateTime.Now);

                            builder.Click(e.FindElement(By.ClassName("sqdOP"))).Perform();
                            

                        }
                        Thread.Sleep(500);

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }


        }
        #endregion
        #region UnFollowVoid
        static void UnFollow(object user)
        {
            string targ1 = (string)user;
            string us = "null";
            string pa = "null";
            if (targ1 == targ2)
            {
                us = username2;
                pa = password2;
            }
            if (targ1 == targ)
            {
                us = username;
                pa = password;
            }
            IWebDriver driver = Login(us, pa);
            driver.Navigate().GoToUrl("https://www.instagram.com/" + us + "/");
            Thread.Sleep(1000);
            WaitForPageLoad(driver);

            //Y8-fY 
            int i = 0;
            foreach (IWebElement e in driver.FindElements(By.ClassName("Y8-fY")))
            {
                i++;
                if (i == 3)
                    e.Click();

            }
            Thread.Sleep(6000);
            i = 0;
            driver.Manage().Window.Size = new System.Drawing.Size(333, 969);
            while (true)
            {
                try
                {
                    Actions builder = new Actions(driver);
                    bool flag = false;
                    foreach (IWebElement e in driver.FindElements(By.XPath("//*[contains(@class, 'wo9IH')]")))
                    {    //HoLwm
                        int i1 = 0;
                        foreach (string s in File.ReadAllText("following.txt").Split(
    new[] { "\n" },
    StringSplitOptions.None))
                        {
                            
                            DateTime myDate = DateTime.Parse(s.Split(new[] { "\n" },StringSplitOptions.None)[0].Split('|')[1]);
                            
                            Console.WriteLine(myDate);
                            int result = DateTime.Compare(myDate, DateTime.Now.AddDays(-2));
                            Console.WriteLine(result);
                            if (result ==-1 && s.Split('|')[0] == e.FindElement(By.ClassName("FPmhX")).Text)
                            {
                                flag = true;
                                break;
                            }
                            
                        }
                        if (flag == true)
                            if (e.FindElement(By.ClassName("sqdOP")).Text == "Following")
                            {
                                Thread.Sleep(10000);
                                Console.WriteLine("UnFollowed " + e.FindElement(By.ClassName("FPmhX")).Text + ".");
                                (e.FindElement(By.ClassName("sqdOP"))).Click();

                                Thread.Sleep(4500);

                                driver.FindElement(By.ClassName("aOOlW")).Click();
                                Thread.Sleep(2000);
                                

                                string tempFile = Path.GetTempFileName();

                                using (var sr = new StreamReader("following.txt"))
                                using (var sw = new StreamWriter(tempFile))
                                {
                                    string line;

                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        if (!line.Contains(e.FindElement(By.ClassName("FPmhX")).Text))
                                            sw.Write("\n"+line);
                                    }
                                }

                                File.Delete("following.txt");
                                File.Move(tempFile, "following.txt");



                            }

                        if (flag != true)
                        {
                            Console.WriteLine("Unable to unfollow " + e.FindElement(By.ClassName("FPmhX")).Text + "Due to following them less than two days ago.");
                            flag = false;
                        }
                        i1++;
                        Thread.Sleep(500);

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }



        }
        #endregion
        #region Handlers

        static void WaitForPageLoad(IWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            //This loop will rotate for 100 times to check If page Is ready after every 1 second.
            //You can replace your if you wants to Increase or decrease wait time.
            int waittime;
            waittime = 60;
            for (int i = 0; i < waittime; i++)
            {
                try
                {
                    Thread.Sleep(1000);
                }
                catch (ThreadInterruptedException e) { }
                //To check page ready state.
                if (js.ExecuteScript("return document.readyState").ToString().Equals("complete"))
                {
                    //System.out.println("Wait for Page Load : "+js.executeScript("return document.readyState").toString());
                    break;
                }
            }
            Console.Write("\nWeb-Page Loaded.");
        }
        static void BsThread() { }
        #endregion
    }
    }

