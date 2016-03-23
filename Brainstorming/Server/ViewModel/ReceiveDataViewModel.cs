using System.Threading;
using GalaSoft.MvvmLight;

namespace Koopakiller.Apps.Brainstorming.Server.ViewModel
{
    public class ReceiveDataViewModel : ViewModelBase
    {
        public ReceiveDataViewModel(Model.Server server)
        {
            if (this.IsInDesignMode)
            {

            }
            else
            {
                server.Start(CancellationToken.None);
            }
        }
    }
}
