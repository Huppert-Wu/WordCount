//字符串处理参考  博客 https://blog.csdn.net/tiandijun/article/details/40401655


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyWordCount
{
    class Program
    {
        static void Main(string[] args)
        {
            MainScreen newscreen = new MainScreen();
            newscreen.mainScreen(args);
            newscreen.Processresult();
        }
    }
}
