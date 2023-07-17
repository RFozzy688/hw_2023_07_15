using System.Runtime;
using System.Drawing;
using System.IO;

namespace hw_2023_07_15
{
    public partial class Form1 : Form
    {
        string _path;
        ImageList _imageList;
        public Form1()
        {
            InitializeComponent();

            _path = @"G:\ÿ¿√\";
            _imageList = new ImageList();
            //treeView1.ImageList = _imageList;

            treeView1.BeginUpdate();

            CreateTree();

            treeView1.EndUpdate();
        }
        public void CreateTree()
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                TreeNode node = new TreeNode { Text = drive.Name + drive.VolumeLabel };

                AddDirectoryOrFile(node, drive.Name);
                treeView1.Nodes.Add(node);
            }

            //files = root.GetFiles();

            //foreach (FileInfo file in files)
            //{
            //    //if (!_imageList.Images.ContainsKey(file.Extension))
            //    //{
            //    //    iconForDir = Icon.ExtractAssociatedIcon(file.FullName);
            //    //    _imageList.Images.Add(file.Extension, iconForDir);
            //    //}

            //    //treeView1.ImageKey = file.Extension;

            //    TreeNode node = new TreeNode(file.Name);
            //    treeView1.Nodes.Add(node);
            //}
        }
        public void AddDirectoryOrFile(TreeNode node, string path)
        {
            string[] dirs = Directory.GetDirectories(path);

            foreach(string dir in dirs)
            {
                TreeNode nodeDir = new TreeNode();
                nodeDir.Text = dir.Remove(0, dir.LastIndexOf("\\") + 1);
                node.Nodes.Add(nodeDir);

            }

            string[] files = Directory.GetFiles(path);

            foreach (string file in files)
            {
                TreeNode nodeFile = new TreeNode();
                nodeFile.Text = file.Remove(0, file.LastIndexOf("\\") + 1);
                node.Nodes.Add(nodeFile);
            }
        }
    }
}