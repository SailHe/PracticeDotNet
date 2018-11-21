// The following using directive requires a project reference to Microsoft.VisualBasic.
using Microsoft.VisualBasic.FileIO;

using System;
using System.Collections.Generic;
using SailHeCSharpClassLib;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using SqlKata;
using SqlKata.Compilers;
using System.Data;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using Cat.Database;
using ConsoleApp;
using System.Linq;
//using SailHeModel;

namespace LearnDotNet
{
    //using Shell = Debug;
    using StringMapInt = Dictionary<string, int>;
    using intMapString = Dictionary<int, string>;

    public enum EnumGender { UNKNOWN, MALE, WOMAN }

    // 性别, 出生日期, 班级名称, 联系电话
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class StudentInfo : BaseStudent{
        //按住ctrl+R不动，然后再按E
        private string gender = EnumGender.UNKNOWN.ToString();
        private string birthDay;
        private int classId;
        private string className;
        private string phone;
        public StudentInfo() : base("unknown") { }
        public StudentInfo(string name) : base(name){}
        public StudentInfo(string name, EnumGender enumGender, DateTime birthDay, string className, string phone) : this(name)
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
        public int ClassId { get => classId; set => classId = value; }

        override
        public string ToString()
        {
            return base.ToString() + "; 性别: " + gender + "; 生日: " + birthDay + "; 班级名: " + className + "; 联系电话: " + phone;
        }
        public string tabString()
        {
            return getName() + "\t"
                + (getSduId() == null ? "---------" : getSduId())
                + "\t" + gender + "\t\t" + birthDay + "\t" + className + "\t" + phone;
        }
    }

    class ClassAndGrade
    {
        private string name;
        private string classId;
    }

    // .NET Framework控制台程序
    public class Program
    {

