using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZipNet4WebForm
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_down_Click(object sender, EventArgs e)
        {
            var name = Server.MapPath("file");
            if (!Directory.Exists(name))
                throw new FileNotFoundException("");
            var zipName = "test";

            Response.Write(AppDomain.CurrentDomain.BaseDirectory);
            Response.Write(Server.MapPath("a"));
            //DownZip(name, zipName, 9);
            //DirectoryInfo dir = new DirectoryInfo(name);
            //string aa="";
            //foreach (FileSystemInfo item in dir.GetFileSystemInfos())
            //{
            //    aa += item;
            //}
            //Response.Write(aa);

            //Response.Write(AppDomain.CurrentDomain.BaseDirectory);

            //string file1 = "http://c.hiphotos.baidu.com/image/pic/item/7acb0a46f21fbe09810db97167600c338744ad00.jpg";

            //WebClient myWebClient = new WebClient();
            //myWebClient.DownloadFile(file1, name + "//aaa.png");

            //Response.Write(file1.Substring(file1.LastIndexOf('/') + 1));
        }

        private void DownZip(string dirname, string zipfile, int level = 6, string password = "")
        {
            MemoryStream ms = new MemoryStream();//支持存储区为内存的流
            ZipOutputStream zos = new ZipOutputStream(ms);
            if (password != "")
                zos.Password = password;

            zos.SetLevel(level);
            AddZipEntry(dirname, zos, dirname);
            zos.Finish();
            zos.Close();
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlDecode(zipfile, System.Text.Encoding.UTF8) + ".zip");
            Response.BinaryWrite(ms.ToArray());
            ms.Close();
            Response.Flush();
            Response.End();
        }
        private void AddZipEntry(string strPath, ZipOutputStream zos, string baseDirName)
        {
            //http://c.hiphotos.baidu.com/image/pic/item/7acb0a46f21fbe09810db97167600c338744ad00.jpg
            //http://c.hiphotos.baidu.com/image/pic/item/b21c8701a18b87d65d3311770b0828381f30fd61.jpg
            DirectoryInfo dir = new DirectoryInfo(strPath);

            List<string> files = new List<string>();
            files.Add("http://c.hiphotos.baidu.com/image/pic/item/7acb0a46f21fbe09810db97167600c338744ad00.jpg");
            files.Add("http://c.hiphotos.baidu.com/image/pic/item/b21c8701a18b87d65d3311770b0828381f30fd61.jpg");

            foreach (FileSystemInfo item in dir.GetFileSystemInfos())

            //foreach (FileSystemInfo item in files)
            {
                //if ((item.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                //{
                //    AddZipEntry(item.FullName, zos, baseDirName);
                //}
                //else
                //{
                FileInfo f_item = (FileInfo)item;
                using (FileStream fs = f_item.OpenRead())
                {
                    byte[] buffer = new byte[(int)fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    ZipEntry entry = new ZipEntry(f_item.FullName.Replace(baseDirName, ""));
                    zos.PutNextEntry(entry);
                    zos.Write(buffer, 0, buffer.Length);
                }
                //}
            }
        }


    }


}