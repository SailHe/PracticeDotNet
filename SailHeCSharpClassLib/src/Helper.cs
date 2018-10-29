using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SailHeCSharpClassLib
{
    ///  ====== @see https://my.oschina.net/Tsybius2014/blog/352409
    ///  ====== @see https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/generics/
    /// <summary>
    /// 工具类：对象与二进制流间的转换
    /// </summary>
    public class ByteConvertHelper
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

        // 用于测试参数是否支持此序列化方法 返回反序列化后的结果
        public static T Test<T>(T obj)
        {
            byte[] serializeObj = ByteConvertHelper.Object2Bytes(obj);
            Console.WriteLine("Byte数组长度：" + serializeObj.Length);
            T dserializeObj = (T)ByteConvertHelper.Bytes2Object(serializeObj);
            Console.WriteLine(dserializeObj.ToString());
            Console.ReadKey();
            return dserializeObj;
        }
    }

    /// <summary>
    /// 工具类：对象与二进制流间的转换
    /// </summary>
    public class ByteConvertHelperNoneSerializable
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
        
        // 用于测试参数是否支持此序列化方法 返回反序列化后的结果
        public static T Test<T>(T obj, Type type)
        {
            byte[] serializeObj = Object2Bytes(obj);
            Console.WriteLine("数组长度：" + serializeObj.Length);
            //Type.GetType(type.ToString())
            T dserializeObj = (T)Bytes2Object(serializeObj, type);
            Console.WriteLine(dserializeObj.ToString());
            Console.ReadKey();
            return dserializeObj;
        }
    }

    /// <summary>
    /// 工具类：文件与二进制流间的转换
    /// </summary>
    public class FileBinaryConvertHelper
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

        // 用于测试参数是否支持此序列化方法 读入文件将其转换为Byte后写出到另一个文件
        public static void Test<T>(T obj)
        {
            Bytes2File(ByteConvertHelperNoneSerializable.Object2Bytes(obj), "tempInput.txt");
            byte[] serializeObj = File2Bytes("tempInput.txt");
            Console.WriteLine("数组长度：" + serializeObj.Length);
            Bytes2File(serializeObj, "tempOutput.txt");
            Console.ReadKey();
        }
    }


    /// <summary>
    ///  @see http://www.lixuejiang.me/2016/10/13/%E5%B8%B8%E8%A7%81%E6%AD%A3%E5%88%99%E8%A1%A8%E8%BE%BE%E5%BC%8F/
    /// </summary>
    public class Verify
    {
        /// <summary>
        /// 1.验证电话号码  正则
        /// </summary>
        /// <param name="str_telephone"></param>
        /// <returns></returns>
        public static bool IsTelephone(string str_telephone)
        {
            //@"(0|\+86)?(13[0-9]|15[0-356]|18[025-9])\d{8}"
            return System.Text.RegularExpressions.Regex.IsMatch(str_telephone
                , @"^(\d{3,4}-)?\d{6,8}$");
        }

        /// <summary>
        /// 2.验证手机号码  正则
        /// </summary>
        /// <param name="str_handset"></param>
        /// <returns></returns>
        public static bool IsHandset(string str_handset)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_handset
                , @"^[1]+[3,4,5,8]+\d{9}");
        }

        /// <summary>
        /// 3.验证身份证号  正则
        /// </summary>
        /// <param name="str_idcard"></param>
        /// <returns></returns>
        public static  bool IsIDcard(string str_idcard)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_idcard
                , @"(^\d{18}$)|(^\d{15}$)");
        }

        /// <summary>
        /// 4.验证输入为数字  正则
        /// </summary>
        /// <param name="str_number"></param>
        /// <returns></returns>
        public static  bool IsNumber(string str_number)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_number
                , @"^[0-9]*$");
        }

        /// <summary>
        /// 5.验证邮编  正则
        /// </summary>
        /// <param name="str_postalcode"></param>
        /// <returns></returns>
        public static  bool IsPostalcode(string str_postalcode)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_postalcode
                , @"^\d{6}$");
        }
        ///6.验证邮箱  正则
        public static  bool IsEmail(string str_Email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_Email
                , @"\\w{1,}@\\w{1,}\\.\\w{1,}");
        }
        /// <summary>
        /// 7.验证整数  正则
        /// </summary>
        /// <param name="str_Integer"></param>
        /// <returns></returns>
        public static  bool IsInteger(string str_Integer)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_Integer
                , @"[0-9]+");
        }
        /// <summary>
        /// 8.验证逗号分隔的整数  正则
        /// </summary>
        /// <param name="str_Integer"></param>
        /// <returns></returns>
        public static  bool IsIntegerCommaSeparate(string str_Integer)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_Integer
                , @"\b[0-9]{1,3}(,[0-9]{3})*\b");
        }
        /// <summary>
        /// 9.验证浮点数  正则
        /// </summary>
        /// <param name="str_FloatNum"></param>
        /// <returns></returns>
        public static  bool IsFloatNumber(string str_FloatNum)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_FloatNum
                , @"(\+?(\d+|\.\d+|\d+\.\d+)|-?(\d+|\d+\.\d+))");
        }
        /// <summary>
        /// 10.验证[0,255]之间的数字
        /// </summary>
        /// <param name="str_NumBet2_255"></param>
        /// <returns></returns>
        public static  bool IsNumberBetween2_255(string str_NumBet2_255)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_NumBet2_255
                , @"^([0-9]|[0-9]{2}|1[0-9]{2}|2[0-4][0-9]|25[0-5])$");
        }
        /// <summary>
        /// 11.验证国际图书标准号
        /// </summary>
        /// <param name="str_ISBN"></param>
        /// <returns></returns>
        public static  bool IsISBN(string str_ISBN)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_ISBN
                , @"((ISBN(-13)?:?\s)?97[89][-\s]?[0-9][-\s]?[0-9]{3}[-\s]?[0-9]{5}[-\s]?[0-9]|(ISBN(-10)?:?\s)?[0-9][-\s]?[0-9]{3}[-\s]?[0-9]{5}[-\s]?[0-9x])");
        }
        /// <summary>
        /// 12.验证是否成对的html tag 如 <code> test</code>
        /// </summary>
        /// <param name="str_HtmlTagTwin"></param>
        /// <returns></returns>
        public static  bool IsHtmlTagTwin(string str_HtmlTagTwin)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_HtmlTagTwin
                , @"<([^>]+)>[\s\S]*?<\/\1>");
        }
    }
}
