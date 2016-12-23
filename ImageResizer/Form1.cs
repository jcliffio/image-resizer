using System;
using System.Windows.Forms;

namespace ImageResizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected string Directory
        {
            get { return directoryTextBox.Text; }
            set { directoryTextBox.Text = value; }
        }

        protected int ContainerHeight
        {
            get
            {
                int height;
                if (int.TryParse(heightTextBox.Text, out height))
                    return height;

                MessageBox.Show("Height must be a whole number.");
                return -1;
            }
        }

        protected int ContainerWidth
        {
            get
            {
                int width;
                if (int.TryParse(widthTextBox.Text, out width))
                    return width;

                MessageBox.Show("Width must be a whole number.");
                return -1;
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                Directory = folderBrowserDialog1.SelectedPath;
        }

        private void resizeAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                ImageResizer.ResizeImages(ContainerHeight, ContainerWidth, Directory);
            }
            catch (Exception)
            {
                
            }
        }
    }
}
