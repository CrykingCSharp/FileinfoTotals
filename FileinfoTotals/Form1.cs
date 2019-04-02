using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileinfoTotals
{
    public partial class FormFileinfoTotals : Form
    {
        public FormFileinfoTotals()
        {
            InitializeComponent();
        }

        private void FormFileinfoTotals_Load(object sender, EventArgs e)
        {
            //get all volumes
            var dirves = DriveInfo.GetDrives();
            foreach (var vol in dirves)
            {
                lbVolumns.Items.Add(vol.Name);
            }
        }

        List<Volume> vols = new List<Volume>();
        bool isInAnaly = false;
        private void btnAnaly_Click(object sender, EventArgs e)
        {
            if (lbVolumns.SelectedItems != null)
            {
                if (isInAnaly)
                {
                    MessageBox.Show("上次分析还在进行中,请稍等!");
                    return;
                }
                vols.Clear();
                rtbOut.Text = "正在分析中...";
                isInAnaly = true;
                var selectedVols = new List<string>();
                foreach (var item in lbVolumns.SelectedItems)
                {
                    selectedVols.Add(item.ToString());
                }
                System.Threading.ThreadPool.QueueUserWorkItem(s =>
                {
                    foreach (var vol in selectedVols)
                    {
                        var driveInfo = new DriveInfo(vol);
                        var volume = new Volume()
                        {
                            VolName = driveInfo.Name,
                            volInfo = driveInfo
                        };
                        if (driveInfo.DriveType != DriveType.CDRom)
                        {
                            volume.TotalSize = Math.Round(driveInfo.TotalSize / (1024 * 1024d), 3);
                            volume.FreeSize = Math.Round(driveInfo.AvailableFreeSpace / (1024 * 1024d), 3);

                            volume.FilesCount = GetFilesInfo(volume);
                        }
                        vols.Add(volume);
                    }
                    if (vols.Count > 0)
                    {
                        DisplayOut();
                    }
                    isInAnaly = false;
                });
            }
        }

        /// <summary>
        /// 输出显示
        /// </summary>
        private void DisplayOut()
        {
            var strOut = "";
            this.rtbOut.BeginInvoke(new Action(() => { rtbOut.Text = ""; }));

            foreach (var vol in vols)
            {
                strOut = string.Format("{0}磁盘名: {1,18}{2}",
                    strOut, vol.VolName, Environment.NewLine);
                strOut = string.Format("{0}磁盘类型:{1,16}{2}",
                    strOut, vol.volInfo.DriveType, Environment.NewLine);
                strOut = string.Format("{0}磁盘总空间:{1,14} GB{2}",
                    strOut, vol.TotalSize, Environment.NewLine);
                strOut = string.Format("{0}磁盘可用空间:{1,12} GB{2}",
                    strOut, vol.FreeSize, Environment.NewLine);
                strOut = string.Format("{0}磁盘中文件总数:{1,12}{2}",
                    strOut, vol.FilesCount, Environment.NewLine);
                strOut = string.Format("{0}磁盘中目录总数:{1,12}{2}",
                    strOut, vol.dirInfos==null?0: vol.dirInfos.Count, Environment.NewLine);
                if (vol.fileInfos != null)
                {
                    var filesByExtensions = vol.fileInfos.GroupBy(g => g.Extension)
                        .Select(s => new { ExtenName = s.Key, FileCnt = s.Count() });
                    if (filesByExtensions != null)
                    {
                        foreach (var e in filesByExtensions)
                        {
                            strOut = string.Format("{0}扩展名为{1}的文件总数:{2}{3}",
                         strOut, e.ExtenName, e.FileCnt, Environment.NewLine);
                        }
                    }
                }
            }
            this.rtbOut.BeginInvoke(new Action(() => { rtbOut.Text = strOut; }));
        }

        /// <summary>
        /// 获取指定磁盘的所有文件信息
        /// </summary>
        /// <param name="vol"></param>
        /// <returns></returns>
        private int GetFilesInfo(Volume vol)
        {
            int filesCnt = 0;
            var filesInfoList = new List<FileInfo>();
            var subDirInfo = new List<DirectoryInfo>();
            DirectoryInfo dir = new DirectoryInfo(vol.VolName);
            var filesInfo = dir.GetFiles("*", SearchOption.TopDirectoryOnly);
            filesInfoList.AddRange(filesInfo);
             dir.GetDirectories().ToList()
                .ForEach(s =>
                {
                    try
                    {
                        subDirInfo.AddRange(s.GetDirectories("*",SearchOption.AllDirectories));
                    }
                    catch (System.UnauthorizedAccessException u)
                    {
                        Console.WriteLine(u.Message);
                    }
                });
            if (subDirInfo != null&& subDirInfo.Count>0)
            {
                vol.dirInfos = subDirInfo;
                foreach (var dirInfo in vol.dirInfos)
                {
                    try
                    {
                        filesInfoList.AddRange(dir.GetFiles("*", SearchOption.TopDirectoryOnly));
                    }
                    catch (System.UnauthorizedAccessException u)
                    {
                        Console.WriteLine(u.Message);
                    }
                }
            }
            if (filesInfo != null)
            {
                vol.fileInfos = filesInfoList;
                filesCnt = vol.fileInfos.Count;
            }
            return filesCnt;
        }
    }
}
