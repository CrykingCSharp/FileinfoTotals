using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileinfoTotals
{
    /// <summary>
    /// 卷信息类
    /// </summary>
    public class Volume
    {
        /// <summary>
        /// 磁盘名称
        /// </summary>
        public string VolName { get; set; }

        /// <summary>
        /// 总空间大小 单位GB
        /// </summary>
        public double TotalSize { get; set; }

        /// <summary>
        /// 剩余空间大小  单位GB
        /// </summary>
        public double FreeSize { get; set; }

        /// <summary>
        /// 总文件数
        /// </summary>
        public int FilesCount { get; set; }

        /// <summary>
        /// 盘中所有文件的文件信息
        /// </summary>
        public List<FileInfo> fileInfos { get; set; }

        /// <summary>
        /// 磁盘信息
        /// </summary>
        public DriveInfo volInfo { get; set; }

        /// <summary>
        /// 文件夹信息
        /// </summary>
        public List<DirectoryInfo> dirInfos { get; set; }
    }
}
