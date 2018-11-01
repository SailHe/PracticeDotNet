using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SailHeCSharpClassLib
{
    using Regex = System.Text.RegularExpressions.Regex;

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
        /// 0.验证手机号码   正则
        /// </summary>
        /// <param name="str_cellPhoneNum"></param>
        /// <returns></returns>
        public static bool IsCellphone(string str_cellPhoneNum)
        {
            return Regex.IsMatch(str_cellPhoneNum
                , @"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$");
        }
        /// <summary>
        /// 1.验证电话号码 ("XXX-XXXXXXX"、"XXXX-XXXXXXXX"、"XXX-XXXXXXX"、"XXX-XXXXXXXX"、"XXXXXXX"和"XXXXXXXX)  正则
        /// </summary>
        /// <param name="str_telephone"></param>
        /// <returns></returns>
        public static bool IsTelephone(string str_telephone)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_telephone
                , @"^(\(\d{3,4}-)|\d{3.4}-)?\d{7,8}$");
        }
        /// <summary>
        /// 1.1验证国内电话号码 (0511-4405222、021-87888822)
        /// </summary>
        /// <param name="str_telephone"></param>
        /// <returns></returns>
        public static bool IsTelephoneInchina(string str_telephone)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_telephone
                , @"\d{3}-\d{8}|\d{4}-\d{7}");
        }

        /// <summary>
        /// 2.验证手机号码，3-4位区号，7-8位直播号码，1－4位分机号  正则
        /// </summary>
        /// <param name="str_PhoneNum"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(string str_PhoneNum)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_PhoneNum
                , @"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)");
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

        /// <summary>
        /// 使用正则表达式判断是否为日期
        /// </summary>
        /// <param name="str" type=string></param>
        /// <returns name="isDateTime" type=bool></returns>
        public static bool IsDateTime(string str)
        {
            bool isDateTime = false;
            // yyyy/MM/dd
            if (Regex.IsMatch(str, "^(?<year>\\d{2,4})/(?<month>\\d{1,2})/(?<day>\\d{1,2})$"))
                isDateTime = true;
            // yyyy-MM-dd 
            else if (Regex.IsMatch(str, "^(?<year>\\d{2,4})-(?<month>\\d{1,2})-(?<day>\\d{1,2})$"))
                isDateTime = true;
            // yyyy.MM.dd 
            else if (Regex.IsMatch(str, "^(?<year>\\d{2,4})[.](?<month>\\d{1,2})[.](?<day>\\d{1,2})$"))
                isDateTime = true;
            // yyyy年MM月dd日
            else if (Regex.IsMatch(str, "^((?<year>\\d{2,4})年)?(?<month>\\d{1,2})月((?<day>\\d{1,2})日)?$"))
                isDateTime = true;
            // yyyy年MM月dd日
            else if (Regex.IsMatch(str, "^((?<year>\\d{2,4})年)?(正|一|二|三|四|五|六|七|八|九|十|十一|十二)月((一|二|三|四|五|六|七|八|九|十){1,3}日)?$"))
                isDateTime = true;

            // yyyy年MM月dd日
            else if (Regex.IsMatch(str, "^(零|〇|一|二|三|四|五|六|七|八|九|十){2,4}年((正|一|二|三|四|五|六|七|八|九|十|十一|十二)月((一|二|三|四|五|六|七|八|九|十){1,3}(日)?)?)?$"))
                isDateTime = true;
            // yyyy年
            //else if (Regex.IsMatch(str, "^(?<year>\\d{2,4})年$"))
            //    isDateTime = true;

            // 农历1
            else if (Regex.IsMatch(str, "^(甲|乙|丙|丁|戊|己|庚|辛|壬|癸)(子|丑|寅|卯|辰|巳|午|未|申|酉|戌|亥)年((正|一|二|三|四|五|六|七|八|九|十|十一|十二)月((一|二|三|四|五|六|七|八|九|十){1,3}(日)?)?)?$"))
                isDateTime = true;
            // 农历2
            else if (Regex.IsMatch(str, "^((甲|乙|丙|丁|戊|己|庚|辛|壬|癸)(子|丑|寅|卯|辰|巳|午|未|申|酉|戌|亥)年)?(正|一|二|三|四|五|六|七|八|九|十|十一|十二)月初(一|二|三|四|五|六|七|八|九|十)$"))
                isDateTime = true;

            // XX时XX分XX秒
            else if (Regex.IsMatch(str, "^(?<hour>\\d{1,2})(时|点)(?<minute>\\d{1,2})分((?<second>\\d{1,2})秒)?$"))
                isDateTime = true;
            // XX时XX分XX秒
            else if (Regex.IsMatch(str, "^((零|一|二|三|四|五|六|七|八|九|十){1,3})(时|点)((零|一|二|三|四|五|六|七|八|九|十){1,3})分(((零|一|二|三|四|五|六|七|八|九|十){1,3})秒)?$"))
                isDateTime = true;
            // XX分XX秒
            else if (Regex.IsMatch(str, "^(?<minute>\\d{1,2})分(?<second>\\d{1,2})秒$"))
                isDateTime = true;
            // XX分XX秒
            else if (Regex.IsMatch(str, "^((零|一|二|三|四|五|六|七|八|九|十){1,3})分((零|一|二|三|四|五|六|七|八|九|十){1,3})秒$"))
                isDateTime = true;

            // XX时
            else if (Regex.IsMatch(str, "\\b(?<hour>\\d{1,2})(时|点钟)\\b"))
                isDateTime = true;
            else
                isDateTime = false;

            return isDateTime;
        }

    }
    /*
    public class MySQLHelper
    {
        private static string connectionString = ConfigurationManager
            .ConnectionStrings["mysqlconn"].ConnectionString;
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    MySqlDataAdapter command = new MySqlDataAdapter(SQLString, connection);
                    command.Fill(ds);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                return ds;
            }
        }
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
        }
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        
        public static int ExecuteSql(string[] arrSql)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                try
                {
                    connection.Open();
                    MySqlCommand cmdEncoding = new MySqlCommand(SET_ENCODING, connection);
                    cmdEncoding.ExecuteNonQuery();
                    int rows = 0;
                    foreach (string strN in arrSql)
                    {
                        using (MySqlCommand cmd = new MySqlCommand(strN, connection))
                        {
                            rows += cmd.ExecuteNonQuery();
                        }
                    }
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    connection.Close();
                    throw e;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
*/
}
