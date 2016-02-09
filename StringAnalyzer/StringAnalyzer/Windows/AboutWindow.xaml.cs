using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringAnalyzer.ViewModel;
using StringAnalyzer.WindowFactories;

namespace StringAnalyzer.Windows
{
    public partial class AboutWindow: IModalDialog<bool?>
    {
        public AboutWindow()
        {
            this.InitializeComponent();
        }
    }
}
