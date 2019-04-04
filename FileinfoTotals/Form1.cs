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
                rtbOut.Text = "正在分析中..."+Environment.NewLine;
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
                            volume.TotalSize = Math.Round(driveInfo.TotalSize / (1024*1024 * 1024d), 3);
                            volume.FreeSize = Math.Round(driveInfo.AvailableFreeSpace / (1024*1024 * 1024d), 3);

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
                strOut = string.Format("{0}磁盘类型:{1,19}{2}",
                    strOut, vol.volInfo.DriveType, Environment.NewLine);
                strOut = string.Format("{0}磁盘总空间:{1,14} GB{2}",
                    strOut, vol.TotalSize, Environment.NewLine);
                strOut = string.Format("{0}磁盘可用空间:{1,12} GB{2}",
                    strOut, vol.FreeSize, Environment.NewLine);
                strOut = string.Format("{0}磁盘中文件总数:{1,13}{2}",
                    strOut, vol.FilesCount.ToString("###,###"), Environment.NewLine);
                strOut = string.Format("{0}磁盘中目录总数:{1,13}{2}{2}",
                    strOut, vol.dirInfos==null?"0": vol.dirInfos.Count.ToString("###,###"),
                    Environment.NewLine);
                if (vol.fileInfos != null)
                {
                    var filesByExtensions = vol.fileInfos.GroupBy(g => g.Extension)
                        .Select(s => new { ExtenName = s.Key, FileCnt = s.Count() })
                        .OrderByDescending(o => o.FileCnt);
                    if (filesByExtensions != null)
                    {
                        foreach (var e in filesByExtensions)
                        {
                            strOut = string.Format("{0}扩展名为 {1,-20}的文件总数:{2,6}{3}",
                         strOut, e.ExtenName, e.FileCnt.ToString("###,###"), Environment.NewLine);
                        }
                    }
                    var modifyByOneMonth = vol.fileInfos.Where(w => w.LastWriteTime > DateTime.Now.AddMonths(-1))
                        .OrderByDescending(o => o.LastWriteTime);
                    if (modifyByOneMonth != null && modifyByOneMonth.Count() > 0)
                    {
                        strOut = string.Format("{0}{2}最近1月内修改过的文件数:{1}{2}",
                             strOut, modifyByOneMonth.Count(), Environment.NewLine);
                        foreach (var m in modifyByOneMonth)
                        {
                            strOut = string.Format("{0} {1,-40} 修改时间:{2}{3}",
                         strOut, m.FullName, m.LastWriteTime.ToString(), Environment.NewLine);
                        }
                    }
                    //Over 1GB size files
                    var overOneGBFiles = vol.fileInfos.Where(w => w.Length>1024*1024*1024)
                        .OrderByDescending(o => o.Length);
                    if (overOneGBFiles != null && overOneGBFiles.Count() > 0)
                    {
                        strOut = string.Format("{0}{2}大小超过1GB的文件数:{1}{2}",
                             strOut, overOneGBFiles.Count(), Environment.NewLine);
                        foreach (var m in overOneGBFiles)
                        {
                            strOut = string.Format("{0} {1,-40} 大小:{2,3}GB{3}",
                         strOut, m.FullName, m.Length/(1024*1024*1024), Environment.NewLine);
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
            this.rtbOut.BeginInvoke(new Action(() =>
            {
                this.rtbOut.Text = string.Format("{0}正在分析磁盘名[{1}]中的所有文件及目录...{2}",
this.rtbOut.Text, vol.VolName, Environment.NewLine);
            }));
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
            this.rtbOut.BeginInvoke(new Action(() =>
            {
                this.rtbOut.Text = string.Format("{0}分析磁盘名[{1}]中的目录完成{2}",
this.rtbOut.Text, vol.VolName, Environment.NewLine);
            }));
            if (subDirInfo != null&& subDirInfo.Count>0)
            {
                vol.dirInfos = subDirInfo;
                foreach (var dirInfo in vol.dirInfos)
                {
                    try
                    {
                        filesInfoList.AddRange(dirInfo.GetFiles("*", SearchOption.TopDirectoryOnly));
                    }
                    catch (System.UnauthorizedAccessException u)
                    {
                        Console.WriteLine(u.Message);
                    }
                }
            }
            this.rtbOut.BeginInvoke(new Action(() =>
            {
                this.rtbOut.Text = string.Format("{0}分析磁盘名[{1}]中的文件完成{2}",
this.rtbOut.Text, vol.VolName, Environment.NewLine);
            }));
            if (filesInfo != null)
            {
                vol.fileInfos = filesInfoList;
                filesCnt = vol.fileInfos.Count;
            }
            return filesCnt;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (rtbOut.Text.Length>0)
            {
                File.WriteAllText(string.Format("{0}.ft",DateTime.Now.ToString("yyyyMMddHHmmss")),
                    rtbOut.Text, Encoding.Default);
                MessageBox.Show("导出完成!");
                System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath);
            }
        }
    }
}
