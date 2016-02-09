using Microsoft.Win32;

namespace StringAnalyzer.WindowFactories
{
    public class SaveFileDialogFactory : IModalFileDialogFactory
    {
        public IModalFileDialog GetInstance()
        {
            return new InternalSaveFileDialog();
        }

        private class InternalSaveFileDialog : IModalFileDialog
        {
            public bool? ShowDialog()
            {
                var ofd = new SaveFileDialog()
                {
                    Filter = this.Filter,
                    FileName = this.FileName
                };
                if (ofd.ShowDialog() != true) return false;
                this.FileName = ofd.FileName;
                return true;
            }
            public string Filter { get; set; }
            public string FileName { get; set; }
        }
    }
}