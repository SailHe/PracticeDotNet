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

}
