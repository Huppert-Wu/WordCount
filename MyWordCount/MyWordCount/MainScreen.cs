using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;//正则表达式

namespace MyWordCount
{
    class MainScreen
    {
        //保存命令行参数选项
        bool c = false, a = false, w = false, l = false, s = false, e = false, o = false, x = false;
        static public string inputfile = null;
        static public string outputfile = null;//未实例化类也可以引用
        static public string stopfile = null;
        static public string regfile = null;//正则文件名
        /// <summary>
        /// 处理命令行参数
        /// </summary>
        /// <param name="args"></param>
        public void mainScreen(string[] args)
        {

            if (args.Length < 1)
            {
                Usage();
            }
            //命令行参数处理
            for (int index = 0; index < args.Length; index++)
            {
                switch (args[index])
                {
                    case "-l"://行数处理
                        l = true;
                        break;
                    case "-s"://递归目录
                        s = true;
                        break;
                    case "-a"://注释行
                        a = true;
                        break;
                    case "-c"://字符数
                        c = true;
                        break;
                    case "-w"://单词数
                        w = true;
                        break;
                    case "-x"://当前目录所有文件
                        x = true;
                        //args长度只能为3
                        if (args.Length > 3)
                            Usage();
                        if (++index < args.Length)
                            regfile = args[index];//保存文件名后index增加1
                        else
                            Usage();
                        break;
                    default:
                        inputfile = args[index];//待处理文件
                        break;
                    case "-e"://停用词处理
                        e = true;
                        if (++index < args.Length)
                            stopfile = args[index];//保存文件名后index增加1
                        else
                            Usage();
                        break;
                    case "-o"://保存输出文件名
                        o = true;
                        if (++index < args.Length)
                            outputfile = args[index];//保存文件名后index增加1
                        else
                            Usage();
                        break;
                }
            }
        }

        public void Processresult()
        {
            ProcessingData processingData = new ProcessingData();
            //读取文件
            string filestr = processingData.Readfile(inputfile);
            string outstr = "";
            //保存处理结果
           if (w == true)
                outstr = string.Format(outstr + processingData.wProsess(filestr) + "\r\n");
            if(c == true)
                outstr = string.Format(outstr + processingData.cProsess(filestr) + "\r\n");
            if(l == true)
                outstr = string.Format(outstr + processingData.lProsess(filestr) + "\r\n");
            if(e == true)
            {
                //打开停用词文件
                string stopstr = processingData.Readfile(stopfile);
                //将原文本文件和停用词文本传入
                outstr = string.Format(outstr + processingData.eProcess(filestr, stopstr) + "\r\n");
            }
            if(x == true)
            {
                //当前根目录
                string path = ".";
                //保存当前目录所有文件名
                List<string> files =  processingData.GetAllDirFiles(path);
                string pattern = regfile;//正则匹配表达式
                Regex regex = new Regex(@"\?" + pattern);
                
                foreach (string file in files)
                {
                    MainScreen.inputfile = file;//暂时先这样，有问题
                    //如果文件名匹配regfile，则处理该文件
                    Match match = regex.Match(file);
                    if (match.Success == true)
                    {
                        //处理该文件
                        filestr = processingData.Readfile(file);
                        //windows行结束"\r\n"
                        outstr = string.Format(outstr + processingData.wProsess(file) + "\r\n");
                        outstr = string.Format(outstr + processingData.cProsess(file) + "\r\n");
                        outstr = string.Format(outstr + processingData.lProsess(file) + "\r\n");
                    }
                }
            }

            //写入文件
            if (outputfile == null)
                outputfile = "output.txt";
            processingData.Writefile(outputfile, outstr);
            Console.WriteLine(outstr);
            Console.ReadLine();
        }

        /// <summary>
        /// 使用方法
        /// </summary>
        public void Usage()
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("--------------WordCount 1.0-------------");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("统计字符数        wc.exe -c file.c  ");
            Console.WriteLine("统计单词总数      wc.exe -w file.c  ");
            Console.WriteLine("统计总行数        wc.exe -l file.c  ");
            Console.WriteLine("统计输出到文件    wc.exe -o outputfile.txt  ");
            Console.WriteLine("统计所有            wc.exe -c -l -w file -e file -o outputfile.txt");
            Console.WriteLine("----------------------------------------");
        }

    }
}
