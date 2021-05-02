using Prism.Ioc;
using Prism.Unity;
using Soncoord.Client.WPF.Modules.Shell;
using System.Windows;

namespace Soncoord.Client.WPF
{
    class Bootstrapper : PrismBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
