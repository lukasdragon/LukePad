using ComponentFactory.Krypton.Toolkit;
using EncryptedNotePad.Serialization;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;


namespace EncryptedNotePad
{
    public partial class LukePad : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public LukePad()
        {        
            InitializeComponent();
        }

        private string _filepath = "";
        private string FilePath
        {
            get
            {
                return _filepath;
            }
            set
            {
                _filepath = value;
                toolStripStatusLabel1.Text = _filepath;
            }

        }


        private void TextRightClick_Opening(object sender, CancelEventArgs e)
        {

        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NotePad_Load(object sender, EventArgs e)
        {

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }



        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void undoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void redoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = string.Empty;
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = string.Empty;
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void selectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (openLukieFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileManager fm = new FileManager();
                    var color = new System.Drawing.Color();
                    var font = richTextBox1.Font;

                    richTextBox1.Text = fm.LoadFile(openLukieFile.FileName, passwordBox.Text, ref color, ref font);
                    richTextBox1.ForeColor = color;
                    richTextBox1.Font = font;
                    FilePath = openLukieFile.FileName;
                }
                catch (Exception ex) when (ex is System.Runtime.Serialization.SerializationException || ex is System.IO.InvalidDataException)
                {
                    KryptonMessageBox.Show("FILE DAMAGED OR CORRUPTED", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex) when (ex is System.Security.Cryptography.CryptographicException || ex is System.ArgumentNullException)
                {
                    KryptonMessageBox.Show("INCORRECT PASSWORD", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void newJournalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FilePath = string.Empty;
            richTextBox1.Text = string.Empty;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(FilePath))
                {
                    FileManager fm = new FileManager();
                    fm.SaveFile(FilePath, passwordBox.Text, richTextBox1.Text, richTextBox1.ForeColor, richTextBox1.Font);
                }
                else
                {
                    saveAsToolStripMenuItem_Click(sender, e);
                }
            }
            catch (ArgumentException)
            {
                KryptonMessageBox.Show("PASSWORD MUST BE ATLEAST " + AESThenHMAC.MinPasswordLength.ToString() + " CHARACTERS LONG.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                richTextBox1.Font = fontDialog1.Font;
                richTextBox1.ForeColor = fontDialog1.Color;

            }
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void passwordBox_Click(object sender, EventArgs e)
        {
            if (passwordBox.Text == "REPLACEME")
            {
                passwordBox.Clear();
                if (this.passwordBox.Control is TextBox)
                {
                    TextBox tb = this.passwordBox.Control as TextBox;
                    tb.PasswordChar = '*';
                }

            }

        }

        private void aboutLukePadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutForm = new AboutBox1();
            aboutForm.Show();
        }



        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Select(richTextBox1.SelectionStart, 0);
            richTextBox1.SelectedText = DateTime.Now.ToString();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveLukieFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileManager fm = new FileManager();
                    FilePath = saveLukieFile.FileName;
                    fm.SaveFile(FilePath, passwordBox.Text, richTextBox1.Text, richTextBox1.ForeColor, richTextBox1.Font);
                }
                catch (ArgumentException)
                {
                    KryptonMessageBox.Show("PASSWORD MUST BE ATLEAST " + AESThenHMAC.MinPasswordLength.ToString() + " CHARACTERS LONG.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
