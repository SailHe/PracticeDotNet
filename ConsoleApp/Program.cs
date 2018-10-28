// The following using directive requires a project reference to Microsoft.VisualBasic.
using Microsoft.VisualBasic.FileIO;

using System;
using System.Collections.Generic;
using SailHeCSharpClassLib;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;

namespace LearnDotNet
{
    using StringMapDouble = SortedDictionary<string, double>;
    using StringUnorderedMapDouble = Dictionary<string, double>;

    //using Shell = Debug;
    using Shell = Console;
    
    enum EnumGender { UNKNOWN, MALE, WOMAN }

    // 性别, 出生日期, 班级名称, 联系电话
    //    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    class ClassStudent : BaseStudent{
        //按住ctrl+R不动，然后再按E
        private string gender = EnumGender.UNKNOWN.ToString();
        private string birthDay;
        private string className;
        private string phone;
        public ClassStudent(string name) : base(name){}
        public ClassStudent(string name, EnumGender enumGender, DateTime birthDay, string className, string phone) : this(name)
        {
            this.gender = enumGender.ToString();
            this.birthDay = birthDay.ToString();

            this.className = className;
            this.phone = phone;
        }

        public string Gender { get => gender; set => gender = value; }
        public string BirthDay { get => birthDay; set => birthDay = value; }
        public string ClassName { get => className; set => className = value; }
        public string Phone { get => phone; set => phone = value; }