        static void CreateFile(string pathString, string fileName)
        {
            // Use Combine again to add the file name to the path.
            pathString = System.IO.Path.Combine(pathString, fileName);

            // Verify the path that you have constructed.
            Console.WriteLine("Path to my file: {0}\n", pathString);

            // Check that the file doesn't already exist. If it doesn't exist, create
            // the file and write integers 0 - 99 to it.
            // DANGER: System.IO.File.Create will overwrite the file if it already exists.
            // This could happen even with random file names, although it is unlikely.
            if (!System.IO.File.Exists(pathString))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(pathString))
                {
                    for (byte i = 0; i < 100; i++)
                    {
                        fs.WriteByte(i);
                    }
                }
            }
            else
            {
                Console.WriteLine("File \"{0}\" already exists.", fileName);
                return;
            }
        }

        static void CreateFileAndFolder(string pathString)
        {
            // You can extend the depth of your path if you want to.
            //pathString = System.IO.Path.Combine(pathString, "SubSubFolder");

            // Create the subfolder. You can verify in File Explorer that you have this
            // structure in the C: drive.
            //    Local Disk (C:)
            //        Top-Level Folder
            //            SubFolder
            System.IO.Directory.CreateDirectory(pathString);

            // Create a file name for the file you want to create. 
            string fileName = System.IO.Path.GetRandomFileName();

            // This example uses a random string for the name, but you also can specify
            // a particular name.
            //string fileName = "MyNewFile.txt";

            CreateFile(pathString, fileName);

            // Read and display the data from your file.
            try
            {
                byte[] readBuffer = System.IO.File.ReadAllBytes(pathString);
                foreach (byte b in readBuffer)
                {
                    Console.Write(b + " ");
                }
                Console.WriteLine();
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }

            // Keep the console window open in debug mode.
            System.Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

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
        
        static void ShowAll<T>(List<T> eleList)
        {
            eleList.ForEach(ele => Console.WriteLine(ele));
        }



        static void generateStuAndClass()
        {
            LinkedList<StudentInfo> studentList = new LinkedList<StudentInfo>();
            for (int i = 0; i < 10; ++i)
            {
                StudentInfo student = new StudentInfo("学生" + i.ToString(), EnumGender.UNKNOWN, DateTime.Now, "161", "15258989411");
                studentList.AddLast(student);
                Console.WriteLine(student);
            }
            //ArrayList studentS = new ArrayList(studentList);
            List<StudentInfo> studentS = new List<StudentInfo>(studentList);
            //var tempResult = ByteConvertHelper.Test(studentS.ToArray());
            var seriResult = ByteConvertHelper.Object2Bytes(studentS.ToArray());
            FileBinaryConvertHelper.Bytes2File(seriResult, "Student.txt");
            //ByteConvertHelperNoneSerializable.Object2Bytes(studentS.ToArray());
        }

        static StudentInfo readAStudent(StringMapInt gnameMapGid = null)
        {
            EnumGender enumGender;
            DateTime birthDay;
            string className, phone, stuNameBuffer, birthDayBuffer;
            Console.WriteLine("输入学生姓名:");
            stuNameBuffer = Console.ReadLine();
            if (gnameMapGid != null)
            {
                string allInMap = "";
                int i = -1;
                foreach (KeyValuePair<string, int> kv in gnameMapGid)
                {
                    allInMap += ++i == 0 ? "" : "; ";
                    allInMap += kv.Key;// + ": " + kv.Value.ToString("0.") + "号";
                }
                while (true)
                {
                    Console.WriteLine("输入班级名:");
                    Console.WriteLine(allInMap);
                    className = Console.ReadLine();
                    if (gnameMapGid.ContainsKey(className))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("班级不存在!请重输");
                    }
                }
            }
            else
            {
                Console.WriteLine("输入班级名:");
                className = Console.ReadLine();
            }
            Console.WriteLine("输入性别: '男' '女' 其余视为'未知'");
            switch (Console.ReadLine())
            {
                case "男": enumGender = EnumGender.MALE; break;
                case "女": enumGender = EnumGender.WOMAN; break;
                default: enumGender = EnumGender.UNKNOWN; break;
            }
            while(true)
            {
                Console.WriteLine("输入生日:");
                birthDayBuffer = Console.ReadLine();
                if (Verify.IsDateTime(birthDayBuffer))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("格式错误!请重输");
                }
            }
            birthDay = DateTime.Parse(birthDayBuffer);

            while (true)
            {
                Console.WriteLine("输入联系方式(电话,手机号):");
                phone = Console.ReadLine();
                if (Verify.IsPhoneNumber(phone))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("格式错误!请重输");
                }
            }
            var result =new StudentInfo(stuNameBuffer, enumGender, birthDay, className, phone);
            result.ClassId = gnameMapGid[className];
            return result;
        }
        
        static void solve1_10_24()
        {
            //generateStuAndClass();
            //读取
            var seriResult = FileBinaryConvertHelper.File2Bytes("Student.txt");
            List<StudentInfo> studentS
                = new List<StudentInfo>((StudentInfo[])ByteConvertHelper.Bytes2Object(seriResult));
            ShellFor_10_24(studentS);
            //写入
            seriResult = ByteConvertHelper.Object2Bytes(studentS.ToArray());
            FileBinaryConvertHelper.Bytes2File(seriResult, "Student.txt");
        }
        
        static void ShellFor_10_24(List<StudentInfo> studentS, StringMapInt gidMapGname = null)
        {

        //查询
        start:
            string input = "- ";
            do
            {

                Console.Clear();
                Console.WriteLine("姓名\t学号\t性别\t\t\t生日\t\t班级名\t联系电话");
                if (input.Length < 2)
                {
                    input += " ";
                }
                switch (input[0])
                {
                    // 按姓名查询
                    case '0':
                        studentS.FindAll(ele => ele.getName().Contains(input.Substring(2)))
                      .ForEach(ele => Console.WriteLine(ele.tabString())); break;
                    // 按班级查询
                    case '1':
                        studentS.FindAll(ele => ele.ClassName.Contains(input.Substring(2)))
                      .ForEach(ele => Console.WriteLine(ele.tabString())); break;
                    // 新增
                    case '2': {
                            var temp = readAStudent(gidMapGname);
                            temp.resetSduId();
                            studentS.Add(temp);
                            goto start;
                        }
                    // 显示所有
                    default: studentS.ForEach(ele => Console.WriteLine(ele.tabString())); break;
                }
                Console.WriteLine(
                    "Shell提示: \n\r" +
                    " 0 name: 按姓名查询;" +
                    " 1 className: 按班级查询;" +
                    " 2: 新增;" +
                    " else: 显示所有;" +
                    " 回车: 保存并退出"
                    );

            } while ((input = Console.ReadLine()) != string.Empty);
        }

        static void ShellFor_11_15(List<StudentInfo> studentS, StringMapInt gidMapGname = null)
        {

        //查询
        start:
            string input = "- ";
            do
            {

                Console.Clear();
                Console.WriteLine("姓名\t学号\t性别\t\t\t生日\t\t班级名\t联系电话");
                if (input.Length < 2)
                {
                    input += " ";
                }
                switch (input[0])
                {
                    // 按姓名查询
                    case '0':
                        studentS.FindAll(ele => ele.getName().Contains(input.Substring(2)))
                      .ForEach(ele => Console.WriteLine(ele.tabString())); break;
                    // 按班级查询
                    case '1':
                        studentS.FindAll(ele => ele.ClassName.Contains(input.Substring(2)))
                      .ForEach(ele => Console.WriteLine(ele.tabString())); break;
                    // 新增
                    case '2':
                        {
                            var temp = readAStudent(gidMapGname);
                            temp.resetSduId();
                            studentS.Add(temp);
                            goto start;
                        }
                    // 更改
                    case '3':
                        {
                            
                            string sidBuffer = "";
                            int index = -1;
                            while (true)
                            {
                                Console.WriteLine("输入需要更改者的学号 直接回车返回:");
                                sidBuffer = Console.ReadLine();
                                index = -1;
                                StudentInfo studentInfo = studentS.Find(ele => {
                                    ++index;
                                    return ele.getSduId().Equals(sidBuffer);
                                });
                                if (studentInfo != null)
                                {
                                    break;
                                }
                                else if(sidBuffer == "")
                                {
                                    goto start;
                                }
                                else
                                {
                                    Console.WriteLine("学号不存在!请重输 或回车返回");
                                }
                            }
                            Console.WriteLine("原信息: ");
                            Console.WriteLine(studentS[index].tabString());
                            var temp = readAStudent(gidMapGname);
                            temp.setSduId(int.Parse(studentS[index].getSduId()));
                            studentS[index] = temp;
                            goto start;
                        }
                    // 按学号查询
                    case '4':
                        studentS.FindAll(ele => ele.getSduId().Contains(input.Substring(2)))
                      .ForEach(ele => Console.WriteLine(ele.tabString())); break;
                    // 显示所有
                    default: studentS.ForEach(ele => Console.WriteLine(ele.tabString())); break;
                }
                Console.WriteLine(
                    "Shell提示: \n\r" +
                    " 0 name: 按姓名查询;" +
                    " 1 className: 按班级查询;" +
                    " 2: 新增;" +
                    " 3: 更改;" +
                    " 4 学号: 按学号查询;" +
                    " else: 显示所有;" +
                    " 回车: 保存并退出"
                    );

            } while ((input = Console.ReadLine()) != string.Empty);
        }

        static void TestSqlServer()
        {
            SqlConnection conn = new SqlConnection();
            //为数据库连接设置连接字符串：
            string connStr
                = "Data Source=localhost; User ID=sailhe; Password=123456@password; Initial Catalog=Sailhe";
            // myDbIp，数据库连接的IP地址
            // myUserName，数据库连接的用户名
            // myPass，数据库连接的密码
            // myDbName，数据库名
            /* 打开数据库 */
            conn.ConnectionString = connStr;

            //3、判断是否连接成功

            if (conn.State == ConnectionState.Open)
            {
                Win32API.MessageBox("数据库连接成功！", "Tip");
            }
            else
            {
                Win32API.MessageBox(conn.State.ToString(), "Tip2");
            }

            //4、关闭数据库连接
            conn.Close();


            // Create an instance of SQLServer
            var compiler = new SqlServerCompiler();

            var query = new Query("Users").Where("Id", 1).Where("Status", "Active");

            SqlResult result = compiler.Compile(query);

            string sql = result.Sql;
            List<object> bindings = result.Bindings; // [ 1, "Active" ]
        }

        //需要导入MySql.Data.Dll
        //@see https://blog.csdn.net/xiajian2010/article/details/25965109
        static void TestMySQL()
        {
            string query = "select * from sys_user";
            MySqlConnection myConnection = new MySqlConnection("server=localhost;user id=sailhe;password=123456@password;database=lost_and_found");
            MySqlCommand myCommand = new MySqlCommand(query, myConnection);
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            MySqlDataReader myDataReader = myCommand.ExecuteReader();
            string result = "";
            while (myDataReader.Read() == true)
            {
                result += myDataReader["user_id"];
                result += myDataReader["user_username"];
                result += myDataReader["user_password"];
            }
            myDataReader.Close();
            myConnection.Close();
            Debug.WriteLine(result);
        }

        static void TestMySQLUseEFModel()
        {
            //sail_heEntities context = new sail_heEntities();
            ///var result = from stuList in context.ustudent select stuList;
            ///SELECT * FROM dbo.Posts WHERE Author = @p0
            //var stuList = context.ustudent.SqlQuery("SELECT * FROM usudent WHERE sid = @p0", 12005001);
            using (var context = new sail_heEntities())
            {
                //context.ustudent.Where
                //需要using System.Linq;
                var stuList = context.ustudent.ToList();
                Debug.WriteLine(stuList.ToString());
            }
        }

        static void showAll(string query, MySqlConnection myConnection)
        {
            MySqlCommand myCommand = new MySqlCommand(query, myConnection);
            myCommand.ExecuteNonQuery();
            MySqlDataReader myDataReader = myCommand.ExecuteReader();
            string result = "";
            while (myDataReader.Read() == true)
            {
                result += myDataReader["sid"];
                result += "\t" + myDataReader["sname"];
                result += "\t" + myDataReader["ssexy"];
                result += "\t" + myDataReader["sbdate"];
                result += "\t" + myDataReader["gid"];
                result += "\t" + myDataReader["stele"];
                result += "\n\r";
            }
            Debug.WriteLine(result);
            myDataReader.Close();
        }

        static List<StudentInfo> calcAll(string query, MySqlConnection myConnection, intMapString gidMapGname)
        {
            List<StudentInfo> studentS = new List<StudentInfo>();

            MySqlCommand myCommand = new MySqlCommand(query, myConnection);
            myCommand.ExecuteNonQuery();
            MySqlDataReader myDataReader = myCommand.ExecuteReader();
            //string result = "";
            while (myDataReader.Read() == true)
            {

                /*result += myDataReader["sid"];
                result += "\t" + myDataReader["sname"];
                result += "\t" + myDataReader["ssexy"];
                result += "\t" + myDataReader["sbdate"];
                result += "\t" + myDataReader["gid"];
                result += "\t" + myDataReader["stele"];
                result += "\n\r";*/
                int gid = int.Parse((string)myDataReader["gid"]);
                StudentInfo temp = new StudentInfo(
                    (string)myDataReader["sname"]
                    , ((string)myDataReader["ssexy"]) == "男" ? EnumGender.MALE : EnumGender.WOMAN
                    , DateTime.Parse((string)myDataReader["sbdate"])
                    , gidMapGname[gid]
                    , (string)myDataReader["stele"]
                    );
                temp.setSduId((int)myDataReader["sid"]);
                temp.ClassId = gid;
                studentS.Add(temp);
            }
            myDataReader.Close();
            return studentS;
        }

        static StringMapInt calcAllClass(string query, MySqlConnection myConnection, out intMapString gidMapGname)
        {
            StringMapInt result = new StringMapInt();
            gidMapGname = new intMapString();

            MySqlCommand myCommand = new MySqlCommand(query, myConnection);
            myCommand.ExecuteNonQuery();
            MySqlDataReader myDataReader = myCommand.ExecuteReader();
            //string result = "";
            while (myDataReader.Read() == true)
            {
                string gname = (string)myDataReader["gname"];
                int gid = int.Parse((string)myDataReader["gid"]);
                gidMapGname.Add(gid, gname);
                result.Add(gname, gid);
            }
            myDataReader.Close();
            return result;
        }

        public static StringMapInt calcAllClass(out intMapString gidMapGname)
        {
            StringMapInt result = new StringMapInt();
            intMapString gidMapGnameBuffer = new intMapString();
            using (var context = new sail_heEntities())
            {
                var ugradeList = context.ugrade.ToList();
                ugradeList.ForEach(ele => {
                    string gname = ele.gname;
                    gidMapGnameBuffer.Add(ele.gid, ele.gname);
                    result.Add(gname, ele.gid);
                });
            }
            gidMapGname = gidMapGnameBuffer;
            return result;
        }

        public static List<StudentInfo> calcAll(intMapString gidMapGname)
        {
            List<StudentInfo> studentS = new List<StudentInfo>();

            StringMapInt result = new StringMapInt();
            using (var context = new sail_heEntities())
            {
                var resultList = context.ustudent.ToList();
                resultList.ForEach(ele => {
                    StudentInfo temp = new StudentInfo(
                    ele.sname
                    , (ele.ssexy) == "男" ? EnumGender.MALE : EnumGender.WOMAN
                    , DateTime.Parse(ele.sbdate)
                    , gidMapGname[int.Parse(ele.gid)]
                    , ele.stele
                    );
                    temp.setSduId(ele.sid);
                    temp.ClassId = int.Parse(ele.gid);
                    studentS.Add(temp);
                });
            }
            return studentS;
        }

        static void saveAll(MySqlConnection myConnection, List<StudentInfo> studentS)
        {
            //string insert = "INSERT INTO ustudent(sname, ssexy, sbdate, gid, stele)VALUES('新同学', '男', '1988/10/12', '1', '660780')";
            //string update = "UPDATE ustudent SET -`2sname = '李山',ssexy = '男',sbdate = '1988/10/11',gid = '1',stele = '660780' WHERE sid = '12005001'";
            studentS.ForEach(ele => {
                if (ele.getSduId() == null)
                {
                    string insert = "INSERT INTO ustudent(sname, ssexy, sbdate, gid, stele)VALUES('"
                    + ele.getName()
                    + "', '"
                    + (ele.Gender == EnumGender.MALE.ToString() ? '男' : '女')
                    + "', '" + ele.BirthDay
                    + "', '" + ele.ClassId
                    + "', '"+ ele.Phone
                    + "')";
                    MySqlCommand insertCommand = new MySqlCommand(insert, myConnection);
                    //Console.ReadKey();
                    insertCommand.ExecuteNonQuery();
                }
                else
                {
                    /*string update = "UPDATE ustudent SET -`2sname = '"
                    + ele.getName()
                    +"',ssexy = '" + (ele.Gender == EnumGender.MALE.ToString() ? '男' : '女')
                    + "',sbdate = '" + "', '" + ele.BirthDay
                    + "',gid = '" + ele.ClassId
                    +"',stele = '" + ele.Phone
                    + "' WHERE sid = '" + ele.getSduId()
                    + "'";
                    MySqlCommand updateCommand = new MySqlCommand(update, myConnection);
                    updateCommand.ExecuteNonQuery();*/
                }
            });
        }

        static void saveAll(List<StudentInfo> studentS)
        {
            studentS.ForEach(ele => {
                if (ele.getSduId() == null)
                {
                    using (var context = new sail_heEntities())
                    {
                        ustudent temp = new ustudent();
                        temp.gid = ele.ClassId.ToString();
                        //temp.sid = int.Parse(ele.getSduId());
                        temp.sname = ele.getName().ToString();
                        temp.sbdate = ele.BirthDay.ToString();
                        temp.ssexy = (ele.Gender == EnumGender.MALE.ToString() ? "男" : "女");
                        temp.stele = ele.Phone.ToString();
                        context.ustudent.Add(temp);
                        context.SaveChanges();
                    }
                }
                else
                {
                    using (var context = new sail_heEntities())
                    {
                        int sid = int.Parse(ele.getSduId());
                        var stuBuffer = context.ustudent.Where(e => e.sid == sid).First();
                        ustudent temp = stuBuffer;
                        temp.gid = ele.ClassId.ToString();
                        temp.sid = int.Parse(ele.getSduId());
                        temp.sname = ele.getName().ToString();
                        temp.sbdate = ele.BirthDay.ToString();
                        temp.ssexy = (ele.Gender == EnumGender.MALE.ToString() ? "男" : "女");
                        temp.stele = ele.Phone.ToString();
                        //stuBuffer = temp;
                        context.SaveChanges();
                    }
                }
            });
        }

        static void solve1_10_31()
        {
            //TestMySQL();
            //TestSqlServer();
            MySqlConnection myConnection
                = new MySqlConnection("server=localhost;user id=sailhe;password=123456@password;database=sail_he");
            myConnection.Open();
            List<StudentInfo> studentS = new List<StudentInfo>();
            intMapString gidMapGname = null;
            var gredeIdMapName = calcAllClass("select * from ugrade", myConnection, out gidMapGname);
            studentS = calcAll("select * from ustudent", myConnection, gidMapGname);
            //showAll(query, myConnection);
            ShellFor_10_24(studentS, gredeIdMapName);
            saveAll(myConnection, studentS);

            myConnection.Close();
            Console.ReadKey();
        }

        static void solve1_11_14()
        {
            //TestMySQLUseEFModel();
            List<StudentInfo> studentS = new List<StudentInfo>();
            intMapString gidMapGname = null;
            var gredeIdMapName = calcAllClass(out gidMapGname);
            studentS = calcAll(gidMapGname);
            //showAll();
            ShellFor_11_15(studentS, gredeIdMapName);
            saveAll(studentS);
            Console.ReadKey();
        }

        static void Demo()
        {
            StudentInfo student = readAStudent();
            ByteConvertHelper.Test(student);
            ByteConvertHelperNoneSerializable.Test(student, student.GetType());
            FileBinaryConvertHelper.Test(student);


            string fileName = "test.txt";
            string sourcePath = @"TestFolder";
            string targetPath = @"TestFolder\SubDir";
            string[] lines = { "First line", "Second line", "Third line" };

            System.IO.Directory.CreateDirectory(sourcePath);
            CreateFile(sourcePath, fileName);
            System.IO.Directory.CreateDirectory(targetPath);

            ReadFromFile(sourcePath, fileName);
            SimpleFileCopy(sourcePath, targetPath, fileName);
            //FileProgress(sourcePath + "\\SubDir2", targetPath);
            //WriteTextFile(targetPath, fileName, lines);
            WriteTextFileByStreamWriter(targetPath, "test2.txt", lines);
        }

        static void DbHerperDemo()
        {
            //这个DbHelper不支持MySQL
            DbInsert insert = new DbInsert();
            insert.Add("user_name", "DbHelperTest");
            insert.Execute("sys_user");
        }

        static void Main(string[] args)
        {
            //OLD: server=localhost;user id=sailhe;password=123456@password;database=lost_and_found
            //server=localhost; user id=sailhe; persistsecurityinfo=True; database=sail_he
            //SailHeModel
            //server=localhost; user id=sailhe; persistsecurityinfo=True; database=sail_he

            //DbHerperDemo();
            //solve1_10_24();
            // 官方教程: @see https://docs.microsoft.com/zh-cn/ef/core/saving/basic
            //貌似使用EF模型后就不允许使用之前这种连接方式了
            //solve1_10_31();
            solve1_11_14();
        }
    }
}
