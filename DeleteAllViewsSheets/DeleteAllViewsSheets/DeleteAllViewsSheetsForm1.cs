using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Autodesk.Revit.UI;

namespace GrimshawRibbon
{
    public partial class ExternalEventExampleDialog : Form
    {
        private ExternalEvent m_ExEvent;
        private ExternalEventExample m_Handler;

        public ExternalEventExampleDialog(ExternalEvent exEvent, ExternalEventExample handler)
        {
            InitializeComponent();
            m_ExEvent = exEvent;
            m_Handler = handler;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // we own both the event and the handler
            // we should dispose it before we are closed
            m_ExEvent.Dispose();
            m_ExEvent = null;
            m_Handler = null;

            // do not forget to call the base class
            base.OnFormClosed(e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void cmdDeleteSheets(object sender, EventArgs e)
        {

        }

        private void cmdDeleteViews(object sender, EventArgs e)
        {

        }

        private void cmdDeleteLinks(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                // Set DeleteViewsBool to True
                Globals.deleteViewsBool = true;
            }

            if (checkBox2.Checked)
            {
                // Set DeleteSheets to True
                Globals.deleteSheetsBool = true;
            }
            m_ExEvent.Raise();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CheckAll(object sender, EventArgs e)
        {
            checkBox1.CheckState = CheckState.Checked;
            checkBox2.CheckState = CheckState.Checked;
            checkBox3.CheckState = CheckState.Checked;
        }

        private void DeleteAllViewsSheetsForm1_Load(object sender, EventArgs e)
        {

        }
    }
}
