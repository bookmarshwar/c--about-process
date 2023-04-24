
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace ConsoleApp1
{
    class Test
    {
        static Dictionary<string, Process> processMap = new Dictionary<string, Process>();//构建name映射Pr的散列
        static Dictionary<string, string> processPath = new Dictionary<string, string>();//构建name->path散列
        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("请决定你的操作/close/open/start/reopen/hide/show/help");
                string string1 = Console.ReadLine();

                string[] string2 = System.Text.RegularExpressions.Regex.Split(string1, @"[ ]+");
                try
                {
                    switch (string2[0])
                    {
                        case "help":
                            Console.WriteLine("close:关闭程序\nopen:开启程序\nstart:初始化程序到内存,所有未加载到内存的程序都需要执行此操作\nreopen:重启程序\nhide:隐藏程序界面\nshow:显示程序界面");
                            break;
                        case "close":
                            Killfile(string2[1]);
                            Console.WriteLine("已关闭命名为:" + string2[1] + "的程序");

                            break;
                        case "start":
                            Startfile(string2[1], string2[2]);
                            Console.WriteLine("载入" + string2[1] + "程序\n" + "该程序命名为:" + string2[2]);
                            break;
                        case "open":
                            Runfile(string2[1]);
                            Console.WriteLine("已打开命名为:" + string2[1] + "的程序");

                            break;
                        case "reopen":
                            Rerunfile(string2[1]);
                            Console.WriteLine("已重启命名为:" + string2[1] + "的程序");

                            break;
                        case "hide":
                            Hidenfile(string2[1]);
                            Console.WriteLine("已隐藏命名为:" + string2[1] + "的程序");

                            break;
                        case "show":
                            Showfile(string2[1]);
                            Console.WriteLine("已显示命名为:" + string2[1] + "的程序");

                            break;
                        default:
                            Console.WriteLine("输入错误请输入help查看方法");
                            break;
                    }
                }

                catch
                {
                    Console.WriteLine("参数输入错误,请输入help查看方法和参数");
                }
            }
            static void Startfile(string file, string name)//初始化
            {
                Process p = new Process();
                processMap.Add(name, p);
                processPath.Add(name, file);
            }
            static void Runfile(string name)//启动
            {
                Process p = processMap[name];
                p.StartInfo.FileName = processPath[name];
                p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                p.StartInfo.UseShellExecute = true;
                p.Start();
                Console.WriteLine(p.Id);
            }
            static void Rerunfile(string name)//重启
            {
                Killfile(name);

                Runfile(name);
            }
            static void Killfile(string name)//杀死
            {
                Process p = processMap[name];
                Console.WriteLine(p.Id);
                if (p != null)
                {
                    p.CloseMainWindow();
                    Console.WriteLine("进程关闭成功！");

                }
            }
            static void Hidenfile(string name)//隐藏
            {
                [DllImport("user32.dll")]
                static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
                Process p = processMap[name];
                ShowWindow(p.MainWindowHandle, 0);
            }
            static void Showfile(string name)//显示
            {
                [DllImport("user32.dll")]
                static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
                Process p = processMap[name];
                ShowWindow(p.MainWindowHandle, 1);
            }
        }
    }
}