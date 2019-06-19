using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NBandServer
{
    public class FileBlock
    {
        public string ID { get; set; }
        public int Index { get; set; }
        public long Length { get; set; }
        public long Start { get; set; }
        public long End { get; set; }
        public long TotalLength { get; set; }
        public string FileName { get; set; }
        public bool IsSendOver { get; set; }
        public string Status { get; set; }
        public byte[] Buffer { get; set; }
        private static object writeLock=new object();
        public static NormalResponse WritrBuffer(FileBlock block)
        {
            try
            {
                if (block.Buffer == null) return new NormalResponse(false, "buffer不可为空");
                if (block.Buffer.Length != block.Length) return new NormalResponse(false, "Buffer.Length!=Lengt");
                if (string.IsNullOrEmpty(block.ID)) return new NormalResponse(false, "ID不可为空");
                if (string.IsNullOrEmpty(block.FileName)) return new NormalResponse(false, "文件名不可为空");
                if (block.Start >= block.End) return new NormalResponse(false, "start必须小于end");
                if (block.Length != (block.End - block.Start)) return new NormalResponse(false, "Length!=(End-Start)");
                if (block.TotalLength < block.Length) return new NormalResponse(false, "文件总字节数不可小于本次传输字节数");
                lock (writeLock)
                {
                    string dirPath = Directory.GetCurrentDirectory() + $"/Download/{block.ID}/";
                    if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
                    string filePath = dirPath + block.FileName;
                    bool isNeedReWrite = true;
                    if (File.Exists(filePath))
                    {
                        FileInfo finfo = new FileInfo(filePath);
                        if (block.TotalLength != finfo.Length)
                        {
                            File.Delete(filePath);
                            isNeedReWrite = true;
                        }
                        else
                        {
                            FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate);
                            stream.Position = block.Start;
                            stream.Write(block.Buffer);
                            stream.Close();
                            isNeedReWrite = false;
                        }                                           
                    }
                    if (isNeedReWrite)
                    {
                        byte[] totalBuffer = new byte[block.TotalLength];
                        Array.Copy(block.Buffer, 0, totalBuffer, block.Start, block.Length);
                        File.WriteAllBytes(filePath, totalBuffer);
                    }
                }               
                return new NormalResponse(true, "");
            }
            catch (Exception e)
            {
                return new NormalResponse(false,e.ToString());
            }
           
        }
    }
}
