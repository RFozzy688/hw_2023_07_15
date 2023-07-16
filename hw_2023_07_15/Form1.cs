using System.Runtime;

namespace hw_2023_07_15
{
    public partial class Form1 : Form
    {
        string _path;

        public Form1()
        {
            InitializeComponent();

            _path = @"G:\ÿ¿√\";

            CreateTree(new DirectoryInfo(_path));
        }
        public void CreateTree(DirectoryInfo root)
        {
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            subDirs = root.GetDirectories();

            foreach (DirectoryInfo dirInfo in subDirs)
            {
                TreeNode node = treeView1.Nodes.Add(dirInfo.Name);

                AddDirectoryOrFile(dirInfo, node);
            }

            files = root.GetFiles();

            foreach (FileInfo file in files)
            {
                treeView1.Nodes.Add(file.Name);
            }
        }
        public void AddDirectoryOrFile(DirectoryInfo root, TreeNode node)
        {
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            subDirs = root.GetDirectories();

            foreach(DirectoryInfo dirInfo in subDirs)
            {
                TreeNode n = node.Nodes.Add(dirInfo.Name);

                AddDirectoryOrFile(dirInfo, n);
            }

            files = root.GetFiles();

            foreach (FileInfo file in files)
            {
                node.Nodes.Add(file.Name);
            }
        }
    }
}