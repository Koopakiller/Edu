using GalaSoft.MvvmLight.Threading;

namespace Koopakiller.Apps.Brainstorming.Server
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
