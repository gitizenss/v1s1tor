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


        static string threadnum = "1";
        static int fnum = 0;
        static int bt = 0;
        static bool unrestricted = false;
        static bool followday = false;
        static string fdays;
        static string udays;

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
            string target = "null";
            string tosend = "null";
            Console.WriteLine("Menu");
            Console.WriteLine("1> Start Bot");
            Console.WriteLine("2> Set Threads");
            Console.WriteLine("3> Check Daily Stats (WIP)");
            Console.WriteLine("4> Unrestricted Follow Bot");
            Console.WriteLine("5> Unrestricted Unfollow Bot");
            
            #region Calendar

            Console.WriteLine(" ");
                Console.WriteLine("Today is " + DateTime.Now.DayOfWeek.ToString() + ".");
                Console.WriteLine(" ");
                if (File.Exists("config.txt"))
                {
                     fdays = File.ReadAllLines("config.txt")[0].Split(':')[1];
                    Console.WriteLine("Your following days consist of: " + fdays + ".");
                    Console.WriteLine(" ");
                     udays = File.ReadAllLines("config.txt")[1].Split(':')[1];
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
            if (input == "2")
            {
                Console.Clear();
                Console.WriteLine("How many threads?");
                threadnum = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Threads changed to " + threadnum + ".");
                Thread.Sleep(3000);
                Menu();
            }
            Thread thread = new Thread(BsThread);
            if (input == "1" || input == "4" || input == "5")
            {
                for (int i = Convert.ToInt32(threadnum); i > 0; i--)
                {
                    Console.Clear();
                    Console.WriteLine("This will repeat depending on how many threads are present.");
                    Console.ForegroundColor =ConsoleColor.Red;
                    Console.WriteLine("");
                    Console.WriteLine("Who is your target?");
                    Console.ForegroundColor = ConsoleColor.White;
                    target = Console.ReadLine();
                    
                    Console.WriteLine("Input your login username.");
                    string username = Console.ReadLine();
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Will log into " + username);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Input your login password.");
                    password = Console.ReadLine();


                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Logging in...");
                    Console.ForegroundColor = ConsoleColor.White;

                    tosend = target + ":" + username + ":" + password;
                    #region StartBot
                    if (input == "1")
                    {
                        unrestricted = false;
                        if (followday == true)
                        {
                            thread = new Thread(new ParameterizedThreadStart(Follow));
                            thread.Start(tosend);
                        }
                        if (followday == false)
                        {
                            thread = new Thread(new ParameterizedThreadStart(UnFollow));
                            thread.Start(tosend);
                        }
                    }

                    if (input == "4")
                    {
                        unrestricted = true;
                        thread = new Thread(new ParameterizedThreadStart(Follow));
                        thread.Start(tosend);
                    }
                    if (input == "5")
                    {
                        unrestricted = true;
                        thread = new Thread(new ParameterizedThreadStart(UnFollow));
                        thread.Start(tosend);
                    }
                    #endregion
                }
            }
        }

        #region LoginVoid
        static IWebDriver Login(string username, string password)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.SuppressInitialDiagnosticInformation = true;
            IWebDriver driver = new ChromeDriver(service,options);
            driver.Navigate().GoToUrl("https://www.instagram.com/accounts/login/");
            WaitForPageLoad(driver);
            Thread.Sleep(5000);
            driver.FindElement(By.ClassName("_2hvTZ")).SendKeys(username);
            int i = 0;
            foreach (IWebElement e in driver.FindElements(By.ClassName("_2hvTZ")))
            {
                i++;
                if (i == 2)
                    e.SendKeys(password);
            }
            Thread.Sleep(4000);
            i = 0;
            foreach (IWebElement e in driver.FindElements(By.ClassName("_4EzTm")))
            {
                i++;
                if (e.Text == "Log in")
                {
                    e.Click();
                    break;
                }
            }
            Thread.Sleep(2000);
            WaitForPageLoad(driver);
            
            Console.WriteLine("\nLogged in to " + username);
            return driver;
        }
        #endregion
        #region FollowVoid
        static void Follow(object user)
        {
            string sent = (string) user;
            string target = sent.Split(':')[0];
            string us = sent.Split(':')[1];
            string pa = sent.Split(':')[2];
            IWebDriver driver = Login(us,pa);
            driver.Navigate().GoToUrl("https://www.instagram.com/" + target + "/");
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
                    int eo = 0;
                    foreach (IWebElement e in driver.FindElements(By.XPath("//*[contains(@class, 'wo9IH')]")))
                    {
                        fdays = File.ReadAllLines("config.txt")[0].Split(':')[1];
                        udays = File.ReadAllLines("config.txt")[1].Split(':')[1];
                        bool curfollowday = followday;
                        eo = 1;
                        if (unrestricted == false)
                        {
                            
                            foreach (string s in fdays.Split(','))
                            {

                                if (s == DateTime.Now.DayOfWeek.ToString())
                                {
                                    followday = true;
                                }
                            }
                            foreach (string s in udays.Split(','))
                            {

                                if (s == DateTime.Now.DayOfWeek.ToString())
                                {
                                    followday = false;
                                }
                            }
                            if (curfollowday != followday)
                            {
                                driver.Dispose();
                                Thread thread = new Thread(new ParameterizedThreadStart(UnFollow));
                                thread.Start(user);
                                Thread.CurrentThread.Abort();
                            }
                        }
                        
                        //HoLwm
                        if (e.FindElement(By.ClassName("sqdOP")).Text == "Follow")
                        {
                            Thread.Sleep(30000);
                            Console.WriteLine("Followed " + e.FindElement(By.ClassName("FPmhX")).Text + ".");
                            fnum++;
                            Console.WriteLine(fnum);
                            if (!File.Exists("following.txt"))
                                File.Create("following.txt").Close();
                            File.WriteAllText("following.txt",File.ReadAllText("following.txt") + "\n" + e.FindElement(By.ClassName("FPmhX")).Text + "|" + DateTime.Now);

                            e.FindElement(By.ClassName("sqdOP")).Click();
                            if (Environment.UserName == "qianlong")
                            {
                                File.WriteAllText(@"C:\Users\qianlong\Documents\Rainmeter\Skins\LuaTextFile\Test.txt", "FOLLOWED");
                                File.WriteAllText(@"C:\Users\qianlong\Documents\Rainmeter\Skins\lt2\Test.txt", Convert.ToString(fnum));
                            }
                            bt = 0;

                        }
                        Thread.Sleep(500);

                    }
                    if (eo == 0)
                    {
                        Console.WriteLine("Users not loading, will retry in 30 seconds.");
                        Thread.Sleep(30000);
                        driver.Dispose();
                        UnFollow(user);
                    }
                }
                catch (Exception ex)
                {
                    bt++;
                    Console.WriteLine(ex.ToString());
                    if (bt > 5)
                    {

                        driver.Dispose();
                        Follow(user);
                    }
                }

            }


        }
        #endregion
        #region UnFollowVoid
        static void UnFollow(object user)
        {
            fnum = 0;
            string sent = (string)user;
            string us = sent.Split(':')[1];
            string pa = sent.Split(':')[2];
            
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
                    int eo = 0;
                    foreach (IWebElement e in driver.FindElements(By.XPath("//*[contains(@class, 'wo9IH')]")))
                    {
                        fdays = File.ReadAllLines("config.txt")[0].Split(':')[1];
                        udays = File.ReadAllLines("config.txt")[1].Split(':')[1];
                        eo = 1;
                        bool curfollowday = followday;
                        if (unrestricted == false)
                        {
                            foreach (string s in fdays.Split(','))
                            {

                                if (s == DateTime.Now.DayOfWeek.ToString())
                                {
                                    followday = true;
                                }
                            }
                            foreach (string s in udays.Split(','))
                            {

                                if (s == DateTime.Now.DayOfWeek.ToString())
                                {
                                    followday = false;
                                }
                            }
                            if (curfollowday != followday)
                            {
                                driver.Dispose();
                                Thread thread = new Thread(new ParameterizedThreadStart(Follow));
                                thread.Start(user);
                                Thread.CurrentThread.Abort();
                            }
                        }
                        //HoLwm
                        int i1 = 0;
                        if(unrestricted == true)
                        {
                            flag = true;
                        }
                        else
                        foreach (string s in File.ReadAllText("following.txt").Split(
    new[] { "\n" },
    StringSplitOptions.None))
                        {
                            if (s.Length > 1)
                            {


                                DateTime myDate = DateTime.Parse(s.Split(new[] { "\n" }, StringSplitOptions.None)[0].Split('|')[1]);

                                Console.WriteLine(myDate);
                                int result = DateTime.Compare(myDate, DateTime.Now.AddDays(-2));
                                Console.WriteLine(result);
                                if (result == -1 && s.Split('|')[0] == e.FindElement(By.ClassName("FPmhX")).Text)
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            
                        }
                        if (flag != true)
                        {
                            Console.WriteLine("Unable to unfollow " + e.FindElement(By.ClassName("FPmhX")).Text + "Due to following them less than two days ago.");
                            flag = false; bt = 0;
                        }
                        if (flag == true)
                            if (e.FindElement(By.ClassName("sqdOP")).Text == "Following")
                            {
                                Thread.Sleep(10000);
                                
                                
                                
                                (e.FindElement(By.ClassName("sqdOP"))).Click();

                                Thread.Sleep(2000);

                                driver.FindElement(By.ClassName("aOOlW")).Click();
                                Thread.Sleep(2000);

                                
                                    string tempFile = Path.GetTempFileName();
                                Console.WriteLine("UnFollowed " + e.FindElement(By.ClassName("FPmhX")).Text + ".");
                                int x1 = 0;
                                bool flag2 = false;

                                foreach(string s in File.ReadAllLines("following.txt"))
                                {
                                    if (s.Contains(e.FindElement(By.ClassName("FPmhX")).Text))
                                    {
                                        string[] follows = File.ReadAllLines("following.txt");
                                        follows[x1] = "";
                                        File.WriteAllLines("following.txt", follows);
                                        break;
                                    }
                                    x1++;
                                }
                                fnum++;
                                Console.WriteLine(fnum);
                                File.Delete("following.txt");
                                File.Move(tempFile, "following.txt");
                                if (Environment.UserName == "qianlong")
                                {
                                    File.WriteAllText(@"C:\Users\qianlong\Documents\Rainmeter\Skins\LuaTextFile\Test.txt", "UNFOLLOWED");
                                    File.WriteAllText(@"C:\Users\qianlong\Documents\Rainmeter\Skins\lt2\Test.txt", Convert.ToString(fnum));
                                }
                                bt = 0;

                                flag = false;
                                
                            }

                        
                        i1++;
                        Thread.Sleep(500);

                    }
                    if(eo == 0)
                    {
                        Console.WriteLine("Users not loading, will retry in 30 seconds.");
                        Thread.Sleep(30000);
                        driver.Dispose();
                        UnFollow(user);
                    }
                }
                catch (Exception ex)
                {
                    bt++;
                    Console.WriteLine(ex.ToString());
                    if (bt > 5)
                    {
                        
                        driver.Dispose();
                        UnFollow(user);
                    }
                    
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

