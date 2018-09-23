#include<stdio.h>
#include<sys/types.h>
#include<sys/stat.h>
#include<fcntl.h>
#include<unistd.h>
#include<stdlib.h>
#include<string.h>

#define DEF_MODER S_IRUSR|S_IRGRP|S_IROTH
#define DEF_MODEW S_IWUSR|S_IWGRP|S_IWOTH
#define Maxchar (2048)
void * Readfile(int fd);
int Charnum(char * instr);
int Outfd(int argc, char **argv);


int main(int argc,char **argv)
{
    if(argc < 3)
    {
        printf("使用方法");
        exit(0);
    }
    int a=0,b=0,c=0;
    char* inputfile=NULL,outputfile;
    int infd,outfd;
    char* instr;
    char outstr[Maxchar];
    //printf("%s %s",argv[1],argv[2]);
    //exit(0);
    for(int count = 1; count < argc; count++)
    {
        //判断必需参数
        printf("%d\n",count);
        if(!strcmp(argv[count], "-c"))
        {
            c = 1;
            //Method1
        }
        else if(!strcmp(argv[count] ,"-b"))
        {
            ;
        }
        else if(!strcmp(argv[count] ,"-w"))
        {
            ;
        }
        else
        {
        //搜索输入文件名
            inputfile = argv[count];
            break;
        }

    }

    //printf("%s",inputfile);
    //exit(0);
    outfd = Outfd(argc,argv);

    if(infd = open(inputfile,O_RDWR, DEF_MODER) == -1)
    {
        fprintf(stderr,"open inputfile error");
        exit(0);
    }
    instr = Readfile(infd);
    close(infd);
    sprintf(outstr,"argv[0] 字符数: %d",Charnum(instr));
    printf("%d\n",Charnum(instr));

    if (write(outfd,outstr,Maxchar) < 0)
    {
        fprintf(stderr,"out error");
    }
    close(outfd);
}

//read file
void * Readfile(int fd)
{
    void * buf;
    if(read(fd,buf,Maxchar)<0)
    {
        fprintf(stderr,"error");
    }
    return buf;
}
int Charnum(char* str)
{
    int totalchar=0;
    while(*str++ != '\0')
    {
        totalchar++;
    }
    return totalchar;

}
int Outfd(int argc, char **argv)
{
    int outfd;
        //搜索 "-o"
    if(!strcmp(argv[argc-2] ,"-o"))
    {
        if(argv[argc-1] != NULL)
        {
           //输出文件fd
           if(outfd = open(argv[argc - 1],O_CREAT|O_WRONLY,DEF_MODEW) == -1)
           {
               fprintf(stderr,"open out file error1");
               exit(0);
           }
        }
        else
            fprintf(stderr,"no out file exit");
    }
    else
    {
        if(outfd = open("Outfile.txt",O_CREAT|O_TRUNC|O_RDWR,DEF_MODEW) == -1)
        {
            fprintf(stderr,"open out file error2");
            exit(0);
        }
    }

   return outfd;

}


