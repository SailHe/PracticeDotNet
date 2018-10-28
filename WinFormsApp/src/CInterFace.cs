using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace WinFormsApp
{
    public struct SystemTime
    {
        public int year;
        public int month;
        public int day;
        public int hour;
        public int minute;
        public int second;
        public int millsecond;

        public SystemTime(DateTime dt)
        {
            this.year = dt.Year;
            this.month = dt.Month;
            this.day = dt.Day;
            this.hour = dt.Hour;
            this.minute = dt.Minute;
            this.second = dt.Second;
            this.millsecond = dt.Millisecond;
        }

        public override string ToString()
        {
            return this.year.ToString() + "-" + this.month.ToString() + "-" + this.day.ToString() + "  "
                + this.hour.ToString() + ":" + this.minute.ToString() + "-" + this.second.ToString() + "-"
                + this.millsecond.ToString();
        }
    };
    
    class CInterFace
    {
        const string path = "E:/Projects/Source/Repos/archives-algorithm/Debug/";

        [DllImport(path + "InterfaceForDotNet.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public extern static int add(int x, int y);

        [DllImport(path + "InterfaceForDotNet.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public extern static int sub(int x, int y);

        [DllImport(path + "InterfaceForDotNet.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public extern static int testChar(ref byte src, ref byte res, int nCount);

        [DllImport(path + "InterfaceForDotNet.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public extern static int testStruct(ref SystemTime stSrc, ref SystemTime stRes);

        [DllImport(path + "InterfaceForDotNet.dll", EntryPoint = "SubInDll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public extern static int Sub(int x, int y);

        [DllImport(path + "InterfaceForDotNet.dll", EntryPoint = "A", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public extern static int A(int n, int m);
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public int x;
        public int y;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct Rect
    {
        [FieldOffset(0)] public int left;
        [FieldOffset(4)] public int top;
        [FieldOffset(8)] public int right;
        [FieldOffset(12)] public int bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class MySystemTime
    {
        public ushort wYear;
        public ushort wMonth;
        public ushort wDayOfWeek;
        public ushort wDay;
        public ushort wHour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMilliseconds;
    }
    class Win32API
    {
        [DllImport("User32.dll")]
        public static extern bool PtInRect(ref Rect r, Point p);

        [DllImport("Kernel32.dll")]
        public static extern void GetSystemTime(MySystemTime st);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MessageBox(IntPtr hWnd,
             string text, string caption, int options);
    }

    class DllTest
    {
        public static void mainForCinterface()
        {
            char[] s = new char[10];
            //直接使用
            Debug.WriteLine(CInterFace.Sub(10, 1));
            //需要解决InterfaceForDotNet.dll的DSAUtilityExtension.dll依赖: 将两者都放至bin目录下
            Debug.WriteLine(CInterFace.A(10, 10));


            //https://docs.microsoft.com/en-us/dotnet/api/system.numerics.biginteger?redirectedfrom=MSDN&view=netframework-4.7.2
            //BigInteger bigIntFromDouble = new BigInteger(179032.6541);

            int temp = CInterFace.A(10, 1);
            temp = CInterFace.Sub(10, 1);

            int a = CInterFace.add(100, 50);
            int b = CInterFace.sub(100, 50);
            Debug.WriteLine("add = " + a.ToString() + "  b = " + b.ToString());
            Debug.WriteLine("\r\n");

            string src = "123456";
            byte[] srcBytes = System.Text.Encoding.ASCII.GetBytes(src);
            byte[] resBytes = new byte[100];
            a = CInterFace.testChar(ref srcBytes[0], ref resBytes[0], src.Length);
            string res = (System.Text.Encoding.ASCII.GetString(resBytes, 0, resBytes.Length)).TrimEnd();
            Debug.WriteLine(res.ToString());
            Debug.WriteLine("\r\n");

            SystemTime stSrc = new SystemTime(DateTime.Now);
            SystemTime stRes = new SystemTime();
            a = CInterFace.testStruct(ref stSrc, ref stRes);
            Debug.WriteLine(stRes.ToString());
            Debug.WriteLine("\r\n");
        }

        public static void MainForTest()
        {
            mainForCinterface();


            MySystemTime sysTime = new MySystemTime();
            Win32API.GetSystemTime(sysTime);

            string dt;
            dt = "System time is: \n" +
                  "Year: " + sysTime.wYear + "\n" +
                  "Month: " + sysTime.wMonth + "\n" +
                  "DayOfWeek: " + sysTime.wDayOfWeek + "\n" +
                  "Day: " + sysTime.wDay;
            Win32API.MessageBox(IntPtr.Zero, dt, "Platform Invoke Sample", 0);

            Rect rect = new Rect();
            rect.bottom = 30;
            rect.right = 30;
            Win32API.PtInRect(ref rect, new Point());
        }
    }

}
