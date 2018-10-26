using Ku.Common;
using System;
using System.IO;
using System.IO.Compression;

namespace Ku.Common
{
    /// <summary>
    /// 数据压缩处理类
    /// </summary>
    public class zipClass
    {
        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="sourceStream">待解压缩的流</param>
        /// <returns></returns>
        public static Stream Decompress(Stream sourceStream)
        {
            try
            {
                sourceStream.Position = 0;
                GZipStream stream = new GZipStream(sourceStream, CompressionMode.Decompress, true);
                byte[] buffer = new byte[1024];
                MemoryStream temp = new MemoryStream();
                int read = stream.Read(buffer, 0, buffer.Length);
                while (read > 0)
                {
                    temp.Write(buffer, 0, read);
                    read = stream.Read(buffer, 0, buffer.Length);
                }
                stream.Close();
                stream.Dispose();
                sourceStream.Close();
                sourceStream.Dispose();
                temp.Position = 0;
                return temp;
            }
            catch (Exception ex)
            {
                LogsRecord.write("zipClass", ex.ToString());
            }
            return null;
        }

        /// <summary>
        /// 压缩流
        /// </summary>
        /// <param name="sourceStream">待压缩的流</param>
        /// <returns></returns>
        public static Stream Compress(Stream sourceStream)
        {
            try
            {
                sourceStream.Position = 0;
                byte[] data = new byte[sourceStream.Length];
                sourceStream.Read(data, 0, data.Length);
                sourceStream.Close();
                sourceStream.Dispose();

                MemoryStream ms = new MemoryStream();
                GZipStream stream = new GZipStream(ms, CompressionMode.Compress, true);
                stream.Write(data, 0, data.Length);
                stream.Close();
                stream.Dispose();

                ms.Position = 0;
                return ms;
            }
            catch (OutOfMemoryException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                LogsRecord.write("zipClass", ex.ToString());
            }
            return null;
        }
    }
}
