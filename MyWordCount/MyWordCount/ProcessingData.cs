using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MyWordCount
{
    /// <summary>
    /// 指令操作
    /// </summary>
    class ProcessingData
    {
        /// <summary>
        /// 统计文件中的字符个数
        /// </summary>
        /// <param name="fstr">已读取文件的字符串</param>
        /// <returns>返回待打印字符串</returns>
        public string cProsess(string fstr)
        {
            //string str = openFile(fstr);
            string output = null;
            int charCount = 0;//记录字符个数
            foreach (char c in fstr)
            {
                    charCount++;
            }
            output = string.Format(MainScreen.inputfile + "字符数：" + Convert.ToString(charCount));
            return output;
        }

        /// <summary>
        /// 统计文件中的单词数
        /// </summary>
        /// <param name="wstr">文件的内容</param>
        /// <returns>返回单词数</returns>
        public string wProsess(string wstr)
        {
            //string str = openFile(wstr);
            int count = 0;//记录单词个数
            string outstr = null;

            for (int i = 0; i < wstr.Length; i++)//遍历字符串，
            {
                //判断一个英文字符
                if (((wstr[i] >= 0x41 && wstr[i] <= 0x5A) || (wstr[i] >= 0x61) && wstr[i] <= 0x7A))
                {
                    int next = i;
                    //遇到第一个英文字母开始到其他字符结束
                    do
                    {
                        if (!((wstr[next] >= 0x41 && wstr[next] <= 0x5A) || (wstr[next] >= 0x61) && wstr[next] <= 0x7A))
                        {
                            count++;
                            i = next;//更新i的值
                            break;
                        }
                    } while (++next < wstr.Length);//wstr[++next] != '\0'
                    //if (wstr[next] == '\0')
                        //count++;
                }

            }
            outstr = string.Format(MainScreen.inputfile + "单词数" + Convert.ToString(count));
            return outstr;
        }

        /// <summary>
        /// 统计文件中的行数
        /// </summary>
        /// <param name="lstr">要统计的文件内容</param>
        /// <returns>返回行数</returns>
        public string lProsess(string lstr)
        {
            //string str = openFile(lstr);
            string outstr = null;
            int count = 0;//记录行数
            if(lstr != "")
            {
                count++;//第一行无换行符
            }
            foreach (char s in lstr)
            {
                if (s == '\n')
                {
                    count++;
                }
            }
            outstr = string.Format(MainScreen.inputfile + "行数" + count.ToString());
            return outstr;
        }

        /// <summary>
        /// 统计文件中单词数（停用词除外）
        /// </summary>
        /// <param name="estr"></param>
        /// <param name="stopstr"></param>
        /// <returns></returns>
        public string eProcess(string estr, string stopstr)
        {
            int count = 0;//记录单词个数
            string outstr = "";//结果保存字符串
            string curword = "";//当前处理的单词
            string[] stopwords = stopstr.Split(new char[] { '\r', '\n','\0' },StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < estr.Length; i++)//遍历字符串，
            {
                char curchar = estr[i];
                //判断一个英文字符
                if (((curchar >= 0x41 && curchar <= 0x5A) || (curchar >= 0x61 && curchar <= 0x7A)))
                {
                    int next = i-1;
                    while(true)
                    {
                        next++;//初始化为i
                        //遇到第一个非英文字母结束
                        if (!((estr[next] >= 0x41 && estr[next] <= 0x5A) || (estr[next] >= 0x61) && estr[next] <= 0x7A))
                        {
                            
                            count++;
                            foreach (string stopword in stopwords)
                            {
                                //比较当前处理的单词与停用词
                                if (stopword.CompareTo(curword) == 0)
                                {
                                    count--;
                                    break;
                                }
                            }
                            curword = "";//重新设置当前单词为空
                            break;
                        }
                        //处理结束符
                        if (estr[next] == '\0')
                            break;
                        curword += estr[next];//保存英文单词
                        
                    } 

                    i = next;//更新i的值
                }
            }
            outstr = string.Format(MainScreen.inputfile + "除停用词单词数" + Convert.ToString(count));
            return outstr;
        }



        /// <summary>
        /// 从文件读取内容
        /// </summary>
        /// <param name=""></param>
        public string Readfile(string filename)
        {
            try
            {
                //先实例化文件流对象
                FileStream fileStream = File.OpenRead(filename);

                //然后准备存放文件内容的字节数组
                byte[] data = new byte[fileStream.Length];
                //开始读
                fileStream.Read(data, 0, data.Length);
                fileStream.Close();
                //字节数组转字符串
                return Encoding.UTF8.GetString(data) + '\0'; //字符串结束标志
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        /// <summary>
        /// 写内容到文件
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool Writefile(string filename, string str)
        {
            try
            {
                //实例化文件流对象
                FileStream fileStream = File.Open(filename, FileMode.Truncate);//打开文件并清除之前数据
                //操作字符
                byte[] data = Encoding.Default.GetBytes(str);
                
                fileStream.Write(data,0,data.Length);
                fileStream.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        /// <summary>
        /// 得到根目录下所有文件名
        /// </summary>
        /// <param name="path">根目录名</param>
        /// <returns>文件名列表</returns>
        public List<string> GetAllDirFiles(string path)
        {
            List<string> files = new List<string>();
            DirectoryInfo dir = new DirectoryInfo(path);//目录
            FileInfo[] fil = dir.GetFiles();//当前目录下的文件
            DirectoryInfo[] dii = dir.GetDirectories();//子目录

            foreach (FileInfo file in fil)
            {
                files.Add(file.FullName);
                //Console.WriteLine(file.FullName);
            }
            foreach (DirectoryInfo directoryInfo in dii)
            {
                //循环保存根目录
                files.AddRange( GetAllDirFiles(directoryInfo.FullName));
            }
            return files;
        }
    }
}