        override
        public string ToString()
        {
            return base.ToString() + "; 性别: " + gender + "; 生日: " + birthDay + "; 班级名: " + className + "; 联系电话: " + phone;
        }
        public string writeString()
        {
            return getName() + ";" + getSduId().ToString() + ";" + gender + ";" + birthDay + ";" + className + ";" + phone;
        }
    }

    ///  ====== @see https://my.oschina.net/Tsybius2014/blog/352409

    /// <summary>
    /// 工具类：对象与二进制流间的转换
    /// </summary>
    class ByteConvertHelper
    {
        /// <summary>
        /// 将对象转换为byte数组
        /// </summary>
        /// <param name="obj">被转换对象</param>
        /// <returns>转换后byte数组</returns>
        public static byte[] Object2Bytes(object obj)
        {
            byte[] buff;
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter iFormatter = new BinaryFormatter();
                iFormatter.Serialize(ms, obj);
                buff = ms.GetBuffer();
            }
            return buff;
        }

        /// <summary>
        /// 将byte数组转换成对象
        /// </summary>
        /// <param name="buff">被转换byte数组</param>
        /// <returns>转换完成后的对象</returns>
        public static object Bytes2Object(byte[] buff)
        {
            object obj;
            using (MemoryStream ms = new MemoryStream(buff))
            {
                IFormatter iFormatter = new BinaryFormatter();
                obj = iFormatter.Deserialize(ms);
            }
            return obj;
        }
        
        public static void Test<T>(T obj)
        {
            byte[] bytTemp = ByteConvertHelper.Object2Bytes(obj);
            Console.WriteLine("Byte数组长度：" + bytTemp.Length);
            T tsB = (T)ByteConvertHelper.Bytes2Object(bytTemp);
            Console.WriteLine(tsB.ToString());
            Console.ReadKey();
        }
    }

    /// <summary>
    /// 工具类：对象与二进制流间的转换
    /// </summary>
    class ByteConvertHelperNoneSerializable
    {
        /// <summary>
        /// 将对象转换为byte数组
        /// </summary>
        /// <param name="obj">被转换对象</param>
        /// <returns>转换后byte数组</returns>
        public static byte[] Object2Bytes(object obj)
        {
            byte[] buff = new byte[Marshal.SizeOf(obj)];
            IntPtr ptr = Marshal.UnsafeAddrOfPinnedArrayElement(buff, 0);
            Marshal.StructureToPtr(obj, ptr, true);
            return buff;
        }

        /// <summary>
        /// 将byte数组转换成对象
        /// </summary>
        /// <param name="buff">被转换byte数组</param>
        /// <param name="typ">转换成的类名</param>
        /// <returns>转换完成后的对象</returns>
        public static object Bytes2Object(byte[] buff, Type typ)
        {
            IntPtr ptr = Marshal.UnsafeAddrOfPinnedArrayElement(buff, 0);
            return Marshal.PtrToStructure(ptr, typ);
        }

        //https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/generics/
        public static void Test<T>(T obj, Type type)
        {
            byte[] bytTemp = Object2Bytes(obj);
            Console.WriteLine("数组长度：" + bytTemp.Length);
            T tsB = (T)Bytes2Object(
                bytTemp, Type.GetType(type.ToString()));
            Console.WriteLine(tsB.ToString());
            Console.ReadKey();
        }
    }

    /// <summary>
    /// 工具类：文件与二进制流间的转换
    /// </summary>
    class FileBinaryConvertHelper
    {
        /// <summary>
        /// 将文件转换为byte数组
        /// </summary>
        /// <param name="path">文件地址</param>
        /// <returns>转换后的byte数组</returns>
        public static byte[] File2Bytes(string path)
        {
            if (!File.Exists(path))
            {
                return new byte[0];
            }

            FileInfo fi = new FileInfo(path);
            byte[] buff = new byte[fi.Length];

            FileStream fs = fi.OpenRead();
            fs.Read(buff, 0, Convert.ToInt32(fs.Length));
            fs.Close();

            return buff;
        }

        /// <summary>
        /// 将byte数组转换为文件并保存到指定地址
        /// </summary>
        /// <param name="buff">byte数组</param>
        /// <param name="savepath">保存地址</param>
        public static void Bytes2File(byte[] buff, string savepath)
        {
            if (File.Exists(savepath))
            {
                File.Delete(savepath);
            }

            FileStream fs = new FileStream(savepath, FileMode.CreateNew);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(buff, 0, buff.Length);
            bw.Close();
            fs.Close();
        }

        //读入文件将其转换为Byte后写出到另一个文件
        public static void Test()
        {
            byte[] bytTemp = File2Bytes("test.txt");
            Console.WriteLine("数组长度：" + bytTemp.Length);
            Bytes2File(bytTemp, "tempOutput.txt");
            Console.ReadKey();
        }
    }

    // .NET Framework控制台程序
    class Program
    {
        
        // Simple synchronous file copy operations with no user interface.
        // To run this sample, 确保对应的文件或目录已经存在
        static void SimpleFileCopy(string sourcePath, string targetPath, string fileName)
        {
            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFile = System.IO.Path.Combine(targetPath, fileName);

            // To copy a folder's contents to a new location:
            // Create a new target folder, if necessary.
            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }

            // To copy a file to another location and 
            // overwrite the destination file if it already exists.
            System.IO.File.Copy(sourceFile, destFile, true);

            // To copy all the files in one directory to another directory.
            // Get the files in the source folder. (To recursively iterate through
            // all subfolders under the current directory, see
            // "How to: Iterate Through a Directory Tree.")
            // Note: Check for target path was performed previously
            //       in this code example.
            if (System.IO.Directory.Exists(sourcePath))
            {
                string[] files = System.IO.Directory.GetFiles(sourcePath);

                // Copy the files and overwrite destination files if they already exist.
                foreach (string s in files)
                {
                    // Use static Path methods to extract only the file name from the path.
                    fileName = System.IO.Path.GetFileName(s);
                    destFile = System.IO.Path.Combine(targetPath, fileName);
                    System.IO.File.Copy(s, destFile, true);
                }
            }
            else
            {
                Console.WriteLine("Source path does not exist!");
            }

            // Keep console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        // 将 sourcePath 指定的目录(如果提供了文件则复制文件)复制到 destinationPath 指定的目录。
        // 此代码还提供了标准的对话框，其中显示在操作完成前估计的剩余时间量。
        static void FileProgress(string sourcePath, string targetPath)
        {
            FileSystem.CopyDirectory(sourcePath, targetPath, UIOption.AllDialogs);
        }

        //写文件
        static void WriteTextFile(string targetPath, string fileName, string[] lines)
        {
            string destFile = System.IO.Path.Combine(targetPath, fileName);
            
            // WriteAllLines creates a file, writes a collection of strings to the file,
            // and then closes the file.  You do NOT need to call Flush() or Close().
            System.IO.File.WriteAllLines(destFile, lines);


            // Example #2: Write one string to a text file.
            string text = "A class is the most powerful data type in C#. Like a structure, " +
                           "a class defines the data and behavior of the data type. ";
            // WriteAllText creates a file, writes the specified string to the file,
            // and then closes the file.    You do NOT need to call Flush() or Close().
            System.IO.File.WriteAllText(destFile, text);
        }
        static void WriteTextFileByStreamWriter(string targetPath, string fileName, string[] lines)
        {
            string destFile = System.IO.Path.Combine(targetPath, fileName);
            
            // Example #3: Write only some strings in an array to a file.
            // The using statement automatically flushes AND CLOSES the stream and calls 
            // IDisposable.Dispose on the stream object.
            // NOTE: do not use FileStream for text files because it writes bytes, but StreamWriter
            // encodes the output as text.
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(destFile))
            {
                foreach (string line in lines)
                {
                    // If the line doesn't contain the word 'Second', write the line to the file.
                    if (!line.Contains("Second"))
                    {
                        file.WriteLine(line);
                    }
                }
            }

            // Example #4: Append new text to an existing file.
            // The using statement automatically flushes AND CLOSES the stream and calls 
            // IDisposable.Dispose on the stream object.
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(destFile, true))
            {
                file.WriteLine("Fourth line");
            }
        }

        //读文件
        static void ReadFromFile(string sourcePath, string fileName)
        {
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            // The files used in this example are created in the topic
            // How to: Write to a Text File. You can change the path and
            // file name to substitute text files of your own.

            // Example #1
            // Read the file as one string.
            string text = System.IO.File.ReadAllText(sourceFile);
            System.IO.File.ReadAllBytes(sourceFile);

            // Display the file contents to the console. Variable text is a string.
            System.Console.WriteLine("Contents of WriteText.txt = {0}", text);

            // Example #2
            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.
            string[] lines = System.IO.File.ReadAllLines(sourceFile);

            // Display the file contents by using a foreach loop.
            System.Console.WriteLine("Contents of WriteLines2.txt = ");
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                Console.WriteLine("\t" + line);
            }

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }
        /*//读一个学生
        static ClassStudent ReadStu(string sourcePath, string fileName)
        {
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string text = System.IO.File.ReadAllText(sourceFile);
            System.IO.File.ReadAllBytes(sourceFile);
            ClassStudent student = new ClassStudent(stuNameBuffer, EnumGender.UNKNOWN, DateTime.Now, "161", "12345678910");
            return student;
        }
        //写一个学生
        static void WriteStu(string sourcePath, string fileName, ClassStudent student)
        {
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string text = System.IO.File.ReadAllText(sourceFile);
            System.IO.File.ReadAllBytes(sourceFile);
            Convert.ToByte
            Byte[] message = Convert.StructToBytes(student);
        }*/
        /// <summary>
        /// 测试结构
        /// </summary>
        struct TestStructure
        {
            public string A; //变量A
            public char B;   //变量B
            public int C;    //变量C

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="paraA"></param>
            /// <param name="paraB"></param>
            /// <param name="paraC"></param>
            public TestStructure(string paraA, char paraB, int paraC)
            {
                this.A = paraA;
                this.B = paraB;
                this.C = paraC;
            }

            /// <summary>
            /// 输出本结构中内容
            /// </summary>
            /// <returns></returns>
            public string DisplayInfo()
            {
                return string.Format("A:{0};B:{1};C:{2}", this.A, this.B, this.C);
            }
        }
        static void Main(string[] args)
        {
            EnumGender enumGender;
            DateTime birthDay;
            string className;
            string phone;
            Console.WriteLine("输入学生姓名:");
            string stuNameBuffer = Console.ReadLine();
            ClassStudent student = new ClassStudent(stuNameBuffer, EnumGender.UNKNOWN, DateTime.Now, "161", "12345678910");
            Console.WriteLine(student);
            Console.WriteLine(student.writeString());

            //ByteConvertHelper.Test(student);
            var testStructre = new TestStructure();
            ByteConvertHelperNoneSerializable.Test(testStructre, testStructre.GetType());
            ByteConvertHelperNoneSerializable.Test(student, student.GetType());
            //solve1_10_17(new Random());
            string fileName = "test.txt";
            string sourcePath = @"F:\Temper\TestFolder";
            string targetPath = @"F:\Temper\TestFolder\SubDir";
            // Example #1: Write an array of strings to a file.
            // Create a string array that consists of three lines.
            string[] lines = { "First line", "Second line", "Third line" };
            ReadFromFile(sourcePath, fileName);
            //SimpleFileCopy(sourcePath, targetPath, fileName);
            //FileProgress(@"F:\Temper\TestFolder\SubDir", @"F:\Temper\TestFolder\SubDir2");
            WriteTextFile(targetPath, fileName, lines);
            WriteTextFileByStreamWriter(targetPath, "test2.txt", lines);
            Console.ReadKey();
        }
    }
}
