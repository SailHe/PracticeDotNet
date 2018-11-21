using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SailHeCSharpClassLib
{
    using StringMapDouble = SortedDictionary<string, double>;
    using StringUnorderedMapDouble = Dictionary<string, double>;

    //学生类 姓名, 学号
    [Serializable]
    //Sequential有序，可不连续 @see https://blog.csdn.net/bugDemo/article/details/18036147
    [StructLayout(LayoutKind.Sequential)]
    public class BaseStudent
    {
        static int STU_ID = 0;
        static int STU_COUNT = 0;
        //姓名
        string name;
        //学号
        string stuId;
       
        public static void init()
        {
            STU_COUNT = 0;
        }

        public BaseStudent(string name)
        {
            this.name = name;
            ++STU_COUNT;
            this.stuId = STU_ID++.ToString("00");
        }
        
        public static int getStuCount()
        {
            return STU_COUNT;
        }

        public string getStuId()
        {
            return stuId;
        }
        //将stuId置为null
        public void resetSduId()
        {
            stuId = null;
        }
        public void setSduId(int stuId)
        {
            this.stuId = stuId.ToString();
        }
        public string getName()
        {
            return name;
        }
        override
        public string ToString()
        {
            return "姓名: " + name + "; 学号: " + stuId;
        }
    }

}
