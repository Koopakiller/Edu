using Microsoft.Win32;

namespace StringAnalyzer.WindowFactories
{
    public class OpenFileDialogFactory : IModalFileDialogFactory
    {
        public IModalFileDialog GetInstance()
        {
            return new InternalOpenFileDialog();
        }

        private class InternalOpenFileDialog : IModalFileDialog
        {
            public bool? ShowDialog()
            {
                var ofd = new OpenFileDialog()
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