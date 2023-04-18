
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

        }
        static void Startfile(string file,string name)//初始化
        {   Process p=new Process();
            processMap.Add(name, p);
            processPath.Add(name, file);
        }
        static void Runfile(string name)//启动
        {   Process p=processMap[name];
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
            Process p=processMap[name];
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