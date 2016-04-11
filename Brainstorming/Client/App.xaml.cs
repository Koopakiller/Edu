using GalaSoft.MvvmLight.Threading;

namespace Koopakiller.Apps.Brainstorming.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App 
    {
        public App()
        {
            DispatcherHelper.Initialize();
        }
    }
}
