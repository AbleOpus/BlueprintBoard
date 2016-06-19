using System.Windows.Forms;

namespace BlueprintBoardDemo.Forms
{
    public partial class CSharpSourceDialog : Form
    {
        public string SourceCode
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }

        public CSharpSourceDialog()
        {
            InitializeComponent();
        }
    }
}
