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
            try
            {
                string[] dirs = Directory.GetDirectories(path);

                foreach (string dir in dirs)
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
            catch (Exception ex) { }
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            string str = null;
            string path = null;

            if (e.Node.Level == 0)
            {
                str = e.Node.Text;
                e.Node.Text = e.Node.Text.Remove(3);
                path = e.Node.FullPath;
            }
            else
            {
                path = e.Node.FullPath.Remove(3, e.Node.FullPath.IndexOf('\\', 3) - 2);
            }

            e.Node.Nodes.Clear();
            string[] dirs;

            if (Directory.Exists(path/*e.Node.FullPath*/))
            {
                dirs = Directory.GetDirectories(path/*e.Node.FullPath*/);
                if (dirs.Length != 0)
                {
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        TreeNode dirNode = new TreeNode(new DirectoryInfo(dirs[i]).Name);
                        AddDirectoryOrFile(dirNode, dirs[i]);
                        e.Node.Nodes.Add(dirNode);
                    }
                }
            }

            if (e.Node.Level == 0) e.Node.Text = str;
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            string str = null;
            string path = null;

            if (e.Node.Level == 0)
            {
                str = e.Node.Text;
                e.Node.Text = e.Node.Text.Remove(3);
                path = e.Node.FullPath;
            }
            else
            {
                path = e.Node.FullPath.Remove(3, e.Node.FullPath.IndexOf('\\', 3) - 2);
            }
            e.Node.Nodes.Clear();

            string[] dirs;

            if (Directory.Exists(path))
            {
                dirs = Directory.GetDirectories(path);
                if (dirs.Length != 0)
                {
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        TreeNode dirNode = new TreeNode(new DirectoryInfo(dirs[i]).Name);
                        AddDirectoryOrFile(dirNode, dirs[i]);
                        e.Node.Nodes.Add(dirNode);
                    }
                }
            }

            if (e.Node.Level == 0) e.Node.Text = str;
        }
    }
}