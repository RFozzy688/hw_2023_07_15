using System.Runtime;
using System.Drawing;
using System.IO;
using System.Xml.Linq;

namespace hw_2023_07_15
{
    public partial class Form1 : Form
    {
        ImageList _imageList;
        public Form1()
        {
            InitializeComponent();

            _imageList = new ImageList();
            treeView1.ImageList = _imageList;

            _imageList.Images.Add(new Bitmap(Properties.Resources.hd_hard));
            _imageList.Images.Add(new Bitmap(Properties.Resources.folder));
            _imageList.Images.Add(new Bitmap(Properties.Resources.open_folder));
            
            treeView1.BeginUpdate();

            CreateTree();

            treeView1.EndUpdate();

            this.Text = "TreeView";
        }
        public void CreateTree()
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                TreeNode node = new TreeNode(Text = drive.Name + drive.VolumeLabel, 0, 0);

                AddDirectoryOrFile(node, drive.Name);
                treeView1.Nodes.Add(node);
            }
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
                e.Node.ImageIndex = 2;
                e.Node.SelectedImageIndex = 2;

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
                        TreeNode dirNode = new TreeNode(new DirectoryInfo(dirs[i]).Name, 1, 1);
                        AddDirectoryOrFile(dirNode, dirs[i]);
                        e.Node.Nodes.Add(dirNode);
                    }
                }

                FileInfo[] files = new DirectoryInfo(path).GetFiles();

                foreach (FileInfo file in files)
                {
                    int index = SetIcon(file);

                    TreeNode nodeFile = new TreeNode("", index, index);
                    nodeFile.Text = file.Name.Remove(0, file.Name.LastIndexOf("\\") + 1);
                    e.Node.Nodes.Add(nodeFile);
                }
            }

            if (e.Node.Level == 0) e.Node.Text = str;
        }
        private int SetIcon(FileInfo file)
        {
            Icon iconForFile = SystemIcons.WinLogo;
            int index;

            if (!_imageList.Images.ContainsKey(file.Extension))
            {
                iconForFile = Icon.ExtractAssociatedIcon(file.FullName);
                _imageList.Images.Add(file.Extension, iconForFile);
            }

            index = _imageList.Images.IndexOfKey(file.Extension);

            return index;
        }

        private void treeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Level != 0)
            {
                e.Node.ImageIndex = 1;
                e.Node.SelectedImageIndex = 1;
            }
        }
    }
}